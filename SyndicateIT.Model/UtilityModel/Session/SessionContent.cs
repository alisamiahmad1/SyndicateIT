using SyndicateIT.Model.UtilityModel.Session.ContentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SyndicateIT.Model.UtilityModel.Session
{
    [Serializable()]
    public class SessionContent : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>The current.</value>
        public static SessionContent Current
        {
            get
            {
                if (HttpContext.Current.Session[SyndicateIT.UtilityComponent.Constants.SESSIONCONTENTNAME] == null)
                {
                    HttpContext.Current.Session[SyndicateIT.UtilityComponent.Constants.SESSIONCONTENTNAME] = new SessionContent();
                }
                return new SessionObject().GetSessionValue<SessionContent>(SyndicateIT.UtilityComponent.Constants.SESSIONCONTENTNAME);
            }
            set
            {
                new SessionObject().SetSessionValue<SessionContent>(SyndicateIT.UtilityComponent.Constants.SESSIONCONTENTNAME, value);
            }
        }
        #endregion

        #region Variables

        /** @brief The container. */
        private static SessionContainer container;

        #endregion

        #region Session Properties 

        /// <summary>
        /// Gets or Sets the Application Store Instance
        /// </summary>
        public static ApplicationStore AppStore
        {
            get { return System.Web.HttpContext.Current.Application["ApplicationStore"] != null ? (ApplicationStore)System.Web.HttpContext.Current.Application["ApplicationStore"] : new ApplicationStore(); }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public static SessionContainer Container
        {
            get
            {
                if (container == null)
                    Container = new SessionContainer();
                return container;
            }
            private set
            {
                container = value;
            }
        }

        #region Corporate

        private CorporateEntity corporate;
        /// <summary>
        /// Gets or sets the corporate.
        /// </summary>
        /// <value>
        /// The corporate.
        /// </value>
        public CorporateEntity Corporate
        {
            get
            {
                if (corporate == null)
                {
                    corporate = new CorporateEntity();
                }
                return corporate ?? new CorporateEntity();
            }
            set
            {
                corporate = value;
            }
        }

        #endregion

        #endregion

        #region Methods


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
        }

        #endregion
    }
}
