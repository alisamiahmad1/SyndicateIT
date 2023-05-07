using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.DomainModel.ProfileManagement;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SyndicateIT.Model.ProcessorModel.ProfileManagement
{
    public class ProfileManagementProcessor : ProcessorBase
    {
        public string[] SaveContactInformation(ContactInformationsViewModel etContactInformationsViewModel)
        {
            User_ContactViewModel etUser_ContactViewModel = new User_ContactViewModel();
            String[] message = new string[2];
            bool isSuccess = true;
            int pUserContactId = 0;
            int pUserSecondContactId = 0;
            using (TransactionScope scope = new TransactionScope())
            {
               
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        etUser_ContactViewModel = new User_ContactViewModel()
                        {
                            User_Contact_ID = etContactInformationsViewModel.User_Contact_ID,
                            User_ID = etContactInformationsViewModel.User_ID,
                            COUNTRY_ID = etContactInformationsViewModel.COUNTRY_ID,
                            CITY_ID = etContactInformationsViewModel.CITY_ID,
                            STATE_ID = etContactInformationsViewModel.STATE_ID,
                            REGION_ID = etContactInformationsViewModel.REGION_ID,
                            TownShip = etContactInformationsViewModel.TownShip,
                            Street = etContactInformationsViewModel.Street,
                            Building = etContactInformationsViewModel.Building,
                            Floor = etContactInformationsViewModel.Floor,
                            POBox = etContactInformationsViewModel.POBox,
                            Fax = etContactInformationsViewModel.Fax,
                            IS_ACTIVE = true,
                            Flag=0
                        };

                        if(etContactInformationsViewModel.User_Contact_ID == 0)
                        {
                            etUser_ContactViewModel.IS_ACTIVE = true;
                            etUser_ContactViewModel.ENTRY_DATE = DateTimeOffset.Now;
                            etUser_ContactViewModel.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            etUser_ContactViewModel.Flag = 0;
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                etUser_ContactViewModel.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                        }
                        else
                        {
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                etUser_ContactViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                        }
                        
                        isSuccess =  new ProfileManagementDomainModel().SaveProfileContact(db,etUser_ContactViewModel, out pUserContactId);
                      
                        if (isSuccess == true)
                        {
                            etUser_ContactViewModel = new User_ContactViewModel()
                            {
                                User_Contact_ID=etContactInformationsViewModel.SecondUser_Contact_ID,
                                User_ID = etContactInformationsViewModel.User_ID,
                                COUNTRY_ID = etContactInformationsViewModel.SecondCOUNTRY_ID,
                                CITY_ID = etContactInformationsViewModel.SecondCITY_ID,
                                STATE_ID = etContactInformationsViewModel.SecondSTATE_ID,
                                REGION_ID = etContactInformationsViewModel.SecondREGION_ID,
                                TownShip = etContactInformationsViewModel.SecondTownShip,
                                Street = etContactInformationsViewModel.SecondStreet,
                                Building = etContactInformationsViewModel.SecondBuilding,
                                Floor = etContactInformationsViewModel.SecondFloor,
                                POBox = etContactInformationsViewModel.SecondPOBox,
                                Fax = etContactInformationsViewModel.SecondFax,
                                IS_ACTIVE = true,
                                ENTRY_DATE = DateTimeOffset.UtcNow,
                                ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString()),
                                Flag = 1,
                                MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString())
                        };
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                etUser_ContactViewModel.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());                          
                            isSuccess = new ProfileManagementDomainModel().SaveProfileContact(db, etUser_ContactViewModel,out pUserSecondContactId);
                            SessionContent.Container.Profiles.ContactInformations.User_Contact_ID = pUserContactId;
                            SessionContent.Container.Profiles.ContactInformations.SecondUser_Contact_ID = pUserSecondContactId;
                        }

                        if(isSuccess == true){ message[0] = "s"; message[1] = "Saved Successfully"; }
                        else { message[0] = "e"; message[1] = "Error During Saved"; }
                        scope.Complete();
                    }
                }

                catch (DbException ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            return message;

        }
        public ContactInformationsViewModel GetContactInformation(Guid User_ID)
        {
            ContactInformationsViewModel etContactViewModel = new ContactInformationsViewModel();
            String[] message = new string[2];
            
            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {                 

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etContactViewModel = new ProfileManagementDomainModel().GetContactInformations(db, User_ID);
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etContactViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etContactViewModel;
        }

        public PersonalInformationsViewModel GetPersonalInformation(Guid user_ID)
        {
            PersonalInformationsViewModel etContactViewModel = new PersonalInformationsViewModel();
            String[] message = new string[2];

            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etContactViewModel = new ProfileManagementDomainModel().GetPersonalInformation(db, user_ID);
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etContactViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etContactViewModel;
        }

        public InsuranceViewModel GetInsurance(Guid User_ID)
        {
            InsuranceViewModel etContactViewModel = new InsuranceViewModel();
            String[] message = new string[2];

            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etContactViewModel = new ProfileManagementDomainModel().GetInsurance(db, User_ID);
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etContactViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etContactViewModel;
        }
       public EmploymentViewModel GetEmploymentViewModel(Guid user_ID)
        {
            EmploymentViewModel etViewModel = new EmploymentViewModel();
            String[] message = new string[2];

            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etViewModel = new ProfileManagementDomainModel().GetEmploymentViewModel(db, user_ID);
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etViewModel;
        }

        //GetDependentViewModel
        public DependentViewModel GetDependentViewModel()
        {
            DependentViewModel etViewModel = new DependentViewModel();
            String[] message = new string[2];

            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etViewModel = new ProfileManagementDomainModel().GetDependentViewModel();
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etViewModel;
        }

        public ExperienceViewModel GetExperienceViewModel()
        {
            ExperienceViewModel etViewModel = new ExperienceViewModel();
            String[] message = new string[2];

            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etViewModel = new ProfileManagementDomainModel().GetExperienceViewModel();
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etViewModel;
        }

        public EducationViewModel GetEducationViewModel()
        {
            EducationViewModel etViewModel = new EducationViewModel();
            String[] message = new string[2];

            using (TransactionScope scope = new TransactionScope())
            {

                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                        {
                            etViewModel = new ProfileManagementDomainModel().GetEducationViewModel();
                        }
                        scope.Complete();
                    }
                }

                catch (Exception ex)
                {
                    message[0] = "e";
                    message[1] = ex.Message;
                }

            }
            etViewModel.Alert = new AlertViewModel() { HasAlert = false };
            return etViewModel;
        }
    }

}

