using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.UtilityComponent;
using SyndicateIT.UtilityComponent.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.UserManagement
{
    [Serializable]
    public class UserManagementViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Display(Name = "Username", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserManagementViewModel))]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Branch")]
        public string Branch
        {
            get
            {
                var br = SessionContent.AppStore.AssignedBranchList.Where(p => p.Value == BranchId).FirstOrDefault();

                if (br != null)
                    return br.Text;
                else
                    return "";

            }
        }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        public string BranchId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        public string RoleID { get; set; }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <value>The role.</value>
        [Display(Name = "Role", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserManagementViewModel))]
        public string Role
        {
            get
            {
                return RoleID;
            }
        }

        /// <summary>
        /// Gets or sets the biller.
        /// </summary>
        /// <value>The biller.</value>
        [Display(Name = "Biller", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserManagementViewModel))]
        public string Biller { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets the creation date string.
        /// </summary>
        /// <value>The creation date string.</value>
        [Display(Name = "CreationDate", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserManagementViewModel))]
        public String CreationDateString
        {
            get
            {
                return CreationDate.ToString(Constants.DATEFORMAT);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [status identifier].
        /// </summary>
        /// <value><c>true</c> if [status identifier]; otherwise, <c>false</c>.</value>
        public bool StatusID { get; set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        [Display(Name = "Status", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserManagementViewModel))]
        public string Status
        {
            get
            {
                if (!StatusID)
                    return UserStatus.Active.ToString();
                else
                    return "Inactive";
            }
        }

        #endregion Properties
    }
}
