using AutoMapper;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SyndicateIT.Model.DomainModel.ProfileManagement
{
  public  class User_ContactDomainModel : DomainModelBase
    {
        ////public User_ContactContentViewModel GetUser_ContactContent()
        ////{
        ////    User_ContactContentViewModel model = new User_ContactContentViewModel();
        ////    model.Navigation = GetNavigationList("list", "User_Contact");
        ////    model.Title = "User_Contact";
        ////    model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
        ////    model.Alert = new AlertViewModel { HasAlert = false };
        ////    return model;
        ////}

        public User_ContactViewModel GetUser_ContactViewModel(String User_Contact_ID)
        {
            User_ContactViewModel model = new User_ContactViewModel();
            string title = string.Empty;
            if (User_Contact_ID != null && User_Contact_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etUser_Contact = db.Tbl_User_Contact.Where(p => p.User_Contact_ID.ToString() == User_Contact_ID).FirstOrDefault();
                    model = Mapper.Map<Tbl_User_Contact, User_ContactViewModel>(etUser_Contact);
                }
                title = "Edit User_Contact";
            }
            else
            {
                title = "New User_Contact";
            }
           
            model.Title = title;
           
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public User_ContactViewModel GetUser_ContactViewModel(User_ContactViewModel model)
        {
            string title = string.Empty;
            if (model.User_Contact_ID != null && model.User_Contact_ID > 0)
            {
                title = "Edit User_Contact";
            }
            else
            {
                title = "New User_Contact";
            }
           
            model.Title = title;
            
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        //public List<User_ContactGridViewModel> GetListUser_Contact(User_ContactContentViewModel etUser_ContactContentViewModel)
        //{
        //    string[] messages = new string[2];
        //    messages[1] = GetListUser_ContactBuisnessLogic(etUser_ContactContentViewModel);

        //    List<User_ContactGridViewModel> lst = new List<User_ContactGridViewModel>();

        //    SyndicateIT.UtilityComponent.Encryption.Encrypt(etUser_ContactContentViewModel.User_Contact_ID.ToString());

        //    using (var db = new SyndicateDataEntities())
        //    {


        //        var listdb = db.USP_GET_User_Contact(
        //           etUser_ContactContentViewModel.User_Contact_ID,
        //            etUser_ContactContentViewModel.User_Contact_NAME,
        //            etUser_ContactContentViewModel.LANGUAGE_ID,
        //            etUser_ContactContentViewModel.IS_ACTIVE,
        //            etUser_ContactContentViewModel.START_ROW,
        //            etUser_ContactContentViewModel.END_ROW,
        //            etUser_ContactContentViewModel.TOP,
        //            etUser_ContactContentViewModel.ENTRY_DATE,
        //            etUser_ContactContentViewModel.ENTRY_USER_ID,
        //            etUser_ContactContentViewModel.MODIFICATION_USER_ID,
        //            etUser_ContactContentViewModel.Modification_Date
        //            ).ToList();

        //        List<User_ContactGridViewModel> User_Contacts = Mapper.Map<List<USP_GET_User_Contact_Result>, List<User_ContactGridViewModel>>(listdb);

        //        lst = User_Contacts;
        //    };

        //    return lst;

        //}

        public String[] DeleteUser_Contact(User_ContactViewModel etUser_ContactViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteUser_ContactBuisnessLogic(etUser_ContactViewModel);
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
                       
                        
                        var etUser_Contact = db.Tbl_User_Contact.Where(p => p.User_Contact_ID == etUser_ContactViewModel.User_Contact_ID).FirstOrDefault();
                        if (etUser_Contact != null)
                        {
                            db.Tbl_User_Contact.Remove(etUser_Contact);
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

        public string[] SaveUser_Contact(User_ContactViewModel etUser_ContactViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveUser_ContactBuisnessLogic(etUser_ContactViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            Tbl_User_Contact etUser_Contact = new Tbl_User_Contact();
          
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etUser_ContactViewModel.User_Contact_ID != 0 && etUser_ContactViewModel.User_Contact_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etUser_ContactViewModel.ENTRY_USER_ID = etUser_ContactViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                              
                            }
                            var etUser_Contactdb = db.Tbl_User_Contact.Where(p => p.User_Contact_ID.ToString() == etUser_ContactViewModel.User_Contact_ID.ToString()).FirstOrDefault();
                            etUser_Contact = Mapper.Map<User_ContactViewModel, Tbl_User_Contact>(etUser_ContactViewModel);
                            
                            if (etUser_Contact != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etUser_ContactViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

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

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etUser_Contactdb, etUser_Contact, true,
                                      properties
                                     );
                                etUser_Contactdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etUser_Contact = Mapper.Map<User_ContactViewModel, Tbl_User_Contact>(etUser_ContactViewModel);

                            etUser_Contact.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etUser_Contact.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Tbl_User_Contact.Add(etUser_Contact);
                          
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etUser_Contact.User_Contact_ID.ToString();
                        //   savedSuccessfully = true;
                        scope.Complete();

                    }
                }

                catch (Exception ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.Message;
                }

            }
            return messages;

        }

        public string DeleteUser_ContactBuisnessLogic(User_ContactViewModel etUser_Contact)
        {
            string message = string.Empty;
            if (etUser_Contact.User_Contact_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        //public string GetListUser_ContactBuisnessLogic(User_ContactContentViewModel etUser_ContactContentViewModel)
        //{
        //    string message = string.Empty;
        //    return message;
        //}

        public string SaveUser_ContactBuisnessLogic(User_ContactViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods

        //public List<NavigationViewModel> GetNavigationList(string type, string title)
        //{
        //    var model = new List<NavigationViewModel>();

        //    if (type == "list")
        //    {
        //        model.Add(new NavigationViewModel() { NavigationName = "Setup", NavigationUrl = "" });
        //        model.Add(new NavigationViewModel() { NavigationName = title });
        //    }
        //    else if (type == "update")
        //    {
        //        model.Add(new NavigationViewModel() { NavigationName = "Setup", NavigationUrl = "" });
        //        model.Add(new NavigationViewModel() { NavigationName = "User_Contact", NavigationUrl = Utilities.GetUrl("User_Contacts", "User_Contact", SessionContent.Current.Corporate.IsSecure) });
        //        model.Add(new NavigationViewModel() { NavigationName = title });
        //    }

        //    return model;
        //}

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<Tbl_User_Contact, User_ContactViewModel>()
                  .IgnoreAllNonExisting();

            //Mapper.CreateMap<USP_GET_User_Contact_Result, User_ContactGridViewModel>()
            //     .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<User_ContactViewModel, Tbl_User_Contact>()
                  .IgnoreAllNonExisting();
            //Mapper.CreateMap<User_ContactGridViewModel, USP_GET_User_Contact_Result>()
            //   .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }
}
