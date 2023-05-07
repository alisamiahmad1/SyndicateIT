using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.SetupManagement.Language;
using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Language
{
    public class LanguageDomainModel : DomainModelBase
    {
        public LanguageContentViewModel GetLanguageContent()
        {
            LanguageContentViewModel etLanguageContentViewModel = new LanguageContentViewModel();
            return etLanguageContentViewModel;
        }

        public LanguageViewModel GetLanguageViewModel(String Language_ID)
        {
            LanguageViewModel model = new LanguageViewModel();
            if (Language_ID != null && Language_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etLanguage = db.LANGUAGEs.Where(p => p.LANGUAGE_ID.ToString() == Language_ID).FirstOrDefault();
                    model = Mapper.Map<LANGUAGE, LanguageViewModel>(etLanguage);
                }

            }
            return model;
        }

        // Get List Function
        public string GetListLanguageBuisnessLogic(LanguageContentViewModel etLanguageContentViewModel)
        {
            string message = string.Empty;
            return message;
        }
        public List<LanguageGridViewModel> GetListLanguage(LanguageContentViewModel etLanguageContentViewModel, bool isAPI = true)
        {
            string[] message = new string[2];
            message[1] = GetListLanguageBuisnessLogic(etLanguageContentViewModel);
            List<LanguageGridViewModel> list = new List<LanguageGridViewModel>();

            using (var db = new SyndicatDataEntities())
            {
                var Listdb = db.USP_GET_Language(
                    etLanguageContentViewModel.Language_ID,
                    etLanguageContentViewModel.Language_Name,
                    etLanguageContentViewModel.Culture,
                    etLanguageContentViewModel.Code,
                    etLanguageContentViewModel.Direction,
                    etLanguageContentViewModel.IS_ACTIVE,
                    etLanguageContentViewModel.START_ROW,
                    etLanguageContentViewModel.END_ROW,
                    etLanguageContentViewModel.TOP,
                    etLanguageContentViewModel.ENTRY_DATE,
                    etLanguageContentViewModel.ENTRY_USER_ID,
                    etLanguageContentViewModel.MODIFICATION_USER_ID,
                    etLanguageContentViewModel.Modification_Date
                    ).ToList();
                List<LanguageGridViewModel> Language = Mapper.Map<List<USP_GET_Language_Result>, List<LanguageGridViewModel>>(Listdb);
                list = Language;
            };
            return list;
        }

        // DELETE Function
        public string DeleteLanguageBuisnessLogic(LanguageViewModel etLanguage)
        {
            string message = string.Empty;
            if (etLanguage.LANGUAGE_ID == -1)
            {
                message = "There is no record";
            }
            return message;
        }
        public string[] DeleteLanguage(LanguageViewModel etLanguageViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteLanguageBuisnessLogic(etLanguageViewModel);

            if (message[1] != null)
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

                        // Delete From By Language Table

                        var etLanguageByCountry = db.LANGUAGE_BY_COUNTRY.Where(p => p.LANGUAGE_ID == etLanguageViewModel.LANGUAGE_ID).ToList();
                        if (etLanguageByCountry.Count > 0)
                        {
                            db.LANGUAGE_BY_COUNTRY.RemoveRange(etLanguageByCountry);

                        }
                        db.SaveChanges();

                        // Delete From Original Table
                        var etLanguage = db.LANGUAGEs.Where(p => p.LANGUAGE_ID == etLanguageViewModel.LANGUAGE_ID).FirstOrDefault();
                        if (etLanguage != null)
                        {
                            db.LANGUAGEs.Remove(etLanguage);
                            db.SaveChanges();
                            message[0] = "s";
                            return message;
                        }
                        else
                        {
                            message[0] = "e";
                            return message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.ToString();
                return message;
            }

        }

        //  ADD Function
        public string SaveLanguageBuisnessLogic(LanguageViewModel model)
        {
            string message = string.Empty;
            return message;
        }
        public string[] SaveLanguage(LanguageViewModel etLanguageViewModel, bool isAPI = true)
        {
            string[] messages = new string[2];
            messages[1] = SaveLanguageBuisnessLogic(etLanguageViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            LANGUAGE etLanguage = new LANGUAGE();
            LANGUAGE_BY_COUNTRY etLanguageByCountryDb = new LANGUAGE_BY_COUNTRY();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etLanguageViewModel.LANGUAGE_ID != 0 && etLanguageViewModel.LANGUAGE_ID.ToString() != "")
                        {
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etLanguageViewModel.ENTRY_USER_ID= new Guid(SessionContent.Container.Login.UserID.ToString());
                                etLanguageViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etLanguagedb = db.LANGUAGEs.Where(p => p.LANGUAGE_ID.ToString() == etLanguageViewModel.LANGUAGE_ID.ToString()).FirstOrDefault();
                            etLanguage = Mapper.Map<LanguageViewModel, LANGUAGE>(etLanguageViewModel);

                            if (etLanguage != null)
                            {

                               

                                    if (SessionContent.Container.Login.UserID.ToString() != string .Empty)
                                    {
                                        etLanguageViewModel.MODIFICATION_USER_ID =new Guid(SessionContent.Container.Login.UserID.ToString());
                                    }
                                
                                string[] properties = new string[8];

                                int count = -1;




                                if (etLanguageViewModel.LANGUAGE_ID != -1)
                                {
                                    count++;
                                    properties[count] = "Language_ID";
                                }

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etLanguagedb, etLanguage, true,
                                      properties
                                     );
                                etLanguagedb.MODIFICATION_DATE = DateTime.Now;
                                LANGUAGE_BY_COUNTRY etLanguageByCountry = new LANGUAGE_BY_COUNTRY();
                                etLanguageByCountry = etLanguageByCountryDb;
                                etLanguageByCountry.LANGUAGE_BY_COUNTRY_ID= etLanguage.LANGUAGE_ID;
                                UtilityComponent.Utilities.MergeObject(etLanguageByCountryDb, etLanguageByCountry, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            // Need to add ENTRY_USER_ID 
                            //   etFitnessLevelViewModel.ENTRY_DATE = DateTime.Now;
                            // Need to add code to enter record in FitnessLevel_status with status id=1(pending)  
                            etLanguage = Mapper.Map<LanguageViewModel, LANGUAGE>(etLanguageViewModel);

                            etLanguage.ENTRY_DATE = DateTime.Now;
                            if (isAPI)
                            {

                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etLanguage.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }
                            }
                            db.LANGUAGEs.Add(etLanguage);
                            db.SaveChanges();
                            LANGUAGE_BY_COUNTRY erTranslation = new LANGUAGE_BY_COUNTRY();
                            erTranslation.LANGUAGE_BY_COUNTRY_ID = etLanguage.LANGUAGE_ID;
                            erTranslation.LANGUAGE_BY_COUNTRY_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID =new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.LANGUAGE_BY_COUNTRY.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etLanguage.LANGUAGE_ID.ToString();
                        //   savedSuccessfully = true;
                        scope.Complete();

                    }
                }
                catch (Exception ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.InnerException.Message;
                    //  message = ex.Message;
                    return messages;
                    //   savedSuccessfully = false;
                    //return false;
                }
            }
            return messages;

        }

        #region Private Methods
        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<LANGUAGE, LanguageViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Language_Result, LanguageGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<LanguageViewModel, LANGUAGE>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<LanguageGridViewModel, USP_GET_Language_Result>()
               .IgnoreAllNonExisting();

            #endregion

            base.InitializeMapper();
        }
        #endregion 

    }
}
