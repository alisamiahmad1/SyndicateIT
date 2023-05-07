using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.SetupManagement.Gender;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Gender
{
    public class GenderDomainModel : DomainModelBase
    {
       public GenderContentViewModel GetGenderContent()
        {
            GenderContentViewModel model = new GenderContentViewModel();
            model.Navigation = GetNavigationList("list", "Gender");
            model.Title = "Gender";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

       public GenderViewModel GetGenderViewModel(String Gender_ID)
        {
            GenderViewModel model = new GenderViewModel();
            string title = string.Empty;
            if (Gender_ID != null && Gender_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etGender = db.GENDERs.Where(p => p.GENDER_ID.ToString() == Gender_ID).FirstOrDefault();
                    model = Mapper.Map<GENDER, GenderViewModel>(etGender);
                }
                title = "Edit Gender";
            }
            else
            {
                title = "New Gender";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public GenderViewModel GetGenderViewModel(GenderViewModel model)
        {
            string title = string.Empty;
            if (model.GENDER_ID != null && model.GENDER_ID >0)
            {               
                title = "Edit Gender";
            }
            else
            {
                title = "New Gender";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<GenderGridViewModel> GetListGender(GenderContentViewModel etGenderContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListGenderBuisnessLogic(etGenderContentViewModel);

            List<GenderGridViewModel> lst = new List<GenderGridViewModel>();
        
            SyndicateIT.UtilityComponent.Encryption.Encrypt(etGenderContentViewModel.Gender_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Gender(
                   etGenderContentViewModel.Gender_ID,
                    etGenderContentViewModel.Gender_NAME,
                    etGenderContentViewModel.LANGUAGE_ID,
                    etGenderContentViewModel.IS_ACTIVE,
                    etGenderContentViewModel.START_ROW,
                    etGenderContentViewModel.END_ROW,
                    etGenderContentViewModel.TOP,
                    etGenderContentViewModel.ENTRY_DATE,
                    etGenderContentViewModel.ENTRY_USER_ID,
                    etGenderContentViewModel.MODIFICATION_USER_ID,
                    etGenderContentViewModel.Modification_Date
                    ).ToList();

                List<GenderGridViewModel> Genders = Mapper.Map<List<USP_GET_Gender_Result>, List<GenderGridViewModel>>(listdb);

                lst = Genders;
            };

            return lst;

        }

        public String[] DeleteGender(GenderViewModel etGenderViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteGenderBuisnessLogic(etGenderViewModel);
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
                        var etGenderByLanguage = db.GENDER_BY_LANGUAGE.Where(p => p.GENDER_ID == etGenderViewModel.GENDER_ID).ToList();
                        if (etGenderByLanguage.Count > 0)
                        {
                            db.GENDER_BY_LANGUAGE.RemoveRange(etGenderByLanguage);
                        }
                        db.SaveChanges();
                        var etGender = db.GENDERs.Where(p => p.GENDER_ID == etGenderViewModel.GENDER_ID).FirstOrDefault();
                        if (etGender != null)
                        {
                            db.GENDERs.Remove(etGender);
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

        public string[] SaveGender(GenderViewModel etGenderViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveGenderBuisnessLogic(etGenderViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            GENDER etGender = new GENDER();
            GENDER_BY_LANGUAGE etGenderByLanguageDb = new GENDER_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etGenderViewModel.GENDER_ID != 0 && etGenderViewModel.GENDER_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString ()!= string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etGenderViewModel.ENTRY_USER_ID = etGenderViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etGenderdb = db.GENDERs.Where(p => p.GENDER_ID.ToString() == etGenderViewModel.GENDER_ID.ToString()).FirstOrDefault();
                            etGender = Mapper.Map<GenderViewModel, GENDER>(etGenderViewModel);

                            if (etGender != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                        etGenderViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                    }
                                
                                string[] properties = new string[8];

                                int count = -1;
                                
                                if (etGenderViewModel.GENDER_NAME != null)
                                {
                                    count++;
                                    properties[count] = "GENDER_NAME";
                                }
                                
                                    count++;
                                    properties[count] = "IS_ACTIVE";
                               
                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etGenderdb, etGender, true,
                                      properties
                                     );
                                etGenderdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etGenderByLanguageDb = db.GENDER_BY_LANGUAGE.Where(p => p.GENDER_ID == etGender.GENDER_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                GENDER_BY_LANGUAGE etGenderByLanguage = new GENDER_BY_LANGUAGE();
                                etGenderByLanguage = etGenderByLanguageDb;
                                etGenderByLanguage.GENDER_NAME = etGender.GENDER_NAME;
                                etGenderByLanguage.IS_ACTIVE = etGender.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etGenderByLanguageDb, etGenderByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etGender = Mapper.Map<GenderViewModel, GENDER>(etGenderViewModel);

                            etGender.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                    etGender.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }
                            
                            db.GENDERs.Add(etGender);
                            db.SaveChanges();
                            GENDER_BY_LANGUAGE erTranslation = new GENDER_BY_LANGUAGE();
                            erTranslation.GENDER_NAME = etGender.GENDER_NAME;
                            erTranslation.GENDER_ID = etGender.GENDER_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etGender.IS_ACTIVE;
                            db.GENDER_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etGender.GENDER_ID.ToString();
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

        public string DeleteGenderBuisnessLogic(GenderViewModel etGender)
        {
            string message = string.Empty;
            if (etGender.GENDER_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListGenderBuisnessLogic(GenderContentViewModel etGenderContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveGenderBuisnessLogic(GenderViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Gender", NavigationUrl = Utilities.GetUrl("index", "Gender", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
           
            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<GENDER, GenderViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Gender_Result, GenderGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<GenderViewModel, GENDER>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<GenderGridViewModel, USP_GET_Gender_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }

}
