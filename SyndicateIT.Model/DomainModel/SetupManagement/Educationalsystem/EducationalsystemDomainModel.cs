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
using SyndicateIT.Model.ViewModel.SetupManagement.Educationalsystem;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Educationalsystem
{
    public class EducationalsystemDomainModel : DomainModelBase
    {

        public EducationalsystemContentViewModel GetEducationalsystemContent()
        {
            EducationalsystemContentViewModel model = new EducationalsystemContentViewModel();
            model.Navigation = GetNavigationList("list", "Educational System");
            model.Title = "Educational System";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public EducationalsystemViewModel GetEducationalsystemViewModel(String Educationalsystem_ID)
        {
            EducationalsystemViewModel model = new EducationalsystemViewModel();
            string title = string.Empty;
            if (Educationalsystem_ID != null && Educationalsystem_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etEducationalsystem = db.Educationalsystems.Where(p => p.Educationalsystem_ID.ToString() == Educationalsystem_ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.Educationalsystem, EducationalsystemViewModel>(etEducationalsystem);
                }
                title = "Edit Educational System";
            }
            else
            {
                title = "New Educational System";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public EducationalsystemViewModel GetEducationalsystemViewModel(EducationalsystemViewModel model)
        {
            string title = string.Empty;
            if (model.Educationalsystem_ID != null && model.Educationalsystem_ID > 0)
            {
                title = "Edit Educational System";
            }
            else
            {
                title = "New Educational System";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<EducationalsystemGridViewModel> GetListEducationalsystem(EducationalsystemContentViewModel etEducationalsystemContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListEducationalsystemBuisnessLogic(etEducationalsystemContentViewModel);

            List<EducationalsystemGridViewModel> lst = new List<EducationalsystemGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etEducationalsystemContentViewModel.Educationalsystem_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Educationalsystem(etEducationalsystemContentViewModel.Educationalsystem_ID, etEducationalsystemContentViewModel.Educationalsystem_Name, etEducationalsystemContentViewModel.Educationalsystem_Description, etEducationalsystemContentViewModel.LANGUAGE_ID, etEducationalsystemContentViewModel.IS_ACTIVE, etEducationalsystemContentViewModel.START_ROW, etEducationalsystemContentViewModel.END_ROW, etEducationalsystemContentViewModel.TOP, etEducationalsystemContentViewModel.ENTRY_DATE, etEducationalsystemContentViewModel.ENTRY_USER_ID, etEducationalsystemContentViewModel.MODIFICATION_USER_ID, etEducationalsystemContentViewModel.MODIFICATION_DATE).ToList();

                List<EducationalsystemGridViewModel> Educationalsystems = Mapper.Map<List<USP_GET_Educationalsystem_Result>, List<EducationalsystemGridViewModel>>(listdb);

                lst = Educationalsystems;
            };

            return lst;

        }

        public String[] DeleteEducationalsystem(EducationalsystemViewModel etEducationalsystemViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteEducationalsystemBuisnessLogic(etEducationalsystemViewModel);
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
                        var etEducationalsystemByLanguage = db.Educationalsystem_By_Language.Where(p => p.Educationalsystem_ID == etEducationalsystemViewModel.Educationalsystem_ID).ToList();
                        if (etEducationalsystemByLanguage.Count > 0)
                        {
                            db.Educationalsystem_By_Language.RemoveRange(etEducationalsystemByLanguage);
                        }
                        db.SaveChanges();
                        var etEducationalsystem = db.Educationalsystems.Where(p => p.Educationalsystem_ID == etEducationalsystemViewModel.Educationalsystem_ID).FirstOrDefault();
                        if (etEducationalsystem != null)
                        {
                            db.Educationalsystems.Remove(etEducationalsystem);
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

        public string[] SaveEducationalsystem(EducationalsystemViewModel etEducationalsystemViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveEducationalsystemBuisnessLogic(etEducationalsystemViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.Educationalsystem etEducationalsystem = new DataLayer.DataContext.Educationalsystem();
            Educationalsystem_By_Language etEducationalsystemByLanguageDb = new Educationalsystem_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etEducationalsystemViewModel.Educationalsystem_ID != 0 && etEducationalsystemViewModel.Educationalsystem_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etEducationalsystemViewModel.ENTRY_USER_ID = etEducationalsystemViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etEducationalsystemdb = db.Educationalsystems.Where(p => p.Educationalsystem_ID.ToString() == etEducationalsystemViewModel.Educationalsystem_ID.ToString()).FirstOrDefault();
                            etEducationalsystem = Mapper.Map<EducationalsystemViewModel, DataLayer.DataContext.Educationalsystem>(etEducationalsystemViewModel);

                            if (etEducationalsystem != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etEducationalsystemViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etEducationalsystemViewModel.Educationalsystem_Name != null)
                                {
                                    count++;
                                    properties[count] = "Educationalsystem_Name";
                                }

                                if (etEducationalsystemViewModel.Educationalsystem_Description != null)
                                {
                                    count++;
                                    properties[count] = "Educationalsystem_Description";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etEducationalsystemdb, etEducationalsystem, true,
                                      properties
                                     );
                                etEducationalsystemdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etEducationalsystemByLanguageDb = db.Educationalsystem_By_Language.Where(p => p.Educationalsystem_ID == etEducationalsystem.Educationalsystem_ID && p.Language_ID == 1).FirstOrDefault();
                                Educationalsystem_By_Language etEducationalsystemByLanguage = new Educationalsystem_By_Language();
                                etEducationalsystemByLanguage = etEducationalsystemByLanguageDb;
                                etEducationalsystemByLanguage.Name = etEducationalsystem.Educationalsystem_Name;
                                etEducationalsystemByLanguage.IS_ACTIVE = etEducationalsystem.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etEducationalsystemByLanguageDb, etEducationalsystemByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etEducationalsystem = Mapper.Map<EducationalsystemViewModel, DataLayer.DataContext.Educationalsystem>(etEducationalsystemViewModel);

                            etEducationalsystem.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etEducationalsystem.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Educationalsystems.Add(etEducationalsystem);
                            db.SaveChanges();
                            Educationalsystem_By_Language erTranslation = new Educationalsystem_By_Language();
                            erTranslation.Name = etEducationalsystem.Educationalsystem_Name;
                            erTranslation.Educationalsystem_ID = etEducationalsystem.Educationalsystem_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etEducationalsystem.IS_ACTIVE;
                            db.Educationalsystem_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etEducationalsystem.Educationalsystem_ID.ToString();
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

        public string DeleteEducationalsystemBuisnessLogic(EducationalsystemViewModel etEducationalsystem)
        {
            string message = string.Empty;
            if (etEducationalsystem.Educationalsystem_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListEducationalsystemBuisnessLogic(EducationalsystemContentViewModel etEducationalsystemContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveEducationalsystemBuisnessLogic(EducationalsystemViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Educational system", NavigationUrl = Utilities.GetUrl("Index", "Educational system", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.Educationalsystem, EducationalsystemViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Educationalsystem_Result, EducationalsystemGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<EducationalsystemViewModel, DataLayer.DataContext.Educationalsystem>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<EducationalsystemGridViewModel, USP_GET_Educationalsystem_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
