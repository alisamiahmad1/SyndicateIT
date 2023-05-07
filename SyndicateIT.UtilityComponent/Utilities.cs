using Microsoft.CSharp;
using Newtonsoft.Json;
using SyndicateIT.UtilityComponent.Enum.Attribute;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SyndicateIT.UtilityComponent
{
    [Serializable()]
    public static class Utilities
    {
        public static object SessionContent { get; private set; }
        #region Localization
        /// <summary>
        /// Gets the localized value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetLocalizedValue(ResourceManager resourceManager, string key)
        {
            string localizedDescription = resourceManager != null ? resourceManager.GetString(key) : key;
            return !string.IsNullOrEmpty(localizedDescription) ? localizedDescription : key;
        }

        #endregion

        #region Enum Methods

        ///<summary>
        /// Allows the discovery of an enumeration text value based on the <c>EnumTextValueAttribute</c>
        ///</summary>
        /// <param name="e">The enum to get the reader friendly text value for.</param>
        /// <returns><see cref="System.String"/> </returns>
        public static string GetEnumTextValue<T>(T e, bool fromResource) where T : struct
        {
            string ret = "";
            Type t = typeof(T);
            MemberInfo[] members = t.GetMember(e.ToString());
            if (members != null && members.Length == 1)
            {
                object[] attrs = members[0].GetCustomAttributes(typeof(EnumTextValueAttribute), false);
                if (attrs.Length == 1)
                    ret = ((EnumTextValueAttribute)attrs[0]).Text;

                if (fromResource)
                {
                    var resourceManager = GetResourceManagerFromClass(((EnumTextValueAttribute)attrs[0]).ResourceManager);
                    ret = GetLocalizedValue(resourceManager, ret);
                }
            }
            return ret;
        }

        /// <summary>
        /// Allows the discovery of an enumeration value based on the <c>EnumTextValueAttribute</c>
        /// </summary>
        /// <param name="text">The text of the <c>EnumTextValueAttribute</c>.</param>
        /// <param name="enumType">The type of the enum to get the value for.</param>
        /// <returns><see cref="System.Object"/> boxed representation of the enum value </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static object GetEnumValue(string text, Type enumType, bool fromResource)
        {
            List<MemberInfo> members = enumType.GetMembers().ToList();
            for (int i = 0; i < members.Count; i++)
            {
                object[] attrs = members[i].GetCustomAttributes(typeof(EnumTextValueAttribute), false);
                if (attrs.Length == 1)
                {
                    if (fromResource)
                    {

                        var resourceManager = GetResourceManagerFromClass(((EnumTextValueAttribute)attrs[0]).ResourceManager);
                        var value = GetLocalizedValue(resourceManager, ((EnumTextValueAttribute)attrs[0]).Text);
                        if (value.ToLower() == text.ToLower())
                            return System.Enum.Parse(enumType, members[i].Name);
                    }
                    else
                    {
                        if (((EnumTextValueAttribute)attrs[0]).Text.ToLower() == text.ToLower())
                        {
                            return System.Enum.Parse(enumType, members[i].Name);
                        }
                    }
                }
            }
            throw new ArgumentOutOfRangeException("text", text, "The text passed does not correspond to an attributed enum value");
        }

        /// <summary>
        /// Gets the resource manager from class.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <returns></returns>
        private static ResourceManager GetResourceManagerFromClass(Type classType)
        {
            foreach (PropertyInfo staticProperty in classType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                    return (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
            }
            return null;
        }

        /// <summary>
        /// Allows the discovery of an enumeration value based on the <c>Enum.Parse</c>
        /// </summary>
        /// <param name="text">The text of a enum value of the specified enumeration.</param>
        /// <returns>A enum value of the specified enumeration,</returns>
        public static T GetEnumValue<T>(string text) where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), text, true);
        }

        /// <summary>
        /// Tries the get enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGetEnumValue<T>(string text, out T value) where T : struct
        {
            value = default(T);
            try
            {
                value = (T)System.Enum.Parse(typeof(T), text, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region List Methods

        public static List<T> CloneList<T>(List<T> initialList)
        {
            return initialList != null ? new List<T>(initialList.ToArray()) : new List<T>();
        }

        /// <summary>
        /// Converts a list of objects into an entity collection of objects
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="sourceList">The source list</param>
        /// <returns>The entity collection</returns>
        public static EntityCollection<T> ConvertListToEntityCollection<T>(List<T> sourceList) where T : class
        {
            EntityCollection<T> result = new EntityCollection<T>();
            for (int i = 0; i < sourceList.Count; i++)
                result.Add(sourceList[i]);
            return result;
        }

        #endregion

        #region Url Methods

        #region Get Url

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="action">The action.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        /// <returns></returns>
        public static string GetUrl(this HtmlHelper helper, string action, bool? isSecure = null)
        {
            return GetUrl(helper, action, null, isSecure);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        /// <returns></returns>
        public static string GetUrl(this HtmlHelper helper, string action, string controller, object routeValues = null, bool? isSecure = null)
        {
            controller = controller ?? helper.ViewContext.RouteData.Values["controller"].ToString();
            if (isSecure.HasValue)
                return new UrlHelper(helper.ViewContext.RequestContext).Action(action, controller, routeValues, isSecure.Value ? "https" : "http");
            else
                return new UrlHelper(helper.ViewContext.RequestContext).Action(action, controller, routeValues);

        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        /// <returns></returns>
        public static string GetUrl(string action, bool? isSecure = null)
        {
            return GetUrl(action, null, isSecure);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        /// <returns></returns>
        public static string GetUrl(string action, string controller, bool? isSecure = null)
        {
            return GetUrl(action, controller, null, isSecure);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        /// <returns></returns>
        public static string GetUrl(string action, string controller, object routeValues, bool? isSecure = null)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                var request = new HttpRequest("/", "", "");
                var response = new HttpResponse(new StringWriter());
                httpContext = new HttpContext(request, response);
            }

            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase, routeData);
            UrlHelper urlHelper = new UrlHelper(requestContext);
            if (isSecure.HasValue)
                return urlHelper.Action(action, controller, routeValues, isSecure.Value ? "https" : "http");
            else
                return urlHelper.Action(action, controller, routeValues);

        }


        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="isSecure">if set to <c>true</c> [is secure].</param>
        /// <returns></returns>
        public static string GetUrl(string action, string controller, RouteValueDictionary routeValues, bool? isSecure = null)
        {
            RouteValueDictionary decodedRouteValues = null;
            if (routeValues != null)
            {
                Dictionary<string, object> parameters = routeValues.ToDictionary(k => k.Key, v => (object)HttpUtility.UrlDecode(v.Value.ToString()));
                decodedRouteValues = new RouteValueDictionary(parameters);
            }

            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                var request = new HttpRequest("/", "", "");
                var response = new HttpResponse(new StringWriter());
                httpContext = new HttpContext(request, response);
            }

            var httpContextBase = new HttpContextWrapper(httpContext);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContextBase, routeData);
            UrlHelper urlHelper = new UrlHelper(requestContext);
            if (isSecure.HasValue)
                return urlHelper.Action(action, controller, decodedRouteValues, isSecure.Value ? "https" : "http");
            else
                return urlHelper.Action(action, controller, decodedRouteValues);
        }

        #endregion

        /// <summary>
        /// Determines whether [is valid URL] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        ///   <c>true</c> if [is valid URL] [the specified URL]; otherwise, <c>false</c>.
        /// </returns>
        public static bool UrlExists(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request == null) return false;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (UriFormatException)
            {
                //Invalid Url
                return false;
            }
            catch (WebException)
            {
                //Unable to access url
                return false;
            }
        }

        /// <summary>
        /// Get absolute path from virtual path
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string GetAbsotuleUrl(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        /// <summary>
        /// Gets the virtual URL.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <returns></returns>
        public static string GetVirtualUrl(string absolutePath)
        {
            return VirtualPathUtility.ToAppRelative(absolutePath);
        }

        /// <summary>
        /// Check if value is URL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUrl(string value)
        {
            Regex expr = new Regex(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
            return expr.IsMatch(value);
        }

        /// <summary>
        /// Gets the action from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetActionFromUrl(string url)
        {
            return url.Split('/')[2];
        }

        /// <summary>
        /// Gets the controller from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetControllerFromUrl(string url)
        {
            return url.Split('/')[1];
        }

        #endregion

        #region Parsing

        /// <summary>
        /// Merges the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="PropertiesNotToBeMerged">The properties not to be merged.</param>
        /// <returns></returns>
        public static T MergeObject<T>(this T destination, T source, bool toBeMerged = true, params string[] properties)
        {
            if (destination == null) return default(T);

            PropertyInfo[] instanceProperties = typeof(T).GetProperties();
            if (properties.Length > 0)
                instanceProperties = instanceProperties.Where(prop => !(toBeMerged ^ properties.Contains(prop.Name))).ToArray();
            bool isEntityReference = false;
            List<PropertyInfo> property = instanceProperties.ToList();
            for (int i = 0; i < property.Count; i++)
            {
                isEntityReference = property[i].PropertyType.BaseType.Equals(typeof(EntityReference));
                if (isEntityReference ||
                    (properties.Length != 0 && ((!toBeMerged && properties.Contains(property[i].Name)) || (toBeMerged && !properties.Contains(property[i].Name)))) ||
                    property[i].GetSetMethod() == null)
                    continue;
                property[i].SetValue(destination, property[i].GetValue(source, null), null);
            }
            return destination;
        }

        /// <summary>
        /// Merges the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <param name="toBeMerged">if set to <c>true</c> [to be merged].</param>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static T MergeObject<T>(this T destination, T source, bool toBeMerged = true, params Expression<Func<T, object>>[] properties)
        {
            string[] parameters = properties.Select(expr => GetPropertyName(expr.Body)).ToArray();

            return MergeObject(destination, source, toBeMerged, parameters);
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="propertyRefExpr">The property ref expr.</param>
        /// <returns></returns>
        public static string GetPropertyName(Expression propertyRefExpr)
        {
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                return memberExpr.Member.Name;

            throw new ArgumentException("No property reference expression was found.",
                             "propertyRefExpr");
        }

        /// <summary>
        /// Gets the prop value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The SRC.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <returns></returns>
        public static T GetPropValue<T>(object src, string propName)
        {
            try
            {
                var property = src.GetType().GetProperty(propName);
                if (property != null)
                {
                    object obj = property.GetValue(src, null);
                    if (obj != null)
                        return (T)obj;
                }
            }
            catch (InvalidCastException) { }

            return default(T);
        }

        /// <summary>
        /// Gets the prop value.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <returns></returns>
        public static object GetPropValue(object src, string propName)
        {
            return GetPropValue<object>(src, propName);
        }

        /// <summary>
        /// Sets the prop value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="value">The value.</param>
        public static void SetPropValue<T, Q>(this T instance, string propName, Q value)
        {
            PropertyInfo prop = instance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(instance, value, null);
            }
        }

        /// <summary>
        /// Toes the query string.
        /// </summary>
        /// <param name="dict">The dict.</param>
        /// <returns></returns>
        public static string ToQueryString(this IDictionary<string, string> dict)
        {
            if (dict.Count == 0) return string.Empty;

            var buffer = new StringBuilder();
            int count = 0;
            bool end = false;

            foreach (var key in dict.Keys)
            {
                if (count == dict.Count - 1) end = true;

                if (end)
                    buffer.AppendFormat("{0}={1}", key, dict[key]);
                else
                    buffer.AppendFormat("{0}={1}&", key, dict[key]);

                count++;
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Parses the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form">The form.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="propertyMap">The property map.</param>
        /// <returns></returns>
        public static T ParseObject<T>(FormCollection form, T obj, IDictionary<string, string> propertyMap = null)
        {
            var queryString = form.AllKeys.ToDictionary(k => k, k => form[k]).ToQueryString();
            return ParseObject(queryString, obj, propertyMap);
        }

        /// <summary>
        /// Parse query string to dynamic object type
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="querystring">Query string to parse</param>
        /// <param name="obj">Object result</param>
        /// <returns>Filled entity</returns>
        public static T ParseObject<T>(string querystring, T obj, IDictionary<string, string> propertyMap = null)
        {
            if (!string.IsNullOrEmpty(querystring))
            {
                NameValueCollection collection = HttpUtility.ParseQueryString(querystring);
                List<PropertyInfo> properties = typeof(T).GetProperties().ToList();

                string value = string.Empty;
                for (int i = 0; i < properties.Count; i++)
                {
                    value = collection[properties[i].Name] != null ?
                                collection[properties[i].Name].ToString() :
                            propertyMap != null && propertyMap.ContainsKey(properties[i].Name) && collection[propertyMap[properties[i].Name]] != null ?
                                collection[propertyMap[properties[i].Name]].ToString() :
                                string.Empty;
                    var hasSetter = properties[i].GetSetMethod() != null;
                    if (string.IsNullOrEmpty(value) || !hasSetter) continue;
                    properties[i].SetValue(obj, DynamicParse(value, properties[i].PropertyType), null);
                }

            }
            return obj;
        }

        /// <summary>
        /// Parse string to dynamic type
        /// </summary>
        /// <param name="text">Text to parse</param>
        /// <param name="type">Type to parse to</param>
        /// <returns>Object value</returns>
        public static object DynamicParse(string text, Type type)
        {
            if (string.IsNullOrEmpty(text)) return GetDefault(type);

            if ((type == typeof(bool) || type == typeof(bool?)) && text.ToLower() == "on")
                text = "True";
            TypeConverter tc = TypeDescriptor.GetConverter(type);
            return tc.ConvertFromString(null, CultureInfo.InvariantCulture, text);
        }

        /// <summary>
        /// Gets the default value of the type provided
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>object value</returns>
        public static T GetDefault<T>(params object[] args)
        {
            Type type = typeof(T);
            return (T)Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        /// <summary>
        /// Creates a function from a property name
        /// </summary>
        /// <typeparam name="T">The type T</typeparam>
        /// <param name="propertyName">The property name</param>
        /// <returns>Func<T,object></returns>
        public static Func<T, object> CreateSelectorFunction<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.PropertyOrField(parameter, propertyName);
            var convert = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda(typeof(Func<T, object>), convert, parameter);
            return (Func<T, object>)lambda.Compile();
        }

        /// <summary>
        /// Converts the object to dictionary.
        /// </summary>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ConvertObjectToDictionary(object htmlAttributes)
        {
            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();
            if (htmlAttributes != null)
            {
                Type t = htmlAttributes.GetType();

                if (t == typeof(Dictionary<string, object>))
                    return ((Dictionary<string, object>)htmlAttributes);

                var property = t.GetProperties().ToList();
                for (int i = 0; i < property.Count; i++)
                {
                    if (!property[i].CanRead) continue;
                    values.Add(new KeyValuePair<string, object>(property[i].Name, property[i].GetValue(htmlAttributes, null)));
                }
            }

            return values.ToDictionary(k => k.Key, v => (object)v.Value);
        }
        #endregion

        #region Extension Methods

        /// <summary>
        /// Gets the Last count element from the items list
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="items">Ienumerable of items</param>
        /// <param name="count">Number of items to fetch</param>
        /// <returns>Last count items from the list</returns>
        public static IEnumerable<T> Last<T>(this IEnumerable<T> items, int count)
        {
            return items.Skip(Math.Max(0, items.Count() - count)).Take(count);
        }

        /// <summary>
        /// Gets the Last count element from the items list
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="items">Ienumerable of items</param>
        /// <param name="count">Number of items to fetch</param>
        /// <param name="func">A function to test each element for a condition</param>
        /// <returns>Last count items from the list</returns>
        public static IEnumerable<T> Last<T>(this IEnumerable<T> items, int count, Func<T, bool> func)
        {
            return items.Where(func).Last(count);
        }

        /// <summary>
        /// Gets the Last count element from the items list
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="items">Ienumerable of items</param>
        /// <param name="count">Number of items to fetch</param>
        /// <returns>Last count items from the list in reverse order</returns>
        public static IEnumerable<T> ReverseLast<T>(this IEnumerable<T> items, int count)
        {
            return items.Last(count).Reverse();
        }

        /// <summary>
        /// Gets the Last count element from the items list
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="items">Ienumerable of items</param>
        /// <param name="count">Number of items to fetch</param>
        /// <param name="func">A function to test each element for a condition</param>
        /// <returns>Last count items from the list in reverse order</returns>
        public static IEnumerable<T> ReverseLast<T>(this IEnumerable<T> items, int count, Func<T, bool> func)
        {
            return items.Last(count, func).Reverse();
        }

        /// <summary>
        /// Determines whether [contains all items] [the specified container list].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containerList">The container list.</param>
        /// <param name="containedList">The contained list.</param>
        /// <returns>
        ///   <c>true</c> if [contains all items] [the specified container list]; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> containerList, IEnumerable<T> containedList)
        {
            return !containedList.Except(containerList).Any();
        }

        /// <summary>
        /// Removes the empty items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<T> RemoveEmptyItems<T>(this IEnumerable<T> items)
        {
            return items.Where(item => item != null);
        }

        /// <summary>
        /// Removes the empty items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<string> RemoveEmptyItems(this IEnumerable<string> items)
        {
            return items.Where(item => !string.IsNullOrEmpty(item));
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified items].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified items]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || items.Count() == 0;
        }

        /// <summary>
        /// Distincts the by.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static string Concat<T>(this IEnumerable<T> items)
        {
            return items.Concat(string.Empty);
        }

        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string Concat<T>(this IEnumerable<T> items, char delimiter)
        {
            return items.Concat(delimiter.ToString());
        }

        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string Concat<T>(this IEnumerable<T> items, string delimiter)
        {
            return items.Select(v => v.ToString()).Concat(delimiter);
        }

        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> items, char delimiter)
        {
            return items.Concat(delimiter.ToString());
        }

        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> items)
        {
            return items.Concat(string.Empty);
        }

        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> items, string delimiter)
        {
            return items.IsNullOrEmpty() ? string.Empty : items.Aggregate((current, next) => current + delimiter + next);
        }

        /// <summary>
        /// Toes the literal.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string ToLiteral(this string input)
        {
            var writer = new StringWriter();
            CSharpCodeProvider provider = new CSharpCodeProvider();
            provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
            return writer.GetStringBuilder().ToString();
        }

        /// <summary>
        /// Fixes the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string FixFileName(this string fileName)
        {
            return string.IsNullOrEmpty(fileName) ? "no-name" : fileName.Replace('"', '_').Replace("'", "_").Replace(" ", "_").Replace(",", "_").Replace("-", "_");
        }

        /// <summary>
        /// Replace an item in the collection
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="items">Collection of items</param>
        /// <param name="predicate">The System.Predicate<T> delegate that defines the conditions of the elements to remove</param>
        /// <param name="item">The value to locate in the sequence</param>
        public static bool ReplaceIf<T>(this List<T> items, ref T item, Predicate<T> predicate, params Expression<Func<T, object>>[] properties)
        {
            if (items.Contains(item))
            {
                var itm = items.Find(predicate);
                if (itm != null)
                    item = itm.MergeObject(items.Find(predicate), true, properties);
                return true;
            }
            //Add the item to the collection
            items.Add(item);
            return false;
        }

        /// <summary>
        /// Replace an item in the collection
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="items">Collection of items</param>
        /// <param name="predicate">The System.Predicate<T> delegate that defines the conditions of the elements to remove</param>
        /// <param name="comparer">An Equility Comparer to compare values</param>
        /// <param name="item">The value to locate in the sequence</param>
        public static bool ReplaceIf<T>(this List<T> items, ref T item, Predicate<T> predicate, IEqualityComparer<T> comparer, params Expression<Func<T, object>>[] properties)
        {
            if (items.Contains(item, comparer))
            {
                var itm = items.Find(predicate);
                if (itm != null)
                    item = itm.MergeObject(item, true, properties);
                return true;
            }
            //Add the item to the collection
            items.Add(item);
            return false;
        }

        /// <summary>
        /// Tries the equals.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public new static bool Equals(this object source, object destination)
        {
            if (source == null)
            {
                if (destination == null)
                    return true;
                return false;
            }
            else
            {
                if (destination == null)
                    return false;
                return source.Equals(destination);
            }
        }


        /// <summary>
        /// Clones the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("The type must be serializable.", "source");


            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Converts int to the size of the file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToFileSize(this int source)
        {
            return ToFileSize(Convert.ToInt64(source));
        }

        /// <summary>
        /// Converts int to the size of the file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToFileSize(this long source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }
            else if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }
            else //Bytes
            {
                return string.Concat(bytes, " Bytes");
            }
        }

        /// <summary>
        /// Converts string to upper first letter
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToLower().ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        /// <summary>
        /// Toes the paragraph.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string ToParagraph(this string text, bool splitLines = false, IEnumerable<string> capitalWords = null)
        {
            Regex reg = new Regex(@"^[a-z]|\b\. [a-z0-9]?| i ", RegexOptions.Multiline);
            if (splitLines)
                text = text.Replace(".", ".\r\n");
            text = reg.Replace(text.ToLower(), ToParagraphMatcher);
            if (capitalWords != null)
                capitalWords.ToList().ForEach(word =>
                {
                    var template = " {0} ";
                    var capitalWord = string.Format(template, word.ToUpper());
                    text = text.Replace(string.Format(template, word.ToLower()), capitalWord).Replace(string.Format(template, word.ToUpperFirstLetter()), capitalWord);
                });

            return text;//.Replace("--", string.Empty).Replace("**", string.Empty);
        }

        /// <summary>
        /// Toes the HTML paragraph.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="changeCasing">if set to <c>true</c> [change casing].</param>
        /// <param name="splitLines">if set to <c>true</c> [split lines].</param>
        /// <param name="capitalWords">The capital words.</param>
        /// <returns></returns>
        public static string ToHTMLParagraph(this string text, bool changeCasing = false, bool splitLines = false, IEnumerable<string> capitalWords = null)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (changeCasing)
                text = text.ToParagraph(splitLines, capitalWords);
            return text.Replace("\r", string.Empty).Replace("\n", "<br />");
        }

        /// <summary>
        /// matcher method
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        private static string ToParagraphMatcher(Match m)
        {
            return m.ToString().ToUpper();
        }

        /// <summary>
        /// Reverses the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string Reverse(this string source)
        {
            char[] charactersArray = source.ToCharArray();
            Array.Reverse(charactersArray);
            return new string(charactersArray);
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <returns></returns>
        public static double Round(this double value, int decimalPlaces)
        {
            return Convert.ToDouble(decimal.Round((decimal)value, decimalPlaces));
        }

        #endregion

        #region Encoding

        /// <summary>
        /// Encodes the specified STR.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return Uri.EscapeDataString(str);
        }

        /// <summary>
        /// Decodes the specified STR.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string Decode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            return Uri.UnescapeDataString(str);
        }

        #endregion

        #region Encryption

        /// <summary>
        /// Encrypt a string into a string using a data 
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public static string Encrypt(string text, bool encode = false)
        {
            if (encode)
                return !string.IsNullOrEmpty(text) ? HttpUtility.UrlEncode(Encryption.Encrypt(text)) : string.Empty;
            else
                return !string.IsNullOrEmpty(text) ? Encryption.Encrypt(text) : string.Empty;
        }

        /// <summary>
        /// Decrypt an encrypted string into a string
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public static string Decrypt(string text, bool decode = false)
        {
            if (decode)
                return !string.IsNullOrEmpty(text) ? Encryption.Decrypt(HttpUtility.UrlDecode(text).Replace(" ", "+")) : string.Empty;
            else
                return !string.IsNullOrEmpty(text) ? Encryption.Decrypt(text).Replace(" ", "+") : string.Empty;
        }

        /// <summary>
        /// Decrypts the specified text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <param name="decode">if set to <c>true</c> [decode].</param>
        /// <returns></returns>
        public static T Decrypt<T>(string text, bool decode = false)
        {
            var result = Decrypt(text, decode);

            return (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// Tries the encrypt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <param name="encode">if set to <c>true</c> [encode].</param>
        /// <returns></returns>
        public static bool TryEncrypt(string text, out string encryptedText, bool encode = false)
        {
            encryptedText = string.Empty;
            try
            {
                encryptedText = Encrypt(text, encode);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries the decrypt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="decreptedText">The decrepted text.</param>
        /// <param name="decode">if set to <c>true</c> [decode].</param>
        /// <returns></returns>
        public static bool TryDecrypt<T>(string text, out T decreptedText, bool decode = false)
        {
            decreptedText = default(T);
            try
            {
                decreptedText = Decrypt<T>(text, decode);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Compare

        /// <summary>
        /// Compares the entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity1">The entity1.</param>
        /// <param name="entity2">The entity2.</param>
        /// <returns></returns>
        public static IEnumerable<string> CompareWith<T>(this T entity1, T entity2)
        {
            Type t = entity2.GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            PropertyInfo currentProperty;
            object value1, value2;
            foreach (PropertyInfo pi in properties)
            {
                currentProperty = t.GetProperties().First(p => p.Name == pi.Name && t.Equals(p.ReflectedType));
                if (currentProperty == null)
                    continue;

                value1 = currentProperty.GetValue(entity1, null);
                value2 = currentProperty.GetValue(entity2, null);

                if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                    yield return pi.Name;
            }
        }

        #endregion

        #region Format Number

        public static string FormatThousands(int number)
        {
            return FormatThousands(Convert.ToDouble(number), 0);
        }

        /// <summary>
        /// Formats the thousands.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <returns></returns>
        public static string FormatThousands(decimal number, int decimalPlaces = 2)
        {
            return FormatThousands(Convert.ToDouble(number), decimalPlaces);
        }

        /// <summary>
        /// Formats the thousands.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string FormatThousands(double number, int decimalPlaces = 2)
        {
            NumberFormatInfo nfi = (NumberFormatInfo)
            CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = ",";
            nfi.NumberDecimalDigits = decimalPlaces;

            return number.ToString("n", nfi);
        }


        #endregion

        #region MD5

        public static string md5(string sPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        #endregion

        public static DateTime ConvetToLocal(DateTime dt)
        {
            return dt.ToLocalTime();
        }

        /// <summary>
        /// Serializes the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static string Serialize(object response)
        {
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        /// <summary>
        /// Tries the deserialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="deserializedObject">The deserialized object.</param>
        /// <returns></returns>
        public static bool TryDeserialize<T>(string serializedObject, out T deserializedObject)
        {
            deserializedObject = default(T);
            try
            {
                deserializedObject = JsonConvert.DeserializeObject<T>(serializedObject);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Serializes the no null.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static string SerializeNoNull(object response)
        {
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org");
            WebResponse IPresponse = request.GetResponse();
            StreamReader stream = new StreamReader(IPresponse.GetResponseStream());
            request.Proxy = null;
            return stream.ReadToEnd();
        }

        #region Helper

        public static bool IsNumberStringOfExactLength(string input, int length)
        {
            if (input != null && input.Length == length && input.All(c => Char.IsDigit(c)))
                return true;

            return false;
        }

        public static bool CheckCustomerID(string CustomerID, int length)
        {

            if (IsNumberStringOfExactLength(CustomerID, length))
                return true;

            return false;
        }

        public static bool CheckNickName(string NickName)
        {
            if (NickName != null)
            {
                //if (NickName.Length == 4 /*&& NickName.All(c => char.IsUpper(c)*/))
                if (NickName.Length == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public static string ServiceStatus(string value)
        {
            switch (value)
            {
                case "E":
                    return "Created";
                case "A":
                    return "Active";
                case "B":
                    return "Blocked";
                case "C":
                    return "Cancelled";
                case "L":
                    return "Locked";
                case "R":
                    return "Reactivated ";
            }

            return "";
        }

        public static string StatusTransaction(string value)
        {
            switch (value)
            {
                case "0010":
                    return "Not Approved ";
                case "0020":
                    return "Pending Execution ";
                case "0510":
                    return "Executing";
                case "0930":
                    return "ForReview ";
                case "0940":
                    return "Failed Needs Reversal ";
                case "0520":
                    return "Executed";
                case "0910":
                    return "Aborted";
                case "0920":
                    return "Failed";
                case "0950":
                    return "FailedReveresed";
            }

            return "";
        }

        public static string ServiceStatusLabel(string value)
        {
            switch (value)
            {
                case "E":
                    return "label-info";
                case "A":
                    return "label-success";
                case "B":
                    return "label-warning";
                case "C":
                    return "label-danger";
                case "L":
                    return "label-warning";
            }

            return "";
        }

        public static string CustomerCategory(string value)
        {
            switch (value)
            {
                case "1":
                    return "Individual";
                case "2":
                    return "Priority";
                case "E":
                    return "Employee";
            }

            return "";
        }

        #endregion

    }
}
