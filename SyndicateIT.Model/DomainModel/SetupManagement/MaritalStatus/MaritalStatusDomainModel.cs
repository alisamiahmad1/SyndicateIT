using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.SetupManagement.MaritalStatus;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.DomainModel.SetupManagement.MaritalStatus
{
  public  class MaritalStatusDomainModel : DomainModelBase
    {

        public MaritalStatusContentViewModel GetMaritalStatusContent()
        {
            MaritalStatusContentViewModel model = new MaritalStatusContentViewModel();
            model.Navigation = GetNavigationList("list", "Marital Status");
            model.Title = "Marital Status";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public MaritalStatusViewModel GetMaritalStatusViewModel(String MaritalStatus_ID)
        {
            MaritalStatusViewModel model = new MaritalStatusViewModel();
            string title = string.Empty;
            if (MaritalStatus_ID != null && MaritalStatus_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etMaritalStatus = db.Marital_Status.Where(p => p.Marital_Status_ID.ToString() == MaritalStatus_ID).FirstOrDefault();
                    model = Mapper.Map<Marital_Status, MaritalStatusViewModel>(etMaritalStatus);
                }
                title = "Edit Marital Status";
            }
            else
            {
                title = "New Marital Status";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public MaritalStatusViewModel GetMaritalStatusViewModel(MaritalStatusViewModel model)
        {
            string title = string.Empty;
            if (model.Marital_Status_ID != null && model.Marital_Status_ID > 0)
            {
                title = "Edit Marital Status";
            }
            else
            {
                title = "New Marital Status";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<MaritalStatusGridViewModel> GetListMaritalStatus(MaritalStatusContentViewModel etMaritalStatusContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListMaritalStatusBuisnessLogic(etMaritalStatusContentViewModel);

            List<MaritalStatusGridViewModel> lst = new List<MaritalStatusGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etMaritalStatusContentViewModel.Marital_Status_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Marital_Status(etMaritalStatusContentViewModel.Marital_Status_ID, etMaritalStatusContentViewModel.Marital_Status_Title, etMaritalStatusContentViewModel.Marital_Status_Description, etMaritalStatusContentViewModel.LANGUAGE_ID, etMaritalStatusContentViewModel.IS_ACTIVE, etMaritalStatusContentViewModel.START_ROW, etMaritalStatusContentViewModel.END_ROW, etMaritalStatusContentViewModel.TOP, etMaritalStatusContentViewModel.ENTRY_DATE, etMaritalStatusContentViewModel.ENTRY_USER_ID, etMaritalStatusContentViewModel.MODIFICATION_USER_ID, etMaritalStatusContentViewModel.MODIFICATION_DATE).ToList();

                List<MaritalStatusGridViewModel> MaritalStatuss = Mapper.Map<List<USP_GET_Marital_Status_Result>, List<MaritalStatusGridViewModel>>(listdb);

                lst = MaritalStatuss;
            };

            return lst;

        }

        public String[] DeleteMaritalStatus(MaritalStatusViewModel etMaritalStatusViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteMaritalStatusBuisnessLogic(etMaritalStatusViewModel);
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
                        var etMaritalStatusByLanguage = db.Marital_Status_By_Language.Where(p => p.Marital_Status_ID == etMaritalStatusViewModel.Marital_Status_ID).ToList();
                        if (etMaritalStatusByLanguage.Count > 0)
                        {
                            db.Marital_Status_By_Language.RemoveRange(etMaritalStatusByLanguage);
                        }
                        db.SaveChanges();
                        var etMaritalStatus = db.Marital_Status.Where(p => p.Marital_Status_ID == etMaritalStatusViewModel.Marital_Status_ID).FirstOrDefault();
                        if (etMaritalStatus != null)
                        {
                            db.Marital_Status.Remove(etMaritalStatus);
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

        public string[] SaveMaritalStatus(MaritalStatusViewModel etMaritalStatusViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveMaritalStatusBuisnessLogic(etMaritalStatusViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            Marital_Status etMaritalStatus = new Marital_Status();
            Marital_Status_By_Language etMaritalStatusByLanguageDb = new Marital_Status_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etMaritalStatusViewModel.Marital_Status_ID != 0 && etMaritalStatusViewModel.Marital_Status_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etMaritalStatusViewModel.ENTRY_USER_ID = etMaritalStatusViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etMaritalStatusdb = db.Marital_Status.Where(p => p.Marital_Status_ID.ToString() == etMaritalStatusViewModel.Marital_Status_ID.ToString()).FirstOrDefault();
                            etMaritalStatus = Mapper.Map<MaritalStatusViewModel, Marital_Status>(etMaritalStatusViewModel);

                            if (etMaritalStatus != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etMaritalStatusViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etMaritalStatusViewModel.Marital_Status_Title != null)
                                {
                                    count++;
                                    properties[count] = "Marital_Status_Title";
                                }

                                if (etMaritalStatusViewModel.Marital_Status_Description != null)
                                {
                                    count++;
                                    properties[count] = "Marital_Status_Description";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etMaritalStatusdb, etMaritalStatus, true,
                                      properties
                                     );
                                etMaritalStatusdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etMaritalStatusByLanguageDb = db.Marital_Status_By_Language.Where(p => p.Marital_Status_ID == etMaritalStatus.Marital_Status_ID && p.Language_ID == 1).FirstOrDefault();
                                Marital_Status_By_Language etMaritalStatusByLanguage = new Marital_Status_By_Language();
                                etMaritalStatusByLanguage = etMaritalStatusByLanguageDb;
                                etMaritalStatusByLanguage.Marital_Status_Title = etMaritalStatus.Marital_Status_Title;
                                etMaritalStatusByLanguage.IS_ACTIVE = etMaritalStatus.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etMaritalStatusByLanguageDb, etMaritalStatusByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etMaritalStatus = Mapper.Map<MaritalStatusViewModel, Marital_Status>(etMaritalStatusViewModel);

                            etMaritalStatus.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etMaritalStatus.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Marital_Status.Add(etMaritalStatus);
                            db.SaveChanges();
                            Marital_Status_By_Language erTranslation = new Marital_Status_By_Language();
                            erTranslation.Marital_Status_Title = etMaritalStatus.Marital_Status_Title;
                            erTranslation.Marital_Status_ID = etMaritalStatus.Marital_Status_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etMaritalStatus.IS_ACTIVE;
                            db.Marital_Status_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etMaritalStatus.Marital_Status_ID.ToString();
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

        public string DeleteMaritalStatusBuisnessLogic(MaritalStatusViewModel etMaritalStatus)
        {
            string message = string.Empty;
            if (etMaritalStatus.Marital_Status_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListMaritalStatusBuisnessLogic(MaritalStatusContentViewModel etMaritalStatusContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveMaritalStatusBuisnessLogic(MaritalStatusViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Marital Status", NavigationUrl = Utilities.GetUrl("Index", "MaritalStatus", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<Marital_Status, MaritalStatusViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Marital_Status_Result, MaritalStatusGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<MaritalStatusViewModel, Marital_Status>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<MaritalStatusGridViewModel, USP_GET_Marital_Status_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion
    }
}
