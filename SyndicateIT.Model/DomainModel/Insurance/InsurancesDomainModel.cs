using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Insurance;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.Model.UtilityModel.Session;

namespace SyndicateIT.Model.DomainModel.Insurance
{
   public class InsurancesDomainModel : DomainModelBase
    {

        public String[] SaveInsurance(InsuranceViewModel etInsuranceViewModel, out string message, out AlertType alertType)
        {
            message = string.Empty;
            alertType = AlertType.Success;
            string[] messages = new string[3];
            messages[1] = SaveInsuranceBusinessLogic(etInsuranceViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }

            TBL_Insurances etInsurance = new TBL_Insurances();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        if (etInsuranceViewModel.Insurance_Id.ToString() != "" && etInsuranceViewModel.Insurance_Id.ToString()!= "00000000-0000-0000-0000-000000000000")
                        {
                            //if (SessionContent.Container.Login.UserID != null)
                            //{
                            //    // Set the entry user id and the modification user id.

                            //    etAddressViewModel.ENTRY_USER_ID = etAddressViewModel.MODIFICATION_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            //}
                           
                            var etInsuranceDb = db.TBL_Insurances.Where(p => p.Insurance_Id.ToString() == etInsuranceViewModel.Insurance_Id.ToString()).FirstOrDefault();
                            etInsurance = Mapper.Map<InsuranceViewModel, TBL_Insurances>(etInsuranceViewModel);
                            if (etInsuranceDb != null)
                            {
                                //if (SessionContent.Container.Login.UserID != null)
                                //{
                                //    etAddressViewModel.MODIFICATION_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                //}
                                string[] properties = new string[12];

                                int count = -1;
                             

                                if (etInsuranceViewModel.IsActive != null)
                                {
                                    count++;
                                    properties[count] = "IsActive";
                                }

                                if (etInsuranceViewModel.HasGuarantee != null)
                                {
                                    count++;
                                    properties[count] = "HasGuarantee";
                                }

                                if (etInsuranceViewModel.HasInsurance != null)
                                {
                                    count++;
                                    properties[count] = "HasInsurance";
                                }
                               
                                if (etInsuranceViewModel.Users_Id.ToString() != "")
                                {
                                    count++;
                                    properties[count] = "User_Id";
                                }
                                if (etInsuranceViewModel.DegreeGuaranteeNotes.ToString() != "")
                                {
                                    count++;
                                    properties[count] = "DegreeGuaranteeNotes";
                                }
                                if (etInsuranceViewModel.TypeInsuranceNotes.ToString() != "")
                                {
                                    count++;
                                    properties[count] = "TypeInsuranceNotes";
                                }
                                if (etInsuranceViewModel.TypeGuarantee_Id.ToString() != "")
                                {
                                    count++;
                                    properties[count] = "TypeGuarantee_Id";
                                }
                                if (etInsuranceViewModel.TypeInsurance_Id.ToString() != "")
                                {
                                    count++;
                                    properties[count] = "TypeInsurance_Id";
                                }
                                if (etInsuranceViewModel.DegreeInsurance_Id.ToString() != "")
                                {
                                    count++;
                                    properties[count] = "DegreeInsurance_Id";
                                    if (etInsuranceViewModel.DegreeGuarantee_Id.ToString() != "")
                                    {
                                        count++;
                                        properties[count] = "DegreeGuarantee_Id";
                                    }
                                }

                                count++;
                                properties[count] = "MODIFICATION_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";

                                UtilityComponent.Utilities.MergeObject(etInsuranceDb, etInsurance, true,
                                     properties
                                    );
                                etInsuranceDb.Modification_Date = DateTime.Now;
                                if (SessionContent.Container.Login.UserID != null)
                                {
                                    etInsuranceDb.Modification_Users_Id = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }
                                db.SaveChanges();
                            }
                        }
                        else
                        {

                            etInsurance = Mapper.Map<InsuranceViewModel, TBL_Insurances>(etInsuranceViewModel);
                            etInsurance.DegreeGuarantee_Id = etInsuranceViewModel.DegreeGuarantee_Id;
                            etInsurance.DegreeInsurance_Id = etInsuranceViewModel.DegreeInsurance_Id;
                            etInsurance.TypeGuarantee_Id = etInsuranceViewModel.TypeGuarantee_Id;
                            etInsurance.TypeInsurance_Id = etInsuranceViewModel.TypeInsurance_Id;
                            etInsurance.Users_Id = etInsuranceViewModel.Users_Id;
                            etInsurance.Entry_Date = DateTime.Now;
                            etInsurance.TypeInsuranceNotes = etInsuranceViewModel.TypeInsuranceNotes;
                            etInsurance.DegreeGuaranteeNotes = etInsuranceViewModel.DegreeGuaranteeNotes;
                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etInsurance.Entry_Users_Id = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }
                            etInsurance.Insurance_Id = Guid.NewGuid();
                            etInsurance.IsActive = true;
                            db.TBL_Insurances.Add(etInsurance);
                            db.SaveChanges();
                     

                        }
                    }
                    messages[0] = "s";
                    messages[1] = "Saved Successfully";
                    messages[2] = etInsurance.Insurance_Id .ToString ();
                    //   savedSuccessfully = true;
                    scope.Complete();

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
        public InsurancesContentViewModel GetInsuranceContent()
        {
            InsurancesContentViewModel etInsuranceContentViewModel = new InsurancesContentViewModel();
            return etInsuranceContentViewModel;
        }
        // Get specific data row of a ID for edit 
        public InsuranceViewModel GetInsuranceViewModel(String Insurance_Id)
        {
            InsuranceViewModel model = new InsuranceViewModel();
            if (Insurance_Id != null && Insurance_Id != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etInsurance = db.TBL_Insurances.Where(p => p.Insurance_Id.ToString() == Insurance_Id).FirstOrDefault();
                    model = Mapper.Map<TBL_Insurances, InsuranceViewModel>(etInsurance);
                }

            }
            return model;
        }
        public string SaveInsuranceBusinessLogic(InsuranceViewModel etInsuranceViewModel)
        {
            string message = string.Empty;


            return message;
        }
        public string GetListInsuranceBusinessLogic(InsurancesContentViewModel etInsuranceContentViewModel)
        {
            string message = string.Empty;


            return message;
        }
        public string[] DeleteInsurance(InsuranceViewModel etInsuranceViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveInsuranceBusinessLogic(etInsuranceViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }

            TBL_Insurances ETInsurance = new TBL_Insurances();


            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etInsuranceViewModel.Insurance_Id.ToString() != "")
                        {
                           

                            var etInsurance = db.TBL_Insurances.Where(p => p.Insurance_Id == etInsuranceViewModel.Insurance_Id).FirstOrDefault();
                            if (etInsurance != null)
                            {
                                db.TBL_Insurances.Remove(etInsurance);
                            }
                            db.SaveChanges();
                            messages[0] = "s";
                            messages[1] = etInsuranceViewModel.Insurance_Id.ToString();


                        }


                    }
                    messages[0] = "s";
                    messages[1] = ETInsurance.Insurance_Id.ToString();
                    //   savedSuccessfully = true;
                    scope.Complete();
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
        public List<InsurancesGridViewModel> GetListInsurance(InsurancesContentViewModel etInsuranceContentViewModel, bool isAPI = true)
        {
            List<InsurancesGridViewModel> etLsitInsuranceGridViewModel = new List<InsurancesGridViewModel>();
            String[] messages = new String[2];
            messages[1] = GetListInsuranceBusinessLogic(etInsuranceContentViewModel);
            using (var db = new SyndicatDataEntities())
            {
                var listdb = db.USP_Insurance(etInsuranceContentViewModel.Insurance_Id, etInsuranceContentViewModel.IsActive,
                    etInsuranceContentViewModel.HasGuarantee, etInsuranceContentViewModel.HasInsurance,
                    -1, -1, -1, etInsuranceContentViewModel.Entry_DateFrom, etInsuranceContentViewModel.Entry_DateTo,
                    etInsuranceContentViewModel.MODIFICATION_DATEfrom, etInsuranceContentViewModel.MODIFICATION_DATETo, etInsuranceContentViewModel.Modification_User_Id, etInsuranceContentViewModel.Entry_User_Id).ToList();
                List<InsurancesGridViewModel> Insurance = Mapper.Map<List<USP_Insurance_Result>, List<InsurancesGridViewModel>>(listdb);
                etLsitInsuranceGridViewModel = Insurance;

                //    if (isAPI)
                //        SessionContent.Container.FitnessLevelAPI.FitnessLevels = lst;
                //    else
                //        SessionContent.Container.FitnessLevel.FitnessLevels = lst;


                return etLsitInsuranceGridViewModel;
            }




        }
        public void InitializeMapper()
        {

            #region Database To View
            Mapper.CreateMap<TBL_Insurances, InsuranceViewModel>().IgnoreAllNonExisting();
            Mapper.CreateMap<USP_Insurance_Result, InsurancesGridViewModel>().IgnoreAllNonExisting();
            #endregion
            #region Database To View
            Mapper.CreateMap<InsuranceViewModel, TBL_Insurances>().IgnoreAllNonExisting();
            Mapper.CreateMap<InsurancesGridViewModel, USP_Insurance_Result>().IgnoreAllNonExisting();

            #endregion

        }
    }
}
