using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.UserManagement
{
    [Serializable]
    public class UserViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Display(Name = "Username", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessageResourceName = "NotAValidUsername", ErrorMessageResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]
        [Required]
        public string Username { get; set; }


        /// <value>The username.</value>
        [Required]
        [Display(Name = "Email", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[A-Za-z0-9-@.-_]*$", ErrorMessageResourceName = "NotAValidEmail", ErrorMessageResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.EditUserViewModel))]
        public string Email { get; set; }



        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [Display(Name = "Status", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserViewModel))]
        public Boolean Status { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleID { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public string BranchID { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        [Display(Name = "CreationDate", ResourceType = typeof(SyndicateIT.Model.ViewModel.UserManagement.Resources.UserViewModel))]
        [Required]
        public DateTime CreationDate { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = "Corporate User Id")]
        public string CorporateUserId { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        public int MobileNumber { get; set; }

        #endregion
    }
}
