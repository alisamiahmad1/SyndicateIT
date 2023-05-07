using SyndicateIT.Model.UtilityModel.Session.ContainerEntities.Files;
using SyndicateIT.Model.UtilityModel.Session.ContainerEntities.Login;
using SyndicateIT.Model.UtilityModel.Session.ContainerEntities.Profiles;
using SyndicateIT.Model.UtilityModel.Session.ContainerEntities.RoleManagement;
using SyndicateIT.Model.UtilityModel.Session.ContainerEntities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session
{
    [Serializable()]
    public class SessionContainer : SessionObject, IDisposable
    {
        #region Properties   
                    

        #endregion

        #region Login     

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>The login.</value>
        public Login Login
        {
            get
            {
                return GetSessionValue<Login>(Login.SESSION_KEY);
            }

            set
            {
                SetSessionValue(Login.SESSION_KEY, value);
            }
        }
        #endregion

        #region Login      

        public Login LoginAPI
        {
            get
            {
                return SessionGetValue<Login>(Login.SESSION_KEY);
            }

            set
            {
                SessionSetValue(Login.SESSION_KEY, value);
            }
        }

        #endregion

        #region User Management 

        /// <summary>
        /// Gets or sets the user management.
        /// </summary>
        /// <value>The user management.</value>
        public UserManagement UserManagement
        {
            get
            {
                return GetSessionValue<UserManagement>(UserManagement.SESSION_KEY);
            }

            set
            {
                SetSessionValue(UserManagement.SESSION_KEY, value);
            }
        }

        #region role Management 

        /// <summary>
        /// Gets or sets the user management.
        /// </summary>
        /// <value>The user management.</value>
        public RoleManagement RoleManagement
        {
            get
            {
                return GetSessionValue<RoleManagement>(RoleManagement.SESSION_KEY);
            }

            set
            {
                SetSessionValue(RoleManagement.SESSION_KEY, value);
            }
        }
        #endregion

        #endregion

        #region Profiles

        public Profiles Profiles
        {
            get
            {
                return GetSessionValue<Profiles>(Profiles.SESSION_KEY);
            }

            set
            {
                SetSessionValue(Profiles.SESSION_KEY, value);
            }
        }

        public Files Files
        {
            get
            {
                return GetSessionValue<Files>(Files.SESSION_KEY);
            }

            set
            {
                SetSessionValue(Files.SESSION_KEY, value);
            }
        }


        #endregion

        #region Methods      

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
