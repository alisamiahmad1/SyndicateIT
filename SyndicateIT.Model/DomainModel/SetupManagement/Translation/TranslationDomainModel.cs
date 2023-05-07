using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SyndicateIT.Model.ViewModel.SetupManagement.Translation;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Translation
{
    public class TranslationDomainModel : DomainModelBase
    {
        public TranslationContentViewModel GetTranslationContent()
        {
            TranslationContentViewModel model = new TranslationContentViewModel();
            model.TranslationList = GetTranslationList();
            return model;

        }
        public List<TranslationViewModel> GetTranslationList()
        {
            List<TranslationViewModel> lst = new List<TranslationViewModel>();
            return lst;
        }
        public List<TranslationViewModel> LoadTranslationList(string TableID, String TableName)
        {
            List<TranslationViewModel> lst = new List<TranslationViewModel>();
            string FieldName = "";
            string descField = "";
            List<SelectListItem> ItemList = new List<SelectListItem>();
            using (var db = new SyndicatDataEntities())
            {
                var listlan = db.LANGUAGEs.ToList();
                if (listlan != null && listlan.Count > 0)
                {
                    foreach (LANGUAGE lang in listlan)
                    {
                        FieldName = "";
                        descField = "";
                        int CountableTableLanguage;
                        var Translationid = lang.LANGUAGE_ID.ToString();
                        switch (TableName)
                        {

                         

                            case "Gender_BY_LANGUAGE":
                                int Gender_ID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.GENDER_BY_LANGUAGE.Where(p => p.GENDER_ID == Gender_ID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.GENDER_BY_LANGUAGE.Where(p => p.GENDER_ID == Gender_ID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.GENDER_NAME;
                                }
                                break;




                            case "COUNTRY_BY_LANGUAGE":
                                int COUNTRYID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.COUNTRY_BY_LANGUAGE.Where(p => p.COUNTRY_ID == COUNTRYID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.COUNTRY_BY_LANGUAGE.Where(p => p.COUNTRY_ID == COUNTRYID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.COUNTRY_NAME;
                                }
                                break;

                           

                            case "Branches_By_Language":
                                int BranchesID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Branches_By_Language.Where(p => p.BranchId == BranchesID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Branches_By_Language.Where(p => p.BranchId == BranchesID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Name;
                                }
                                break;

                            case "SHIFT_BY_LANGUAGE":
                                int ShiftID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.SHIFT_BY_LANGUAGE.Where(p => p.SHIFT_ID == ShiftID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.SHIFT_BY_LANGUAGE.Where(p => p.SHIFT_ID == ShiftID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.SHIFT_NAME;
                                }
                                break;

                            case "Relation_Types_By_Language":
                                int RelationTypeID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Relation_Types_By_Language.Where(p => p.Relation_Type_ID == RelationTypeID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Relation_Types_By_Language.Where(p => p.Relation_Type_ID == RelationTypeID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Relation_Type_Title;
                                }
                                break;

                            case "Phone_Type_BY_LANGUAGE":
                                int PhoneTypeID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Phone_Type_BY_LANGUAGE.Where(p => p.Phone_Type_ID == PhoneTypeID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Phone_Type_BY_LANGUAGE.Where(p => p.Phone_Type_ID == PhoneTypeID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Phone_Type_NAME;
                                }
                                break;

                            case "NATIONALITY_BY_LANGUAGE":
                                int NATIONALITYID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.TBL_Nationality_By_Language.Where(p => p.Nationality_ID == NATIONALITYID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.TBL_Nationality_By_Language.Where(p => p.Nationality_ID == NATIONALITYID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Nationality_Title;
                                }
                                break;

                            case "JOB_BY_LANGUAGE":
                                int jobID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.JOB_BY_LANGUAGE.Where(p => p.JOB_ID == jobID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.JOB_BY_LANGUAGE.Where(p => p.JOB_ID == jobID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.JOB_NAME;
                                }
                                break;

                            case "Educationalsystem_By_Language":
                                int EducationalsystemID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Educationalsystem_By_Language.Where(p => p.Educationalsystem_ID == EducationalsystemID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Educationalsystem_By_Language.Where(p => p.Educationalsystem_ID == EducationalsystemID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Name;
                                }
                                break;

                            

                            case "DEPARTMENT_BY_LANGUAGE":
                                int DEPARTMENTID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.DEPARTMENT_BY_LANGUAGE.Where(p => p.DEPARTMENT_ID == DEPARTMENTID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.DEPARTMENT_BY_LANGUAGE.Where(p => p.DEPARTMENT_ID == DEPARTMENTID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.DEPARTMENT_NAME;
                                }
                                break;

                             case "Company_By_Language":
                                int Companyid = Convert.ToInt32(TableID);

                                CountableTableLanguage = db.Company_By_Language.Where(p => p.Company_ID == Companyid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Company_By_Language.Where(p => p.Company_ID == Companyid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Company_Name;
                                }
                                break;

                            case "Company_Type_BY_LANGUAGE":
                                int CompanyTypeid = Convert.ToInt32(TableID);

                                CountableTableLanguage = db.Company_Type_BY_LANGUAGE.Where(p => p.Company_Type_ID == CompanyTypeid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Company_Type_BY_LANGUAGE.Where(p => p.Company_Type_ID == CompanyTypeid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Company_Type_NAME;
                                }
                                break;

                            case "Document_Extension_By_Language":
                                int DocExtID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Document_Extension_By_Language.Where(p => p.Document_Ext_ID == DocExtID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Document_Extension_By_Language.Where(p => p.Document_Ext_ID == DocExtID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Extension_Name;
                                }
                                break;

                            case "Marital_Status_By_Language":
                                int MaritalStatusid = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Marital_Status_By_Language.Where(p => p.Marital_Status_ID == MaritalStatusid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Marital_Status_By_Language.Where(p => p.Marital_Status_ID == MaritalStatusid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Marital_Status_Title;
                                }
                                break;

                            case "STATUS_BY_LANGUAGE":
                                int Statusid = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.STATUS_BY_LANGUAGE.Where(p => p.Status_ID == Statusid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.STATUS_BY_LANGUAGE.Where(p => p.Status_ID == Statusid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Status_Name;
                                }
                                break;


                            case "Region_BY_LANGUAGE":
                                int DISTID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Region_BY_LANGUAGE.Where(p => p.Region_ID == DISTID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Region_BY_LANGUAGE.Where(p => p.Region_ID == DISTID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Region_Name;
                                }
                                break;

                            case "Kaddaa_BY_LANGUAGE":
                                int KaddaaID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Kaddaa_BY_LANGUAGE.Where(p => p.Kaddaa_ID == KaddaaID && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Kaddaa_BY_LANGUAGE.Where(p => p.Kaddaa_ID == KaddaaID && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Kaddaa_NAME;
                                }
                                break;

                            

                            

                            case "Place_BY_LANGUAGE":
                                int tracksid = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.Place_BY_LANGUAGE.Where(p => p.Place_ID == tracksid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.Place_BY_LANGUAGE.Where(p => p.Place_ID == tracksid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Place_Name;
                                }
                                break;
                            
                            case "RELIGION_BY_LANGUAGE":
                                int ReligionID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.RELIGION_BY_LANGUAGE.Where(p => p.Religion_ID == ReligionID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.RELIGION_BY_LANGUAGE.Where(p => p.Religion_ID == ReligionID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Religion_Name;
                                }
                                break;
                            
                            case "SOCIAL_BY_LANGUAGE":
                                int SocialID = Convert.ToInt32(TableID);
                                CountableTableLanguage = db.SOCIAL_BY_LANGUAGE.Where(p => p.Social_ID == SocialID && p.Language_ID.ToString() == Translationid).Count();
                                if (CountableTableLanguage > 0)
                                {
                                    var etTranslation = db.SOCIAL_BY_LANGUAGE.Where(p => p.Social_ID == SocialID && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    FieldName = etTranslation.Social_Type;
                                }
                                break;

                      
                          
                        }
                        TranslationViewModel model = new TranslationViewModel()
                        {
                            FieldID = TableID,
                            LanguageID = lang.LANGUAGE_ID,
                            LanguageName = lang.LANGUAGE_NAME,
                            FieldName = FieldName,
                            descriptionField=descField,
                            TableName = TableName,

                        };
                        lst.Add(model);

                    }

                }
                return lst;

            }

        }
        public string[] SaveTranslations(List<TranslationViewModel> etTranslationViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveTranslationBusinessLogic(etTranslationViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";

                return messages;
            }
            try
            {
                foreach (TranslationViewModel transaltion in etTranslationViewModel)
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        int CountFitnessLanguage = -1;
                        var Fitnessid = "";

                        var Translationid = "";
                        switch (transaltion.TableName)
                        {
                           
                         
                            case "Gender_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.GENDER_BY_LANGUAGE.Where(p => p.GENDER_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.GENDER_BY_LANGUAGE.Where(p => p.GENDER_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.GENDER_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    int DefaultLanguageId = (int)Languages.English;
                                    if (Translationid.ToString() == DefaultLanguageId.ToString())
                                    {
                                        var etGenderdb = db.GENDERs.Where(p => p.GENDER_ID.ToString() == Fitnessid).FirstOrDefault();
                                        etGenderdb.GENDER_NAME = transaltion.FieldName;
                                        db.SaveChanges();
                                    }
                                    break;
                                }
                                else
                                {
                                    GENDER_BY_LANGUAGE etTranslation = new GENDER_BY_LANGUAGE();

                                    etTranslation.GENDER_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.GENDER_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    db.GENDER_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                           


                            case "COUNTRY_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.COUNTRY_BY_LANGUAGE.Where(p => p.COUNTRY_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.COUNTRY_BY_LANGUAGE.Where(p => p.COUNTRY_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.COUNTRY_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    COUNTRY_BY_LANGUAGE etTranslation = new COUNTRY_BY_LANGUAGE();
                                    etTranslation.COUNTRY_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.COUNTRY_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.COUNTRY_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                           

                            case "Branches_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Branches_By_Language.Where(p => p.BranchId.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Branches_By_Language.Where(p => p.BranchId.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Branches_By_Language etTranslation = new Branches_By_Language();
                                    etTranslation.Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.BranchId = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Branches_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                            case "SHIFT_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.SHIFT_BY_LANGUAGE.Where(p => p.SHIFT_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.SHIFT_BY_LANGUAGE.Where(p => p.SHIFT_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.SHIFT_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    SHIFT_BY_LANGUAGE etTranslation = new SHIFT_BY_LANGUAGE();
                                    etTranslation.SHIFT_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.SHIFT_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SHIFT_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                            case "Relation_Types_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Relation_Types_By_Language.Where(p => p.Relation_Type_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Relation_Types_By_Language.Where(p => p.Relation_Type_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Relation_Type_Title = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Relation_Types_By_Language etTranslation = new Relation_Types_By_Language();
                                    etTranslation.Relation_Type_Title = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Relation_Type_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Relation_Types_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }




                            case "Phone_Type_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Phone_Type_BY_LANGUAGE.Where(p => p.Phone_Type_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Phone_Type_BY_LANGUAGE.Where(p => p.Phone_Type_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Phone_Type_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Phone_Type_BY_LANGUAGE etTranslation = new Phone_Type_BY_LANGUAGE();
                                    etTranslation.Phone_Type_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.Phone_Type_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Phone_Type_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                            case "NATIONALITY_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.TBL_Nationality_By_Language.Where(p => p.Nationality_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.TBL_Nationality_By_Language.Where(p => p.Nationality_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Nationality_Title = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    TBL_Nationality_By_Language etTranslation = new TBL_Nationality_By_Language();
                                    etTranslation.Nationality_Title = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Nationality_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.TBL_Nationality_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                            case "JOB_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.JOB_BY_LANGUAGE.Where(p => p.JOB_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.JOB_BY_LANGUAGE.Where(p => p.JOB_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.JOB_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    JOB_BY_LANGUAGE etTranslation = new JOB_BY_LANGUAGE();
                                    etTranslation.JOB_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.JOB_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.JOB_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }



                            case "Educationalsystem_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Educationalsystem_By_Language.Where(p => p.Educationalsystem_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Educationalsystem_By_Language.Where(p => p.Educationalsystem_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Educationalsystem_By_Language etTranslation = new Educationalsystem_By_Language();
                                    etTranslation.Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Educationalsystem_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Educationalsystem_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                          


                            case "DEPARTMENT_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.DEPARTMENT_BY_LANGUAGE.Where(p => p.DEPARTMENT_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.DEPARTMENT_BY_LANGUAGE.Where(p => p.DEPARTMENT_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.DEPARTMENT_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    DEPARTMENT_BY_LANGUAGE etTranslation = new DEPARTMENT_BY_LANGUAGE();
                                    etTranslation.DEPARTMENT_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.DEPARTMENT_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.DEPARTMENT_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }




                           

                            case "Place_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Place_BY_LANGUAGE.Where(p => p.Place_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Place_BY_LANGUAGE.Where(p => p.Place_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Place_Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Place_BY_LANGUAGE etTranslation = new Place_BY_LANGUAGE();

                                    etTranslation.Place_Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Place_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    db.Place_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                         
                           
                            case "Company_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Company_By_Language.Where(p => p.Company_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Company_By_Language.Where(p => p.Company_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Company_Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Company_By_Language etTranslation = new Company_By_Language();

                                    etTranslation.Company_Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Company_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    db.Company_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                            case "Company_Type_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Company_Type_BY_LANGUAGE.Where(p => p.Company_Type_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Company_Type_BY_LANGUAGE.Where(p => p.Company_Type_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Company_Type_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Company_Type_BY_LANGUAGE etTranslation = new Company_Type_BY_LANGUAGE();

                                    etTranslation.Company_Type_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.Company_Type_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    db.Company_Type_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                            case "Corporate_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Corporate_By_Language.Where(p => p.ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Corporate_By_Language.Where(p => p.ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Corporate_By_Language etTranslation = new Corporate_By_Language();

                                    etTranslation.Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    db.Corporate_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                            case "Document_Extension_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Document_Extension_By_Language.Where(p => p.Document_Ext_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Document_Extension_By_Language.Where(p => p.Document_Ext_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Extension_Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Document_Extension_By_Language etTranslation = new Document_Extension_By_Language();

                                    etTranslation.Extension_Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Document_Ext_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    db.Document_Extension_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                           

                            case "Marital_Status_By_Language":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Marital_Status_By_Language.Where(p => p.Marital_Status_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Marital_Status_By_Language.Where(p => p.Marital_Status_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Marital_Status_Title = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Marital_Status_By_Language etTranslation = new Marital_Status_By_Language();
                                    etTranslation.Marital_Status_Title = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Marital_Status_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Marital_Status_By_Language.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                            case "STATUS_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.STATUS_BY_LANGUAGE.Where(p => p.Status_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.STATUS_BY_LANGUAGE.Where(p => p.Status_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Status_Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    STATUS_BY_LANGUAGE etTranslation = new STATUS_BY_LANGUAGE();
                                    etTranslation.Status_Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Status_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.STATUS_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                            case "Region_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Region_BY_LANGUAGE.Where(p => p.Region_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Region_BY_LANGUAGE.Where(p => p.Region_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Region_Name = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Region_BY_LANGUAGE etTranslation = new Region_BY_LANGUAGE();
                                    etTranslation.Region_Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Region_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Region_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                            case "Kaddaa_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.Kaddaa_BY_LANGUAGE.Where(p => p.Kaddaa_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.Kaddaa_BY_LANGUAGE.Where(p => p.Kaddaa_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Kaddaa_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    Kaddaa_BY_LANGUAGE etTranslation = new Kaddaa_BY_LANGUAGE();
                                    etTranslation.Kaddaa_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.Kaddaa_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.Kaddaa_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                           

                           
                            case "RELIGION_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.RELIGION_BY_LANGUAGE.Where(p => p.Religion_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.RELIGION_BY_LANGUAGE.Where(p => p.Religion_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Religion_Name = transaltion.FieldName;
                                    etTranslation.Modification_ID = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    RELIGION_BY_LANGUAGE etTranslation = new RELIGION_BY_LANGUAGE();
                                    etTranslation.Religion_Name = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Religion_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.RELIGION_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                           
                            case "ROLE_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.ROLE_BY_LANGUAGE.Where(p => p.Role_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.ROLE_BY_LANGUAGE.Where(p => p.Role_ID.ToString() == Fitnessid && p.LANGUAGE_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.ROLE_NAME = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    ROLE_BY_LANGUAGE etTranslation = new ROLE_BY_LANGUAGE();
                                    etTranslation.ROLE_NAME = transaltion.FieldName;
                                    etTranslation.LANGUAGE_ID = transaltion.LanguageID;
                                    etTranslation.Role_ID = new  Guid (transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.ROLE_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();

                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }

                            case "SOCIAL_BY_LANGUAGE":
                                Fitnessid = transaltion.FieldID.ToString();
                                Translationid = transaltion.LanguageID.ToString();
                                CountFitnessLanguage = db.SOCIAL_BY_LANGUAGE.Where(p => p.Social_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).Count();
                                if (CountFitnessLanguage > 0)
                                {
                                    var etTranslation = db.SOCIAL_BY_LANGUAGE.Where(p => p.Social_ID.ToString() == Fitnessid && p.Language_ID.ToString() == Translationid).FirstOrDefault();
                                    etTranslation.Social_Type = transaltion.FieldName;
                                    etTranslation.MODIFICATION_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }
                                else
                                {
                                    SOCIAL_BY_LANGUAGE etTranslation = new SOCIAL_BY_LANGUAGE();
                                    etTranslation.Social_Type = transaltion.FieldName;
                                    etTranslation.Language_ID = transaltion.LanguageID;
                                    etTranslation.Social_ID = Convert.ToInt32(transaltion.FieldID);
                                    etTranslation.ENTRY_DATE = DateTime.Now;
                                    etTranslation.IS_ACTIVE = true;
                                    db.SOCIAL_BY_LANGUAGE.Add(etTranslation);
                                    db.SaveChanges();
                                    messages[0] = "s";
                                    messages[1] = Fitnessid.ToString();
                                    break;
                                }


                          


                           
                            

                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                messages[0] = "e";
                messages[1] = ex.ToString();
                return messages;
            }
            return messages;
        }
        public string SaveTranslationBusinessLogic(List<TranslationViewModel> etTranslationViewModel)
        {
            string message = string.Empty;

            return message;
        }


    }
}
