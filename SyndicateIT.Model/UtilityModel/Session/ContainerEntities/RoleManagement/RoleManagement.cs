using SyndicateIT.Model.ViewModel.RoleManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContainerEntities.RoleManagement
{
    [Serializable()]
    public class RoleManagement : SessionElement
    {
        #region Constant

        /// <summary>
        /// [ERROR: invalid expression FieldName.Words.TheAndAllAsSentence]
        /// </summary>
        public const string SESSION_KEY = "RoleManagement";

        #endregion

        #region Properties      

        /// <summary>
        /// Gets or sets the list user management.
        /// </summary>
        /// <value>The list user management.</value>
        public List<GridRoleManagementViewModel> ListRoleManagement { get; set; }

        #endregion
    }
}
