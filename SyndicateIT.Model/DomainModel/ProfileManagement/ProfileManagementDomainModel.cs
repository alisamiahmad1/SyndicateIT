using AutoMapper;

using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.ProfileManagement;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.DataLayer.DataContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.DomainModel.User_Documents;

namespace SyndicateIT.Model.DomainModel.ProfileManagement
{
    [Serializable]
    public class ProfileManagementDomainModel : DomainModelBase
    {
       
        public List<User> GetProfilesParentList(string ParentId)
        {

            List<User> lstusers = new List<User>();
            User etusers = new User();
            List< User_Relations> Parents = new List<User_Relations>();
           
            
           
            using (var db = new SyndicatDataEntities())
            {
                //1 parents
                Parents = db.User_Relations.Where(p => p.User_Relation_ID.ToString() == ParentId.ToString() && p.Relation_Type_ID ==1).ToList();
                foreach(var EtParents in Parents) {
                 etusers = db.Users.Where(p => p.User_ID.ToString() == EtParents.User_ID.ToString()).FirstOrDefault ();
                 lstusers.Add(etusers);
                }
            }


            return lstusers;
        }
        
        #region Public Methods

        #region Profiles 

        public ProfilesViewModel GetProfiles(UserType userType, int sourceId)
        {
            return new ProfilesViewModel()
            {
                PartialViewName = "~/Views/ProfileManagement/Profiles/_ProfilesContentPartial.cshtml",
                PartialViewNameModel = ProfilesContent(userType, sourceId),
                Title = GetTitle(userType, sourceId),
            };
        }

        public ProfilesContentViewModel ProfilesContent(UserType userType, int sourceId)
        {
            ProfilesContentViewModel model = new ProfilesContentViewModel();
            SessionContent.Container.Profiles.UserType = userType;
            model.Navigation = GetProfilesNavigationList(userType, sourceId);
            model.Title = GetTitle(userType, sourceId);
            model.ClassTitle = "    ";
            model.UserType = userType.ToString();
            model.ProfilesSearch = new ProfilesSearchViewModel() { UserTypeID = (int)userType };
            if (SessionContent.Container.Profiles.ProfilesSearch != null && SessionContent.Container.Profiles.IsLoadProfilesContent == true)
            {
                model.ProfilesSearch.SearchFirstName = SessionContent.Container.Profiles.ProfilesSearch.SearchFirstName;
                model.ProfilesSearch.SearchLastName = SessionContent.Container.Profiles.ProfilesSearch.SearchLastName;
                model.ProfilesSearch.UserType = SessionContent.Container.Profiles.ProfilesSearch.UserType;
            }

            return model;
        }

        public List<GridProfilesViewModel> GetProfilesList(bool isRefresh, UserType userType, string firstName, string lastName,string searchCycle, string searchClass, string searchDivision)
        {
            List<User> lstUsers = null;

            List<GridProfilesViewModel> lst = new List<GridProfilesViewModel>();
            if (!isRefresh)
            {
                lst = SessionContent.Container.Profiles.ListProfiles;
            }
            else
            {
                using (var db = new SyndicatDataEntities())
                {
                    bool hasEnabled = false;
                    var role = db.AspNetRoles.Where(p => p.Name == userType.ToString()).FirstOrDefault();
                    var roleList = db.AspNetRoles.Where(p => p.Name == UserType.Members.ToString() || p.Name == UserType.MemberBoard.ToString() ||  p.Name == UserType.Administrator.ToString() || p.Name == UserType.SuperAdministrator.ToString()).Select(p => p.Id).ToList();

                    if (role != null)
                    {


                        if (userType == UserType.Employees)
                        {
                            lstUsers = db.Users.Where(p => !roleList.Contains(p.Roles_ID.ToString())).ToList();
                        }
                        //else if (userType == UserType.Members)
                        //{
                        //    //var lstUserRegistration = db.User_Registration.ToList();
                        //    //lstUsers = db.Users.Where(p => p.Roles_ID.ToString() == role.Id).ToList();
                        //    //if (lstUserRegistration!= null)
                        //    //{

                        //    //    var lstUserRegistrationIds = lstUserRegistration.Select(p => p.User_ID).ToList();
                        //    //    lstUsers = db.Users.Where(p => lstUserRegistrationIds.Contains(p.User_ID)).ToList();
                        //    //  }   
                        //    lstUsers = db.Users.Where(p => !roleList.Contains(p.Roles_ID.ToString())).ToList();

                        //}
                        else
                        {
                            lstUsers = db.Users.Where(p => p.Roles_ID.ToString() == role.Id).ToList();
                        }


                        if (firstName != null && firstName != "")
                            lstUsers = lstUsers.Where(p => p.FirstName.Contains(firstName)).ToList();


                        if (lastName != null && lastName != "")
                            lstUsers = lstUsers.Where(p => p.LastName.Contains(lastName)).ToList();


                        for (int i = 0; i < lstUsers.Count(); i++)
                        {
                            var user = lstUsers[i].User_ID;
                            var userLogin = db.AspNetUsers.Where(p => p.UserId == user).FirstOrDefault();
                            if (userLogin != null && userLogin.LockoutEnabled == false)
                            {
                                hasEnabled = true;
                            }
                            else
                            {
                                hasEnabled = false;
                            }
                            GridProfilesViewModel etGridProfilesViewModel = new GridProfilesViewModel()
                            {
                                ProfileID = lstUsers[i].User_ID,
                                FirstName = lstUsers[i].FirstName,
                                LastName = lstUsers[i].LastName,
                                DateBirth = Convert.ToDateTime(lstUsers[i].Date_Of_Birth),
                                Enabled = Convert.ToBoolean(lstUsers[i].IS_ACTIVE)
                            };
                            lst.Add(etGridProfilesViewModel);
                        }
                    }
                }
            }


            SessionContent.Container.Profiles.ListProfiles = lst;
            return lst;
        }


        #endregion

        #region Profile List

        public ProfilesContentViewModel GetProfilesGridViewModel(string userType, string searchFirstName, string searchLastName, string searchCycle, string searchClass, string searchDivision, out string message, out bool isSuccess, out AlertType alertType)
        {
            message = string.Empty;
            isSuccess = true;
            alertType = AlertType.Success;
            ProfilesContentViewModel model = new ProfilesContentViewModel();
            model.UserType = userType;
            model.ProfilesSearch.SearchFirstName = searchFirstName;
            model.ProfilesSearch.SearchLastName = searchLastName;

            model.ProfilesSearch.SearchCycle = searchCycle;
            model.ProfilesSearch.SearchClass = searchClass;
            model.ProfilesSearch.SearchDivision = searchDivision;

            model.Title = "Profiles";
            model.ClassTitle = "fa fa-lg fa-fw fa-pencil-square-o";
            model.Alert = new AlertViewModel() { HasAlert = false };
            return model;
        }


        #endregion

        #region Profile Details

        public ProfilesContentDetailsViewModel GetProfilesContentDetailsViewModel(UserType type, int sourceId)
        {
            ProfilesContentDetailsViewModel model = new ProfilesContentDetailsViewModel();
            model.Navigation = GetProfilesDeatilsNavigationList(type, sourceId);
            model.Title = GetTitle(type, sourceId);
            model.UserType = type;
            model.SourceId = sourceId;
            model.SubTitle = GetSubTitle(type);
            model.TitleURL = GetTitleURL(type, sourceId);
            model.ClassTitle = "fa fa-lg fa-fw fa-pencil-square-o";
            model.Alert = new AlertViewModel() { HasAlert = false };

            if (SessionContent.Container.Profiles.CurrentProfilesID != null)
            {
                using (var db = new SyndicatDataEntities())
                {
                    var lstApplication = db.TBL_ApplicationStatus.Where(p => p.Users_Id == SessionContent.Container.Profiles.CurrentProfilesID).ToList();
                    var pFinancialCommitte = lstApplication.Where(p => p.Committe_Id == (int)FileType.FinancialCommitte).OrderByDescending(p=>p.Entry_Date).FirstOrDefault();
                    var pEnrolmentCommitte = lstApplication.Where(p => p.Committe_Id == (int)FileType.EnrolmentCommitte).OrderByDescending(p => p.Entry_Date).FirstOrDefault();
                    var pDataEntryOperator = lstApplication.Where(p => p.Committe_Id == (int)FileType.DataEntryOperator).OrderByDescending(p => p.Entry_Date).FirstOrDefault();

                    if (pDataEntryOperator != null)
                    {
                        if (pDataEntryOperator.HasStatus == true)
                            model.DataEntryOperatorStatus = "Approved";
                        else
                            model.DataEntryOperatorStatus = "Rejected";
                    }
                    else
                    {
                        model.DataEntryOperatorStatus = "Not Received";
                    }

                    if (pEnrolmentCommitte != null)
                    {
                        if (pEnrolmentCommitte.HasStatus == true)
                            model.EntolmentCommitteStatus = "Approved";
                        else
                            model.EntolmentCommitteStatus = "Rejected";
                    }
                    else
                    {
                        if (pDataEntryOperator != null)
                        {
                            model.EntolmentCommitteStatus = "Pending";
                        }
                        else
                        {
                            model.EntolmentCommitteStatus = "Not Received";
                        }
                            
                    }

                    if (pFinancialCommitte!= null)
                    {
                        if(pFinancialCommitte.HasStatus == true)
                            model.FinancialCommitteStatus = "Approved";
                        else
                            model.FinancialCommitteStatus = "Rejected";
                    }
                    else
                    {
                        if (pEnrolmentCommitte != null)
                        {
                            model.FinancialCommitteStatus = "Pending";
                        }
                        else
                        {
                            model.FinancialCommitteStatus = "Not Received";
                        }
                    }
                }

            }


            return model;
        }

        public List<ExperienceViewModel> GetExperienceListViewModel(out string message, out bool isSuccess, Guid User_Id)
        {
            string[] messages = new string[2];
         
           using (var db = new SyndicatDataEntities())
            {
                var listdb = db.TBL_User_Experience.Where(p => p.User_ID == User_Id).ToList();

                List<ExperienceViewModel> lstExperience = new List<ExperienceViewModel>();
                for (int i = 0; i < listdb.Count; i++)
                {
                    lstExperience.Add(new ExperienceViewModel()
                    {
                        Experience_ID = listdb[i].User_Experience_ID,
                        JobTitle = listdb[i].JobTitle,
                        User_ID = User_Id,
                        Company = listdb[i].Company,
                        Department = listdb[i].Department,
                        Description = listdb[i].Description,
                        FromDate = listdb[i].FromDate,
                        ToDate = listdb[i].ToDate,
                        ENTRY_DATE = listdb[i].ENTRY_DATE,
                        Location = listdb[i].Location,
                        ENTRY_USER_ID = listdb[i].ENTRY_USER_ID
                    });                  
                }
                message = messages[0];
                isSuccess = true;
                return lstExperience;
            };
        }

