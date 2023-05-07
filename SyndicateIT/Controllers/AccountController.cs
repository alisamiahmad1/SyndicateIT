
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SyndicateIT.Models;
using SyndicateIT.Filters;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Helper;
using SyndicateIT.Model.DomainModel.Corporte;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Controllers
{
    /// <summary>
    /// Class AccountController.
    /// </summary>
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : BaseController
    {

        #region Init

        /// <summary>
        /// The _sign in manager
        /// </summary>
        private ApplicationSignInManager _signInManager;

        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        /// <value>The sign in manager.</value>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        /// <summary>
        /// The _user manager
        /// </summary>
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>The user manager.</value>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }




        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {

        }

        #endregion

        #region Action      

        public ActionResult SetCulture(string culture)
        {

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion

        #region Login    

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            new CorporateDomaineModel().SetSessionCoporate();
            AuthenticationManager.SignOut();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string culture)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int bankId = SessionContent.Current.Corporate.BankID;
            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);


            if (user == null)
            {
                ViewBag.Alert = "alert-warning";
                ModelState.AddModelError("Error", "Invalid login attempt.");
                return View(model);
            }

            var rolesForUser = await UserManager.GetRolesAsync(user.Id);

            //Add this to check if the email was confirmed.
            if (!await UserManager.IsEmailConfirmedAsync(user.Id))
            {
                ViewBag.Alert = "alert-warning";
                ModelState.AddModelError("Error", "You need to confirm your email.");
                return View(model);
            }
            if (user.LockoutEnabled)
            {
                return View("Lockout");
            }

            //// Validate input
            //culture = CultureHelper.GetImplementedCulture(culture);
            //// Save culture in a cookie
            //HttpCookie cookie = Request.Cookies["_culture"];
            //if (cookie != null)
            //    cookie.Value = culture;   // update cookie value
            //else
            //{
            //    cookie = new HttpCookie("_culture");
            //    cookie.Value = culture;
            //    cookie.Expires = DateTime.Now.AddYears(1);
            //}
            //Response.Cookies.Add(cookie);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:

                    SessionContent.Container.Login.UserName = user.UserName;
                    SessionContent.Container.Login.Email = user.Email;
                    SessionContent.Container.Login.FirstName = user.FirstName;
                    SessionContent.Container.Login.LastName = user.LastName;
                   SessionContent.Container.Login.UserID = user.UserId;
                    SessionContent.Container.Login.UserGuidID = user.Id;
                    SessionContent.Container.Login.MenuPage = GetMenuPage();


                    var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Text == rolesForUser.FirstOrDefault()).FirstOrDefault();
                    SessionContent.Container.Login.UserRole = role.Text;
                    if (SessionContent.Container.Login.UserRole == UserRole.SuperAdministrator.ToString())
                    {
                        returnUrl = Utilities.GetUrl("index", "UserManagement", SessionContent.Current.Corporate.IsSecure);
                    }
                    else
                    {
                        var currentMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.Action != "").FirstOrDefault();
                        if(currentMenu!=null)
                        {
                            returnUrl = Utilities.GetUrl(currentMenu.Action, currentMenu.Controller, SessionContent.Current.Corporate.IsSecure);                           
                        }
                        else
                        {
                            ViewBag.Alert = "alert-warning";
                            ModelState.AddModelError("Error", "You don't have access in this application.");
                            return View(model);
                        }
                       
                    }

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ViewBag.Alert = "alert-warning";
                    ModelState.AddModelError("Error", "Invalid login attempt.");
                    return View(model);
            }
        }

        #endregion

        #region LogOff  

        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns>ActionResult.</returns>


        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Times the out.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult TimeOut()
        {

            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Verify Code

        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        #endregion

        #region Register   

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                bool isExitEmail = IsExitEmail(model.Email);
                if (isExitEmail)
                {                 
                    model.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = "Email already exists" };
                    ViewBag.Alert = "alert-warning";
                    ModelState.AddModelError("Error", "Email already exists.");
                    return View("Register", model);
                }

                bool isExitUserName = IsExitUserName(model.Username);
                if (isExitUserName)
                {
                    ViewBag.Alert = "alert-warning";
                    ModelState.AddModelError("Error", "UserName already exists.");
                    model.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = "UserName already exists" };
                    return View("Register", model);
                }

                var user = new ApplicationUser
                {
                    //UserId = GetNewUserID(),
                    UserName = model.Username,
                    Email = model.Email,
                    CreationDate = DateTime.UtcNow,
                    FirstName = model.FirstName,
                    LastName = model.LastName,    
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                 
                };

                string[] selectedRoles = null;
                var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Value == "fc185540-fd2a-4cf8-b7d0-425308e00b66").FirstOrDefault();
                if (role != null)
                    selectedRoles = new string[] { role.Text };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                   
                    var results = await UserManager.AddToRolesAsync(user.Id, selectedRoles);

                    using (var db1 = new SyndicatDataEntities())
                    {
                        
                        var etUsers = new User()
                        {
                            ENTRY_DATE = DateTime.Now,
                            Roles_ID = new Guid("fc185540-fd2a-4cf8-b7d0-425308e00b66"),
                            User_ID = Guid.NewGuid(),
                            Date_Of_Birth = Convert.ToDateTime(model.DateOfBirth),
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            MIddleName = model.MiddleName,
                            IS_ACTIVE = true,
                            Email = model.Email,
                            UserName = model.Username,
                            ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString())

                         };

                        db1.Users.Add(etUsers);
                        db1.SaveChanges();


                        var aspnetUser = db1.AspNetUsers.Where(p => p.Id == user.Id).FirstOrDefault();

                        if (aspnetUser != null)
                        {
                            aspnetUser.LockoutEnabled = false;
                            aspnetUser.UserId = etUsers.User_ID;
                            db1.SaveChanges();
                        }

                        var rolesForUser = await UserManager.GetRolesAsync(user.Id);
                        var roles = SessionContent.AppStore.RoleCodeList.Where(p => p.Text == rolesForUser.FirstOrDefault()).FirstOrDefault();
                        SessionContent.Container.Login.UserRole = roles.Text;


                        await SignInManager.PasswordSignInAsync(user.UserName, model.Password, false, shouldLockout: false);
                        SessionContent.Container.Login.UserID = etUsers.User_ID;
                    }
                    var  returnUrl = Utilities.GetUrl("MyProfile", "ProfileManagement", SessionContent.Current.Corporate.IsSecure);

                    SessionContent.Container.Login.UserName = user.UserName;
                    SessionContent.Container.Login.Email = user.Email;
                    SessionContent.Container.Login.FirstName = user.FirstName;
                    SessionContent.Container.Login.LastName = user.LastName;
                  
                    SessionContent.Container.Login.UserGuidID = user.Id;
                    SessionContent.Container.Login.MenuPage = GetMenuPage();

                    return RedirectToLocal(returnUrl);
                }

                
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Confirm Email

        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        #endregion

        #region Forgot Password

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        /// <summary>
        /// Forgots the password confirmation.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region Reset Password

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// Resets the password confirmation.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region Send Code

        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        #endregion

        #region External Login

        /// <summary>
        /// Externals the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        /// <summary>
        /// Externals the login callback.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        /// <summary>
        /// Externals the login confirmation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        /// <summary>
        /// Externals the login failure.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion       

        #region Helpers



        private MenuPage GetMenuPage()
        {
            return new MenuPage()
            {
                Text = "SkyWeb",
                MenuItems = new SyndicateIT.Model.DomainModel.Menu.MenuDomainModel().GetMenuItem()
            };
        }


        // Used for XSRF protection when adding external logins
        /// <summary>
        /// The XSRF key
        /// </summary>
        private const string XsrfKey = "XsrfId";

        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>The authentication manager.</value>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /// <summary>
        /// Class ChallengeResult.
        /// </summary>
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ChallengeResult"/> class.
            /// </summary>
            /// <param name="provider">The provider.</param>
            /// <param name="redirectUri">The redirect URI.</param>
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ChallengeResult"/> class.
            /// </summary>
            /// <param name="provider">The provider.</param>
            /// <param name="redirectUri">The redirect URI.</param>
            /// <param name="userId">The user identifier.</param>
            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            /// <summary>
            /// Gets or sets the login provider.
            /// </summary>
            /// <value>The login provider.</value>
            public string LoginProvider { get; set; }

            /// <summary>
            /// Gets or sets the redirect URI.
            /// </summary>
            /// <value>The redirect URI.</value>
            public string RedirectUri { get; set; }
            /// <summary>
            /// Gets or sets the user identifier.
            /// </summary>
            /// <value>The user identifier.</value>
            public string UserId { get; set; }

            /// <summary>
            /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
            /// </summary>
            /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        private bool IsExitEmail(string email)
        {
            using (var db = new SyndicatDataEntities())
            {
                var etUsers = db.AspNetUsers.Where(p => p.Email == email).FirstOrDefault();
                if (etUsers != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        private bool IsExitUserName(string username)
        {
            using (var db = new SyndicatDataEntities())
            {
                var etUsers = db.AspNetUsers.Where(p => p.UserName == username).FirstOrDefault();
                if (etUsers != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
