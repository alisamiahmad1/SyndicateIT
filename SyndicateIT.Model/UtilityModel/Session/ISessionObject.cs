using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session
{
    public interface ISessionObject
    {
        #region Methods      

        /// <summary>
        /// Gets the session value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">The session key.</param>
        /// <returns>T.</returns>
        T GetSessionValue<T>(string sessionKey);

        /// <summary>
        /// Sets the session value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="value">The value.</param>
        void SetSessionValue<T>(string sessionKey, T value);

        #endregion
    }
}
