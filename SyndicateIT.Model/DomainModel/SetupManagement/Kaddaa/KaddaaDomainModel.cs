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
using SyndicateIT.Model.ViewModel.SetupManagement.Kaddaa;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Kaddaa
{
    public class KaddaaDomainModel : DomainModelBase
    {
        public KaddaaContentViewModel GetKaddaaContent()
        {
            KaddaaContentViewModel model = new KaddaaContentViewModel();
            model.Navigation = GetNavigationList("list", "Kaddaa");
            model.Title = "Kaddaa";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public KaddaaViewModel GetKaddaaViewModel(KaddaaViewModel model)
        {
            string title = string.Empty;
            if (model.STP_Kaddaa_ID != null && model.STP_Kaddaa_ID > 0)
            {
                title = "Edit Kaddaa";
            }
            else
            {
                title = "New Kaddaa";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }

        public KaddaaViewModel GetKaddaaViewModel(String Educationalsystem_ID)
        {
            KaddaaViewModel model = new KaddaaViewModel();
            string title = string.Empty;
            if (Educationalsystem_ID != null && Educationalsystem_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etEducationalsystem = db.Kaddaas.Where(p => p.STP_Kaddaa_ID.ToString() == Educationalsystem_ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.Kaddaa, KaddaaViewModel>(etEducationalsystem);
                }
                title = "Edit Kaddaa";
            }
            else
            {
                title = "New Kaddaa";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }



        public List<KaddaaGridViewModel> GetListKaddaa(KaddaaContentViewModel etKaddaaContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListKaddaaBuisnessLogic(etKaddaaContentViewModel);

            List<KaddaaGridViewModel> lst = new List<KaddaaGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etKaddaaContentViewModel.STP_Kaddaa_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Kaddaa(etKaddaaContentViewModel.STP_Kaddaa_ID, etKaddaaContentViewModel.Kaddaa_NAME, etKaddaaContentViewModel.Region_ID,etKaddaaContentViewModel.COUNTRY_ID, etKaddaaContentViewModel.LANGUAGE_ID, etKaddaaContentViewModel.IS_ACTIVE, etKaddaaContentViewModel.START_ROW, etKaddaaContentViewModel.END_ROW, etKaddaaContentViewModel.TOP, etKaddaaContentViewModel.ENTRY_DATE, etKaddaaContentViewModel.ENTRY_USER_ID, etKaddaaContentViewModel.MODIFICATION_USER_ID, etKaddaaContentViewModel.MODIFICATION_DATE).ToList();

                List<KaddaaGridViewModel> Kaddaas = Mapper.Map<List<USP_GET_Kaddaa_Result>, List<KaddaaGridViewModel>>(listdb);

                lst = Kaddaas;
            };

            return lst;

        }

        public String[] DeleteKaddaa(KaddaaViewModel etKaddaaViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteKaddaaBuisnessLogic(etKaddaaViewModel);
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
                        var etKaddaaByLanguage = db.Kaddaa_BY_LANGUAGE.Where(p => p.Kaddaa_ID == etKaddaaViewModel.STP_Kaddaa_ID).ToList();
                        if (etKaddaaByLanguage.Count > 0)
                        {
                            db.Kaddaa_BY_LANGUAGE.RemoveRange(etKaddaaByLanguage);
                        }
                        db.SaveChanges();
                        var etKaddaa = db.Kaddaas.Where(p => p.STP_Kaddaa_ID == etKaddaaViewModel.STP_Kaddaa_ID).FirstOrDefault();
                        if (etKaddaa != null)
                        {
                            db.Kaddaas.Remove(etKaddaa);
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

        public string[] SaveKaddaa(KaddaaViewModel etKaddaaViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveKaddaaBuisnessLogic(etKaddaaViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.Kaddaa etKaddaa = new DataLayer.DataContext.Kaddaa();
            Kaddaa_BY_LANGUAGE etKaddaaByLanguageDb = new Kaddaa_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etKaddaaViewModel.STP_Kaddaa_ID != 0 && etKaddaaViewModel.STP_Kaddaa_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etKaddaaViewModel.ENTRY_USER_ID = etKaddaaViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etKaddaadb = db.Kaddaas.Where(p => p.STP_Kaddaa_ID.ToString() == etKaddaaViewModel.STP_Kaddaa_ID.ToString()).FirstOrDefault();
                            etKaddaa = Mapper.Map<KaddaaViewModel, DataLayer.DataContext.Kaddaa>(etKaddaaViewModel);

                            if (etKaddaa != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etKaddaaViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etKaddaaViewModel.Kaddaa_NAME != null)
                                {
                                    count++;
                                    properties[count] = "Kaddaa_NAME";
                                }

                                if (etKaddaaViewModel.COUNTRY_ID != null)
                                {
                                    count++;
                                    properties[count] = "COUNTRY_ID";
                                }

                                if (etKaddaaViewModel.Region_ID != null)
                                {
                                    count++;
                                    properties[count] = "Region_ID";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etKaddaadb, etKaddaa, true,
                                      properties
                                     );
                                etKaddaadb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etKaddaaByLanguageDb = db.Kaddaa_BY_LANGUAGE.Where(p => p.Kaddaa_ID == etKaddaa.STP_Kaddaa_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                Kaddaa_BY_LANGUAGE etKaddaaByLanguage = new Kaddaa_BY_LANGUAGE();
                                etKaddaaByLanguage = etKaddaaByLanguageDb;
                                etKaddaaByLanguage.Kaddaa_NAME = etKaddaa.Kaddaa_NAME;
                                etKaddaaByLanguage.IS_ACTIVE = etKaddaa.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etKaddaaByLanguageDb, etKaddaaByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etKaddaa = Mapper.Map<KaddaaViewModel, DataLayer.DataContext.Kaddaa>(etKaddaaViewModel);

                            etKaddaa.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etKaddaa.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Kaddaas.Add(etKaddaa);
                            db.SaveChanges();
                            Kaddaa_BY_LANGUAGE erTranslation = new Kaddaa_BY_LANGUAGE();
                            erTranslation.Kaddaa_NAME = etKaddaa.Kaddaa_NAME;
                            erTranslation.Kaddaa_ID = etKaddaa.STP_Kaddaa_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etKaddaa.IS_ACTIVE;
                            db.Kaddaa_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etKaddaa.STP_Kaddaa_ID.ToString();
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

        public string DeleteKaddaaBuisnessLogic(KaddaaViewModel etKaddaa)
        {
            string message = string.Empty;
            if (etKaddaa.STP_Kaddaa_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListKaddaaBuisnessLogic(KaddaaContentViewModel etKaddaaContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveKaddaaBuisnessLogic(KaddaaViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Kaddaa", NavigationUrl = Utilities.GetUrl("Index", "Kaddaa", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.Kaddaa, KaddaaViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Kaddaa_Result, KaddaaGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<KaddaaViewModel, DataLayer.DataContext.Kaddaa>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<KaddaaGridViewModel, USP_GET_Kaddaa_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
