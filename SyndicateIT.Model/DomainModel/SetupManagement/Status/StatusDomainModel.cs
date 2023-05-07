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
using SyndicateIT.Model.ViewModel.SetupManagement.Status;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Status
{
    public class StatusDomainModel : DomainModelBase
    {

        public StatusContentViewModel GetStatusContent()
        {
            StatusContentViewModel model = new StatusContentViewModel();
            model.Navigation = GetNavigationList("list", "Status");
            model.Title = "Status";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public StatusViewModel GetStatusViewModel(String Status_ID)
        {
            StatusViewModel model = new StatusViewModel();
            string title = string.Empty;
            if (Status_ID != null && Status_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etStatus = db.Status.Where(p => p.Status_ID.ToString() == Status_ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.Status, StatusViewModel>(etStatus);
                }
                title = "Edit Status";
            }
            else
            {
                title = "New Status";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public StatusViewModel GetStatusViewModel(StatusViewModel model)
        {
            string title = string.Empty;
            if (model.Status_ID != null && model.Status_ID > 0)
            {
                title = "Edit Status";
            }
            else
            {
                title = "New Status";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<StatusGridViewModel> GetListStatus(StatusContentViewModel etStatusContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListStatusBuisnessLogic(etStatusContentViewModel);

            List<StatusGridViewModel> lst = new List<StatusGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etStatusContentViewModel.Status_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Status(etStatusContentViewModel.Status_ID, etStatusContentViewModel.Status_Name, etStatusContentViewModel.Status_Description, etStatusContentViewModel.LANGUAGE_ID, etStatusContentViewModel.IS_ACTIVE, etStatusContentViewModel.START_ROW, etStatusContentViewModel.END_ROW, etStatusContentViewModel.TOP, etStatusContentViewModel.ENTRY_DATE, etStatusContentViewModel.ENTRY_USER_ID, etStatusContentViewModel.MODIFICATION_USER_ID, etStatusContentViewModel.MODIFICATION_DATE).ToList();

                List<StatusGridViewModel> Statuss = Mapper.Map<List<USP_GET_Status_Result>, List<StatusGridViewModel>>(listdb);

                lst = Statuss;
            };

            return lst;

        }

        public String[] DeleteStatus(StatusViewModel etStatusViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteStatusBuisnessLogic(etStatusViewModel);
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
                        var etStatusByLanguage = db.STATUS_BY_LANGUAGE.Where(p => p.Status_ID == etStatusViewModel.Status_ID).ToList();
                        if (etStatusByLanguage.Count > 0)
                        {
                            db.STATUS_BY_LANGUAGE.RemoveRange(etStatusByLanguage);
                        }
                        db.SaveChanges();
                        var etStatus = db.Status.Where(p => p.Status_ID == etStatusViewModel.Status_ID).FirstOrDefault();
                        if (etStatus != null)
                        {
                            db.Status.Remove(etStatus);
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

        public string[] SaveStatus(StatusViewModel etStatusViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveStatusBuisnessLogic(etStatusViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.Status etStatus = new DataLayer.DataContext.Status();
            STATUS_BY_LANGUAGE etStatusByLanguageDb = new STATUS_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etStatusViewModel.Status_ID != 0 && etStatusViewModel.Status_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etStatusViewModel.ENTRY_USER_ID = etStatusViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etStatusdb = db.Status.Where(p => p.Status_ID.ToString() == etStatusViewModel.Status_ID.ToString()).FirstOrDefault();
                            etStatus = Mapper.Map<StatusViewModel, DataLayer.DataContext.Status>(etStatusViewModel);

                            if (etStatus != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etStatusViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etStatusViewModel.Status_Name != null)
                                {
                                    count++;
                                    properties[count] = "Status_Name";
                                }

                                if (etStatusViewModel.Status_Description != null)
                                {
                                    count++;
                                    properties[count] = "Status_Description";
                                }

                                if (etStatusViewModel.StatusTitle != null)
                                {
                                    count++;
                                    properties[count] = "StatusTitle";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etStatusdb, etStatus, true,
                                      properties
                                     );
                                etStatusdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etStatusByLanguageDb = db.STATUS_BY_LANGUAGE.Where(p => p.Status_ID == etStatus.Status_ID && p.Language_ID == 1).FirstOrDefault();
                                STATUS_BY_LANGUAGE etStatusByLanguage = new STATUS_BY_LANGUAGE();
                                etStatusByLanguage = etStatusByLanguageDb;
                                etStatusByLanguage.Status_Name = etStatus.Status_Name;
                                etStatusByLanguage.IS_ACTIVE = etStatus.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etStatusByLanguageDb, etStatusByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etStatus = Mapper.Map<StatusViewModel, DataLayer.DataContext.Status>(etStatusViewModel);

                            etStatus.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etStatus.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Status.Add(etStatus);
                            db.SaveChanges();
                            STATUS_BY_LANGUAGE erTranslation = new STATUS_BY_LANGUAGE();
                            erTranslation.Status_Name = etStatus.Status_Name;
                            erTranslation.Status_ID = etStatus.Status_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etStatus.IS_ACTIVE;
                            db.STATUS_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etStatus.Status_ID.ToString();
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

        public string DeleteStatusBuisnessLogic(StatusViewModel etStatus)
        {
            string message = string.Empty;
            if (etStatus.Status_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListStatusBuisnessLogic(StatusContentViewModel etStatusContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveStatusBuisnessLogic(StatusViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Status", NavigationUrl = Utilities.GetUrl("Index", "Status", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.Status, StatusViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Status_Result, StatusGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<StatusViewModel, DataLayer.DataContext.Status>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<StatusGridViewModel, USP_GET_Status_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