        public List<EducationViewModel> GetEducationListViewModel(out string message, out bool isSuccess, Guid User_Id)
        {
            string[] messages = new string[2];

            using (var db = new SyndicatDataEntities())
            {
                var listdb = db.TBL_User_Education.Where(p => p.User_ID == User_Id).ToList();

                List<EducationViewModel> lstEducation = new List<EducationViewModel>();
                for (int i = 0; i < listdb.Count; i++)
                {
                    lstEducation.Add(new EducationViewModel()
                    {
                        Education_ID = listdb[i].User_Education_ID,
                        Degree = listdb[i].Degree,
                        User_ID = User_Id,
                        School = listdb[i].School,
                        Grade = listdb[i].Grade,
                        FieldOfStudy = listdb[i].FieldOfStudy,
                        Description = listdb[i].Description,
                        FromDate = listdb[i].FromDate,
                        ToDate = listdb[i].ToDate,
                        ENTRY_DATE = listdb[i].ENTRY_DATE,
                        ActivitiesSocieties = listdb[i].ActivitiesSocieties,
                        ENTRY_USER_ID = listdb[i].ENTRY_USER_ID
                    });
                }
                message = messages[0];
                isSuccess = true;
                return lstEducation;
            };
        }

        public string[] DeleteEducation(EducationViewModel toDelete)
        {
            String[] message = new string[2];


            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        var etUser_Education = db.TBL_User_Education.Where(p => p.User_Education_ID == toDelete.Education_ID).FirstOrDefault();
                        if (etUser_Education != null)
                        {
                            db.TBL_User_Education.Remove(etUser_Education);
                            db.SaveChanges();
                            scope.Complete();
                            message[0] = "s";
                        }
                        else
                        {
                            message[0] = "e";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.ToString();
                return message;
            }
            return message;
        }

        public String[] SaveEducation(EducationViewModel etEucationViewModel)
        {

            String[] messages = new String[2];


            TBL_User_Education etEucations = new TBL_User_Education();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {


                        var etEucationdb = db.TBL_User_Education.Where(p => p.User_Education_ID.ToString() == etEucationViewModel.Education_ID.ToString()).FirstOrDefault();
                        etEucations = Mapper.Map<EducationViewModel, TBL_User_Education>(etEucationViewModel);

                        if (etEucationdb != null)
                        {

                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etEucationViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            string[] properties = new string[35];
                            int count = -1;

                            if (etEucationViewModel.School != null)
                            {
                                count++;
                                properties[count] = "School ";
                            }
                            if (etEucationViewModel.Degree != null)
                            {
                                count++;
                                properties[count] = "Degree";
                            }
                            if (etEucationViewModel.Grade != null)
                            {
                                count++;
                                properties[count] = "Grade";
                            }
                            if (etEucationViewModel.ActivitiesSocieties != null)
                            {
                                count++;
                                properties[count] = "ActivitiesSocieties";
                            }
                            if (etEucationViewModel.Description != null)
                            {
                                count++;
                                properties[count] = "Description";
                            }
                            if (etEucationViewModel.ToDate != null)
                            {
                                count++;
                                properties[count] = "ToDate";
                            }

                            if (etEucationViewModel.FromDate != null)
                            {
                                count++;
                                properties[count] = "FromDate";
                            }
                            if (etEucationViewModel.IsCurrentDate != null)
                            {
                                count++;
                                properties[count] = "IsCurrentDate";
                            }



                            count++;
                            properties[count] = "IS_ACTIVE";


                            count++;
                            properties[count] = "MODIFICATION_USER_ID";

                            count++;
                            properties[count] = "MODIFICATION_DATE";

                            UtilityComponent.Utilities.MergeObject(etEucationdb, etEucations, true, properties);

                            etEucationdb.MODIFICATION_DATE = DateTime.Now;
                            etEucationdb.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.SaveChanges();
                        }
                        else
                        {

                            etEucations = Mapper.Map<EducationViewModel, TBL_User_Education>(etEucationViewModel);
                            etEucations.ENTRY_DATE = DateTime.Now;


                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etEucations.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }


                            etEucations.IS_ACTIVE = true;
                            db.TBL_User_Education.Add(etEucations);

                            db.SaveChanges();
                        }
                        messages[0] = "s";
                        messages[1] = etEucations.User_ID.ToString();
                    }
                    scope.Complete();

                }
                catch (DbException ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }

