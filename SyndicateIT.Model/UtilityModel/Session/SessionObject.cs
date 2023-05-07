using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SyndicateIT.Model.UtilityModel.Session
{
    [Serializable()]
    public class SessionObject : ISessionObject
    {
        #region Methods

        /// <summary>
        /// Gets the session value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">The session key.</param>
        /// <returns>T.</returns>
        public T GetSessionValue<T>(string sessionKey)
        {
            if (string.IsNullOrEmpty(sessionKey)) return default(T);

            if (HttpContext.Current.Session[sessionKey] == null)
                SetSessionValue(sessionKey, (T)Activator.CreateInstance(typeof(T)));

            return (T)HttpContext.Current.Session[sessionKey];
        }

        /// <summary>
        /// Sets the session value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="value">The value.</param>
        public void SetSessionValue<T>(string sessionKey, T value)
        {
            if (string.IsNullOrEmpty(sessionKey) || value == null) return;
            HttpContext.Current.Session[sessionKey] = value;
        }

        #endregion
        #region API

        /// <summary>
        /// Gets the session value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">The session key.</param>
        /// <returns>T.</returns>
        public T SessionGetValue<T>(string sessionKey)
        {
            if (string.IsNullOrEmpty(sessionKey)) return default(T);

            if (HttpContext.Current.Session[sessionKey] == null)
                SessionSetValue(sessionKey, (T)Activator.CreateInstance(typeof(T)));

            return (T)HttpContext.Current.Session[sessionKey];
        }

        /// <summary>
        /// Sets the session value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="value">The value.</param>
        public void SessionSetValue<T>(string sessionKey, T value)
        {
            if (string.IsNullOrEmpty(sessionKey) || value == null) return;
            HttpContext.Current.Session[sessionKey] = value;
        }

        #endregion

    }
}
