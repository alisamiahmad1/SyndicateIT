    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.SetupManagement.ROLE;

namespace SyndicateIT.Model.DomainModel.SetupManagement.ROLE
{
    public class ROLEDomainModel : DomainModelBase
    {

        public ROLEContentViewModel GetROLEContent()
        {
            ROLEContentViewModel model = new ROLEContentViewModel();
            model.Navigation = GetNavigationList("list", "Role");
            model.Title = "Role";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public ROLEViewModel GetROLEViewModel(String ROLE_ID)
        {
            ROLEViewModel model = new ROLEViewModel();
            string title = string.Empty;
            if (ROLE_ID != null && ROLE_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etROLE = db.ROLEs.Where(p => p.Role_ID.ToString() == ROLE_ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.ROLE, ROLEViewModel>(etROLE);
                }
                title = "Edit Role";
            }
            else
            {
                title = "New Role";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public ROLEViewModel GetROLEViewModel(ROLEViewModel model)
        {
            string title = string.Empty;
            if (model.Role_ID != null && model.Role_ID.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                title = "Edit Role";
            }
            else
            {
                title = "New Role";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<ROLEGridViewModel> GetListROLE(ROLEContentViewModel etROLEContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListROLEBuisnessLogic(etROLEContentViewModel);

            List<ROLEGridViewModel> lst = new List<ROLEGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etROLEContentViewModel.Role_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_ROLE(etROLEContentViewModel.Role_ID, etROLEContentViewModel.ROLE_NAME, etROLEContentViewModel.Company_ID, etROLEContentViewModel.LANGUAGE_ID, etROLEContentViewModel.IS_ACTIVE, etROLEContentViewModel.START_ROW, etROLEContentViewModel.END_ROW, etROLEContentViewModel.TOP, etROLEContentViewModel.ENTRY_DATE, etROLEContentViewModel.ENTRY_USER_ID, etROLEContentViewModel.MODIFICATION_USER_ID, etROLEContentViewModel.MODIFICATION_DATE).ToList();

                List<ROLEGridViewModel> ROLEs = Mapper.Map<List<USP_GET_ROLE_Result>, List<ROLEGridViewModel>>(listdb);

                lst = ROLEs;
            };

            return lst;

        }

        public String[] DeleteROLE(ROLEViewModel etROLEViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteROLEBuisnessLogic(etROLEViewModel);
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
                        var etROLEByLanguage = db.ROLE_BY_LANGUAGE.Where(p => p.Role_ID == etROLEViewModel.Role_ID).ToList();
                        if (etROLEByLanguage.Count > 0)
                        {
                            db.ROLE_BY_LANGUAGE.RemoveRange(etROLEByLanguage);
                        }
                        db.SaveChanges();
                        var etROLE = db.ROLEs.Where(p => p.Role_ID == etROLEViewModel.Role_ID).FirstOrDefault();
                        if (etROLE != null)
                        {
                            db.ROLEs.Remove(etROLE);
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

        public string[] SaveROLE(ROLEViewModel etROLEViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveROLEBuisnessLogic(etROLEViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.ROLE etROLE = new DataLayer.DataContext.ROLE();
            ROLE_BY_LANGUAGE etROLEByLanguageDb = new ROLE_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etROLEViewModel.Role_ID.ToString() != "" && etROLEViewModel.Role_ID.ToString() != "00000000-0000-0000-0000-000000000000")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etROLEViewModel.ENTRY_USER_ID = etROLEViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etROLEdb = db.ROLEs.Where(p => p.Role_ID.ToString() == etROLEViewModel.Role_ID.ToString()).FirstOrDefault();
                            etROLE = Mapper.Map<ROLEViewModel, DataLayer.DataContext.ROLE>(etROLEViewModel);

                            if (etROLE != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etROLEViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etROLEViewModel.ROLE_NAME != null)
                                {
                                    count++;
                                    properties[count] = "ROLE_NAME";
                                }


                                if (etROLEViewModel.Company_ID != null)
                                {
                                    count++;
                                    properties[count] = "Company_ID";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etROLEdb, etROLE, true,
                                      properties
                                     );
                                etROLEdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etROLEByLanguageDb = db.ROLE_BY_LANGUAGE.Where(p => p.Role_ID == etROLE.Role_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                ROLE_BY_LANGUAGE etROLEByLanguage = new ROLE_BY_LANGUAGE();
                                etROLEByLanguage = etROLEByLanguageDb;
                                etROLEByLanguage.ROLE_NAME = etROLE.ROLE_NAME;
                                etROLEByLanguage.IS_ACTIVE = etROLE.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etROLEByLanguageDb, etROLEByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etROLE = Mapper.Map<ROLEViewModel, DataLayer.DataContext.ROLE>(etROLEViewModel);

                            etROLE.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etROLE.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }
                            etROLE.Role_ID = Guid.NewGuid();
                            db.ROLEs.Add(etROLE);
                            db.SaveChanges();
                            ROLE_BY_LANGUAGE erTranslation = new ROLE_BY_LANGUAGE();
                            erTranslation.ROLE_NAME = etROLE.ROLE_NAME;
                            erTranslation.Role_ID = etROLE.Role_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etROLE.IS_ACTIVE;
                            db.ROLE_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etROLE.Role_ID.ToString();
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

        public string DeleteROLEBuisnessLogic(ROLEViewModel etROLE)
        {
            string message = string.Empty;
            if (etROLE.Role_ID.ToString() == "-1")
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListROLEBuisnessLogic(ROLEContentViewModel etROLEContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveROLEBuisnessLogic(ROLEViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods

        public List<NavigationViewModel> GetNavigationList(string type, string title)
        {
            var model = new List<NavigationViewModel>();

            if (type == "list")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Setup", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
            else if (type == "update")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Setup", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = "ROLE", NavigationUrl = Utilities.GetUrl("Index", "ROLE", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.ROLE, ROLEViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_ROLE_Result, ROLEGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<ROLEViewModel, DataLayer.DataContext.ROLE>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<ROLEGridViewModel, USP_GET_ROLE_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion
    }
}
