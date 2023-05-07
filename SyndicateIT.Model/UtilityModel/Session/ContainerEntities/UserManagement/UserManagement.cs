using SyndicateIT.Model.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContainerEntities.UserManagement
{
    [Serializable()]
    public class UserManagement : SessionElement
    {
        #region Constant

        /// <summary>
        /// [ERROR: invalid expression FieldName.Words.TheAndAllAsSentence]
        /// </summary>
        public const string SESSION_KEY = "UserManagement";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the biller identifier.
        /// </summary>
        /// <value>The biller identifier.</value>
        public int BillerID { get; set; }

        /// <summary>
        /// Gets or sets the list user management.
        /// </summary>
        /// <value>The list user management.</value>
        public List<UserManagementViewModel> ListUserManagement { get; set; }

        #endregion
    }
}
