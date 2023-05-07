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
using SyndicateIT.Model.ViewModel.SetupManagement.DocumentExtension;

namespace SyndicateIT.Model.DomainModel.SetupManagement.DocumentExtension
{
    public class DocumentExtensionDomainModel : DomainModelBase
    {

        public DocumentExtensionContentViewModel GetDocumentExtensionContent()
        {
            DocumentExtensionContentViewModel model = new DocumentExtensionContentViewModel();
            model.Navigation = GetNavigationList("list", "Document Extension");
            model.Title = "Document Extension";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public DocumentExtensionViewModel GetDocumentExtensionViewModel(String DocumentExtension_ID)
        {
            DocumentExtensionViewModel model = new DocumentExtensionViewModel();
            string title = string.Empty;
            if (DocumentExtension_ID != null && DocumentExtension_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDocumentExtension = db.Document_Extension.Where(p => p.Document_Ext_ID.ToString() == DocumentExtension_ID).FirstOrDefault();
                    model = Mapper.Map<Document_Extension, DocumentExtensionViewModel>(etDocumentExtension);
                }
                title = "Edit Document Extension";
            }
            else
            {
                title = "New Document Extension";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public DocumentExtensionViewModel GetDocumentExtensionViewModel(DocumentExtensionViewModel model)
        {
            string title = string.Empty;
            if (model.Document_Ext_ID != null && model.Document_Ext_ID > 0)
            {
                title = "Edit DocumentExtension";
            }
            else
            {
                title = "New DocumentExtension";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<DocumentExtensionGridViewModel> GetListDocumentExtension(DocumentExtensionContentViewModel etDocumentExtensionContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListDocumentExtensionBuisnessLogic(etDocumentExtensionContentViewModel);

            List<DocumentExtensionGridViewModel> lst = new List<DocumentExtensionGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etDocumentExtensionContentViewModel.Document_Ext_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Document_Extension(etDocumentExtensionContentViewModel.Document_Ext_ID, etDocumentExtensionContentViewModel.Extension_Name, etDocumentExtensionContentViewModel.Extension_Type, etDocumentExtensionContentViewModel.LANGUAGE_ID, etDocumentExtensionContentViewModel.IS_ACTIVE, etDocumentExtensionContentViewModel.START_ROW, etDocumentExtensionContentViewModel.END_ROW, etDocumentExtensionContentViewModel.TOP, etDocumentExtensionContentViewModel.ENTRY_DATE, etDocumentExtensionContentViewModel.ENTRY_USER_ID, etDocumentExtensionContentViewModel.MODIFICATION_USER_ID, etDocumentExtensionContentViewModel.MODIFICATION_DATE).ToList();

               List<DocumentExtensionGridViewModel> DocumentExtensions = Mapper.Map<List<USP_GET_Document_Extension_Result>, List<DocumentExtensionGridViewModel>>(listdb);

                lst = DocumentExtensions;
            };

            return lst;

        }

        public String[] DeleteDocumentExtension(DocumentExtensionViewModel etDocumentExtensionViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteDocumentExtensionBuisnessLogic(etDocumentExtensionViewModel);
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
                        var etDocumentExtensionByLanguage = db.Document_Extension_By_Language.Where(p => p.Document_Ext_ID == etDocumentExtensionViewModel.Document_Ext_ID).ToList();
                        if (etDocumentExtensionByLanguage.Count > 0)
                        {
                            db.Document_Extension_By_Language.RemoveRange(etDocumentExtensionByLanguage);
                        }
                        db.SaveChanges();
                        var etDocumentExtension = db.Document_Extension.Where(p => p.Document_Ext_ID == etDocumentExtensionViewModel.Document_Ext_ID).FirstOrDefault();
                        if (etDocumentExtension != null)
                        {
                            db.Document_Extension.Remove(etDocumentExtension);
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

        public string[] SaveDocumentExtension(DocumentExtensionViewModel etDocumentExtensionViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveDocumentExtensionBuisnessLogic(etDocumentExtensionViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            Document_Extension etDocumentExtension = new Document_Extension();
            Document_Extension_By_Language etDocumentExtensionByLanguageDb = new Document_Extension_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etDocumentExtensionViewModel.Document_Ext_ID != 0 && etDocumentExtensionViewModel.Document_Ext_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etDocumentExtensionViewModel.ENTRY_USER_ID = etDocumentExtensionViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etDocumentExtensiondb = db.Document_Extension.Where(p => p.Document_Ext_ID.ToString() == etDocumentExtensionViewModel.Document_Ext_ID.ToString()).FirstOrDefault();
                            etDocumentExtension = Mapper.Map<DocumentExtensionViewModel, Document_Extension>(etDocumentExtensionViewModel);

                            if (etDocumentExtension != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etDocumentExtensionViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                

                                if (etDocumentExtensionViewModel.Extension_Name != null)
                                {
                                    count++;
                                    properties[count] = "Extension_Name";
                                }

                                if (etDocumentExtensionViewModel.Extension_Type != null)
                                {
                                    count++;
                                    properties[count] = "Extension_Type";
                                }
                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etDocumentExtensiondb, etDocumentExtension, true,
                                      properties
                                     );
                                etDocumentExtensiondb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etDocumentExtensionByLanguageDb = db.Document_Extension_By_Language.Where(p => p.Document_Ext_ID == etDocumentExtension.Document_Ext_ID && p.Language_ID == 1).FirstOrDefault();
                                Document_Extension_By_Language etDocumentExtensionByLanguage = new Document_Extension_By_Language();
                                etDocumentExtensionByLanguage = etDocumentExtensionByLanguageDb;
                                etDocumentExtensionByLanguage.Extension_Name = etDocumentExtension.Extension_Name;
                                etDocumentExtensionByLanguage.IS_ACTIVE = etDocumentExtension.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etDocumentExtensionByLanguageDb, etDocumentExtensionByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etDocumentExtension = Mapper.Map<DocumentExtensionViewModel, Document_Extension>(etDocumentExtensionViewModel);

                            etDocumentExtension.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etDocumentExtension.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Document_Extension.Add(etDocumentExtension);
                            db.SaveChanges();
                            Document_Extension_By_Language erTranslation = new Document_Extension_By_Language();
                            erTranslation.Extension_Name = etDocumentExtension.Extension_Name;
                            erTranslation.Document_Ext_ID = etDocumentExtension.Document_Ext_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etDocumentExtension.IS_ACTIVE;
                            db.Document_Extension_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etDocumentExtension.Document_Ext_ID.ToString();
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

        public string DeleteDocumentExtensionBuisnessLogic(DocumentExtensionViewModel etDocumentExtension)
        {
            string message = string.Empty;
            if (etDocumentExtension.Document_Ext_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListDocumentExtensionBuisnessLogic(DocumentExtensionContentViewModel etDocumentExtensionContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveDocumentExtensionBuisnessLogic(DocumentExtensionViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Document Extension", NavigationUrl = Utilities.GetUrl("Index", "DocumentExtension", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<Document_Extension, DocumentExtensionViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Document_Extension_Result, DocumentExtensionGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<DocumentExtensionViewModel, Document_Extension>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<DocumentExtensionGridViewModel, USP_GET_Document_Extension_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
