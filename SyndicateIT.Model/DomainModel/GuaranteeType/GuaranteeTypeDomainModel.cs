using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.GuarantyType;
using SyndicateIT.Model.ViewModel.InsuranceType;
using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.UtilityModel.Session;

namespace SyndicateIT.Model.DomainModel.GuarantyType
{
  public   class GuaranteeTypeDomainModel : DomainModelBase
    {
        public GuaranteeTypeContentViewModel GetGenderContent()
        {
            GuaranteeTypeContentViewModel model = new GuaranteeTypeContentViewModel();
            model.Navigation = GetNavigationList("list", "Gender");
            model.Title = "GuaranteeType";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }
        public List<NavigationViewModel> GetNavigationList(string type, string title)
        {
            var model = new List<NavigationViewModel>();

            if (type == "list")
            {
                model.Add(new NavigationViewModel() { NavigationName = "GuaranteeType", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
            else if (type == "update")
            {
                model.Add(new NavigationViewModel() { NavigationName = "GuaranteeType", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = "GuaranteeType", NavigationUrl = Utilities.GetUrl("GuaranteeTypes", "GuaranteeType", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }
        public string[] SaveGuaranteeType(GuaranteeTypeViewModel etGuaranteeTypeViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveGuaranteeTypeBuisnessLogic(etGuaranteeTypeViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            TypeGuarantee etDamage = new TypeGuarantee();
            TypeGuaranteeByLanguage etDamageByLanguageDb = new TypeGuaranteeByLanguage();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {


                        if (etGuaranteeTypeViewModel.Type_Guarantee_Id.ToString() != "0" && etGuaranteeTypeViewModel.Type_Guarantee_Id.ToString() != string.Empty)
                        {   // Update Table and Default row in Table_By_language
                            //  if (SessionContent.Container.Login.UserID != null)
                            //  {
                            // Set the entry user id and the modification user id.

                            //   etCountryViewModel.ENTRY_USER_ID = etCountryViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            //   }

                            var etDamageGurantordb = db.TypeGuarantees.Where(p => p.Type_Guarantee_Id.ToString() == etGuaranteeTypeViewModel.Type_Guarantee_Id.ToString()).FirstOrDefault();

                            etDamage = Mapper.Map<GuaranteeTypeViewModel, TypeGuarantee>(etGuaranteeTypeViewModel);

                            if (etDamageGurantordb != null)
                            {
                                /* if (SessionContent.Container.Login.UserID != null)
                                 {
                                     etCountryViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                 }*/
                                string[] properties = new string[8];

                                int count = -1;

                                if (etGuaranteeTypeViewModel.Type_Guarantee_Name != null)
                                {
                                    count++;
                                    properties[count] = "Type_Guarantee_Name";
                                }

                                if (etGuaranteeTypeViewModel.Is_Active != null)
                                {
                                    count++;
                                    properties[count] = "Is_Active";
                                }

                                count++;
                                properties[count] = "Modification_User_Id";
                                count++;
                                properties[count] = "Modification_User_Date";


                                UtilityComponent.Utilities.MergeObject(etDamageGurantordb, etGuaranteeTypeViewModel, true, properties);
                                etDamageGurantordb.Modification_Users_Date = DateTime.Now;

                                etDamageByLanguageDb = db.TypeGuaranteeByLanguages.Where(p => p.Type_Guarantee_Id == etDamageGurantordb.Type_Guarantee_Id && p.Languages_Id == 1).FirstOrDefault();
                                TypeGuaranteeByLanguage etDamageByLanguage = new TypeGuaranteeByLanguage();

                                etDamageByLanguage = etDamageByLanguageDb;
                                etDamageByLanguage.Type_Guarantee_By_Language_Name = etDamage.Type_Guarantee_Name;


                                UtilityComponent.Utilities.MergeObject(etDamageByLanguageDb, etDamageByLanguage, true,
                                                                  properties);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            etDamage = Mapper.Map<GuaranteeTypeViewModel, TypeGuarantee>(etGuaranteeTypeViewModel);

                            etDamage.Entry_Users_Date = DateTime.Now;

                            /*     if (SessionContent.Container.Login.UserID != null)
                                 {
                                     etCountry.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                 }*/

                            db.TypeGuarantees.Add(etDamage);
                            db.SaveChanges();

                            TypeGuaranteeByLanguage erTranslation = new TypeGuaranteeByLanguage();

                            erTranslation.Type_Guarantee_By_Language_Name = etDamage.Type_Guarantee_Name;
                            erTranslation.Type_Guarantee_Id = etDamage.Type_Guarantee_Id;
                            erTranslation.Languages_Id = (int)Languages.English;
                            //  erTranslation.isActive = etDamage.IsActive;

                            erTranslation.Entry_Users_Date = DateTime.Now;
                            //   erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.TypeGuaranteeByLanguages.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etDamage.Type_Guarantee_Id.ToString();
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


        public string SaveGuaranteeTypeBuisnessLogic(GuaranteeTypeViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        public String[] RemoveGuaranteeType(GuaranteeTypeViewModel etGuaranteeTypeViewModel)
        {
            String[] messages = new string[2];
            messages[1] = deleteGuaranteeTypeBusinessLogic(etGuaranteeTypeViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "Error";
                return messages;
            }
            using (TransactionScope scope = new TransactionScope())
            {
                using (var db = new SyndicatDataEntities())
                {
                    try
                    {
                        var etDamageByLanguage = db.TypeGuaranteeByLanguages.Where(p => p.Type_Guarantee_Id == etGuaranteeTypeViewModel.Type_Guarantee_Id).ToList();

                        if (etDamageByLanguage.Count > 0)
                        {
                            db.TypeGuaranteeByLanguages.RemoveRange(etDamageByLanguage);
                        }
                        db.SaveChanges();

                        var etdamage = db.TypeGuarantees.Where(p => p.Type_Guarantee_Id == etGuaranteeTypeViewModel.Type_Guarantee_Id).FirstOrDefault();
                        if (etdamage != null)
                        {
                            db.TypeGuarantees.Remove(etdamage);
                            db.SaveChanges();
                            messages[0] = "s";
                            messages[1] = etdamage.Type_Guarantee_Id.ToString();
                        }

                        else
                        {
                            messages[0] = "e";
                            messages[1] = "already Deleted";
                        }


                        scope.Complete();

                    }
                    catch (Exception ex)
                    {
                        messages[0] = "e";
                        messages[1] = ex.InnerException.InnerException.Message;

                        return messages;

                    }
                }
            }
            return messages;
        }

        public string deleteGuaranteeTypeBusinessLogic(GuaranteeTypeViewModel etGuaranteeTypeViewModel)
        {
            string message = string.Empty;


            return message;
        }

        public GuaranteeTypeContentViewModel GetIGuaranteeTypeContent()
        {
            GuaranteeTypeContentViewModel etDamageCategoryContentViewModel = new GuaranteeTypeContentViewModel();
            return etDamageCategoryContentViewModel;
        }


        public GuaranteeTypeViewModel GetGuaranteeTypeViewModel(string Type_Insurance_Id)
        {
            GuaranteeTypeViewModel model = new GuaranteeTypeViewModel();
            if (Type_Insurance_Id != "0" && Type_Insurance_Id != string.Empty)
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDamage = db.TypeGuarantees.Where(p => p.Type_Guarantee_Id.ToString() == Type_Insurance_Id.ToString()).FirstOrDefault();


                    model = Mapper.Map<TypeGuarantee, GuaranteeTypeViewModel>(etDamage);
                }

            }
            return model;
        }


        public List<GuaranteeTypeGridViewModel> GetListGuaranteeType(GuaranteeTypeContentViewModel etGuaranteeTypeContentViewModel)
        {
            List<GuaranteeTypeGridViewModel> lst = new List<GuaranteeTypeGridViewModel>();

            String[] messages = new String[2];
            messages[1] = GetListGuaranteeTypeBuisnessLogic(etGuaranteeTypeContentViewModel);

            using (var db = new SyndicatDataEntities())
            {
                var listdb = db.USP_Get_TypeGuarantee(
              etGuaranteeTypeContentViewModel.Type_Guarantee_Id,
              etGuaranteeTypeContentViewModel.Type_Guarantee_Name,
              etGuaranteeTypeContentViewModel.Is_Active,
              etGuaranteeTypeContentViewModel.Entry_User_Id,
              etGuaranteeTypeContentViewModel.Entry_User_Date,
              etGuaranteeTypeContentViewModel.Modification_User_Id,
              etGuaranteeTypeContentViewModel.Modification_User_Date,
              etGuaranteeTypeContentViewModel.Languages_Id

                  ).ToList();

                List<GuaranteeTypeGridViewModel> Damages = Mapper.Map<List<USP_Get_TypeGuarantee_Result>, List<GuaranteeTypeGridViewModel>>(listdb);
                lst = Damages;

            };
            return lst;

        }


        public string GetListGuaranteeTypeBuisnessLogic(GuaranteeTypeContentViewModel etGuaranteeTypeContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods
        public void InitializeMapper()

        {
            #region Database To View

            Mapper.CreateMap<TypeGuarantee, GuaranteeTypeViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_Get_TypeGuarantee_Result, GuaranteeTypeGridViewModel>()
                  .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<GuaranteeTypeViewModel, TypeGuarantee>()
           .IgnoreAllNonExisting();

            Mapper.CreateMap<GuaranteeTypeGridViewModel, USP_Get_TypeGuarantee_Result>()
                .IgnoreAllNonExisting();

            #endregion


        }
        #endregion



    }
}
