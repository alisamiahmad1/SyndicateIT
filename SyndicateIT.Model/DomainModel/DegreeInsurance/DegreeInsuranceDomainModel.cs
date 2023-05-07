using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using System.Transactions;
using AutoMapper;
using SyndicateIT.Model.ViewModel.DegreeInsurance;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.UtilityModel.Session;

namespace SyndicateIT.Model.DomainModel.DegreeInsurance
{
    public class DegreeInsuranceDomainModel : DomainModelBase
    {
        public DegreeInsuranceContentViewModel GetDegreeInsuranceContent()
        {
            DegreeInsuranceContentViewModel etDegreeInsuranceContentViewModel = new DegreeInsuranceContentViewModel();
            return etDegreeInsuranceContentViewModel;
        }

        // Get specific data row of a ID for edit 
        public DegreeInsuranceViewModel GetDegreeInsuranceViewModel(String Degree_Insurance_ID)
        {
            DegreeInsuranceViewModel model = new DegreeInsuranceViewModel();
            if (Degree_Insurance_ID != null && Degree_Insurance_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDegreeInsurance = db.Degree_Insurance.Where(p => p.Degree_Insurance_Id.ToString() == Degree_Insurance_ID).FirstOrDefault();
                    model = Mapper.Map<Degree_Insurance, DegreeInsuranceViewModel>(etDegreeInsurance);
                }

            }
            return model;
        }

        // Get all data after search to fill grid
        public List<DegreeInsuranceGridViewModel> GetListDegreeInsurance(DegreeInsuranceContentViewModel etDegreeInsuranceContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListDegreeInsuranceBuisnessLogic(etDegreeInsuranceContentViewModel);

            List<DegreeInsuranceGridViewModel> lst = new List<DegreeInsuranceGridViewModel>();
           
            using (var db = new SyndicatDataEntities())
            {

                var listDb = db.Degree_Insurance.ToList();

                for(int i=0; i< listDb.Count;i++)
                {
                    lst.Add(new DegreeInsuranceGridViewModel()
                    {
                        Degree_Insurance_Name = listDb[i].Degree_Insurance_Name,
                        Degree_Insurance_Id = listDb[i].Degree_Insurance_Id

                    });
                }              
            };          
            return lst;
        }

