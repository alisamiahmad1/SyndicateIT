using SyndicateIT.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SyndicateIT.Models
{
    /// <summary>
    /// Class ExternalLoginConfirmationViewModel.
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /**
         @property  public string Email
        
         @brief Gets or sets the email.
        
         @return    The email.
         */

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// Class ExternalLoginListViewModel.
    /// </summary>
    public class ExternalLoginListViewModel
    {

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public string ReturnUrl { get; set; }
    }
   
    public class RegistrationApViewModel
    {
        [Required]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers of First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers of Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Enter only alphabets and numbers of Middle Name")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }



        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string PASSWORD { get; set; }

  
 
        //[Required]
        //[Display(Name ="Country")]
        //public String COUNTRY_ID { get; set; }
        public string ROLE_ID { get; set; }
        public string FACEBOOK_ID { get; set; }

    }
    /// <summary>
    /// Class SendCodeViewModel.
    /// </summary>
    public class SendCodeViewModel
    {

        /// <summary>
        /// Gets or sets the selected provider.
        /// </summary>
        /// <value>The selected provider.</value>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Class VerifyCodeViewModel.
    /// </summary>
    public class VerifyCodeViewModel
    {

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember browser].
        /// </summary>
        /// <value><c>true</c> if [remember browser]; otherwise, <c>false</c>.</value>
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Class ForgotViewModel.
    /// </summary>
    public class ForgotViewModel
    {

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [Display(Name = "UserName")]
        [MaxLength(100, ErrorMessage = "UserName Or Email cannot be longer than 100 characters.")]

        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(12, ErrorMessage = "Password cannot be longer than 12 characters.")]

        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


        [Display(Name = "Culture")]
        public string Culture { get; set; }
    }

    public class RegisterViewModel : ViewModelBase
    {

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        /// <value>The Username.</value>
        [Required]    
        [Display(Name = "Username")]
        public string Username { get; set; }


        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        /// <value>The FirstName.</value>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        /// <value>The Username.</value>
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

   

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        /// <value>The Username.</value>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        /// e>The password.</value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class ResetPasswordViewModel.
    /// </summary>
    public class ResetPasswordViewModel
    {

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class SocialLoginViewModel
    {
        [EmailAddress]
        public string EMAIL { get; set; }

        public string FACEBOOK_ID { get; set; }
        public string ROLE_ID { get; set; }
    }
}

