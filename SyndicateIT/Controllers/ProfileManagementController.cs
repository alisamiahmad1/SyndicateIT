using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.DomainModel.Insurance;
using SyndicateIT.Model.DomainModel.ProfileManagement;
using SyndicateIT.Model.DomainModel.User_Documents;
using SyndicateIT.Model.ProcessorModel.ProfileManagement;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.ProfileManagement;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.User_Documents;
using SyndicateIT.Models;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace SyndicateIT.Controllers
{
    [Serializable]
    [Authorize]
    public class ProfileManagementController : BaseController
    {
        #region Init      


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

        public ActionResult MyProfile()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Profiles" && p.Action == "MyProfile") != null ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.UserType = "Members";
            
            var sourceId = (int)ProfileSourceType.MyProfile;
            var userId = SessionContent.Container.Login.UserID;

            SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();          
            SessionContent.Container.Profiles.SourceID = sourceId;
            SessionContent.Container.Profiles.CurrentProfilesID = userId;
            SessionContent.Container.Profiles.UserType = UserType.Members;
            var model = new ProfilesViewModel()
            {
                PartialViewName = "~/Views/ProfileManagement/Profiles/_ProfilesContentDetailsPartial.cshtml",
                PartialViewNameModel = new ProfileManagementDomainModel().GetProfilesContentDetailsViewModel(UserType.Members, sourceId),
                Title = new ProfileManagementDomainModel().GetTitle(UserType.Members, sourceId),
            };

            return View("Index", model);
        }

        public ActionResult Members()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Profiles" && p.Action == "Students") != null  ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.UserType = "Members";
            SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();
            SessionContent.Container.Profiles.UserType = UserType.Members;
            SessionContent.Container.Profiles.SourceID = (int)ProfileSourceType.ProfileInforamtion;
            var model = new ProfileManagementDomainModel().GetProfiles(UserType.Members, (int)ProfileSourceType.ProfileInforamtion);
            return View("Index", model);
        }
       
        public ActionResult MemberBoard()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Profiles" && p.Action == "Parents") != null  ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.UserType = "MemberBoard";
            SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();
            SessionContent.Container.Profiles.UserType = UserType.MemberBoard;
            SessionContent.Container.Profiles.SourceID = (int)ProfileSourceType.ProfileInforamtion;
            var model = new ProfileManagementDomainModel().GetProfiles(UserType.MemberBoard, (int)ProfileSourceType.ProfileInforamtion);
            return View("Index", model);
        }

        public ActionResult Employees()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Profiles" && p.Action == "Employees") != null ? true : false ;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.UserType = "Employe";
            SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();
            SessionContent.Container.Profiles.UserType = UserType.Employees;
            SessionContent.Container.Profiles.SourceID = (int)ProfileSourceType.ProfileInforamtion;
            var model = new ProfileManagementDomainModel().GetProfiles(UserType.Employees, (int)ProfileSourceType.ProfileInforamtion);
            return View("Index", model);
        }

        public ActionResult Agent()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Profiles" && p.Action == "Agent") != null ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.UserType = "Agent";
            SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();
            SessionContent.Container.Profiles.UserType = UserType.Agent;
            SessionContent.Container.Profiles.SourceID = (int)ProfileSourceType.ProfileInforamtion;
            var model = new ProfileManagementDomainModel().GetProfiles(UserType.Agent, (int)ProfileSourceType.ProfileInforamtion);
            return View("Index", model);
        }

        public ActionResult Administrator()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Profiles" && p.Action == "Division") != null && SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "SchoolSettings" && p.Controller == "Cycle").Count() > 0 ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.UserType = "Administrator";
            SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();
            SessionContent.Container.Profiles.UserType = UserType.SuperAdministrator;
            SessionContent.Container.Profiles.SourceID = (int)ProfileSourceType.ProfileInforamtion;
            var model = new ProfileManagementDomainModel().GetProfiles(UserType.SuperAdministrator, (int)ProfileSourceType.ProfileInforamtion);
            return View("Index", model);
        }

        public ActionResult LoadProfilesContent()
        {
            SessionContent.Container.Profiles.IsLoadProfilesContent = true;
            return PartialView("~/Views/ProfileManagement/Profiles/_ProfilesContentPartial.cshtml", new ProfileManagementDomainModel().ProfilesContent(SessionContent.Container.Profiles.ProfilesSearch.UserType, (int)ProfileSourceType.ProfileInforamtion));
        }

        #region Profile List 

        public ActionResult LoadProfilesList(string searchFirstName, string searchLastName, string searchCycle, string searchClass, string searchDivision, string userType)
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;
            SessionContent.Container.Profiles.IsLoadProfilesContent = false;
            SessionContent.Container.Profiles.ProfilesSearch = new Model.ViewModel.ProfileManagement.ProfilesSearchViewModel()
            {
                SearchFirstName = searchFirstName,
                SearchLastName = searchLastName,
                SearchCycle = searchCycle,
                SearchClass = searchClass,
                SearchDivision = searchDivision,
            };

            SessionContent.Container.Profiles.ListProfiles = null;
            var model = new ProfileManagementDomainModel().GetProfilesGridViewModel(userType, searchFirstName, searchLastName,  searchCycle,  searchClass,  searchDivision, out message, out isSuccess, out alertType);
            if (isSuccess)
                return PartialView("~/Views/ProfileManagement/Profiles/_GridProfilesPartial.cshtml", model);
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProfileList_Read([DataSourceRequest] DataSourceRequest request, UserType userType, string SearchFirstName, string SearchLastName, string SearchCycle, string SearchClass, string SearchDivision)
        {
            bool isRefresh = SessionContent.Container.Profiles.ListProfiles != null ? false : true;
            var l_PlansSetup = new ProfileManagementDomainModel().GetProfilesList(isRefresh, userType, SearchFirstName, SearchLastName, SearchCycle, SearchClass, SearchDivision);
            return Json(l_PlansSetup.ToDataSourceResult(request));
        }

        #endregion

        #region Profile Details


        public ActionResult ViewProfilesContentDetails(string userId, int sourceId = 1)
        {
            if (userId != null)
            {
                SessionContent.Container.Profiles = new Model.UtilityModel.Session.ContainerEntities.Profiles.Profiles();
                SessionContent.Container.Profiles.CurrentProfilesID = new Guid(userId);
                SessionContent.Container.Profiles.SourceID = sourceId;
            }
            if ((int)ProfileSourceType.FileStudents == sourceId)
            {
                @ViewBag.UserType = "Members";
                SessionContent.Container.Profiles.UserType = UserType.Members;
            }
            else if ((int)ProfileSourceType.FileStudent == sourceId)
            {
                @ViewBag.UserType = "Members";
                SessionContent.Container.Profiles.UserType = UserType.Members;
            }
            else if ((int)ProfileSourceType.FileStudent == sourceId)
            {
                @ViewBag.UserType = "Members";
                SessionContent.Container.Profiles.UserType = UserType.Members;
            }
            else if ((int)ProfileSourceType.FileParent == sourceId)
            {
                @ViewBag.UserType = "MemberBoard";
                SessionContent.Container.Profiles.UserType = UserType.MemberBoard;
            }
            var model = new ProfilesViewModel()
            {
                PartialViewName = "~/Views/ProfileManagement/Profiles/_ProfilesContentDetailsPartial.cshtml",
                PartialViewNameModel = new ProfileManagementDomainModel().GetProfilesContentDetailsViewModel(SessionContent.Container.Profiles.UserType, sourceId),
                Title = new ProfileManagementDomainModel().GetTitle(SessionContent.Container.Profiles.UserType, sourceId),
            };

            return View("Index", model);
        }

        public ActionResult LoadProfilesContentDetails(string userId)
        {
            if (userId != null)
            {
                SessionContent.Container.Profiles.SourceID = (int)ProfileSourceType.ProfileInforamtion;
                SessionContent.Container.Profiles.CurrentProfilesID = new Guid(userId);
            }
            return PartialView("~/Views/ProfileManagement/Profiles/_ProfilesContentDetailsPartial.cshtml", new ProfileManagementDomainModel().GetProfilesContentDetailsViewModel(SessionContent.Container.Profiles.UserType, (int)ProfileSourceType.ProfileInforamtion));
        }

        #region Personal Information
        public ActionResult LoadPersonalInformation()
        {
            Guid User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            var model = new ProfileManagementProcessor().GetPersonalInformation(User_ID);

            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_PersonalInformationsPartial.cshtml", model);
        }
        public async Task<ActionResult> SavePersonalInformation(PersonalInformationsViewModel etPersonalInformationsViewModel)
        {
            string userId = string.Empty;
            if (SessionContent.Container.Profiles.PersonalInformations != null)
            {
                etPersonalInformationsViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            }
            SessionContent.Container.Profiles.PersonalInformations = etPersonalInformationsViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            bool isExitEmail = IsExitEmail(etPersonalInformationsViewModel.Email, etPersonalInformationsViewModel.User_ID);
            if (isExitEmail == true)
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Warning, Message = "Email already exists" };
                var model = SessionContent.Container.Profiles.PersonalInformations;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_PersonalInformationsPartial.cshtml", model);
            }
            isSuccess = new ProfileManagementDomainModel().SaveUsers(etPersonalInformationsViewModel);
            if (isSuccess[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.PersonalInformations;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                bool isExitUser = IsExitUser(etPersonalInformationsViewModel.User_ID, out userId);
                if (isExitUser == true)
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        user.Email = etPersonalInformationsViewModel.Email;
                        user.FirstName = etPersonalInformationsViewModel.FirstName;
                        user.LastName = etPersonalInformationsViewModel.LastName;
                        user.PhoneNumber = etPersonalInformationsViewModel.MobileNumber;
                        user.UserName = etPersonalInformationsViewModel.UserName;
                        var userRoles = await UserManager.GetRolesAsync(user.Id);
                        string[] selectedRoles = null;
                        var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Value == etPersonalInformationsViewModel.Roles_ID.ToString()).FirstOrDefault();
                        if (role != null)
                            selectedRoles = new string[] { role.Text };
                        var result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRoles).ToArray<string>());
                        result = await UserManager.AddToRolesAsync(user.Id, selectedRoles.Except(userRoles).ToArray<string>());
                    }
                }
                else
                {
                    var user = new ApplicationUser
                    {
                        UserId = SessionContent.Container.Profiles.CurrentProfilesID,
                        UserName = etPersonalInformationsViewModel.UserName,
                        Email = etPersonalInformationsViewModel.Email,
                        CreationDate = DateTime.UtcNow,
                        FirstName = etPersonalInformationsViewModel.FirstName,
                        LastName = etPersonalInformationsViewModel.LastName,
                        PhoneNumber = etPersonalInformationsViewModel.MobileNumber,
                    };
                    IdentityResult adminresult = await UserManager.CreateAsync(user);
                    string[] selectedRoles = null;
                    var role = SessionContent.AppStore.RoleCodeList.Where(p => p.Value == etPersonalInformationsViewModel.Roles_ID.ToString()).FirstOrDefault();
                    if (role != null)
                        selectedRoles = new string[] { role.Text };
                    if (adminresult.Succeeded)
                    {
                        if (selectedRoles != null)
                        {
                            var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                            if (!result.Succeeded)
                            {
                                alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = result.Errors.First() };
                                model.Alert = alertModel;
                                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_PersonalInformationsPartial.cshtml", model);
                            }
                            else
                            {
                                bool d_LockoutEnabled = await GetLockoutEnabled(user.Id, true);
                            }
                            model.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
                        }

                    }
                }
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_PersonalInformationsPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.PersonalInformations;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_PersonalInformationsPartial.cshtml", model);
            }
        }

        #endregion

        #region Contact Information

        public ActionResult LoadContactDetails()
        {
            SessionContent.Container.Profiles.ContactInformations = new ContactInformationsViewModel();
            Guid User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            var model = new ProfileManagementProcessor().GetContactInformation(User_ID);
            SessionContent.Container.Profiles.ContactInformations = model;
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ContactInformationsPartial.cshtml", model);
        }

        public ActionResult SaveContactInformations(ContactInformationsViewModel etContactInformationsViewModel)
        {
            if (SessionContent.Container.Profiles.ContactInformations != null)
            {
                etContactInformationsViewModel.User_Contact_ID = SessionContent.Container.Profiles.ContactInformations.User_Contact_ID;
                etContactInformationsViewModel.SecondUser_Contact_ID = SessionContent.Container.Profiles.ContactInformations.SecondUser_Contact_ID;
            }
            //To Do get after saving Profile Information
            etContactInformationsViewModel.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            SessionContent.Container.Profiles.ContactInformations = etContactInformationsViewModel;
            String[] message = new string[2];
            AlertType alertType = AlertType.Success;

            message = new ProfileManagementProcessor().SaveContactInformation(etContactInformationsViewModel);
            if (message[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message[1] };
                var model = SessionContent.Container.Profiles.ContactInformations;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ContactInformationsPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message[1] };
                var model = SessionContent.Container.Profiles.ContactInformations;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ContactInformationsPartial.cshtml", model);
            }

        }

        #endregion

        #region Insurance

        public ActionResult LoadInsurrance()
        {
            Guid User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            var model = new ProfileManagementProcessor().GetInsurance(User_ID);

            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_InsurancetInformationsPartial.cshtml", model);
        }

        public ActionResult SaveInsurance(SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails.InsuranceViewModel etInsuranceViewModel)
        {

            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            string[] messages = new string[2];
            etInsuranceViewModel.Users_Id = SessionContent.Container.Profiles.CurrentProfilesID;
            SessionContent.Container.Profiles.Insurances = etInsuranceViewModel;
            messages = new InsurancesDomainModel().SaveInsurance(etInsuranceViewModel, out message, out alertType);
            if (messages[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = messages[1] };
                var model = SessionContent.Container.Profiles.Insurances;
                model.Alert = alertModel;
                model.Insurance_Id = new Guid(messages[2].ToString());
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_InsurancetInformationsPartial.cshtml", model);

            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = messages[1] };
                var model = SessionContent.Container.Profiles.ContactInformations;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_InsurancetInformationsPartial.cshtml", model);
            }

        }

        #endregion

        #region Employment

        public ActionResult LoadEmployment()
        {
            Guid User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            var model = new ProfileManagementProcessor().GetEmploymentViewModel(User_ID);
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EmploymentPartial.cshtml", model);
        }

        public ActionResult SaveEmployment(EmploymentViewModel etEmploymentViewModel)
        {
            if (SessionContent.Container.Profiles.Employments != null)
            {
                etEmploymentViewModel.User_Employment_ID = SessionContent.Container.Profiles.Employments.User_Employment_ID;
            }
            SessionContent.Container.Profiles.Employments = etEmploymentViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            etEmploymentViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            isSuccess = new ProfileManagementDomainModel().SaveEmployment(etEmploymentViewModel);

            if (isSuccess[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Employments;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EmploymentPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Employments;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EmploymentPartial.cshtml", model);
            }

        }
        #endregion

        #region Dependent

        public ActionResult LoadDependent()
        {

            var model = new ProfileManagementProcessor().GetDependentViewModel();

            LoadDependentsList(model);

            SessionContent.Container.Profiles.Dependents = model;
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DependentPartial.cshtml", model);
        }
        public ActionResult SaveDependent(DependentViewModel etDependentViewModel)
        {
            SessionContent.Container.Profiles.Dependents = etDependentViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            etDependentViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            isSuccess = new ProfileManagementDomainModel().SaveDependent(etDependentViewModel);

            if (isSuccess[0] == "s")
            {
                LoadDependentsList(etDependentViewModel);
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new DependentViewModel();
                model.Dependents = etDependentViewModel.Dependents;
                model.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
                SessionContent.Container.Profiles.Dependents = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;


                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DependentPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new DependentViewModel();
                model.Dependents = SessionContent.Container.Profiles.Dependents.Dependents;
                SessionContent.Container.Profiles.Dependents = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DependentPartial.cshtml", model);
            }

        }
        public ActionResult DeleteDependent(DependentViewModel lstDependent, int i)
        {
            // SessionContent.Container.Profiles.ListDependents = lstDependent;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];

            isSuccess = new ProfileManagementDomainModel().DeleteDependent(SessionContent.Container.Profiles.Dependents.Dependents[i]);

            if (isSuccess[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Dependents;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                LoadDependentsList(model);
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DependentPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Dependents;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DependentPartial.cshtml", model);
            }
        }
        public ActionResult LoadDependentsList(DependentViewModel dep)
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;

            SessionContent.Container.Profiles.ListDependents = null;
            var model = new ProfileManagementDomainModel().GetDependentListViewModel(out message, out isSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
            if (isSuccess)
            {
                dep.Dependents = model;
                return PartialView("~/Views/ProfileManagement/Profiles/_ListDependentsPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        #endregion

        #region Accounts
        public ActionResult LoadAccount()
        {

            var model = new AccountViewModel();
            //LoadAccountsList(model);    
            var lst = new List<AccountViewModel>();          
            var modelAccount = new AccountViewModel()
            {
                AccountName = "Ac123",
                AccountNumber = "4534534534534",
                BankName = "Audi",
                BankNumber = "1234567898",
                Country = "Lebanon",
                CloseDate = DateTime.Now,
                CreationDate = DateTime.Now,
                Currency = "USD",
                Description = "",
                AccountType = "Credit",

            };


            lst.Add(modelAccount);


            model.Accounts = lst;
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_AccountPartial.cshtml", model);
        }

        public ActionResult LoadAccountsList(AccountViewModel account)
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;
            var lst = new List<AccountViewModel>();
            SessionContent.Container.Profiles.ListExperiences = null;
            var model = new AccountViewModel()
            {
                AccountName = "Ac123",
                AccountNumber = "4534534534534",
                BankName = "Audi",
                BankNumber = "1234567898",
                Country = "Lebanon",
                CloseDate = DateTime.Now,
                CreationDate = DateTime.Now,
                Currency = "USD",
                Description = "",
                AccountType ="Credit",             
                
            };


            lst.Add(model);



            if (isSuccess)
            {               
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails_ListAccountPartial.cshtml", lst);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        #endregion

        #region Transaction

        public ActionResult LoadTransaction()
        {

            var model = new TransactionViewModel();              
            var lst = new List<TransactionViewModel>();
            var modelTransaction = new TransactionViewModel()
            {                

            };          
     
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_TransactionPartial.cshtml", model);
        }

        

        #endregion


        #region Experience

        public ActionResult LoadExperience()
        {

            var model = new ProfileManagementProcessor().GetExperienceViewModel();

            LoadExperiencesList(model);

            SessionContent.Container.Profiles.Experiences = model;
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ExperiencePartial.cshtml", model);
        }
        public ActionResult SaveExperience(ExperienceViewModel etExperienceViewModel)
        {
            SessionContent.Container.Profiles.Experiences = etExperienceViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            etExperienceViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            isSuccess = new ProfileManagementDomainModel().SaveExperience(etExperienceViewModel);

            if (isSuccess[0] == "s")
            {
                LoadExperiencesList(etExperienceViewModel);
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new ExperienceViewModel();
                model.Experiences = etExperienceViewModel.Experiences;
                model.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
                SessionContent.Container.Profiles.Experiences = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;


                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ExperiencePartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new ExperienceViewModel();
                model.Experiences = SessionContent.Container.Profiles.Experiences.Experiences;
                SessionContent.Container.Profiles.Experiences = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ExperiencePartial.cshtml", model);
            }

        }
        public ActionResult DeleteExperience(ExperienceViewModel lstDependent, int i)
        {
            
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];

            isSuccess = new ProfileManagementDomainModel().DeleteExperience(SessionContent.Container.Profiles.Experiences.Experiences[i]);

            if (isSuccess[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Experiences;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                LoadExperiencesList(model);
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ExperiencePartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Experiences;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ExperiencePartial.cshtml", model);
            }
        }
        public ActionResult LoadExperiencesList(ExperienceViewModel exp)
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;

            SessionContent.Container.Profiles.ListExperiences = null;
            var model = new ProfileManagementDomainModel().GetExperienceListViewModel(out message, out isSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
            if (isSuccess)
            {
                exp.Experiences = model;
                return PartialView("~/Views/ProfileManagement/Profiles/_ListExperiencePartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        #endregion

        #region Education

        public ActionResult LoadEducation()
        {

            var model = new ProfileManagementProcessor().GetEducationViewModel();

            LoadEducationsList(model);

            SessionContent.Container.Profiles.Educations = model;
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EducationPartial.cshtml", model);
        }
        public ActionResult SaveEducation(EducationViewModel etEducationViewModel)
        {
            SessionContent.Container.Profiles.Educations = etEducationViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            etEducationViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            isSuccess = new ProfileManagementDomainModel().SaveEducation(etEducationViewModel);

            if (isSuccess[0] == "s")
            {
                LoadEducationsList(etEducationViewModel);
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new EducationViewModel();
                model.Educations = etEducationViewModel.Educations;
                model.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
                SessionContent.Container.Profiles.Educations = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;


                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EducationPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new EducationViewModel();
                model.Educations = SessionContent.Container.Profiles.Educations.Educations;
                SessionContent.Container.Profiles.Educations = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EducationPartial.cshtml", model);
            }

        }
        public ActionResult DeleteEducation(EducationViewModel lsteducation, int i)
        {

            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];

            isSuccess = new ProfileManagementDomainModel().DeleteEducation(SessionContent.Container.Profiles.Educations.Educations[i]);

            if (isSuccess[0] == "s")
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Educations;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                LoadEducationsList(model);
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EducationPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Educations;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_EducationPartial.cshtml", model);
            }
        }
        public ActionResult LoadEducationsList(EducationViewModel exp)
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;

            SessionContent.Container.Profiles.ListEducations = null;
            var model = new ProfileManagementDomainModel().GetEducationListViewModel(out message, out isSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
            if (isSuccess)
            {
                exp.Educations = model;
                return PartialView("~/Views/ProfileManagement/Profiles/_ListEducationPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        #endregion

        #region System Privileges

        public ActionResult LoadSystemPrivileges()
        {
            Guid profileId = SessionContent.Container.Profiles.CurrentProfilesID;
            var model = new ProfileManagementDomainModel().GetSystemPrivilegeViewModel(profileId, SessionContent.Container.Profiles.UserType);
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_SystemPrivilegesPartial.cshtml", model);
        }

        public async Task<ActionResult> SaveSystemPrivileges(SystemPrivilegesViewModel etSystemPrivilegesViewModel)
        {
            SessionContent.Container.Profiles.SystemPrivileges = etSystemPrivilegesViewModel;
            string message = string.Empty;
            string userId = "";
            AlertType alertType = AlertType.Success;
            AlertViewModel alertModel = new AlertViewModel();
            var model = SessionContent.Container.Profiles.SystemPrivileges;
            etSystemPrivilegesViewModel.Type = SessionContent.Container.Profiles.UserType.ToString();
            try
            {
                bool isExitUser = IsExitUser(etSystemPrivilegesViewModel.UserId, out userId);
                if (isExitUser == true)
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        if (etSystemPrivilegesViewModel.Enable == true)
                        {

                            bool d_Profiles = await GetLockoutEnabled(user.Id, false);
                            if (etSystemPrivilegesViewModel.HasEmail == true)
                            {
                                user.EmailConfirmed = false;
                                UserManager.Update(user);
                                UserManager.RemovePassword(userId);
                                string email = ConfigurationManager.AppSettings["EmailSmtpUsername"];
                                string codeEmailConfirmation = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                                var callbackUrl = Url.Action("ConfirmEmail", "Manage", new { userId = user.Id, code = codeEmailConfirmation }, protocol: Request.Url.Scheme);
                                string body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                                await SendEmail(user.Email, "Confirm your account", "SkyWeb Registration", callbackUrl, body, email);

                            }
                            else
                            {
                                UserManager.AddPassword(userId, etSystemPrivilegesViewModel.Password);
                                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                                var resultCode = await UserManager.ConfirmEmailAsync(user.Id, code);
                                model.HasLoginUser = true;
                            }
                        }
                        else
                        {
                            bool d_Profiles = await GetLockoutEnabled(user.Id, true);
                        }

                    }
                }
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                model.HasLoginUser = true;
                model.HasEmail = false;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_SystemPrivilegesPartial.cshtml", model);
            }
            catch (Exception ex)
            {
                alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = ex.Message };
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_SystemPrivilegesPartial.cshtml", model);
            }
        }

        #endregion

        #region Document

        public ActionResult LoadDocument()
        {
            ViewBag.isEditable = SessionContent.Container.Login.UserRole == UserType.SuperAdministrator.ToString() || SessionContent.Container.Login.UserRole == UserType.Administrator.ToString();
            ViewBag.isFullEdit = SessionContent.Container.Login.UserRole == UserType.SuperAdministrator.ToString();
            var model = new ProfileManagementDomainModel().GetDocumentViewModel(SessionContent.Container.Profiles.CurrentProfilesID);
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DocumentPartial.cshtml", model);
        }

        public ActionResult SaveDocument(DocumentViewModel etDocumentViewModel)
        {
            SessionContent.Container.Profiles.Documents = etDocumentViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            bool isSuccess = false;

            // isSuccess = new ProfileManagementDomainModel().SaveDocument(etDocumentViewModel, out message, out alertType);

            if (isSuccess == true)
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Insurances;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_DocumentPartial.cshtml", model);
            }

        }

        public JsonResult uploadUserDocuments(List<HttpPostedFileBase> files, string User_ID = "")
        {

            if (files == null)
            {
                var pfiles = RenderPartialViewToString("~/Views/ProfileManagement/Profiles/ProfileDetails/_ListFilePartial.cshtml", new User_DocumentsDomainModel().GetListUser_Documents(SessionContent.Container.Profiles.CurrentProfilesID));
                return Json(new { Success = false, File = pfiles });
            }


            string path = "~/Uploads/Document";
            string[] messages = new string[2];
            if (SessionContent.Container.Login.UserID == null || SessionContent.Container.Login.UserID.ToString() == string.Empty)
            {
                messages[0] = "e";
                messages[1] = "uploadUserDocuments";
                return Json(messages, JsonRequestBehavior.AllowGet);
            }
            string documentIDs = "";
            User_DocumentsViewModel etuser_Document = new User_DocumentsViewModel();
            if (files != null && files[0] != null)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    //for witing 1ms for void the same name of file
                    System.Threading.Thread.Sleep(1);

                    HttpPostedFileBase file = files[i];

                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                        string time = DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
                        fname = date + "-" + time + "-" + file.FileName;

                    }

                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath(path), fname);
                    etuser_Document.Document_Path = Path.Combine(path, fname);
                    etuser_Document.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
                    messages = new User_DocumentsDomainModel().SaveUser_Documents(etuser_Document, true, fname);
                    if (messages[0] == "e")
                    {
                        break;
                    }
                    file.SaveAs(fname);
                    documentIDs += messages[1] + ",";
                    messages[1] = documentIDs;
                }
            }


            var pfile = RenderPartialViewToString("~/Views/ProfileManagement/Profiles/ProfileDetails/_ListFilePartial.cshtml", new User_DocumentsDomainModel().GetListUser_Documents(SessionContent.Container.Profiles.CurrentProfilesID));

            return Json(new { Success = true, File = pfile });
        }

        public JsonResult DeleteDocument(string attr, string id)
        {
            var messages = new ProfileManagementDomainModel().DeleteDocument(attr, id);
            if (messages[0] == "s" && attr == "User_Document_ID")
            {
                if (System.IO.File.Exists(Server.MapPath(messages[1])))
                {
                    System.IO.File.Delete(Server.MapPath(messages[1]));
                }
            }

            var file = RenderPartialViewToString("~/Views/ProfileManagement/Profiles/ProfileDetails/_ListFilePartial.cshtml", new User_DocumentsDomainModel().GetListUser_Documents(SessionContent.Container.Profiles.CurrentProfilesID));

            return Json(new { Success = true, File = file });

        }

        #endregion  

        #region  Apllication Status 
        public ActionResult LoadApplicationStatus()
        {
            string message = "";
            bool isSuccess = true;
            var model = new ProfileManagementDomainModel().GetApplicationStatusViewModel(SessionContent.Container.Profiles.CurrentProfilesID);
            model.ApplicationStatusHistories = new ProfileManagementDomainModel().GetApplicationStatusListViewModel(out message, out isSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
            LoadApplicationStatusHistoryList();
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ApplicationStatusPartiat.cshtml", model);
        }

        public ActionResult LoadApplicationStatusHistoryList()
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;

            SessionContent.Container.Profiles.ListSchoolHistories = null;
            var model = new ProfileManagementDomainModel().GetApplicationStatusListViewModel(out message, out isSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
            if (isSuccess)
                return PartialView("~/Views/ProfileManagement/Profiles/_ApplicationStatusHistoryPartial.cshtml", model);
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        public ActionResult SaveApplicationStatus(ApplicationStatusViewModel etApplicationStatusViewModel)
        {
            etApplicationStatusViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            SessionContent.Container.Profiles.ApplicationStatus = etApplicationStatusViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            isSuccess = new ProfileManagementDomainModel().SaveApplicationStatus(etApplicationStatusViewModel);
            if (isSuccess[0] == "s")
            {
                var HasSuccess = true;
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new ProfileManagementDomainModel().GetApplicationStatusViewModel(SessionContent.Container.Profiles.CurrentProfilesID);
                SessionContent.Container.Profiles.ApplicationStatus = model;
                model.ApplicationStatusHistories = new ProfileManagementDomainModel().GetApplicationStatusListViewModel(out message, out HasSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
                SessionContent.Container.Profiles.ApplicationStatus.ApplicationStatusHistories = model.ApplicationStatusHistories;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ApplicationStatusPartiat.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Registration;
                model.SchoolHistories = SessionContent.Container.Profiles.Registration.SchoolHistories;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ApplicationStatusPartiat.cshtml", model);
            }
        }

        #endregion


        #region Registration
        public ActionResult DeleteHistory(SchoolHistoryViewModel lstHistory, int i)
        {
            // SessionContent.Container.Profiles.ListHistorys = lstHistory;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            bool HasSuccess = true;
           // isSuccess = new ProfileManagementDomainModel().DeleteHistory(SessionContent.Container.Profiles.ListSchoolHistories[i]);

            if (isSuccess[0] == "s")
            {
                SessionContent.Container.Profiles.ListSchoolHistories.RemoveAt(i);
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new ProfileManagementDomainModel().GetRegistrationViewModel(SessionContent.Container.Profiles.CurrentProfilesID);
                model.SchoolHistories = new ProfileManagementDomainModel().GetSchoolHistoryListViewModel(out message, out HasSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;


                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Registration;
                model.SchoolHistories = SessionContent.Container.Profiles.Registration.SchoolHistories;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);
            }
        }
        public ActionResult LoadRegistration()
         {
            var model = new ProfileManagementDomainModel().GetRegistrationViewModel(SessionContent.Container.Profiles.CurrentProfilesID);

            LoadSchoolHistoryList();
            model.SchoolHistories = SessionContent.Container.Profiles.ListSchoolHistories;

            model.SchoolHistory = new SchoolHistoryViewModel();
            SessionContent.Container.Profiles.Registration = model;
            return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);
        }

        public ActionResult SaveRegistration(RegistrationViewModel etRegistrationViewModel)
        {
            etRegistrationViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
            SessionContent.Container.Profiles.Registration = etRegistrationViewModel;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
            isSuccess = new ProfileManagementDomainModel().SaveRegistration(etRegistrationViewModel);
            if (isSuccess[0] == "s")
            {
                var HasSuccess = true;
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new ProfileManagementDomainModel().GetRegistrationViewModel(SessionContent.Container.Profiles.CurrentProfilesID);
                SessionContent.Container.Profiles.Registration = model;
                model.SchoolHistories = new ProfileManagementDomainModel().GetSchoolHistoryListViewModel(out message, out HasSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
                SessionContent.Container.Profiles.Registration.SchoolHistories = model.SchoolHistories;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Registration;
                model.SchoolHistories = SessionContent.Container.Profiles.Registration.SchoolHistories;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);
            }
        }
        public ActionResult SaveHistoryRegistration(SchoolHistoryViewModel etHistoryViewModel)
        {
            var etRegistrationViewModel = SessionContent.Container.Profiles.Registration;
            etRegistrationViewModel.SchoolHistory = etHistoryViewModel;
            etRegistrationViewModel.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            String[] isSuccess = new String[2];
           // isSuccess = new ProfileManagementDomainModel().SaveHistoryRegistration(etRegistrationViewModel);
            if (isSuccess[0] == "s")
            {
                SessionContent.Container.Profiles.ListSchoolHistories = null;
                bool HasSuccess = true;
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = new ProfileManagementDomainModel().GetRegistrationViewModel(SessionContent.Container.Profiles.CurrentProfilesID);
                model.SchoolHistories = new ProfileManagementDomainModel().GetSchoolHistoryListViewModel(out message, out HasSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
                model.SchoolHistory = new SchoolHistoryViewModel();
                SessionContent.Container.Profiles.Registration.SchoolHistories = model.SchoolHistories;
                SessionContent.Container.Profiles.Registration = model;
                SessionContent.Container.Profiles.IsLoadProfilesContent = true;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                var model = SessionContent.Container.Profiles.Registration;
                model.SchoolHistories = SessionContent.Container.Profiles.Registration.SchoolHistories;
                model.Alert = alertModel;
                return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_RegistrationPartiat.cshtml", model);

            }

        }

        public ActionResult LoadSchoolHistoryList()
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;

            SessionContent.Container.Profiles.ListSchoolHistories = null;
            var model = new ProfileManagementDomainModel().GetSchoolHistoryListViewModel(out message, out isSuccess, SessionContent.Container.Profiles.CurrentProfilesID);
            if (isSuccess)
                return PartialView("~/Views/ProfileManagement/Profiles/_ListSchoolHistoryPartial.cshtml", model);
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        #endregion

        //#region Assignment

        //public ActionResult LoadAssignment()
        //{
        //    var model = new ProfileManagementDomainModel().GetAssignmentViewModel(SessionContent.Container.Profiles.ProfilesSearch.UserType);
        //   // model.JoinCources = new ProfileManagementDomainModel().GetAssignmentJoinCourcesViewModel(SessionContent.Container.Profiles.PersonalInformations.User_ID);
        //    return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_AssignmentPartiat.cshtml", model);
        //}
        //public ActionResult DeleteJoinCourse(int id)
        //{
        //    string message = string.Empty;
        //    bool isSuccess = false;
        //    AlertType alertType = AlertType.Success;

        //    isSuccess = new ProfileManagementDomainModel().DeleteJoinCourse(id);
        //    if (isSuccess)
        //    {
        //        var model = null;// new ProfileManagementDomainModel().GetAssignmentJoinCourcesViewModel(SessionContent.Container.Profiles.PersonalInformations.User_ID);
        //        return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ListAssignmentJoinCourcesPartial.cshtml", model);
        //    }
        //    else
        //    {
        //        var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
        //        return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
        //    }
        //}


        //public ActionResult SaveAssignment(AssignmentViewModel etAssignmentViewModel)
        //{
        //    etAssignmentViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;
        //    string message = string.Empty;
        //    AlertType alertType = AlertType.Success;
        //    String[] isSuccess = new String[2];

        //    isSuccess = new ProfileManagementDomainModel().SaveAssignments(etAssignmentViewModel);

        //    if (isSuccess[0] == "s")
        //    {
        //        var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
        //        var modelJoinCources = new ProfileManagementDomainModel().GetAssignmentJoinCourcesViewModel(SessionContent.Container.Profiles.PersonalInformations.User_ID);
        //        AssignmentViewModel model = new AssignmentViewModel();
        //        model.JoinCources = modelJoinCources;
        //        model.Alert = alertModel;
        //        SessionContent.Container.Profiles.Assignments = model;
        //        SessionContent.Container.Profiles.IsLoadProfilesContent = true;
        //        return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_AssignmentPartiat.cshtml", model);
        //    }
        //    else
        //    {
        //        var alertModel = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = isSuccess[1] };
        //        var modelJoinCources = new ProfileManagementDomainModel().GetAssignmentJoinCourcesViewModel(SessionContent.Container.Profiles.PersonalInformations.User_ID);
        //        AssignmentViewModel model = new AssignmentViewModel();
        //        model.JoinCources = modelJoinCources;
        //        model.Alert = alertModel;
        //        return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_AssignmentPartiat.cshtml", model);
        //    }

        //}

        //public ActionResult LoadAssignmentJoinCources(AssignmentViewModel mod)
        //{
        //    SessionContent.Container.Profiles.AssignmentJoinCources = null;
        //    var model = new ProfileManagementDomainModel().GetAssignmentJoinCourcesViewModel(SessionContent.Container.Profiles.PersonalInformations.User_ID);
        //    return PartialView("~/Views/ProfileManagement/Profiles/ProfileDetails/_ListAssignmentJoinCourcesPartial.cshtml", model);
        //}



        //#endregion

        #endregion
        public ActionResult ProfilesDelete(String UserID)
        {
            AlertViewModel modelAlert = new AlertViewModel();
            string idProfiles = SyndicateIT.UtilityComponent.Encryption.Decrypt(UserID).ToString();
            bool isSuccess = false;
            string msg = string.Empty;
            Guid id = new Guid(idProfiles);
            var etProfilesViewModel = new PersonalInformationsViewModel() { User_ID = id };
            string[] message = new ProfileManagementDomainModel().DeleteUsers(etProfilesViewModel);
            if (message[0] == "s")
            {
                isSuccess = true;
                msg = "Deleted Successfully";
                modelAlert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Success, Message = "Save Successful" };
            }
            else
            {
                isSuccess = false;
                msg = message[1];
                modelAlert = new AlertViewModel() { HasAlert = false, AlertType = AlertType.Danger, Message = msg };

            }
            var alert = RenderPartialViewToString("~/Views/Shared/_AlertPartial.cshtml", modelAlert);

            return Json(new { Success = isSuccess, msg = msg, Alert = alert });

        }

        [HttpPost]
        public ActionResult AddUpdateProfiles(PersonalInformationsViewModel model)
        {
            string[] message = new string[2];
            message = new ProfileManagementDomainModel().SaveUsers(model);
            if (message[0] == "s")
            {
                var modelContent = new ProfileManagementDomainModel().GetProfilesContent();
                modelContent.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Success, Message = "Save Successful" };
                return PartialView("~/Views/SetupManagement/Profiles/_ProfilesContentPartial.cshtml", modelContent);
            }
            else
            {
                model = new ProfileManagementDomainModel().GetPersonalInformationsViewModel(model);
                model.Alert = new AlertViewModel() { HasAlert = true, AlertType = AlertType.Danger, Message = message[1] };
                return PartialView("~/Views/SetupManagement/Profiles/_AddProfilesPartial.cshtml", model);

            }

        }


        #endregion

        #region Methode    

        private bool IsExitUser(System.Guid id, out string userId)
        {
            using (var db = new SyndicatDataEntities())
            {
                var etUsers = db.AspNetUsers.Where(p => p.UserId == id).FirstOrDefault();
                if (etUsers != null)
                {
                    userId = etUsers.Id;
                    return true;
                }
                else
                    userId = string.Empty;
                return false;
            }
        }

        private bool IsExitEmail(string email, System.Guid user_ID)
        {
            using (var db = new SyndicatDataEntities())
            {
                var etUsers = db.AspNetUsers.Where(p => p.Email == email && p.UserId != user_ID).FirstOrDefault();
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

        public async Task<bool> GetLockoutEnabled(string id, bool type)
        {
            var result = await UserManager.SetLockoutEnabledAsync(id, type);
            if (result.Succeeded)
                return true;
            else
                return false;
        }


        #endregion
    }
}
