using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.SetupManagement.Nationality;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Nationality
{
    public class NationalityDomainModel : DomainModelBase
    {
        public NationalityContentViewModel GetNationalityContent()
        {
            NationalityContentViewModel model = new NationalityContentViewModel();
            model.Navigation = GetNavigationList("list", "Nationality");
            model.Title = "Nationality";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public NationalityViewModel GetNationalityViewModel(String Nationality_ID)
        {
            NationalityViewModel model = new NationalityViewModel();
            string title = string.Empty;
            if (Nationality_ID != null && Nationality_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etNationality = db.TBL_Nationality.Where(p => p.Nationality_ID.ToString() == Nationality_ID).FirstOrDefault();
                    model = Mapper.Map<TBL_Nationality, NationalityViewModel>(etNationality);
                }
                title = "Edit Nationality";
            }
            else
            {
                title = "New Nationality";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public NationalityViewModel GetNationalityViewModel(NationalityViewModel model)
        {
            string title = string.Empty;
            if (model.Nationality_ID != null && model.Nationality_ID > 0)
            {
                title = "Edit Nationality";
            }
            else
            {
                title = "New Nationality";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


            public List<NationalityGridViewModel> GetListNationality(NationalityContentViewModel etNationalityContentViewModel, bool isAPI = true)
            {
                string[] messages = new string[2];
                messages[1] = GetListNationalityBuisnessLogic(etNationalityContentViewModel);

                List<NationalityGridViewModel> lst = new List<NationalityGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etNationalityContentViewModel.NATIONALITY_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                    var listdb = db.USP_GET_Nationality(
                       etNationalityContentViewModel.NATIONALITY_ID,
                        etNationalityContentViewModel.Nationality_Title,
                        "",
                        etNationalityContentViewModel.LANGUAGE_ID,
                        etNationalityContentViewModel.IS_ACTIVE,
                        etNationalityContentViewModel.START_ROW,
                        etNationalityContentViewModel.END_ROW,
                        etNationalityContentViewModel.TOP,
                        etNationalityContentViewModel.ENTRY_DATE,
                        etNationalityContentViewModel.ENTRY_USER_ID,
                        etNationalityContentViewModel.MODIFICATION_USER_ID,
                        etNationalityContentViewModel.MODIFICATION_DATE
                        ).ToList();

                    List<NationalityGridViewModel> Nationalitys = Mapper.Map<List<USP_GET_Nationality_Result>, List<NationalityGridViewModel>>(listdb);

                    lst = Nationalitys;
                };

                return lst;

            }

        public String[] DeleteNationality(NationalityViewModel etNationalityViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteNationalityBuisnessLogic(etNationalityViewModel);
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
                        var etNationalityByLanguage = db.TBL_Nationality_By_Language.Where(p => p.Nationality_ID == etNationalityViewModel.Nationality_ID).ToList();
                        if (etNationalityByLanguage.Count > 0)
                        {
                            db.TBL_Nationality_By_Language.RemoveRange(etNationalityByLanguage);
                        }
                        db.SaveChanges();
                        var etNationality = db.TBL_Nationality.Where(p => p.Nationality_ID == etNationalityViewModel.Nationality_ID).FirstOrDefault();
                        if (etNationality != null)
                        {
                            db.TBL_Nationality.Remove(etNationality);
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

        public string[] SaveNationality(NationalityViewModel etNationalityViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveNationalityBuisnessLogic(etNationalityViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


           TBL_Nationality etNationality = new TBL_Nationality();
            TBL_Nationality_By_Language etNationalityByLanguageDb = new TBL_Nationality_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etNationalityViewModel.Nationality_ID != 0 && etNationalityViewModel.Nationality_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etNationalityViewModel.ENTRY_USER_ID = etNationalityViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etNationalitydb = db.TBL_Nationality.Where(p => p.Nationality_ID.ToString() == etNationalityViewModel.Nationality_ID.ToString()).FirstOrDefault();
                            etNationality = Mapper.Map<NationalityViewModel, TBL_Nationality>(etNationalityViewModel);

                            if (etNationalitydb != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etNationalityViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etNationalityViewModel.Nationality_Title != null)
                                {
                                    count++;
                                    properties[count] = "NATIONALITY_NAME";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etNationalitydb, etNationality, true,
                                      properties
                                     );
                                etNationalitydb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etNationalityByLanguageDb = db.TBL_Nationality_By_Language.Where(p => p.Nationality_ID == etNationality.Nationality_ID && p.Language_ID == 1).FirstOrDefault();
                                TBL_Nationality_By_Language etNationalityByLanguage = new TBL_Nationality_By_Language();
                                etNationalityByLanguage = etNationalityByLanguageDb;
                                etNationalityByLanguage.Nationality_Title = etNationality.Nationality_Title;
                                etNationalityByLanguage.IS_ACTIVE = etNationality.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etNationalityByLanguageDb, etNationalityByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etNationality = Mapper.Map<NationalityViewModel, TBL_Nationality>(etNationalityViewModel);

                            etNationality.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etNationality.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.TBL_Nationality.Add(etNationality);
                            db.SaveChanges();
                            TBL_Nationality_By_Language erTranslation = new TBL_Nationality_By_Language();
                            erTranslation.Nationality_Title = etNationality.Nationality_Title;
                            erTranslation.Nationality_ID = etNationality.Nationality_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etNationality.IS_ACTIVE;
                            db.TBL_Nationality_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etNationality.Nationality_ID.ToString();
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

        public string DeleteNationalityBuisnessLogic(NationalityViewModel etNationality)
        {
            string message = string.Empty;
            if (etNationality.Nationality_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListNationalityBuisnessLogic(NationalityContentViewModel etNationalityContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveNationalityBuisnessLogic(NationalityViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Nationality", NavigationUrl = Utilities.GetUrl("Index", "Nationality", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<TBL_Nationality, NationalityViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Nationality_Result, NationalityGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<NationalityViewModel, TBL_Nationality > ()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<NationalityGridViewModel, USP_GET_Nationality_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }

}
