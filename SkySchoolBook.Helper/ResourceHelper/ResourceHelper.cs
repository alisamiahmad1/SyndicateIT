using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Compilation;
 using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Helper
{
    public static class ResourceHelper
    {
        /// <summary>
        /// Resources the specified htmlhelper.
        /// </summary>
        /// <param name="htmlhelper">The htmlhelper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string Resource(this HtmlHelper htmlhelper, string expression, string resourcePath, params object[] args)
        {
            string virtualPath = Utilities.GetVirtualUrl(resourcePath);

            return GetResourceString(htmlhelper.ViewContext.HttpContext, expression, virtualPath, args);
        }

        /// <summary>
        /// Resources the specified htmlhelper.
        /// </summary>
        /// <param name="htmlhelper">The htmlhelper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string Resource(this HtmlHelper htmlhelper, string expression, params object[] args)
        {
            string virtualPath = GetVirtualPath(htmlhelper);

            return GetResourceString(htmlhelper.ViewContext.HttpContext, expression, virtualPath, args);
        }

        /// <summary>
        /// Resources the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string Resource(this Controller controller, string expression, params object[] args)
        {
            return GetResourceString(controller.HttpContext, expression, "~/", args);
        }

        /// <summary>
        /// Gets the resource string.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        private static string GetResourceString(HttpContextBase httpContext, string expression, string virtualPath, object[] args)
        {
            ExpressionBuilderContext context = new ExpressionBuilderContext(virtualPath);
            ResourceExpressionBuilder builder = new ResourceExpressionBuilder();
            ResourceExpressionFields fields = (ResourceExpressionFields)builder.ParseExpression(expression, typeof(string), context);

            if (!string.IsNullOrEmpty(fields.ClassKey))
                return string.Format((string)httpContext.GetGlobalResourceObject(
                    fields.ClassKey,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args);

            return string.Format((string)httpContext.GetLocalResourceObject(
                virtualPath,
                fields.ResourceKey,
                CultureInfo.CurrentUICulture),
                args);
        }
        private static string GetVirtualPath(HtmlHelper htmlHelper)
        {
            string virtualPath = null;
            WebFormView view = htmlHelper.ViewContext.View as WebFormView;

            if (view != null)
            {
                virtualPath = view.ViewPath;
            }
            return virtualPath;
        }

        /// <summary>
        /// Finds the view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <returns></returns>
        private static ViewEngineResult FindView(ControllerContext controllerContext, string viewName)
        {
            // Result 
            ViewEngineResult result = null;

            // Search only for WebFormViewEngine 
            WebFormViewEngine webFormViewEngine = null;
            var viewEngine = ViewEngines.Engines.ToList();
            for (int i = 0; i < viewEngine.Count; i++)
            {
                webFormViewEngine = viewEngine[i] as WebFormViewEngine;

                if (webFormViewEngine != null)
                    break;
            }

            result = webFormViewEngine.FindView(controllerContext, viewName, "", false);
            if (result.View == null)
            {
                result = webFormViewEngine.FindPartialView(controllerContext, viewName, false);
            }

            // Return 
            return result;
        }
    }
}
