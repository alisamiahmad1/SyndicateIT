using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.SetupManagement.Country;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Country
{
    public class CountryDomainModel : DomainModelBase
    {
        public CountryContentViewModel GetCountryContent()
        {
            CountryContentViewModel model = new CountryContentViewModel();
            model.Navigation = GetNavigationList("list", "Country");
            model.Title = "Country";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public CountryViewModel GetCountryViewModel(String Country_ID)
        {
            CountryViewModel model = new CountryViewModel();
            string title = string.Empty;
            if (Country_ID != null && Country_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etCountry = db.COUNTRies.Where(p => p.COUNTRY_ID.ToString() == Country_ID).FirstOrDefault();
                    model = Mapper.Map<COUNTRY, CountryViewModel>(etCountry);
                }
                title = "Edit Country";
            }
            else
            {
                title = "New Country";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public CountryViewModel GetCountryViewModel(CountryViewModel model)
        {
            string title = string.Empty;
            if (model.COUNTRY_ID != null && model.COUNTRY_ID > 0)
            {
                title = "Edit Country";
            }
            else
            {
                title = "New Country";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


            public List<CountryGridViewModel> GetListCountry(CountryContentViewModel etCountryContentViewModel, bool isAPI = true)
            {
                string[] messages = new string[2];
                messages[1] = GetListCountryBuisnessLogic(etCountryContentViewModel);

                List<CountryGridViewModel> lst = new List<CountryGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etCountryContentViewModel.Country_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                    var listdb = db.USP_GET_Country(
                       etCountryContentViewModel.Country_ID,
                        etCountryContentViewModel.Country_NAME,
                        etCountryContentViewModel.LANGUAGE_ID,
                        etCountryContentViewModel.IS_ACTIVE,
                        etCountryContentViewModel.START_ROW,
                        etCountryContentViewModel.END_ROW,
                        etCountryContentViewModel.TOP,
                        etCountryContentViewModel.ENTRY_DATE,
                        etCountryContentViewModel.ENTRY_USER_ID,
                        etCountryContentViewModel.MODIFICATION_USER_ID,
                        etCountryContentViewModel.Modification_Date
                        ).ToList();

                    List<CountryGridViewModel> Countrys = Mapper.Map<List<USP_GET_Country_Result>, List<CountryGridViewModel>>(listdb);

                    lst = Countrys;
                };

                return lst;

            }

        public String[] DeleteCountry(CountryViewModel etCountryViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteCountryBuisnessLogic(etCountryViewModel);
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
                        var etCountryByLanguage = db.COUNTRY_BY_LANGUAGE.Where(p => p.COUNTRY_ID == etCountryViewModel.COUNTRY_ID).ToList();
                        if (etCountryByLanguage.Count > 0)
                        {
                            db.COUNTRY_BY_LANGUAGE.RemoveRange(etCountryByLanguage);
                        }
                        db.SaveChanges();
                        var etCountry = db.COUNTRies.Where(p => p.COUNTRY_ID == etCountryViewModel.COUNTRY_ID).FirstOrDefault();
                        if (etCountry != null)
                        {
                            db.COUNTRies.Remove(etCountry);
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

        public string[] SaveCountry(CountryViewModel etCountryViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveCountryBuisnessLogic(etCountryViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            COUNTRY etCountry = new COUNTRY();
            COUNTRY_BY_LANGUAGE etCountryByLanguageDb = new COUNTRY_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etCountryViewModel.COUNTRY_ID != 0 && etCountryViewModel.COUNTRY_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etCountryViewModel.ENTRY_USER_ID = etCountryViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etCountrydb = db.COUNTRies.Where(p => p.COUNTRY_ID.ToString() == etCountryViewModel.COUNTRY_ID.ToString()).FirstOrDefault();
                            etCountry = Mapper.Map<CountryViewModel, COUNTRY>(etCountryViewModel);

                            if (etCountry != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etCountryViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etCountryViewModel.COUNTRY_NAME != null)
                                {
                                    count++;
                                    properties[count] = "COUNTRY_NAME";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etCountrydb, etCountry, true,
                                      properties
                                     );
                                etCountrydb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etCountryByLanguageDb = db.COUNTRY_BY_LANGUAGE.Where(p => p.COUNTRY_ID == etCountry.COUNTRY_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                COUNTRY_BY_LANGUAGE etCountryByLanguage = new COUNTRY_BY_LANGUAGE();
                                etCountryByLanguage = etCountryByLanguageDb;
                                etCountryByLanguage.COUNTRY_NAME = etCountry.COUNTRY_NAME;
                                etCountryByLanguage.IS_ACTIVE = etCountry.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etCountryByLanguageDb, etCountryByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etCountry = Mapper.Map<CountryViewModel, COUNTRY>(etCountryViewModel);

                            etCountry.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etCountry.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.COUNTRies.Add(etCountry);
                            db.SaveChanges();
                            COUNTRY_BY_LANGUAGE erTranslation = new COUNTRY_BY_LANGUAGE();
                            erTranslation.COUNTRY_NAME = etCountry.COUNTRY_NAME;
                            erTranslation.COUNTRY_ID = etCountry.COUNTRY_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etCountry.IS_ACTIVE;
                            db.COUNTRY_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etCountry.COUNTRY_ID.ToString();
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

        public string DeleteCountryBuisnessLogic(CountryViewModel etCountry)
        {
            string message = string.Empty;
            if (etCountry.COUNTRY_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListCountryBuisnessLogic(CountryContentViewModel etCountryContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveCountryBuisnessLogic(CountryViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Country", NavigationUrl = Utilities.GetUrl("index", "Country", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<COUNTRY, CountryViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Country_Result, CountryGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<CountryViewModel, COUNTRY>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<CountryGridViewModel, USP_GET_Country_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }

}