        public string[] DeleteExperience(ExperienceViewModel toDelete)
        {
            String[] message = new string[2];


            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        var etUser_Experience = db.TBL_User_Experience.Where(p => p.User_Experience_ID == toDelete.Experience_ID).FirstOrDefault();
                        if (etUser_Experience != null)
                        {
                            db.TBL_User_Experience.Remove(etUser_Experience);
                            db.SaveChanges();
                            scope.Complete();
                            message[0] = "s";
                        }
                        else
                        {
                            message[0] = "e";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.ToString();
                return message;
            }
            return message;
        }
        public String[] SaveExperience(ExperienceViewModel etExperienceViewModel)
        {

            String[] messages = new String[2];


            TBL_User_Experience etExperiences = new TBL_User_Experience();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {


                        var etExperiencedb = db.TBL_User_Experience.Where(p => p.User_Experience_ID.ToString() == etExperienceViewModel.Experience_ID.ToString()).FirstOrDefault();
                        etExperiences = Mapper.Map<ExperienceViewModel, TBL_User_Experience>(etExperienceViewModel);

                        if (etExperiencedb != null)
                        {

                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etExperienceViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            string[] properties = new string[35];
                            int count = -1;
                           
                            if (etExperienceViewModel.JobTitle != null)
                            {
                                count++;
                                properties[count] = "JobTitle ";
                            }
                            if (etExperienceViewModel.Company != null)
                            {
                                count++;
                                properties[count] = "Company";
                            }
                            if (etExperienceViewModel.Location != null)
                            {
                                count++;
                                properties[count] = "Location";
                            }
                            if (etExperienceViewModel.Department != null)
                            {
                                count++;
                                properties[count] = "Department";
                            }
                            if (etExperienceViewModel.Description != null)
                            {
                                count++;
                                properties[count] = "Description";
                            }
                            if (etExperienceViewModel.ToDate != null)
                            {
                                count++;
                                properties[count] = "ToDate";
                            }

                            if (etExperienceViewModel.FromDate != null)
                            {
                                count++;
                                properties[count] = "FromDate";
                            }
                            if (etExperienceViewModel.IsCurrentDate != null)
                            {
                                count++;
                                properties[count] = "IsCurrentDate";
                            }

                           
                           
                            count++;
                            properties[count] = "IS_ACTIVE";


                            count++;
                            properties[count] = "MODIFICATION_USER_ID";

                            count++;
                            properties[count] = "MODIFICATION_DATE";

                            UtilityComponent.Utilities.MergeObject(etExperiencedb, etExperiences, true, properties);

                            etExperiencedb.MODIFICATION_DATE = DateTime.Now;
                            etExperiencedb.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.SaveChanges();
                        }
                        else
                        {
                           
                            etExperiences = Mapper.Map<ExperienceViewModel, TBL_User_Experience>(etExperienceViewModel);
                            etExperiences.ENTRY_DATE = DateTime.Now;


                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etExperiences.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                                                     
                            etExperiences.IS_ACTIVE = true;
                            db.TBL_User_Experience.Add(etExperiences);

                            db.SaveChanges();
                        }
                        messages[0] = "s";
                        messages[1] = etExperiences.User_ID.ToString();
                    }
                    scope.Complete();

                }
                catch (DbException ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }
        public PersonalInformationsViewModel GetPersonalInformation(SyndicatDataEntities db, Guid User_ID)
        {

            var model = SessionContent.Container.Profiles.PersonalInformations;

            if (model != null)
            {
                return model;
            }

            PersonalInformationsViewModel etUserViewModel = new PersonalInformationsViewModel();
            etUserViewModel.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            try
            {


                var etUser = db.Users.Where(p => p.User_ID.ToString() == etUserViewModel.User_ID.ToString()).FirstOrDefault();
                //var etUser = Mapper.Map<Users, PersonalInformationsViewModel>(etUserdb);


                if (etUser != null)
                {

                    PersonalInformationsViewModel user = new PersonalInformationsViewModel()
                    {

                        User_ID = etUser.User_ID,
                        ENTRY_USER_ID = etUser.ENTRY_USER_ID,
                        MODIFICATION_USER_ID = etUser.MODIFICATION_USER_ID,
                        Bus_id = etUser.Bus_id,
                        Companys_ID = etUser.Companys_ID,
                        AspNetUsers_Id = etUser.AspNetUsers_Id,
                        Country_ID = etUser.Country_ID,
                        Date_Of_Birth = etUser.Date_Of_Birth,
                        Email = etUser.Email,
                        ENTRY_DATE = etUser.ENTRY_DATE,
                        Facebook_ID = etUser.Facebook_ID,
                        FirstName = etUser.FirstName,
                        FirstNameName_Ar = etUser.FirstNameName_Ar,
                        FirstNationality = etUser.FirstNationality,
                        Genders_Id = etUser.Genders_Id,
                        Google_ID = etUser.Google_ID,
                        UserName = etUser.UserName,
                        IS_ACTIVE = etUser.IS_ACTIVE,
                        IS_BLACKLISTED = etUser.IS_BLACKLISTED,
                        LastName = etUser.LastName,
                        LastName_Ar = etUser.LastName_Ar,
                        linkedin = etUser.linkedin,
                        Maritals_ID = etUser.Maritals_ID,
                        MIddleName = etUser.MIddleName,
                        MiddleName_Ar = etUser.MiddleName_Ar,
                        MODIFICATION_DATE = etUser.MODIFICATION_DATE,
                        MotherName = etUser.MotherName,
                        MotherName_Ar = etUser.MotherName_Ar,
                        UserReference_ID = etUser.UserReference_ID,
                        Photo = etUser.Photo,
                        PlaceId = etUser.PlaceId,
                        Place_Of_Birth = etUser.Place_Of_Birth,
                        ReadingProficiencyAnglais = etUser.ReadingProficiencyAnglais,
                        ReadingProficiencyArabic = etUser.ReadingProficiencyArabic,
                        ReadingProficiencyFrench = etUser.ReadingProficiencyFrench,
                        Religions_ID = etUser.Religions_ID,
                        Roles_ID = new Guid(etUser.Roles_ID.ToString().ToLower()),
                        SecondNationality = etUser.SecondNationality,
                        SerialNumber = etUser.SerialNumber,
                        SpeakingProficiencyAnglais = etUser.SpeakingProficiencyAnglais,
                        SpeakingProficiencyArabic = etUser.SpeakingProficiencyArabic,
                        SpeakingProficiencyFrench = etUser.SpeakingProficiencyFrench,
                        Subjects_ID = etUser.Subjects_ID,
                        RegisteryNumber = etUser.RegisteryNumber,
                        RegisteryPlace = etUser.RegisteryPlace,
                        ApplicationDate = Convert.ToDateTime(etUser.ApplicationDate),
                        ApplicationNumber = etUser.ApplicationNumber,
                        twitter = etUser.twitter,
                        User_LATITUDE = etUser.User_LATITUDE,
                        User_LONGITUDE = etUser.User_LONGITUDE,
                        WritingProficiencyAnglais = etUser.WritingProficiencyAnglais,
                        WritingProficiencyArabic = etUser.WritingProficiencyArabic,
                        WritingProficiencyFrench = etUser.WritingProficiencyFrench,
                        MobileAlternateNumber = etUser.MobileAlternateNumber,
                        MobileNumber = etUser.MobileNumber,
                        PhoneAlternateNumber = etUser.PhoneAlternateNumber,
                        PhoneNumber = etUser.PhoneNumber
                    };
                    SessionContent.Container.Profiles.PersonalInformations = user;
                    return user;
                }

                else
                {
                    return new PersonalInformationsViewModel();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContactInformationsViewModel GetContactInformations(SyndicatDataEntities db, Guid User_ID)
        {

            //var model = SessionContent.Container.Profiles.ContactInformations;

            //if (model != null)
            //{
            //    return model;
            //}

            User_ContactViewModel etUser_ContactViewModel = new User_ContactViewModel();
            etUser_ContactViewModel.User_ID = User_ID;
            try
            {

                var etUser_Contactdb = db.Tbl_User_Contact.Where(p => p.User_ID.ToString() == etUser_ContactViewModel.User_ID.ToString() && p.Flag == 0).FirstOrDefault();
                var etUser_Contact = Mapper.Map<Tbl_User_Contact, User_ContactViewModel>(etUser_Contactdb);

                if (etUser_Contact != null)
                {
                    var etUser_Contactdb1 = db.Tbl_User_Contact.Where(p => p.User_ID.ToString() == etUser_ContactViewModel.User_ID.ToString() && p.Flag == 1).FirstOrDefault();
                    var etUser_Contact1 = Mapper.Map<Tbl_User_Contact, User_ContactViewModel>(etUser_Contactdb1);
                    ContactInformationsViewModel contact = new ContactInformationsViewModel()
                    {

                        User_ID = etUser_Contact.User_ID,
                        ENTRY_USER_ID = etUser_Contact.ENTRY_USER_ID,
                        MODIFICATION_USER_ID = etUser_Contact.MODIFICATION_USER_ID,
                        User_Contact_ID = etUser_Contact.User_Contact_ID,
                        CITY_ID = etUser_Contact.CITY_ID,
                        COUNTRY_ID = etUser_Contact.COUNTRY_ID,
                        REGION_ID = etUser_Contact.REGION_ID,
                        STATE_ID = etUser_Contact.STATE_ID,
                        Building = etUser_Contact.Building,
                        Street = etUser_Contact.Street,
                        POBox = etUser_Contact.POBox,
                        Fax = etUser_Contact.Fax,
                        Floor = etUser_Contact.Floor,
                        TownShip = etUser_Contact.TownShip,
                        SecondENTRY_USER_ID = etUser_Contact1.ENTRY_USER_ID,
                        SecondMODIFICATION_USER_ID = etUser_Contact1.MODIFICATION_USER_ID,
                        SecondUser_Contact_ID = etUser_Contact1.User_Contact_ID,
                        SecondCITY_ID = etUser_Contact1.CITY_ID,
                        SecondCOUNTRY_ID = etUser_Contact1.COUNTRY_ID,
                        SecondREGION_ID = etUser_Contact1.REGION_ID,
                        SecondSTATE_ID = etUser_Contact1.STATE_ID,
                        SecondBuilding = etUser_Contact1.Building,
                        SecondStreet = etUser_Contact1.Street,
                        SecondPOBox = etUser_Contact1.POBox,
                        SecondFax = etUser_Contact1.Fax,
                        SecondFloor = etUser_Contact1.Floor,
                        SecondTownShip = etUser_Contact1.TownShip,
                    };
                    return contact;
                }

                else
                {
                    return new ContactInformationsViewModel();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public InsuranceViewModel GetInsuranceViewModel(UserType type)
        {
            InsuranceViewModel model = new InsuranceViewModel();
            model.Alert = new AlertViewModel() { HasAlert = false };
            return model;
        }

        public EmploymentViewModel GetEmploymentViewModel(SyndicatDataEntities db, Guid User_ID)
        {


            var model = SessionContent.Container.Profiles.Employments;

            if (model != null)
            {
                return model;
            }

            PersonalInformationsViewModel etUserViewModel = new PersonalInformationsViewModel();
            etUserViewModel.User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID;//User_ID
            try
            {


                var etUser = db.Tbl_User_Employment.Where(p => p.User_ID.ToString() == etUserViewModel.User_ID.ToString()).FirstOrDefault();
                //var etUser = Mapper.Map<Users, PersonalInformationsViewModel>(etUserdb);

                if (etUser != null)
                {

                    EmploymentViewModel user = new EmploymentViewModel()
                    {

                        User_ID = etUser.User_ID,
                        ENTRY_USER_ID = etUser.ENTRY_USER_ID,
                        MODIFICATION_USER_ID = etUser.MODIFICATION_USER_ID,
                        ENTRY_DATE = etUser.ENTRY_DATE,
                        IS_ACTIVE = etUser.IS_ACTIVE,
                        MODIFICATION_DATE = etUser.MODIFICATION_DATE,
                        COUNTRY_ID = etUser.COUNTRY_ID,
                        Contract_Date = etUser.Contract_Date,
                        DEPARTMENT_ID = etUser.DEPARTMENT_ID,
                        Division = etUser.Division,
                        EmployeeNumber = etUser.EmployeeNumber,
                        User_Employment_ID = etUser.User_Employment_ID,
                        Grade = etUser.Grade,
                        HPD = etUser.HPD,
                        IS_INTERNATIONAL = etUser.IS_INTERNATIONAL,
                        JOB_ID = etUser.JOB_ID,
                        LicenseIssuingDate = etUser.LicenseIssuingDate,
                        LicenseNumber = etUser.LicenseNumber,
                        SHIFT_ID = etUser.SHIFT_ID,
                        STATUS_ID = etUser.STATUS_ID,
                        WD = etUser.WD

                    };
                    return user;
                }

                else
                {
                    return new EmploymentViewModel();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InsuranceViewModel GetInsurance(SyndicatDataEntities db, Guid User_ID)
        {

            var model = SessionContent.Container.Profiles.Insurances;

            if (model != null)
            {
                return model;
            }

            InsuranceViewModel etInsuranceViewModel = new InsuranceViewModel();
            etInsuranceViewModel.Users_Id = User_ID;
            try
            {


                var etInsurancedb = db.TBL_Insurances.Where(p => p.Users_Id.ToString() == etInsuranceViewModel.Users_Id.ToString()).FirstOrDefault();
                var etInsurance = Mapper.Map<TBL_Insurances, InsuranceViewModel>(etInsurancedb);

                if (etInsurance != null)
                {

                    InsuranceViewModel Insurance = new InsuranceViewModel()
                    {

                        Users_Id = etInsurance.Users_Id,
                        Entry_Users_Id = etInsurance.Entry_Users_Id,
                        Modification_Users_Id = etInsurance.Modification_Users_Id,
                        Insurance_Id = etInsurance.Insurance_Id,
                        TypeGuarantee_Id = etInsurance.TypeGuarantee_Id,
                        TypeInsurance_Id = etInsurance.TypeInsurance_Id,
                        DegreeInsurance_Id = etInsurance.DegreeInsurance_Id,
                        DegreeGuarantee_Id = etInsurance.DegreeGuarantee_Id,
                        HasGuarantee = etInsurance.HasGuarantee,
                        DegreeGuaranteeNotes = etInsurance.DegreeGuaranteeNotes,
                        TypeInsuranceNotes = etInsurance.TypeInsuranceNotes,
                        HasInsurance = etInsurance.HasInsurance,
                        Entry_Date = etInsurance.Entry_Date,
                        Modification_Date = etInsurance.Modification_Date,
                        IsActive = etInsurance.IsActive
                    };
                    return Insurance;
                }

                else
                {
                    return new InsuranceViewModel();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DependentViewModel GetDependentViewModel(UserType type)
        {
            DependentViewModel model = new DependentViewModel();
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.Dependents.Add(new DependentViewModel() { });
            model.Dependents.Add(new DependentViewModel() { });
            return model;
        }

        public List<DependentViewModel> GetDependentListViewModel(out string message, out bool isSuccess, out AlertType alertType)
        {
            message = string.Empty;
            isSuccess = true;
            alertType = AlertType.Success;
            List<DependentViewModel> model = new List<DependentViewModel>();
            return model;
        }

        public DependentViewModel GetDependentViewModel()
        {
            var model = SessionContent.Container.Profiles.Dependents;

            if (model != null)
            {
                return model;
            }
            else return new DependentViewModel() { User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID };
        }

        public ExperienceViewModel GetExperienceViewModel()
        {
            var model = SessionContent.Container.Profiles.Experiences;

            if (model != null)
            {
                return model;
            }
            else return new ExperienceViewModel() { User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID };
        }

        public EducationViewModel GetEducationViewModel()
        {
            var model = SessionContent.Container.Profiles.Educations;

            if (model != null)
            {
                return model;
            }
            else return new EducationViewModel() { User_ID = SessionContent.Container.Profiles.PersonalInformations.User_ID };
        }

        public String[] SaveDependent(DependentViewModel etRelationsViewModel)
        {

            String[] messages = new String[2];


            User_Relations etRelations = new User_Relations();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {


                        var etRelationsdb = db.User_Relations.Where(p => p.User_Relation_ID.ToString() == etRelationsViewModel.User_Relation_ID.ToString()).FirstOrDefault();
                        etRelations = Mapper.Map<DependentViewModel, User_Relations>(etRelationsViewModel);

                        if (etRelationsdb != null)
                        {

                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etRelationsViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            string[] properties = new string[35];
                            int count = -1;
                            if (etRelations.IsMemberOfSchool == false)
                            {
                                if (etRelationsViewModel.FirstName != null)
                                {
                                    count++;
                                    properties[count] = "FirstName ";
                                }
                                if (etRelationsViewModel.LastName != null)
                                {
                                    count++;
                                    properties[count] = "LastName";
                                }
                                if (etRelationsViewModel.Email != null)
                                {
                                    count++;
                                    properties[count] = "Email";
                                }
                                if (etRelationsViewModel.Mobile != null)
                                {
                                    count++;
                                    properties[count] = "Mobile";
                                }
                                if (etRelationsViewModel.IsMemberOfSchool != null)
                                {
                                    count++;
                                    properties[count] = "IsMemberOfSchool";
                                }
                                if (etRelationsViewModel.HasEmergencyContact != null)
                                {
                                    count++;
                                    properties[count] = "HasEmergencyContact";
                                }

                                if (etRelationsViewModel.DateOfBirth != null)
                                {
                                    count++;
                                    properties[count] = "DateOfBirth";
                                }
                                if (etRelationsViewModel.Title != null)
                                {
                                    count++;
                                    properties[count] = "Title";
                                }

                            }
                            else
                            {
                                if (etRelationsViewModel.Relative_ID != null)
                                {
                                    count++;
                                    properties[count] = "Relative_ID";
                                }
                                if (etRelationsViewModel.Relation_Type_ID != null)
                                {
                                    count++;
                                    properties[count] = "Relation_Type_ID";
                                }
                            }
                            count++;
                            properties[count] = "IS_ACTIVE";


                            count++;
                            properties[count] = "MODIFICATION_USER_ID";

                            count++;
                            properties[count] = "MODIFICATION_DATE";

                            UtilityComponent.Utilities.MergeObject(etRelationsdb, etRelations, true, properties);

                            etRelationsdb.MODIFICATION_DATE = DateTime.Now;
                            etRelationsdb.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.SaveChanges();
                        }


                        else
                        {
                            if (etRelationsViewModel.IsMemberOfSchool != false)
                            {
                                var user = db.Users.Where(p => p.User_ID.ToString() == etRelationsViewModel.User_ID.ToString()).FirstOrDefault();
                                etRelationsViewModel.FirstName = user.FirstName;
                                etRelationsViewModel.LastName = user.LastName;
                                etRelationsViewModel.Email = user.Email;
                                etRelationsViewModel.DateOfBirth = user.Date_Of_Birth;
                                etRelationsViewModel.Mobile = user.MobileNumber;
                            }
                            etRelations = Mapper.Map<DependentViewModel, User_Relations>(etRelationsViewModel);
                            etRelations.ENTRY_DATE = DateTime.Now;


                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etRelations.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }


                            //  etRelations.User_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            etRelations.IS_ACTIVE = true;
                            db.User_Relations.Add(etRelations);

                            db.SaveChanges();
                        }
                        messages[0] = "s";
                        messages[1] = etRelations.User_ID.ToString();
                    }
                    scope.Complete();

                }
                catch (DbException ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }

        public string[] DeleteDependent(DependentViewModel toDelete)
        {
            String[] message = new string[2];


            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        var etUser_Relations = db.User_Relations.Where(p => p.User_Relation_ID == toDelete.User_Relation_ID).FirstOrDefault();
                        if (etUser_Relations != null)
                        {
                            db.User_Relations.Remove(etUser_Relations);
                            db.SaveChanges();
                            scope.Complete();
                            message[0] = "s";
                        }
                        else
                        {
                            message[0] = "e";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.ToString();
                return message;
            }
            return message;
        }

        public String[] SaveAssignments(AssignmentViewModel etAssignmentViewModel)
        {
            String[] messages = new String[2];
            Tbl_User_Course etAssignment = new Tbl_User_Course();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        var pUserCourse = db.Tbl_User_Course.Where(p => p.Cycle_ID == etAssignmentViewModel.Cycle_ID && p.Class_ID == etAssignmentViewModel.Class_ID && p.Division_ID == etAssignmentViewModel.Division_ID && p.Course_ID == etAssignmentViewModel.Course_ID).FirstOrDefault();

                        if (pUserCourse != null)
                        {
                            messages[0] = "e";
                            messages[1] = "this Course Already exist";
                        }
                        else
                        {
                            etAssignment = Mapper.Map<AssignmentViewModel, Tbl_User_Course>(etAssignmentViewModel);
                            etAssignment.ENTRY_DATE = DateTime.Now;
                            if (SessionContent.Container.Login.UserID != null)
                                etAssignment.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            etAssignment.IS_ACTIVE = true;
                            db.Tbl_User_Course.Add(etAssignment);
                            db.SaveChanges();
                            messages[0] = "s";
                            messages[1] = etAssignment.User_ID.ToString();
                            scope.Complete();
                        }
                    }

                }
                catch (DbException ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }
        public List<DependentViewModel> GetDependentListViewModel(out string message, out bool isSuccess, Guid User_Id)
        {
            string[] messages = new string[2];
            List<DependentGridViewModel> lst = new List<DependentGridViewModel>();
            DependentContentViewModel et = new DependentContentViewModel()
            {
                User_ID = User_Id.ToString()
            };

            using (var db = new SyndicatDataEntities())
            {
                try
                {
                    var listdb = db.USP_GET_User_Relations(et.User_Relation_ID, et.User_ID, et.Relative_ID, et.Relation_Type_ID, et.FirstName, et.Email, et.Mobile, et.HasEmergencyContact,
                        et.DateOfBirth, et.IsMemberOfSchool, et.LastName, et.IS_ACTIVE, -1, -1, -1, et.ENTRY_DATE, et.ENTRY_USER_ID, et.MODIFICATION_USER_ID, et.MODIFICATION_DATE).ToList();
                    List<DependentGridViewModel> Users = Mapper.Map<List<USP_GET_User_Relations_Result>, List<DependentGridViewModel>>(listdb);
                    lst = Users;
                }
                catch (Exception e) { }

                List<DependentViewModel> model = new List<DependentViewModel>();
                for (int i = 0; i < lst.Count; i++)

                {
                    var relation = db.USP_GET_Relation_Types(lst[i].Relation_Type_ID, "", "", -1, true, -1, -1, -1, et.ENTRY_DATE, "", "", et.MODIFICATION_DATE).ToList();
                    if (lst[i].Relation_Type_ID == null)
                        relation[0].Relation_Type_Title = "none";
                    model.Add(new DependentViewModel()
                    {
                        User_Relation_ID = lst[i].User_Relation_ID,
                        FirstName = lst[i].FirstName,
                        LastName = lst[i].LastName,
                        DateOfBirth = lst[i].DateOfBirth,
                        Email = lst[i].Email,
                        Mobile = lst[i].Mobile,
                        HasEmergencyContact = lst[i].HasEmergencyContact,
                        IsMemberOfSchool = lst[i].IsMemberOfSchool,
                        Relation_Type_ID = lst[i].Relation_Type_ID,
                        Relation_NAME = relation[0].Relation_Type_Title

                        // Title = lst[i].Title
                    });
                }

                message = messages[0];
                isSuccess = true;
                return model;
            };
        }
        public SystemPrivilegesViewModel GetSystemPrivilegeViewModel(Guid profileId, UserType type)
        {
            SystemPrivilegesViewModel model = new SystemPrivilegesViewModel();
            using (var db = new SyndicatDataEntities())
            {
                var etprofile = db.Users.Where(p => p.User_ID == profileId).FirstOrDefault();

                if (etprofile != null)
                {
                    model = new SystemPrivilegesViewModel()
                    {
                        Email = etprofile.Email,
                        FirstName = etprofile.FirstName,
                        LastName = etprofile.LastName,
                        Username = etprofile.UserName,
                        MobileNumber = etprofile.MobileNumber,
                        RoleName = SessionContent.AppStore.RoleCodeList.FirstOrDefault(p => p.Value == etprofile.Roles_ID.ToString()).Text,
                        Type = type.ToString(),
                        UserId = profileId,
                        HasEmail = false,
                    };
                }
                var etUserNet = db.AspNetUsers.Where(p => p.UserId == profileId).FirstOrDefault();
                if (etUserNet != null)
                {
                    model.Enable = etUserNet.LockoutEnabled == false ? true : false;
                    model.HasLoginUser = etUserNet.LockoutEnabled == false ? true : false;
                }
            }
            model.Alert = new AlertViewModel() { HasAlert = false };
            return model;
        }
  
        public string[] SaveRegistration(RegistrationViewModel etRegistrationViewModel)
        {

            String[] messages = new String[2];


            User_Registration etRegistration = new User_Registration();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        var etUsersRegistrationdb = db.User_Registration.Where(p => p.Registration_ID.ToString() == etRegistrationViewModel.Registration_ID.ToString()).FirstOrDefault();
                        etRegistration = Mapper.Map<RegistrationViewModel, User_Registration>(etRegistrationViewModel);
                        if (etUsersRegistrationdb != null)
                        {
                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etRegistrationViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            string[] properties = new string[10];
                            int count = -1;

                            if (etRegistrationViewModel.HasRegistration != null)
                            {
                                count++;
                                properties[count] = "HasRegistration";
                            }
                            if (etRegistrationViewModel.TypeSpecialty != null)
                            {
                                count++;
                                properties[count] = "TypeSpecialty";
                            }
                            
                            if (etRegistrationViewModel.From != null)
                            {
                                count++;
                                properties[count] = "From";
                            }
                            if (etRegistrationViewModel.To != null)
                            {
                                count++;
                                properties[count] = "To";
                            }
                            if (etRegistrationViewModel.CardNumber != null)
                            {
                                count++;
                                properties[count] = "CardNumber";
                            }

                            if (etRegistrationViewModel.HasCardDelivered != null)
                            {
                                count++;
                                properties[count] = "HasCardDelivered";
                            }

                            if (etRegistrationViewModel.HasCardIssued != null)
                            {
                                count++;
                                properties[count] = "HasCardIssued";
                            }
                            count++;
                            properties[count] = "MODIFICATION_USER_ID";

                            count++;
                            properties[count] = "MODIFICATION_DATE";

                            UtilityComponent.Utilities.MergeObject(etUsersRegistrationdb, etRegistration, true, properties);
                            etUsersRegistrationdb.MODIFICATION_DATE = DateTime.Now;
                            etUsersRegistrationdb.IS_ACTIVE = true;
                            etUsersRegistrationdb.HasRegistration = etRegistration.HasRegistration;
                            db.SaveChanges();
                            //var className = db.Classes_By_Language.Where(p => p.Language_ID == (int)Languages.English && p.Class_ID.ToString().Contains(etRegistrationViewModel.Class_ID.ToString())).FirstOrDefault();
                            //var stageName = db.Cycle_By_Language.Where(p => p.Language_ID == (int)Languages.English && p.Cycle_ID == etRegistration.Cycle_ID).FirstOrDefault().Cycle_Description;
                            //SchoolHistoryViewModel pSchoolHistory = new SchoolHistoryViewModel()
                            //{
                            //    ClassName = etRegistrationViewModel.Class_ID,
                            //    CycleName = etRegistrationViewModel.Cycle_ID,
                            //    FromHistory = etRegistrationViewModel.From,
                            //    ToHistory = etRegistrationViewModel.To,
                            //    User_ID = etRegistrationViewModel.User_ID,
                            //    Location = "",
                            //    SchoolName = "",
                            //    ClassNameS = className.Class_Title,
                            //    StageNameS = stageName
                            //};
                            //etRegistrationViewModel.SchoolHistories.Add(pSchoolHistory);
                            //SessionContent.Container.Profiles.ListSchoolHistories.Add(pSchoolHistory);
                        }
                        else
                        {
                            etRegistration = Mapper.Map<RegistrationViewModel, User_Registration>(etRegistrationViewModel);
                            etRegistration.ENTRY_DATE = DateTime.Now;
                            etRegistration.CurrentRegistration = true;
                            if (SessionContent.Container.Login.UserID != null)
                                etRegistration.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            etRegistration.IS_ACTIVE = true;
                            db.User_Registration.Add(etRegistration);
                            db.SaveChanges();
                            //var className = db.Classes_By_Language.Where(p => p.Language_ID == (int)Languages.English && p.Class_ID.ToString().Contains(etRegistrationViewModel.Class_ID.ToString())).FirstOrDefault();
                            //var stageName = db.Cycle_By_Language.Where(p => p.Language_ID == (int)Languages.English && p.Cycle_ID == etRegistration.Cycle_ID).FirstOrDefault().Cycle_Description;
                            //SchoolHistoryViewModel pSchoolHistory = new SchoolHistoryViewModel()
                            //{
                            //    ClassName = etRegistrationViewModel.Class_ID,
                            //    CycleName = etRegistrationViewModel.Cycle_ID,
                            //    FromHistory = etRegistrationViewModel.From,
                            //    ToHistory = etRegistrationViewModel.To,
                            //    User_ID = etRegistrationViewModel.User_ID,
                            //    Location = "",
                            //    SchoolName = "",
                            //    ClassNameS = className.Class_Title,
                            //    StageNameS = stageName
                            //};
                            //etRegistrationViewModel.SchoolHistories.Add(pSchoolHistory);
                            //SessionContent.Container.Profiles.ListSchoolHistories.Add(pSchoolHistory);
                        }
                    }
                    messages[0] = "s";
                    messages[1] = etRegistration.User_ID.ToString();
                    scope.Complete();
                }
                catch (DbException ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }

        public string[] SaveApplicationStatus(ApplicationStatusViewModel etApplicationStatusViewModel)
        {

            String[] messages = new String[2];


            TBL_ApplicationStatus etApplicationStatus = new TBL_ApplicationStatus();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        etApplicationStatus = Mapper.Map<ApplicationStatusViewModel, TBL_ApplicationStatus>(etApplicationStatusViewModel);
                        etApplicationStatus.ApplicationStatus_Id = Guid.NewGuid();
                        etApplicationStatus.Entry_Date = DateTime.Now;
                        if (SessionContent.Container.Login.UserGuidID != null)
                        {
                            etApplicationStatus.Entry_Users_Id = new Guid(SessionContent.Container.Login.UserGuidID.ToString());
                            etApplicationStatus.Role = SessionContent.Container.Login.UserRole;
                            if (etApplicationStatus.Role == "Data Entry Operator")
                                etApplicationStatus.Committe_Id = (int)FileType.DataEntryOperator;
                           else if (etApplicationStatus.Role == " Entolment Committe")
                                etApplicationStatus.Committe_Id = (int)FileType.EnrolmentCommitte;
                           else if (etApplicationStatus.Role == "Financial Committe")
                                etApplicationStatus.Committe_Id = (int)FileType.FinancialCommitte;
                        }
                            

                        etApplicationStatus.IsActive = true;
                        db.TBL_ApplicationStatus.Add(etApplicationStatus);
                        db.SaveChanges();
                    }
                    messages[0] = "s";
                    messages[1] = etApplicationStatus.Users_Id.ToString();
                    scope.Complete();
                }
                catch (DbException ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }

        public DocumentViewModel GetDocumentViewModel(Guid id)
        {
            DocumentViewModel model = new DocumentViewModel();
            model.ListUser_Documents = new User_DocumentsDomainModel().GetListUser_Documents(id);
            model.Alert = new AlertViewModel() { HasAlert = false };
            return model;
        }

        public RegistrationViewModel GetRegistrationViewModel(Guid user_ID)
        {
            //if (SessionContent.Container.Profiles.Registration != null)
            //    return SessionContent.Container.Profiles.Registration;
            RegistrationViewModel model = new RegistrationViewModel();
            using (var db = new SyndicatDataEntities())
            {
                var pRegistration = db.User_Registration.Where(p => p.User_ID == user_ID && p.CurrentRegistration == true).FirstOrDefault();
                model = Mapper.Map<User_Registration, RegistrationViewModel>(pRegistration);

            }
            if (model == null)
                model = new RegistrationViewModel();
            model.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;
            model.SchoolHistory = new SchoolHistoryViewModel() { User_ID = model.User_ID };
            model.Alert = new AlertViewModel() { HasAlert = false };


            return model;


        }

        public List<SchoolHistoryViewModel> GetSchoolHistoryListViewModel(out string message, out bool isSuccess, Guid User_Id)
        {

            if (SessionContent.Container.Profiles.ListSchoolHistories != null)
            {
                message = "s";
                isSuccess = true;

                return SessionContent.Container.Profiles.ListSchoolHistories;
            }

            string[] messages = new string[2];         

            List<USP_GET_User_School_History_Result> listdb = new List<USP_GET_User_School_History_Result>();
            using (var db = new SyndicatDataEntities())
            {
                try
                {

                    //listdb = db.USP_GET_User_School_History(-1, User_Id.ToString(), true, -1, -1, -1, Convert.ToDateTime("1/1/1900 12:00:00 AM").Date, "", "", Convert.ToDateTime("1/1/1900 12:00:00 AM").Date).ToList();

                    //// List<DependentGridViewModel> Users = Mapper.Map<List<USP_GET_User_Relations_Result>, List<DependentGridViewModel>>(listdb);
                    ////lst = Users;
                    //CycleContentViewModel stagecontent = new CycleContentViewModel()
                    //{
                    //    LANGUAGE_ID = (int)Languages.English
                    //};
                    //cycles = db.USP_GET_Cycle(
                    //    stagecontent.Cycle_ID,
                    //    stagecontent.Cycle_Title,
                    //    stagecontent.Cycle_Description,
                    //    stagecontent.LANGUAGE_ID,
                    //    stagecontent.IS_ACTIVE,
                    //    stagecontent.START_ROW,
                    //    stagecontent.END_ROW,
                    //    stagecontent.TOP,
                    //    stagecontent.ENTRY_DATE,
                    //    stagecontent.ENTRY_USER_ID,
                    //    stagecontent.MODIFICATION_USER_ID,
                    //    stagecontent.MODIFICATION_DATE
                    //    ).ToList();
                    //ClassesContentViewModel Classescontent = new ClassesContentViewModel() { language = (int)Languages.English };
                    //classes = db.USP_GET_Classes(
                    //    Classescontent.Class_ID,
                    //    Classescontent.Class_Title,
                    //    Classescontent.Class_Description,
                    //    Classescontent.Cycle_ID,
                    //    Classescontent.Company_ID,
                    //    Classescontent.language,
                    //    Classescontent.Class_Row,
                    //    Classescontent.Class_Column,
                    //    true,
                    //    -1,
                    //    -1,
                    //    -1,
                    //    Classescontent.ENTRY_DATE,
                    //    Classescontent.ENTRY_USER_ID,
                    //    Classescontent.MODIFICATION_USER_ID,
                    //    Classescontent.MODIFICATION_DATE
                    //    ).ToList();
                }
                catch (DbException e) {; }




                List<SchoolHistoryViewModel> model = new List<SchoolHistoryViewModel>();
                //for (int i = 0; i < listdb.Count; i++)

                //{
                //    var classname = ""; var stagename = "";
                //    foreach (USP_GET_Classes_Result res in classes)
                //        if (res.Class_ID == listdb[i].ClassName)
                //            classname = res.Class_Title;
                //    foreach (USP_GET_Cycle_Result st in cycles)

                //        if (st.Cycle_ID == listdb[i].StageName)
                //            stagename = st.Cycle_Description;

                //    model.Add(new SchoolHistoryViewModel()
                //    {
                //        StageNameS = stagename,
                //        ClassNameS = classname,
                //        SchoolHistory_ID = listdb[i].SchoolHistory_ID,
                //        ToHistory = listdb[i].ToHistory,
                //        FromHistory = listdb[i].FromHistory,
                //        Location = listdb[i].Location,
                //        SchoolName = listdb[i].SchoolName,
                //        Description = listdb[i].Description
                //    });
                //}

                message = messages[0];
                isSuccess = true;
                SessionContent.Container.Profiles.ListSchoolHistories = model;
                return model;
            };
        }
        public AssignmentViewModel GetAssignmentViewModel(UserType type)
        {
            AssignmentViewModel model = new AssignmentViewModel();
            model.Alert = new AlertViewModel() { HasAlert = false };
            return model;
        }

        public bool DeleteJoinCourse(int id)
        {
            bool isSuccess = true;
            using (var db = new SyndicatDataEntities())
            {
                try
                {
                    var etCourse = db.Tbl_User_Course.Where(p => p.User_Course_ID == id).FirstOrDefault();
                    db.Tbl_User_Course.Remove(etCourse);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }

            return isSuccess;
        }    

        public PersonalInformationsViewModel GetPersonalInformationsViewModel(String User_ID)
        {
            PersonalInformationsViewModel model = new PersonalInformationsViewModel();
            if (User_ID != null && User_ID.ToString() != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etUsers = db.Users.Where(p => p.User_ID.ToString() == User_ID).FirstOrDefault();
                    model = Mapper.Map<User, PersonalInformationsViewModel>(etUsers);
                };
            }
            return model;
        }

        public PersonalInformationsViewModel GetPersonalInformationsViewModel(PersonalInformationsViewModel model)
        {
            string title = string.Empty;
            if (model.User_ID != null && model.User_ID.ToString() != "")
            {
                title = "Edit Personnel";
            }
            else
            {
                title = "New Personnel";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }

        //DELETE FUNCTION
        public string DeleteUsersBusinessLogic(PersonalInformationsViewModel etPersonalInformationsViewModel)
        {
            string message = string.Empty;
            if (etPersonalInformationsViewModel.User_ID == null)
            {
                message = "There is no record";
            }
            return message;
        }
        public string[] DeleteUsers(PersonalInformationsViewModel etPersonalInformationsViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteUsersBusinessLogic(etPersonalInformationsViewModel);

            if (message[1] != string.Empty)
            {
                message[0] = "e";
                return message;
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        var etUsers = db.Users.Where(p => p.User_ID == etPersonalInformationsViewModel.User_ID).FirstOrDefault();
                        if (etUsers != null)
                        {
                            db.Users.Remove(etUsers);
                            db.SaveChanges();
                            message[0] = "s";
                            scope.Complete();
                            return message;
                        }
                        else
                        {
                            message[0] = "e";
                            scope.Dispose();
                            return message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = "e";

                return message;
            }
        }

        //ADD Function 
        public string SaveUsersBusinessLogic(PersonalInformationsViewModel model)
        {
            string message = string.Empty;
            return message;
        }
        public List<PersonalInformationsGridViewModel> GetListUsers(PersonalInformationsContentViewModel etUserContentViewModel)
        {
            string[] messages = new string[2];
            List<PersonalInformationsGridViewModel> lst = new List<PersonalInformationsGridViewModel>();
            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Users(etUserContentViewModel.User_ID,
                    etUserContentViewModel.FirstName,
                    etUserContentViewModel.MIddleName,
                    etUserContentViewModel.LastName,
                    etUserContentViewModel.MotherName,
                    etUserContentViewModel.FirstNameName_Ar, etUserContentViewModel.MiddleName_Ar,
                    etUserContentViewModel.LastName_Ar, etUserContentViewModel.MotherName_Ar,
                    etUserContentViewModel.Email, etUserContentViewModel.Date_Of_Birth, etUserContentViewModel.Genders_Id, etUserContentViewModel.Country_ID,
                    etUserContentViewModel.Religions_ID, etUserContentViewModel.Maritals_ID,
                    etUserContentViewModel.Subjects_ID, etUserContentViewModel.Companys_ID, etUserContentViewModel.Roles_ID, etUserContentViewModel.Facebook_ID,
                    etUserContentViewModel.Google_ID, etUserContentViewModel.Photo,
                    etUserContentViewModel.Place_Of_Birth, "", etUserContentViewModel.SerialNumber,
                    etUserContentViewModel.FirstNationality, etUserContentViewModel.SecondNationality,
                    etUserContentViewModel.Bus_id, etUserContentViewModel.PlaceId,
                    etUserContentViewModel.User_LONGITUDE, etUserContentViewModel.User_LATITUDE,
                    etUserContentViewModel.IS_BLACKLISTED, etUserContentViewModel.AspNetUsers_Id,
                    etUserContentViewModel.linkedin, etUserContentViewModel.twitter,
                    etUserContentViewModel.SpeakingProficiencyArabic, etUserContentViewModel.ReadingProficiencyAnglais,
                    etUserContentViewModel.WritingProficiencyArabic, etUserContentViewModel.SpeakingProficiencyAnglais,
                    etUserContentViewModel.ReadingProficiencyArabic, etUserContentViewModel.WritingProficiencyArabic,
                   etUserContentViewModel.SpeakingProficiencyFrench, etUserContentViewModel.ReadingProficiencyFrench, etUserContentViewModel.WritingProficiencyFrench,
                      etUserContentViewModel.IS_ACTIVE, -1, -1, -1, etUserContentViewModel.ENTRY_DATE, etUserContentViewModel.ENTRY_USER_ID,
                      etUserContentViewModel.MODIFICATION_USER_ID, etUserContentViewModel.MODIFICATION_DATE).ToList();
                List<PersonalInformationsGridViewModel> Users = Mapper.Map<List<USP_GET_Users_Result>, List<PersonalInformationsGridViewModel>>(listdb);

                lst = Users;
            };

            return lst;

        }
        public string[] SaveUsers(PersonalInformationsViewModel etPersonalInformationsViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveUsersBusinessLogic(etPersonalInformationsViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }

            User etUsers = new User();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        var UserExist = db.Users.Count(p => p.User_ID == etPersonalInformationsViewModel.User_ID);
                        if (UserExist != 0)
                        {

                            var etUsersdb = db.Users.Where(p => p.User_ID.ToString() == etPersonalInformationsViewModel.User_ID.ToString()).FirstOrDefault();
                            etUsers = Mapper.Map<PersonalInformationsViewModel, User>(etPersonalInformationsViewModel);

                            if (etUsers != null)
                            {

                                if (SessionContent.Container.Login.UserID != null)
                                {
                                    etPersonalInformationsViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[55];
                                int count = -1;

                                if (etPersonalInformationsViewModel.FirstName != null)
                                {
                                    count++;
                                    properties[count] = "FirstName";
                                }
                                if (etPersonalInformationsViewModel.FirstNameName_Ar != null)
                                {
                                    count++;
                                    properties[count] = "FirstNameName_Ar";
                                }
                                if (etPersonalInformationsViewModel.LastName != null)
                                {
                                    count++;
                                    properties[count] = "LastName";
                                }
                                if (etPersonalInformationsViewModel.LastName_Ar != null)
                                {
                                    count++;
                                    properties[count] = "LastName_Ar";
                                }
                                if (etPersonalInformationsViewModel.MIddleName != null)
                                {
                                    count++;
                                    properties[count] = "MIddleName";
                                }
                                if (etPersonalInformationsViewModel.MiddleName_Ar != null)
                                {
                                    count++;
                                    properties[count] = "MiddleName_Ar";
                                }
                                if (etPersonalInformationsViewModel.MotherName != null)
                                {
                                    count++;
                                    properties[count] = "MotherName";
                                }
                                if (etPersonalInformationsViewModel.MotherName_Ar != null)
                                {
                                    count++;
                                    properties[count] = "MotherName_Ar";
                                }
                                if (etPersonalInformationsViewModel.UserReference_ID != null)
                                {
                                    count++;
                                    properties[count] = "UserReference_ID";
                                }
                                if (etPersonalInformationsViewModel.Email != null)
                                {
                                    count++;
                                    properties[count] = "Email";
                                }
                                if (etPersonalInformationsViewModel.Date_Of_Birth != null)
                                {
                                    count++;
                                    properties[count] = "Date_Of_Birth";
                                }
                                if (etPersonalInformationsViewModel.Genders_Id != null)
                                {
                                    count++;
                                    properties[count] = "Gender_ID";
                                }
                                if (etPersonalInformationsViewModel.Country_ID != null)
                                {
                                    count++;
                                    properties[count] = "Country_ID";
                                }
                                if (etPersonalInformationsViewModel.Religions_ID != null)
                                {
                                    count++;
                                    properties[count] = "Religion_ID";
                                }
                                if (etPersonalInformationsViewModel.Maritals_ID != null)
                                {
                                    count++;
                                    properties[count] = "Marital_ID";
                                }
                                if (etPersonalInformationsViewModel.Subjects_ID != null)
                                {
                                    count++;
                                    properties[count] = "Subject_ID";
                                }
                                if (etPersonalInformationsViewModel.Companys_ID != null)
                                {
                                    count++;
                                    properties[count] = "Company_ID";
                                }
                                if (etPersonalInformationsViewModel.Roles_ID != null)
                                {
                                    count++;
                                    properties[count] = "Role_ID";
                                }
                                if (etPersonalInformationsViewModel.ApplicationNumber != null)
                                {
                                    count++;
                                    properties[count] = "ApplicationNumber";
                                }
                                if (etPersonalInformationsViewModel.ApplicationDate != null)
                                {
                                    count++;
                                    properties[count] = "ApplicationDate";
                                }
                                if (etPersonalInformationsViewModel.RegisteryNumber != null)
                                {
                                    count++;
                                    properties[count] = "RegisteryNumber";
                                }
                                if (etPersonalInformationsViewModel.RegisteryPlace != null)
                                {
                                    count++;
                                    properties[count] = "RegisteryPlace";
                                }

                                if (etPersonalInformationsViewModel.Facebook_ID != null)
                                {
                                    count++;
                                    properties[count] = "Facebook_ID";
                                }
                                if (etPersonalInformationsViewModel.Google_ID != null)
                                {
                                    count++;
                                    properties[count] = "Google_ID";
                                }
                                if (etPersonalInformationsViewModel.Photo != null)
                                {
                                    count++;
                                    properties[count] = "Photo";
                                }
                                if (etPersonalInformationsViewModel.Place_Of_Birth != null)
                                {
                                    count++;
                                    properties[count] = "Place_Of_Birth";
                                }
                                if (etPersonalInformationsViewModel.UserName != null)
                                {
                                    count++;
                                    properties[count] = "UserName";
                                }
                                if (etPersonalInformationsViewModel.SerialNumber != null)
                                {
                                    count++;
                                    properties[count] = "SerialNumber";
                                }
                                if (etPersonalInformationsViewModel.FirstNationality != null)
                                {
                                    count++;
                                    properties[count] = "FirstNationality";
                                }
                                if (etPersonalInformationsViewModel.SecondNationality != null)
                                {
                                    count++;
                                    properties[count] = "SecondNationality";
                                }
                                if (etPersonalInformationsViewModel.Bus_id != null)
                                {
                                    count++;
                                    properties[count] = "Bus_id";
                                }
                                if (etPersonalInformationsViewModel.PlaceId != null)
                                {
                                    count++;
                                    properties[count] = "PlaceId";
                                }
                                if (etPersonalInformationsViewModel.User_LONGITUDE != null)
                                {
                                    count++;
                                    properties[count] = "User_LONGITUDE";
                                }
                                if (etPersonalInformationsViewModel.linkedin != null)
                                {
                                    count++;
                                    properties[count] = "linkedin";
                                }
                                if (etPersonalInformationsViewModel.twitter != null)
                                {
                                    count++;
                                    properties[count] = "twitter";
                                }
                                if (etPersonalInformationsViewModel.User_LATITUDE != null)
                                {
                                    count++;
                                    properties[count] = "User_LATITUDE";
                                }
                                count++;
                                properties[count] = "IS_ACTIVE";

                                if (etPersonalInformationsViewModel.IS_BLACKLISTED != null)
                                {
                                    count++;
                                    properties[count] = "IS_BLACKLISTED";
                                }
                                if (etPersonalInformationsViewModel.Roles_ID != null)
                                {
                                    count++;
                                    properties[count] = "Roles_ID";
                                }
                                if (etPersonalInformationsViewModel.Companys_ID != null)
                                {
                                    count++;
                                    properties[count] = "Companys_ID";
                                }
                                if (etPersonalInformationsViewModel.Subjects_ID != null)
                                {
                                    count++;
                                    properties[count] = "Subjects_ID";
                                }
                                if (etPersonalInformationsViewModel.Religions_ID != null)
                                {
                                    count++;
                                    properties[count] = "Religions_ID";
                                }
                                if (etPersonalInformationsViewModel.Genders_Id != null)
                                {
                                    count++;
                                    properties[count] = "Genders_Id";
                                }
                                if (etPersonalInformationsViewModel.Maritals_ID != null)
                                {
                                    count++;
                                    properties[count] = "Maritals_ID";
                                }
                                if (etPersonalInformationsViewModel.MobileNumber != null)
                                {
                                    count++;
                                    properties[count] = "MobileNumber";
                                }
                                if (etPersonalInformationsViewModel.PhoneNumber != null)
                                {
                                    count++;
                                    properties[count] = "PhoneNumber";
                                }
                                if (etPersonalInformationsViewModel.MobileAlternateNumber != null)
                                {
                                    count++;
                                    properties[count] = "MobileAlternateNumber";
                                }
                                if (etPersonalInformationsViewModel.PhoneAlternateNumber != null)
                                {
                                    count++;
                                    properties[count] = "PhoneAlternateNumber";
                                }
                                if (etPersonalInformationsViewModel.SpeakingProficiencyArabicCount != null)
                                {
                                    count++;
                                    properties[count] = "SpeakingProficiencyArabicCount";
                                }
                                if (etPersonalInformationsViewModel.ReadingProficiencyArabicCount != null)
                                {
                                    count++;
                                    properties[count] = "ReadingProficiencyArabicCount";
                                }
                                if (etPersonalInformationsViewModel.WritingProficiencyArabicCount != null)
                                {
                                    count++;
                                    properties[count] = "WritingProficiencyArabicCount";
                                }
                                if (etPersonalInformationsViewModel.SpeakingProficiencyAnglaisCount != null)
                                {
                                    count++;
                                    properties[count] = "SpeakingProficiencyAnglaisCount";
                                }
                                if (etPersonalInformationsViewModel.ReadingProficiencyAnglaisCount != null)
                                {
                                    count++;
                                    properties[count] = "ReadingProficiencyAnglaisCount";
                                }
                                if (etPersonalInformationsViewModel.WritingProficiencyAnglaisCount != null)
                                {
                                    count++;
                                    properties[count] = "WritingProficiencyAnglaisCount";
                                }
                                if (etPersonalInformationsViewModel.SpeakingProficiencyFrenchCount != null)
                                {
                                    count++;
                                    properties[count] = "SpeakingProficiencyFrenchCount";
                                }
                                if (etPersonalInformationsViewModel.ReadingProficiencyFrenchCount != null)
                                {
                                    count++;
                                    properties[count] = "ReadingProficiencyFrenchCount";
                                }
                                if (etPersonalInformationsViewModel.WritingProficiencyFrenchCount != null)
                                {
                                    count++;
                                    properties[count] = "WritingProficiencyFrenchCount";
                                }
                                count++;
                                properties[count] = "MODIFICATION_USER_ID";

                                count++;
                                properties[count] = "MODIFICATION_DATE";

                                UtilityComponent.Utilities.MergeObject(etUsersdb, etUsers, true, properties);
                                etUsersdb.WritingProficiencyAnglais = etUsers.WritingProficiencyAnglais;
                                etUsersdb.ReadingProficiencyAnglais = etUsers.ReadingProficiencyAnglais;
                                etUsersdb.SpeakingProficiencyAnglais = etUsers.SpeakingProficiencyAnglais;
                                etUsersdb.WritingProficiencyArabic = etUsers.WritingProficiencyArabic;
                                etUsersdb.ReadingProficiencyArabic = etUsers.ReadingProficiencyArabic;
                                etUsersdb.SpeakingProficiencyArabic = etUsers.SpeakingProficiencyArabic;
                                etUsersdb.WritingProficiencyFrench = etUsers.WritingProficiencyFrench;
                                etUsersdb.SpeakingProficiencyFrench = etUsers.SpeakingProficiencyFrench;
                                etUsersdb.ReadingProficiencyFrench = etUsers.ReadingProficiencyFrench;
                                //modification user id
                                etUsersdb.MODIFICATION_DATE = DateTime.Now;
                                etUsersdb.IS_ACTIVE = true;
                                db.SaveChanges();


                            }
                        }

                        else
                        {
                            etUsers = Mapper.Map<PersonalInformationsViewModel, User>(etPersonalInformationsViewModel);
                            etUsers.ENTRY_DATE = DateTime.Now;

                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etUsers.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            etUsers.Roles_ID = etPersonalInformationsViewModel.Roles_ID;
                            etUsers.User_ID = Guid.NewGuid();
                            etUsers.IS_ACTIVE = true;

                            db.Users.Add(etUsers);

                            db.SaveChanges();
                            SessionContent.Container.Profiles.PersonalInformations.User_ID = etUsers.User_ID;
                            SessionContent.Container.Profiles.PersonalInformations.IS_ACTIVE = true;
                            SessionContent.Container.Profiles.CurrentProfilesID = etUsers.User_ID;
                        }
                        messages[0] = "s";
                        messages[1] = etUsers.User_ID.ToString();
                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }
        public string[] SaveEmployment(EmploymentViewModel etEmploymentViewModel)
        {
            string[] messages = new string[2];


            Tbl_User_Employment etEmployment = new Tbl_User_Employment();
            TransactionOptions TransOptions = new TransactionOptions();
            TransOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransOptions))
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        var UserExist = db.Tbl_User_Employment.Count(p => p.User_ID == etEmploymentViewModel.User_ID);
                        if (UserExist != 0)
                        {

                            var etEmploymentdb = db.Tbl_User_Employment.Where(p => p.User_Employment_ID.ToString() == etEmploymentViewModel.User_Employment_ID.ToString()).FirstOrDefault();
                            etEmployment = Mapper.Map<EmploymentViewModel, Tbl_User_Employment>(etEmploymentViewModel);

                            if (etEmployment != null)
                            {

                                if (SessionContent.Container.Login.UserID != null)
                                {
                                    etEmploymentViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[35];
                                int count = -1;

                                if (etEmploymentViewModel.Contract_Date != null)
                                {
                                    count++;
                                    properties[count] = "Contract_Date ";
                                }
                                if (etEmploymentViewModel.COUNTRY_ID != null)
                                {
                                    count++;
                                    properties[count] = "COUNTRY_ID";
                                }
                                if (etEmploymentViewModel.DEPARTMENT_ID != null)
                                {
                                    count++;
                                    properties[count] = "DEPARTMENT_ID";
                                }
                                if (etEmploymentViewModel.Division != null)
                                {
                                    count++;
                                    properties[count] = "Division";
                                }
                                if (etEmploymentViewModel.EmployeeNumber != null)
                                {
                                    count++;
                                    properties[count] = "EmployeeNumber";
                                }
                                if (etEmploymentViewModel.Grade != null)
                                {
                                    count++;
                                    properties[count] = "Grade";
                                }
                                if (etEmploymentViewModel.HPD != null)
                                {
                                    count++;
                                    properties[count] = "HPD";
                                }
                                if (etEmploymentViewModel.IS_INTERNATIONAL != null)
                                {
                                    count++;
                                    properties[count] = "IS_INTERNATIONAL";
                                }
                                if (etEmploymentViewModel.JOB_ID != null)
                                {
                                    count++;
                                    properties[count] = "JOB_ID";
                                }
                                if (etEmploymentViewModel.LicenseIssuingDate != null)
                                {
                                    count++;
                                    properties[count] = "LicenseIssuingDate";
                                }
                                if (etEmploymentViewModel.LicenseNumber != null)
                                {
                                    count++;
                                    properties[count] = "LicenseNumber";
                                }
                                if (etEmploymentViewModel.SHIFT_ID != null)
                                {
                                    count++;
                                    properties[count] = "SHIFT_ID";
                                }
                                if (etEmploymentViewModel.STATUS_ID != null)
                                {
                                    count++;
                                    properties[count] = "STATUS_ID";
                                }
                                if (etEmploymentViewModel.WD != null)
                                {
                                    count++;
                                    properties[count] = "WD";
                                }


                                count++;
                                properties[count] = "IS_ACTIVE";


                                count++;
                                properties[count] = "MODIFICATION_USER_ID";

                                count++;
                                properties[count] = "MODIFICATION_DATE";

                                UtilityComponent.Utilities.MergeObject(etEmploymentdb, etEmployment, true, properties);
                                etEmploymentdb.Contract_Date = etEmployment.Contract_Date;
                                etEmploymentdb.MODIFICATION_DATE = DateTime.Now;
                                etEmploymentdb.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                db.SaveChanges();
                            }
                        }

                        else
                        {
                            etEmployment = Mapper.Map<EmploymentViewModel, Tbl_User_Employment>(etEmploymentViewModel);
                            etEmployment.ENTRY_DATE = DateTime.Now;

                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etEmployment.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }


                            //  etEmployment.User_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            etEmployment.IS_ACTIVE = true;
                            db.Tbl_User_Employment.Add(etEmployment);

                            db.SaveChanges();
                            SessionContent.Container.Profiles.Employments.User_Employment_ID = etEmployment.User_Employment_ID;
                        }
                        messages[0] = "s";
                        messages[1] = etEmployment.User_ID.ToString();
                    }
                    scope.Complete();

                }
                catch (Exception ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    return messages;
                }
            }
            return messages;
        }

        public ProfilesContentDetailsViewModel GetProfilesContent()
        {
            ProfilesContentDetailsViewModel model = new ProfilesContentDetailsViewModel();
            model.Navigation = GetNavigationList("list", "Profiles");
            model.Title = "Gender";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }
        ///End
        public List<NavigationViewModel> GetNavigationList(string type, string title)
        {
            var model = new List<NavigationViewModel>();

            if (type == "list")
            {
                model.Add(new NavigationViewModel() { NavigationName = "ProfileManagement", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
            else if (type == "update")
            {
                model.Add(new NavigationViewModel() { NavigationName = "ProfileManagement", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = "ProfileManagement", NavigationUrl = Utilities.GetUrl("ProfileManagement", "ProfileManagement", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        #region Contact

        public bool SaveProfileContact(SyndicatDataEntities db, User_ContactViewModel etUser_ContactViewModel, out int pUserContactId)
        {
            try
            {
                string[] messages = new string[2];
                string[] properties = new string[20];
                int count = -1;
                pUserContactId = 0;
                messages[1] = SaveUser_ContactBuisnessLogic(etUser_ContactViewModel);
                if (messages[1] != string.Empty)
                    return false;


                if (etUser_ContactViewModel.User_Contact_ID != 0 && etUser_ContactViewModel.User_Contact_ID.ToString() != "")
                {

                    var etUser_Contactdb = db.Tbl_User_Contact.Where(p => p.User_Contact_ID.ToString() == etUser_ContactViewModel.User_Contact_ID.ToString()).FirstOrDefault();
                    var etUser_Contact = Mapper.Map<User_ContactViewModel, Tbl_User_Contact>(etUser_ContactViewModel);

                    if (etUser_Contact != null)
                    {


                        if (etUser_ContactViewModel.COUNTRY_ID != null)
                        {
                            count++;
                            properties[count] = "COUNTRY_ID";
                        }
                        if (etUser_ContactViewModel.REGION_ID != null)
                        {
                            count++;
                            properties[count] = "REGION_ID";
                        }
                        if (etUser_ContactViewModel.STATE_ID != null)
                        {
                            count++;
                            properties[count] = "STATE_ID";
                        }
                        if (etUser_ContactViewModel.CITY_ID != null)
                        {
                            count++;
                            properties[count] = "CITY_ID";
                        }
                        if (etUser_ContactViewModel.Floor != null)
                        {
                            count++;
                            properties[count] = "Floor";
                        }
                        if (etUser_ContactViewModel.Street != null)
                        {
                            count++;
                            properties[count] = "Street";
                        }
                        if (etUser_ContactViewModel.Building != null)
                        {
                            count++;
                            properties[count] = "Building";
                        }
                        if (etUser_ContactViewModel.TownShip != null)
                        {
                            count++;
                            properties[count] = "TownShip";
                        }
                        if (etUser_ContactViewModel.POBox != null)
                        {
                            count++;
                            properties[count] = "POBox";
                        }
                        if (etUser_ContactViewModel.Fax != null)
                        {
                            count++;
                            properties[count] = "Fax";
                        }

                        count++;
                        properties[count] = "IS_ACTIVE";
                        count++;
                        properties[count] = "MODIFICATION_USER_ID";
                        count++;
                        properties[count] = "MODIFICATION_DATE";
                        UtilityComponent.Utilities.MergeObject(etUser_Contactdb, etUser_Contact, true, properties);
                        etUser_Contactdb.MODIFICATION_DATE = DateTimeOffset.Now;
                        db.SaveChanges();
                        pUserContactId = etUser_Contactdb.User_Contact_ID;
                    }
                }
                else
                {
                    var etUser_Contact = Mapper.Map<User_ContactViewModel, Tbl_User_Contact>(etUser_ContactViewModel);
                    db.Tbl_User_Contact.Add(etUser_Contact);
                    db.SaveChanges();
                    pUserContactId = etUser_Contact.User_Contact_ID;
                }
                return true;
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }


        #endregion

        public string[] DeleteDocument(string attr, string id)
        {

            string[] messages = new string[2];
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        messages[1] = id;
                        if (attr == "User_Document_ID")
                        {
                            var model = db.Tbl_User_Documents.Where(p => p.User_Document_ID.ToString() == id).FirstOrDefault();
                            if (model == null)
                            {
                                throw new Exception("not found");
                            }
                            messages[1] = model.Document_Path;
                            db.Tbl_User_Documents.Remove(model);
                        }
                        else if (attr == "User_Relation_ID")
                        {
                            var model = db.TBL_User_Relations.Where(p => p.User_Relation_ID.ToString() == id).FirstOrDefault();
                            if (model == null)
                            {
                                throw new Exception("not found");
                            }
                            db.TBL_User_Relations.Remove(model);
                        }
                        else
                        {
                            throw new Exception("undifined attr");
                        }
                        messages[0] = "s";
                        db.SaveChanges();

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                messages[0] = "e";
                messages[1] = ex.Message;
            }
            return messages;
        }

        public ApplicationStatusViewModel GetApplicationStatusViewModel(Guid user_ID)
        {

            ApplicationStatusViewModel model = new ApplicationStatusViewModel();
            //using (var db = new SyndicatDataEntities())
            //{
            //    var user = db.Users.Where(p => p.User_ID == user_ID).FirstOrDefault();
            //    var pstatus = db.TBL_ApplicationStatus.Where(p => p.Users_Id == user_ID ).FirstOrDefault();
            //    model = Mapper.Map<TBL_ApplicationStatus, ApplicationStatusViewModel>(pstatus);
            //   // model.ApplicationName = user.ApplicationNumber;

            //}
            //if (model == null)
            //    model = new ApplicationStatusViewModel();
            model.User_ID = SessionContent.Container.Profiles.CurrentProfilesID;            
            model.Alert = new AlertViewModel() { HasAlert = false };
            return model;
        }

        public List<ApplicationStatusViewModel> GetApplicationStatusListViewModel(out string message, out bool isSuccess, Guid User_Id)
        {           

            string[] messages = new string[2];
            isSuccess = true;
            List<ApplicationStatusViewModel> listApplicationStatus = new List<ApplicationStatusViewModel>();            
            using (var db = new SyndicatDataEntities())
            {             

                var listdb = db.TBL_ApplicationStatus.Where(p => p.Users_Id == User_Id).ToList();

                for (int i = 0; i < listdb.Count; i++)
                {
                    var userId = listdb[i].Entry_Users_Id;

                    var user = db.AspNetUsers.Where(p => p.Id.ToString() == userId.ToString()).FirstOrDefault();

                    listApplicationStatus.Add(new ApplicationStatusViewModel()
                    {
                        ApplicationStatus_Id = listdb[i].ApplicationStatus_Id,
                        Date = Convert.ToDateTime(listdb[i].Entry_Date),
                        HasStatus = Convert.ToBoolean(listdb[i].HasStatus),
                        ApplicationStatus_Description = listdb[i].ApplicationStatus_Description,
                        Role = listdb[i].Role,
                        User = user.FirstName + " " + user.LastName,


                    });
                }

                message = messages[0];
                isSuccess = true;
             //   SessionContent.Container.Profiles.ListSchoolHistories = model;
               
            };

            return listApplicationStatus;
        }


        #endregion

        #endregion

        #region Private Methods

        private string SaveUser_ContactBuisnessLogic(User_ContactViewModel model)
        {
            string message = string.Empty;
            return message;
        }
        private List<NavigationViewModel> GetProfilesNavigationList(UserType type, int sourceId)
        {
            var model = new List<NavigationViewModel>();
            model.Add(new NavigationViewModel() { NavigationName = "Profiles", NavigationUrl = "" });
            model.Add(new NavigationViewModel() { NavigationName = GetTitle(type, sourceId) });

            return model;
        }
        private List<NavigationViewModel> GetProfilesDeatilsNavigationList(UserType type, int sourceId)
        {
            var model = new List<NavigationViewModel>();
            string pNavigationUrl = string.Empty;

            if (UserType.Administrator == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Administrator", "ProfileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Administrators", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileParent == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Parents", "dashboard", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Administrator", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }

            }
            else if (UserType.Members == type)
            {
                if ((int)ProfileSourceType.MyProfile == sourceId)
                {
                    pNavigationUrl = "";
                }
               else if ((int)FileType.EnrolmentCommitte == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("EnrolmentCommitte", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)FileType.FinancialCommitte == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("FinancialCommitte", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }

            }
            else if (UserType.Employees == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Employees", "ProfileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Employees", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Employe", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }

            }
            else if (UserType.Agent == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Agent", "ProfileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Agent", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("Agent", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }

            }
            else if (UserType.MemberBoard == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("MemberBoard", "ProfileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("MemberBoard", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("MemberBoard", "FileManagement", SessionContent.Current.Corporate.IsSecure);
                }
                else if ((int)ProfileSourceType.FileParent == sourceId)
                {
                    pNavigationUrl = Utilities.GetUrl("MemberBoard", "Dashboard", SessionContent.Current.Corporate.IsSecure);
                }

            }
            
            if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
            {
                model.Add(new NavigationViewModel() { NavigationName = "Profiles", NavigationUrl = "" });
            }
            else if ((int)ProfileSourceType.FileStudents == sourceId)
            {
                model.Add(new NavigationViewModel() { NavigationName = "Files", NavigationUrl = "" });
            }
            else if ((int)ProfileSourceType.FileStudent == sourceId)
            {
                model.Add(new NavigationViewModel() { NavigationName = "File", NavigationUrl = "" });
            }


            model.Add(new NavigationViewModel() { NavigationName = GetTitle(type, sourceId), NavigationUrl = pNavigationUrl });
            model.Add(new NavigationViewModel() { NavigationName = GetProfilesDetailsTitle(type) });

            return model;
        }
        public string GetTitle(UserType type, int sourceId)

        {


            if (UserType.Administrator == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "Aministrator Management";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "Aministrators File";
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    return "Aministrator File";
                }
                else return " ";
            }


            else if (UserType.Employees == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "Employees Management";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "Employees File";
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    return "Employe File";
                }
                else return " ";
            }
            else if (UserType.Agent == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "Agent Management";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "Agent File";
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    return "Agent File";
                }
                else return " ";
            }

            else if (UserType.MemberBoard == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "Member Board Management";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "Teachers File";
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    return "Teacher File";
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    return "Dashboard";
                }
                else return " ";

            }            

            else if (UserType.Members == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "Members Management";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "Members File";
                }
                else if ((int)ProfileSourceType.FileStudent == sourceId)
                {
                    return "Members File";
                }
                else if ((int)ProfileSourceType.MyProfile == sourceId)
                {
                    return "My Profile";
                }
                else return " ";

            }

            else
                return "";

        }
        private string GetSubTitle(UserType type)
        {
            if (UserType.Administrator == type)
                return "Aministrators ";
            else if (UserType.Employees == type)
                return "Employees";
            else if (UserType.MemberBoard == type)
                return "MemberBoard";
            else if (UserType.Members == type)
                return "Members";
            else if (UserType.Agent == type)
                return "Agents";
            else
                return "";

        }
        private string GetTitleURL(UserType type, int sourceId)
        {
            if (UserType.Administrator == type)
                return "/ProfileManagement/Admins";
            else if (UserType.Employees == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "/ProfileManagement/Employees";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "/FileManagement/Employees";
                }
                else return " ";
            }


            else if (UserType.MemberBoard == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "/ProfileManagement/MemberBoard";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "/FileManagement/MemberBoard";
                }
                else return " ";
            }
            else if (UserType.Agent == type)
            {
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "/ProfileManagement/Agent";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "/FileManagement/Agent";
                }
                else return " ";
            }

            else if (UserType.Members == type)
                if ((int)ProfileSourceType.ProfileInforamtion == sourceId)
                {
                    return "/ProfileManagement/Members";
                }
                else if ((int)ProfileSourceType.FileStudents == sourceId)
                {
                    return "/FileManagement/Members";
                }
                else return " ";


            else
                return "";

        }
        private string GetProfilesDetailsTitle(UserType type)
        {
            if (UserType.Administrator == type)
                return "Aministrator Details";
            else if (UserType.Employees == type)
                return "Employees Details";
            else if (UserType.Members == type)
                return "Members Details";
            else if (UserType.MemberBoard == type)
                return "Member Board Details";
            else if (UserType.Agent == type)
                return "Agent Details";

            else
                return "";

        }        

        #endregion      
        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<User, PersonalInformationsViewModel>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<Tbl_User_Employment, EmploymentViewModel>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<Tbl_User_Course, AssignmentViewModel>()
                .IgnoreAllNonExisting();
            Mapper.CreateMap<User_Registration, RegistrationViewModel>()
               .IgnoreAllNonExisting();
            Mapper.CreateMap<User_Relations, DependentViewModel>()
                  .IgnoreAllNonExisting();
           
            Mapper.CreateMap<USP_GET_Users_Result, PersonalInformationsGridViewModel>()
              .IgnoreAllNonExisting();
            Mapper.CreateMap<USP_GET_User_Relations_Result, DependentGridViewModel>()
             .IgnoreAllNonExisting();          
            Mapper.CreateMap<USP_GET_User_Relations_Result, SchoolHistoryViewModel>()
           .IgnoreAllNonExisting();

            Mapper.CreateMap<TBL_ApplicationStatus, ApplicationStatusViewModel>()
                 .IgnoreAllNonExisting();

            Mapper.CreateMap<TBL_User_Experience, ExperienceViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<TBL_User_Education, EducationViewModel>()
                .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<EducationViewModel, TBL_User_Education>()
               .IgnoreAllNonExisting();

            Mapper.CreateMap<ExperienceViewModel, TBL_User_Experience>()
              .IgnoreAllNonExisting();

            Mapper.CreateMap<ApplicationStatusViewModel, TBL_ApplicationStatus>()
                .ForMember(d => d.Users_Id, opt => opt.MapFrom(src => src.User_ID))
                .ForMember(d => d.HasStatus, opt => opt.MapFrom(src => src.HasAccepted))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<RegistrationViewModel, User_Registration>()
               .IgnoreAllNonExisting();
           
            Mapper.CreateMap<DependentViewModel, User_Relations>()
                .IgnoreAllNonExisting();
            Mapper.CreateMap<AssignmentViewModel, Tbl_User_Course>()
               .IgnoreAllNonExisting();
            Mapper.CreateMap<PersonalInformationsViewModel, User>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<EmploymentViewModel, Tbl_User_Employment>()
                 .IgnoreAllNonExisting();
            Mapper.CreateMap<PersonalInformationsGridViewModel, USP_GET_Users_Result>()
               .IgnoreAllNonExisting();
            Mapper.CreateMap<DependentGridViewModel, USP_GET_User_Relations_Result>()           

           .IgnoreAllNonExisting();
            Mapper.CreateMap<SchoolHistoryViewModel, USP_GET_User_Relations_Result>()
           .IgnoreAllNonExisting();

            #endregion

            base.InitializeMapper();
        }
    }
}
