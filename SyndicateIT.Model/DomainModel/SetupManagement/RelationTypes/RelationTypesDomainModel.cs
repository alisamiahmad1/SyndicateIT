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
using SyndicateIT.Model.ViewModel.SetupManagement.RelationTypes;

namespace SyndicateIT.Model.DomainModel.SetupManagement.RelationTypes
{
   public  class RelationTypesDomainModel : DomainModelBase
    {


        public RelationTypesViewModel GetRelationTypes()
        {
            RelationTypesViewModel model = new RelationTypesViewModel();
            model.Navigation = GetNavigationList("list", "RelationTypes");
            model.Title = "Relation Type";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public RelationTypesContentViewModel GetRelationTypesContent()
        {
            RelationTypesContentViewModel model = new RelationTypesContentViewModel();
            model.Navigation = GetNavigationList("list", "Relation Type");
            model.Title = "Relation Type";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public RelationTypesViewModel GetRelationTypesViewModel(String RelationTypes_ID)
        {
            RelationTypesViewModel model = new RelationTypesViewModel();
            string title = string.Empty;
            if (RelationTypes_ID != null && RelationTypes_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etRelationTypes = db.Relation_Types.Where(p => p.Relation_Type_ID.ToString() == RelationTypes_ID).FirstOrDefault();
                    model = Mapper.Map<Relation_Types, RelationTypesViewModel>(etRelationTypes);
                }
                title = "Edit Relation Type";
            }
            else
            {
                title = "New Relation Type";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public RelationTypesViewModel GetRelationTypesViewModel(RelationTypesViewModel model)
        {
            string title = string.Empty;
            if (model.Relation_Type_ID != null && model.Relation_Type_ID > 0)
            {
                title = "Edit Relation Type";
            }
            else
            {
                title = "New Relation Type";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<RelationTypesGridViewModel> GetListRelationTypes(RelationTypesContentViewModel etRelationTypesContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListRelationTypesBuisnessLogic(etRelationTypesContentViewModel);

            List<RelationTypesGridViewModel> lst = new List<RelationTypesGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etRelationTypesContentViewModel.Relation_Type_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Relation_Types(etRelationTypesContentViewModel.Relation_Type_ID, etRelationTypesContentViewModel.Relation_Type_Title, etRelationTypesContentViewModel.Relation_Type_Description, etRelationTypesContentViewModel.LANGUAGE_ID, etRelationTypesContentViewModel.IS_ACTIVE, etRelationTypesContentViewModel.START_ROW, etRelationTypesContentViewModel.END_ROW, etRelationTypesContentViewModel.TOP, etRelationTypesContentViewModel.ENTRY_DATE, etRelationTypesContentViewModel.ENTRY_USER_ID, etRelationTypesContentViewModel.MODIFICATION_USER_ID, etRelationTypesContentViewModel.MODIFICATION_DATE).ToList();

                List<RelationTypesGridViewModel> RelationTypess = Mapper.Map<List<USP_GET_Relation_Types_Result>, List<RelationTypesGridViewModel>>(listdb);

                lst = RelationTypess;
            };

            return lst;

        }

        public String[] DeleteRelationTypes(RelationTypesViewModel etRelationTypesViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteRelationTypesBuisnessLogic(etRelationTypesViewModel);
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
                        var etRelationTypesByLanguage = db.Relation_Types_By_Language.Where(p => p.Relation_Type_ID == etRelationTypesViewModel.Relation_Type_ID).ToList();
                        if (etRelationTypesByLanguage.Count > 0)
                        {
                            db.Relation_Types_By_Language.RemoveRange(etRelationTypesByLanguage);
                        }
                        db.SaveChanges();
                        var etRelationTypes = db.Relation_Types.Where(p => p.Relation_Type_ID == etRelationTypesViewModel.Relation_Type_ID).FirstOrDefault();
                        if (etRelationTypes != null)
                        {
                            db.Relation_Types.Remove(etRelationTypes);
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

        public string[] SaveRelationTypes(RelationTypesViewModel etRelationTypesViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveRelationTypesBuisnessLogic(etRelationTypesViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            Relation_Types etRelationTypes = new Relation_Types();
            Relation_Types_By_Language etRelationTypesByLanguageDb = new Relation_Types_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etRelationTypesViewModel.Relation_Type_ID != 0 && etRelationTypesViewModel.Relation_Type_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etRelationTypesViewModel.ENTRY_USER_ID = etRelationTypesViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etRelationTypesdb = db.Relation_Types.Where(p => p.Relation_Type_ID.ToString() == etRelationTypesViewModel.Relation_Type_ID.ToString()).FirstOrDefault();
                            etRelationTypes = Mapper.Map<RelationTypesViewModel, Relation_Types>(etRelationTypesViewModel);

                            if (etRelationTypes != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etRelationTypesViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etRelationTypesViewModel.Relation_Type_Title != null)
                                {
                                    count++;
                                    properties[count] = "Relation_Type_Title";
                                }
                                if (etRelationTypesViewModel.Relation_Type_Description != null)
                                {
                                    count++;
                                    properties[count] = "Relation_Type_Description";
                                }
                                

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etRelationTypesdb, etRelationTypes, true,
                                      properties
                                     );
                                etRelationTypesdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etRelationTypesByLanguageDb = db.Relation_Types_By_Language.Where(p => p.Relation_Type_ID == etRelationTypes.Relation_Type_ID && p.Language_ID == 1).FirstOrDefault();
                                Relation_Types_By_Language etRelationTypesByLanguage = new Relation_Types_By_Language();
                                etRelationTypesByLanguage = etRelationTypesByLanguageDb;
                                etRelationTypesByLanguage.Relation_Type_Title = etRelationTypes.Relation_Type_Title;
                                etRelationTypesByLanguage.IS_ACTIVE = etRelationTypes.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etRelationTypesByLanguageDb, etRelationTypesByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etRelationTypes = Mapper.Map<RelationTypesViewModel, Relation_Types>(etRelationTypesViewModel);

                            etRelationTypes.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etRelationTypes.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Relation_Types.Add(etRelationTypes);
                            db.SaveChanges();
                            Relation_Types_By_Language erTranslation = new Relation_Types_By_Language();
                            erTranslation.Relation_Type_Title = etRelationTypes.Relation_Type_Title;
                            erTranslation.Relation_Type_ID = etRelationTypes.Relation_Type_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etRelationTypes.IS_ACTIVE;
                            db.Relation_Types_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etRelationTypes.Relation_Type_ID.ToString();
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

        public string DeleteRelationTypesBuisnessLogic(RelationTypesViewModel etRelationTypes)
        {
            string message = string.Empty;
            if (etRelationTypes.Relation_Type_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListRelationTypesBuisnessLogic(RelationTypesContentViewModel etRelationTypesViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveRelationTypesBuisnessLogic(RelationTypesViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Relation Type", NavigationUrl = Utilities.GetUrl("index", "RelationTypes", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<Relation_Types, RelationTypesViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Relation_Types_Result, RelationTypesGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<RelationTypesViewModel, Relation_Types>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<RelationTypesGridViewModel, USP_GET_Relation_Types_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
