using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SyndicateIT.Model.ViewModel.UserManagement
{
    [Serializable]
    public class EditUserViewModel : ViewModelBase
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
        [Display(Name = "Username", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]

        public string Username { get; set; }


        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The Email.</value>
        [Required]
        [Display(Name = "Email", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[A-Za-z0-9-@.-_]*$", ErrorMessageResourceName = "NotAValidEmail", ErrorMessageResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]
        public string Email { get; set; }




        /// <summary>
        /// Gets the read only username.
        /// </summary>
        /// <value>The read only username.</value>
        public string ReadOnlyUsername
        {
            get
            {
                return Username;
            }
        }

        /// <summary>
        /// Gets or sets the roles list.
        /// </summary>
        /// <value>The roles list.</value>
        public IEnumerable<SelectListItem> RolesList { get; set; }

        public IEnumerable<SelectListItem> BranchList { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        /// 
        [Required]
        [Display(Name = "Role ")]
        public string RoleID { get; set; }

        [Required]
        [Display(Name = "Branch ")]
        public string BranchID { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Corporate User Id")]
        public string CorporateUserId { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        public int MobileNumber { get; set; }

        public string HiddenUsername { get; set; }

        public string HiddenCorporateUserId { get; set; }


        public bool IsShowResetPassword { get; set; }


        #endregion

    }
}
