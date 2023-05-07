using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SyndicateIT.Model.DomainModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SyndicateIT.Model.UtilityModel;
using SyndicateIT.Model.ViewModel.UserManagement;
using SyndicateIT.Models;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.UtilityModel.Session;
using System.Configuration;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Controllers
{
    [Serializable]
    [Authorize]
    public class UserManagementController : BaseController
    {
        #region Init      

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagementController"/> class.
        /// </summary>
        public UserManagementController()
        {

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
        /// The _role manager
        /// </summary>
        private ApplicationRoleManager _roleManager;

        /// <summary>
        /// Gets the role manager.
        /// </summary>
        /// <value>The role manager.</value>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        #endregion       

        #region Action     

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Administration" && p.Action == "Index") != null && SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Administration" && p.Action == "Index").Count() > 0 ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            SessionContent.Container.UserManagement.ListUserManagement = null;
            var model = new UserManagementDomainModel().UserManagementContent();
            return View("~/Views/UserManagement/Index.cshtml", model);
        }

        #region Manage User

        /// <summary>
        /// Loads the grid user.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult LoadGridUser()
        {
            var model = new UserManagementViewModel();
            SessionContent.Container.UserManagement.ListUserManagement = null;
            return PartialView("_GridUserPartial", model);
        }

        /// <summary>
        /// Users_s the read.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ActionResult.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Read([DataSourceRequest] DataSourceRequest request)
        {
            bool isRefresh = SessionContent.Container.UserManagement.ListUserManagement != null ? false : true;
            var u_Users = GetUserManagementViewModel(isRefresh);
            return Json(u_Users.ToDataSourceResult(request));
        }

        /// <summary>
        /// Deactives the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public async Task<ActionResult> DeactiveUser(string id)
        {
            SessionContent.Container.UserManagement.ListUserManagement = null;
            string message = string.Empty;
            bool d_Profiles = await GetLockoutEnabled(id, true);
            return Json(new { success = d_Profiles, msg = message });
        }

        /// <summary>
        /// Actives the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public async Task<ActionResult> ActiveUser(string id)
        {
            SessionContent.Container.UserManagement.ListUserManagement = null;
            string message = string.Empty;
            bool d_Profiles = await GetLockoutEnabled(id, false);
            return Json(new { success = d_Profiles, msg = message });
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult EditUser(string id)
        {
            return Json(new { url = Url.Action("Edit", "UserManagement", new { id = id }) });
        }

        #endregion

        #region Albert

        //public ActionResult MasterCardPayment()
        //{           
        //    var tokenModel = new Models.TokenResponse()
        //    {
        //        result = "Ok",
        //        status = "Valid",
        //        token = Guid.NewGuid().ToString().ToUpper(),
        //        verificationStrategy = "Basic"
        //    };
        //    return View("~/views/Home/TokenResponse.cshtml", tokenModel);          
        //}

        #endregion

        #region User   


        public async Task<ActionResult> Create()
        {
            var title = "Create a new user";
            UserViewModel etUserViewModel = new UserManagementDomainModel().GetUserViewModel(title);            
            return View("~/Views/UserManagement/Create.cshtml", etUserViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel userViewModel)
        {
            UserViewModel etUserViewModel = new UserViewModel();
            ViewBag.SubTitle = "Create a new user";
            if (ModelState.IsValid)
            {


                bool isExitEmail = IsExitEmail(userViewModel.Email);
                if(isExitEmail)
                {
                    etUserViewModel.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Create a new user");
                    etUserViewModel.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = "Email already exists" };
                    return View("Create", etUserViewModel);
                }

                bool isExitUserName = IsExitUserName(userViewModel.Username);
                if (isExitUserName)
                {
                    etUserViewModel.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Create a new user");
                    etUserViewModel.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = "UserName already exists" };
                    return View("Create", etUserViewModel);
                }

                var user = new ApplicationUser {
                    //UserId = GetNewUserID(),
                    UserName = userViewModel.Username,
                    Email = userViewModel.Email,
                    CreationDate = DateTime.UtcNow,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Branch = userViewModel.BranchID,
                    CorporateUserId = userViewModel.CorporateUserId,
                    PhoneNumber = userViewModel.MobileNumber.ToString(),
                    
                };

                var adminresult = await UserManager.CreateAsync(user);
                string[] selectedRoles = null;
                var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Value == userViewModel.RoleID).FirstOrDefault();
                if(role!= null)
                  selectedRoles = new string[] { role.Text };             

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            etUserViewModel.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Create a new user");
                            etUserViewModel.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = result.Errors.First() };
                            return View("Create", etUserViewModel);
                        }
                        adminresult = UserManager.SetLockoutEnabled(user.Id, false);
                    }
                }
                else
                {
                    etUserViewModel.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Create a new user");
                    etUserViewModel.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = adminresult.Errors.First() };
                    return View("Create", etUserViewModel);
                }
               
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Manage", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                string body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                string email = ConfigurationManager.AppSettings["EmailSmtpUsername"];
                await SendEmail(user.Email, "Confirm your account", "SkyWeb Registration", callbackUrl, body, email);
                return RedirectToAction("Index");
            }

            return View("Create", etUserViewModel);
        }
     
        public async Task<ActionResult> Edit(string id)
        {
            var title = "Edit user";
            EditUserViewModel etUserViewModel = new UserManagementDomainModel().GetEditUserViewModel(title);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userRoles = await UserManager.GetRolesAsync(user.Id);                       
            etUserViewModel.ID = user.Id;
            etUserViewModel.Username = user.UserName;
            etUserViewModel.Email = user.Email;
            if (UserRole.SuperAdministrator.ToString() == SessionContent.Container.Login.UserRole.ToString())
                ViewBag.IsReadOnly = false;

            var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Text == userRoles.FirstOrDefault()).FirstOrDefault();
            if(role!=null)
              etUserViewModel.RoleID = role.Value;
            etUserViewModel.FirstName = user.FirstName;
            etUserViewModel.LastName = user.LastName;
            etUserViewModel.CorporateUserId = user.CorporateUserId;
            etUserViewModel.BranchID = user.Branch;
            etUserViewModel.MobileNumber = Convert.ToInt32(user.PhoneNumber);
            etUserViewModel.HiddenUsername = user.UserName;
            etUserViewModel.HiddenCorporateUserId = user.CorporateUserId;
            etUserViewModel.IsShowResetPassword = user.Email != null && user.Email != string.Empty && (user.IsPasswordTemp == null || user.IsPasswordTemp == false);


            return View("Edit", etUserViewModel);
        }

        /// <summary>
        /// Edits the specified edit user.
        /// </summary>
        /// <param name="editUser">The edit user.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser)
        {
            string username = editUser.HiddenUsername;
            string CorporateUserId = editUser.HiddenCorporateUserId;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.ID);
                if (user == null)
                {
                    return HttpNotFound();
                }              

                user.Email = editUser.Email;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.PhoneNumber = editUser.MobileNumber.ToString();               
                user.Branch = editUser.BranchID;
         
                var userRoles = await UserManager.GetRolesAsync(user.Id);
             

                string[] selectedRoles = null;
                var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Value == editUser.RoleID).FirstOrDefault();
                if (role != null)
                    selectedRoles = new string[] { role.Text };


                var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    editUser.Username = username;
                    editUser.CorporateUserId = CorporateUserId;
                    editUser.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Edit user");
                    editUser.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = result.Errors.First() }; 
                    return View("Edit", editUser);
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    editUser.Username = username;
                    editUser.CorporateUserId = CorporateUserId;
                    editUser.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Edit user");
                    editUser.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = result.Errors.First() };                    
                    return View("Edit", editUser);
                }
                return RedirectToAction("Index");
            }
            editUser.Username = username;
            editUser.CorporateUserId = CorporateUserId;
            editUser.Navigation = new UserManagementDomainModel().GetNavigationList("user", "Edit user");
            editUser.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = "Something failed." };
            return View("Edit", editUser);
        }


        #endregion

        #region Email

        #endregion

        #region Reset Password


        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        public async Task<ActionResult> ResetPassword(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            user.EmailConfirmed = false;
            UserManager.Update(user);
            UserManager.RemovePassword(id);
            string email = ConfigurationManager.AppSettings["EmailSmtpUsername"];
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Manage", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            string body = "Hi" + " " + user.FirstName + " " + user.LastName +
                          "<br/><br/>" + "You recently requested a password reset." +
                          "<br/><br/>" + "To reset your password, click <a href=\"" + callbackUrl + "\"> here</a><br/> If you need to reach us, please drop us a word at <a href='mailto: support @pin-pay.com'>support@pin-pay.com</a>" +
                          "<br/><br/> Thanks for using SkyWeb! <br/>The Pinpay Team";

            await SendEmail(user.Email, "Reset Password", "SkyWeb Registration", callbackUrl, body, email);
            return RedirectToAction("ResetPasswordConfirmation", "UserManagement");
        }

        /// <summary>
        /// Resets the password confirmation.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult ResetPasswordConfirmation()
        {
            string title = "Reset Password";
            ResetPasswordConfirmation etResetPasswordConfirmation = new UserManagementDomainModel().GetResetPasswordConfirmation(title);
            etResetPasswordConfirmation.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Success, Message = "Reset Password Successful! An Email has been sent to the User containing a link to create a new password." };
            return View("ResetPasswordConfirmation", etResetPasswordConfirmation);
        }

        #endregion

        #endregion

        #region Methode    

        /// <summary>
        /// Gets the lockout enabled.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">if set to <c>true</c> [type].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private async Task<bool> GetLockoutEnabled(string id, bool type)
        {
            var result = await UserManager.SetLockoutEnabledAsync(id, type);
            if (result.Succeeded)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the user management view model.
        /// </summary>
        /// <param name="isRefresh">if set to <c>true</c> [is refresh].</param>
        /// <returns>List&lt;UserManagementViewModel&gt;.</returns>
        private List<UserManagementViewModel> GetUserManagementViewModel(bool isRefresh)
        {
            List<UserManagementViewModel> lst = new List<UserManagementViewModel>();
            UserManagementViewModel etUser = new UserManagementViewModel();
            var lstDB = UserManager.Users.Where(p => p.Id != SessionContent.Container.Login.UserGuidID).ToList();
            int roleId = (int)UserRole.CustomerSupport;

            if (isRefresh)
            {
                for (int i = 0; i < lstDB.Count; i++)
                {
                    var roles = UserManager.GetRoles(lstDB[i].Id);
                    etUser = new UserManagementViewModel()
                    {
                        ID = lstDB[i].Id.ToString(),
                        CreationDate = lstDB[i].CreationDate,
                        RoleID = roles.FirstOrDefault(),
                        StatusID = Convert.ToBoolean(lstDB[i].LockoutEnabled),
                        Username = lstDB[i].UserName,
                        Email = lstDB[i].Email,
                        BranchId = lstDB[i].Branch


                    };
                    lst.Add(etUser);
                }

                SessionContent.Container.UserManagement.ListUserManagement = lst;
            }
            else
            {
                lst = SessionContent.Container.UserManagement.ListUserManagement;
            }

            return lst.OrderByDescending(d => d.CreationDate).ToList();
        }

        /// <summary>
        /// Errors the specified alert.
        /// </summary>
        /// <param name="alert">The alert.</param>
        /// <param name="message">The message.</param>
        private void Error(string alert, string message)
        {
            ViewBag.Alert = alert;
            ModelState.AddModelError("Error", message);
        }

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

        //private int GetNewUserID()
        //{
        //    using (var db = new SchoolDataEntities())
        //    {
        //        var userId = db.AspNetUsers.Max(p => p.UserId);
        //        if (userId > 0)
        //        {
        //            return userId + 1;
        //        }
        //        else
        //            return 1;
        //    }
        //}

        #endregion
    }
}