        // Insert to table  and to by language Or Update if an ID is given
        public string[] SaveDegreeInsurance(DegreeInsuranceViewModel etDegreeInsuranceViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveDegreeInsuranceBuisnessLogic(etDegreeInsuranceViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            Degree_Insurance etDegreeInsurance = new Degree_Insurance();
            Degree_Insurance_By_Language etDegreeInsuranceByLanguageDb = new Degree_Insurance_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etDegreeInsuranceViewModel.Degree_Insurance_Id.ToString() != "0" && etDegreeInsuranceViewModel.Degree_Insurance_Id.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            //if (SessionContent.Container.Login.UserID != null)
                            //{
                            //    // Set the entry user id and the modification user id.

                            //    etDegreeInsuranceViewModel.ENTRY_USER_ID = etDegreeInsuranceViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            //}
                            var etDegreeInsurancedb = db.Degree_Insurance.Where(p => p.Degree_Insurance_Id.ToString() == etDegreeInsuranceViewModel.Degree_Insurance_Id.ToString()).FirstOrDefault();
                            etDegreeInsurance = Mapper.Map<DegreeInsuranceViewModel, Degree_Insurance>(etDegreeInsuranceViewModel);

                            if (etDegreeInsurancedb != null)
                            {
                                //if (SessionContent.Container.Login.UserID != null)
                                //{
                                //    etDegreeInsuranceViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                //}
                                string[] properties = new string[16];

                                int count = -1;

                                if (etDegreeInsuranceViewModel.Degree_Insurance_Name != null)
                                {
                                    count++;
                                    properties[count] = "DegreeInsurance_Name";
                                }

                                if (etDegreeInsuranceViewModel.Is_Active != null)
                                {
                                    count++;
                                    properties[count] = "Is_Active";
                                }


                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etDegreeInsurancedb, etDegreeInsurance, true,
                                       properties
                                      );
                                etDegreeInsurancedb.Modification_Users_Date = DateTime.Now;

                                etDegreeInsuranceByLanguageDb = db.Degree_Insurance_By_Language.Where(p => p.Degree_Insurance_Id == etDegreeInsurance.Degree_Insurance_Id && p.Language_Id == (int)Languages.English).FirstOrDefault();
                                Degree_Insurance_By_Language etDegreeInsuranceByLanguage = new Degree_Insurance_By_Language();
                                etDegreeInsuranceByLanguage = etDegreeInsuranceByLanguageDb;
                                etDegreeInsuranceByLanguage.Degree_Insurance_By_Language_Name = etDegreeInsurance.Degree_Insurance_Name;
                                etDegreeInsuranceByLanguage.Is_Active = etDegreeInsurance.Is_Active;
                                UtilityComponent.Utilities.MergeObject(etDegreeInsuranceByLanguageDb, etDegreeInsuranceByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            //   etFitnessLevelViewModel.ENTRY_DATE = DateTime.Now;
                            // Need to add code to enter record in FitnessLevel_status with status id=1(pending)  
                            etDegreeInsurance = Mapper.Map<DegreeInsuranceViewModel, Degree_Insurance>(etDegreeInsuranceViewModel);
                            etDegreeInsurance.Entry_Users_Id = new Guid(SessionContent.Container.Login.UserID.ToString());
                            etDegreeInsurance.Entry_Users_Date = DateTime.Now;

                            //if (SessionContent.Container.Login.UserID != null)
                            //{
                            //    etDegreeInsurance.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            //}
                            db.Degree_Insurance.Add(etDegreeInsurance);
                            db.SaveChanges();


                            Degree_Insurance_By_Language erTranslation = new Degree_Insurance_By_Language();
                            erTranslation.Degree_Insurance_By_Language_Name = etDegreeInsurance.Degree_Insurance_Name;
                            erTranslation.Degree_Insurance_Id = etDegreeInsurance.Degree_Insurance_Id;
                            erTranslation.Language_Id = (int)Languages.English;
                            erTranslation.Is_Active = etDegreeInsurance.Is_Active;
                            erTranslation.Entry_Users_Date = DateTime.Now;
                            //  erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.Degree_Insurance_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etDegreeInsurance.Degree_Insurance_Id.ToString();
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

        public String[] DeleteDegreeInsurance(DegreeInsuranceViewModel etDegreeInsuranceViewModel)
        {
            String[] messages = new string[2];
            messages[1] = GetListDegreeInsuranceBusinessLogic(etDegreeInsuranceViewModel);
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
                        var etDegreeInsuranceByLanguage = db.Degree_Insurance_By_Language.Where(p => p.Degree_Insurance_Id == etDegreeInsuranceViewModel.Degree_Insurance_Id).ToList();
                        if (etDegreeInsuranceByLanguage.Count > 0)
                        {
                            db.Degree_Insurance_By_Language.RemoveRange(etDegreeInsuranceByLanguage);
                        }
                        db.SaveChanges();

                        var etDegreeInsurance = db.Degree_Insurance.Where(p => p.Degree_Insurance_Id == etDegreeInsuranceViewModel.Degree_Insurance_Id).FirstOrDefault();
                        if (etDegreeInsurance != null)
                        {
                            db.Degree_Insurance.Remove(etDegreeInsurance);
                        }
                        db.SaveChanges();
                        messages[0] = "s";
                        messages[1] = etDegreeInsurance.Degree_Insurance_Id.ToString();

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
        public String GetListDegreeInsuranceBusinessLogic(DegreeInsuranceViewModel etDegreeInsuranceViewModel)
        {

            string message = string.Empty;

            // if .....

            return message;
        }

        public string GetListDegreeInsuranceBuisnessLogic(DegreeInsuranceContentViewModel etDegreeInsuranceContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveDegreeInsuranceBuisnessLogic(DegreeInsuranceViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods
        public void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<Degree_Insurance, DegreeInsuranceViewModel>()
                  .IgnoreAllNonExisting();

            //Mapper.CreateMap<USP_GetDegreeInsurance_Result, DegreeInsuranceGridViewModel>()
            //     .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<DegreeInsuranceViewModel, Degree_Insurance>()
                  .IgnoreAllNonExisting();

            //Mapper.CreateMap<DegreeInsuranceGridViewModel, USP_GetDegreeInsurance_Result>()
            //   .IgnoreAllNonExisting();

            #endregion


        }
        #endregion



    }
}
