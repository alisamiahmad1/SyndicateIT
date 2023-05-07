using AutoMapper;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.SetupManagement.Religion;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Religion
{
    public class ReligionDomainModel : DomainModelBase
    {
        public ReligionContentViewModel GetReligionContent()
        {
            ReligionContentViewModel model = new ReligionContentViewModel();
            model.Navigation = GetNavigationList("list", "Religion");
            model.Title = "Religion";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public ReligionViewModel GetReligionViewModel(String Religion_ID)
        {
            ReligionViewModel model = new ReligionViewModel();
            string title = string.Empty;
            if (Religion_ID != null && Religion_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etGender = db.RELIGIONs.Where(p => p.Religion_ID.ToString() == Religion_ID).FirstOrDefault();
                    model = Mapper.Map<RELIGION, ReligionViewModel>(etGender);
                }
                title = "Edit Religion";
            }
            else
            {
                title = "New Religion";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }

        public ReligionViewModel GetReligionViewModel(ReligionViewModel model)
        {
            string title = string.Empty;
            if (model.Religion_ID != null && model.Religion_ID > 0)
            {
                title = "Edit Religion";
            }
            else
            {
                title = "New Religion";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }

        public List<ReligionGridViewModel> GetListReligion(ReligionContentViewModel etReligionContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListReligionBuisnessLogic(etReligionContentViewModel);

            List<ReligionGridViewModel> lst = new List<ReligionGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etReligionContentViewModel.Religion_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Religion(
                    etReligionContentViewModel.Religion_ID,
                    etReligionContentViewModel.Religion_Name,
                    etReligionContentViewModel.Religion_Desc,
                    etReligionContentViewModel.LANGUAGE_ID,
                    etReligionContentViewModel.IS_ACTIVE,
                    etReligionContentViewModel.START_ROW,
                    etReligionContentViewModel.END_ROW,
                    etReligionContentViewModel.TOP,
                    etReligionContentViewModel.ENTRY_DATE,
                    etReligionContentViewModel.ENTRY_USER_ID,
                    etReligionContentViewModel.MODIFICATION_USER_ID,
                    etReligionContentViewModel.Modification_Date
                    ).ToList();

                List<ReligionGridViewModel> Genders = Mapper.Map<List<USP_GET_Religion_Result>, List<ReligionGridViewModel>>(listdb);

                lst = Genders;
            };

            return lst;

        }

        public String[] DeleteReligion(ReligionViewModel etReligionViewModel)
            {
                String[] message = new string[2];
                message[1] = DeleteReligionBuisnessLogic(etReligionViewModel);
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
                            var etReligionByLanguage = db.RELIGION_BY_LANGUAGE.Where(p => p.Religion_ID == etReligionViewModel.Religion_ID).ToList();
                            if (etReligionByLanguage.Count > 0)
                            {
                                db.RELIGION_BY_LANGUAGE.RemoveRange(etReligionByLanguage);
                            }
                            db.SaveChanges();
                            var etReligion = db.RELIGIONs.Where(p => p.Religion_ID == etReligionViewModel.Religion_ID).FirstOrDefault();
                            if (etReligion != null)
                            {
                                db.RELIGIONs.Remove(etReligion);
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
    
        public string[] SaveReligion(ReligionViewModel etReligionViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveReligionBuisnessLogic(etReligionViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            RELIGION etReligion = new RELIGION();
            RELIGION_BY_LANGUAGE etReligionByLanguageDb = new RELIGION_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etReligionViewModel.Religion_ID != 0 && etReligionViewModel.Religion_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etReligionViewModel.ENTRY_USER_ID = etReligionViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etreligiondb = db.RELIGIONs.Where(p => p.Religion_ID.ToString() == etReligionViewModel.Religion_ID.ToString()).FirstOrDefault();
                            etReligion = Mapper.Map<ReligionViewModel, RELIGION>(etReligionViewModel);

                            if (etReligion != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etReligionViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etReligionViewModel.Religion_Name != null)
                                {
                                    count++;
                                    properties[count] = "Religion_Name";
                                }
                                if (etReligionViewModel.Religion_Desc != null)
                                {
                                    count++;
                                    properties[count] = "Religion_Desc";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etreligiondb, etReligion, true,
                                      properties
                                     );
                                etreligiondb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etReligionByLanguageDb = db.RELIGION_BY_LANGUAGE.Where(p => p.Religion_ID == etReligion.Religion_ID && p.Language_ID == 1).FirstOrDefault();
                                RELIGION_BY_LANGUAGE etReligionByLanguage = new RELIGION_BY_LANGUAGE();
                                etReligionByLanguage = etReligionByLanguageDb;
                                etReligionByLanguage.Religion_Name = etReligion.Religion_Name;
                                etReligionByLanguage.Religion_Desc = etReligion.Religion_Desc;
                                etReligionByLanguage.IS_ACTIVE = etReligion.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etReligionByLanguageDb, etReligionByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etReligion = Mapper.Map<ReligionViewModel, RELIGION>(etReligionViewModel);

                            etReligion.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etReligion.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.RELIGIONs.Add(etReligion);
                            db.SaveChanges();
                            RELIGION_BY_LANGUAGE erTranslation = new RELIGION_BY_LANGUAGE();
                            erTranslation.Religion_Name = etReligion.Religion_Name;
                            erTranslation.Religion_Desc = etReligion.Religion_Desc;
                            erTranslation.Religion_ID = etReligion.Religion_ID;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etReligion.IS_ACTIVE;
                            db.RELIGION_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etReligion.Religion_ID.ToString();
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

        public string DeleteReligionBuisnessLogic(ReligionViewModel etGender)
        {
            string message = string.Empty;
            if (etGender.Religion_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListReligionBuisnessLogic(ReligionContentViewModel etReligionContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveReligionBuisnessLogic(ReligionViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Religion", NavigationUrl = Utilities.GetUrl("Index", "Religion", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<RELIGION, ReligionViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Religion_Result, ReligionGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<ReligionViewModel, RELIGION>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<ReligionGridViewModel, USP_GET_Religion_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion
}
}
