using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.InsuranceType;
using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.DomainModel.InsuranceType
{
 public   class InsuranceTypeDomainModel : DomainModelBase
    {

        public string[] SaveInsuranceType(InsuranceTypeViewModel etInsuranceTypeViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveInsuranceTypeBuisnessLogic(etInsuranceTypeViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            TypeInsurance etDamage = new TypeInsurance();
            TypeInsuranceByLanguage etDamageByLanguageDb = new TypeInsuranceByLanguage();

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                
                            if (etInsuranceTypeViewModel.Type_Insurance_Id.ToString() != "0" && etInsuranceTypeViewModel.Type_Insurance_Id.ToString() != string.Empty)
                            {   // Update Table and Default row in Table_By_language
                            //  if (SessionContent.Container.Login.UserID != null)
                            //  {
                            // Set the entry user id and the modification user id.

                            //   etCountryViewModel.ENTRY_USER_ID = etCountryViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            //   }

                            var etDamageGurantordb = db.TypeInsurances.Where(p => p.Type_Insurance_Id.ToString() == etInsuranceTypeViewModel.Type_Insurance_Id.ToString()).FirstOrDefault();

                            etDamage = Mapper.Map<InsuranceTypeViewModel, TypeInsurance>(etInsuranceTypeViewModel);

                            if (etDamageGurantordb != null)
                            {
                                /* if (SessionContent.Container.Login.UserID != null)
                                 {
                                     etCountryViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                 }*/
                                string[] properties = new string[8];

                                int count = -1;

                                if (etInsuranceTypeViewModel.Type_Insurance_Name != null)
                                {
                                    count++;
                                    properties[count] = "Type_Insurance_Name";
                                }

                                if (etInsuranceTypeViewModel.Is_Active != null)
                                {
                                    count++;
                                    properties[count] = "Is_Active";
                                }

                                count++;
                                properties[count] = "Modification_User_Id";
                                count++;
                                properties[count] = "Modification_User_Date";


                                UtilityComponent.Utilities.MergeObject(etDamageGurantordb, etInsuranceTypeViewModel, true, properties );
                                etDamageGurantordb.Modification_Users_Date = DateTime.Now;

                                etDamageByLanguageDb = db.TypeInsuranceByLanguages.Where(p => p.Type_Insurance_Id == etDamageGurantordb.Type_Insurance_Id && p.Languages_Id == 1).FirstOrDefault();
                                TypeInsuranceByLanguage etDamageByLanguage = new TypeInsuranceByLanguage();

                                etDamageByLanguage = etDamageByLanguageDb;
                                etDamageByLanguage.Type_Insurance_By_Language_Name = etDamage.Type_Insurance_Name;
                             

                                UtilityComponent.Utilities.MergeObject(etDamageByLanguageDb, etDamageByLanguage, true,
                                                                  properties
                                                                  );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            etDamage = Mapper.Map<InsuranceTypeViewModel, TypeInsurance>(etInsuranceTypeViewModel);

                            etDamage.Entry_Users_Date = DateTime.Now;

                            /*     if (SessionContent.Container.Login.UserID != null)
                                 {
                                     etCountry.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                 }*/

                            db.TypeInsurances.Add(etDamage);
                            db.SaveChanges();

                            TypeInsuranceByLanguage erTranslation = new TypeInsuranceByLanguage();

                            erTranslation.Type_Insurance_By_Language_Name = etDamage.Type_Insurance_Name;
                            erTranslation.Type_Insurance_Id = etDamage.Type_Insurance_Id;
                            erTranslation.Languages_Id = (int)Languages.English;
                            //  erTranslation.isActive = etDamage.IsActive;

                            erTranslation.Entry_Users_Date = DateTime.Now;
                            //   erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.TypeInsuranceByLanguages.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etDamage.Type_Insurance_Id.ToString();
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


        public string SaveInsuranceTypeBuisnessLogic(InsuranceTypeViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        public String[] RemoveInsuranceType(InsuranceTypeViewModel etInsuranceTypeViewModel)
        {
            String[] messages = new string[2];
            messages[1] = deleteInsuranceTypeBusinessLogic(etInsuranceTypeViewModel);
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
                        var etDamageByLanguage = db.TypeInsuranceByLanguages.Where(p => p.Type_Insurance_Id == etInsuranceTypeViewModel.Type_Insurance_Id).ToList();

                        if (etDamageByLanguage.Count > 0)
                        {
                            db.TypeInsuranceByLanguages.RemoveRange(etDamageByLanguage);
                        }
                        db.SaveChanges();

                        var etdamage = db.TypeInsurances.Where(p => p.Type_Insurance_Id == etInsuranceTypeViewModel.Type_Insurance_Id).FirstOrDefault();
                        if (etdamage != null)
                        {
                            db.TypeInsurances.Remove(etdamage);
                            db.SaveChanges();
                            messages[0] = "s";
                            messages[1] = etdamage.Type_Insurance_Id.ToString();
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

        public string deleteInsuranceTypeBusinessLogic(InsuranceTypeViewModel etInsuranceTypeViewModel)
        {
            string message = string.Empty;


            return message;
        }
       
        public InsuranceTypeContentViewModel GetInsuranceTypeContent()
        {
            InsuranceTypeContentViewModel etDamageCategoryContentViewModel = new InsuranceTypeContentViewModel();
            return etDamageCategoryContentViewModel;
        }

       
        public InsuranceTypeViewModel GetInsuranceTypeViewModel(string  Type_Insurance_Id)
        {
            InsuranceTypeViewModel model = new InsuranceTypeViewModel();
            if (Type_Insurance_Id != "0" && Type_Insurance_Id != string .Empty )
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDamage = db.TypeInsurances.Where(p => p.Type_Insurance_Id.ToString() == Type_Insurance_Id.ToString ()).FirstOrDefault();


                    model = Mapper.Map<TypeInsurance, InsuranceTypeViewModel>(etDamage);
                }

            }
            return model;
        }


        public List<InsuranceTypeGridViewModel> GetListInsuranceType(InsuranceTypeContentViewModel etInsuranceTypeContentViewModel)
        {
            List<InsuranceTypeGridViewModel> lst = new List<InsuranceTypeGridViewModel>();

            String[] messages = new String[2];
            messages[1] = GetListInsuranceTypeBuisnessLogic(etInsuranceTypeContentViewModel);

            using (var db = new SyndicatDataEntities())
            {
                var listdb = db.USP_Get_TypeInsurance(
              etInsuranceTypeContentViewModel.Type_Insurance_Id,
              etInsuranceTypeContentViewModel.Type_Insurance_Name,
              etInsuranceTypeContentViewModel.Is_Active,
              etInsuranceTypeContentViewModel.Entry_User_Id,
              etInsuranceTypeContentViewModel.Entry_User_Date,
              etInsuranceTypeContentViewModel.Modification_User_Id,
              etInsuranceTypeContentViewModel.Modification_User_Date,
              etInsuranceTypeContentViewModel.Languages_Id

                  ).ToList();

                List<InsuranceTypeGridViewModel> Damages = Mapper.Map<List<USP_Get_TypeInsurance_Result>, List<InsuranceTypeGridViewModel>>(listdb);
                lst = Damages;

            };
            return lst;

        }


        public string GetListInsuranceTypeBuisnessLogic(InsuranceTypeContentViewModel etInsuranceTypeContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods
        public void InitializeMapper()

        {
            #region Database To View

       Mapper.CreateMap<TypeInsurance, InsuranceTypeViewModel>()
             .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_Get_TypeInsurance_Result, InsuranceTypeGridViewModel>()
                  .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<InsuranceTypeViewModel, TypeInsurance>()
           .IgnoreAllNonExisting();

            Mapper.CreateMap<InsuranceTypeGridViewModel, USP_Get_TypeInsurance_Result>()
                .IgnoreAllNonExisting();

            #endregion


        }
        #endregion
    }
}
