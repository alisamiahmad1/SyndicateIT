using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.DegreeGuarantee;
using System.Transactions;
using AutoMapper;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.DomainModel.DegreeGuarantee
{
   public class DegreeGuaranteeDomainModel : DomainModelBase
    {

        //Get DegreeGuarantee
        public DegreeGuaranteeContentViewModel GetDegreeGuaranteeContent()
        {
            DegreeGuaranteeContentViewModel etDegreeGuaranteeContentViewModel = new DegreeGuaranteeContentViewModel();
            return etDegreeGuaranteeContentViewModel;
        }

        // Get specific data row of a ID for edit 
        public DegreeGuaranteeViewModel GetDegreeGuaranteeViewModel(String Degree_Guarantee_ID)
        {
            DegreeGuaranteeViewModel model = new DegreeGuaranteeViewModel();
            if (Degree_Guarantee_ID != null && Degree_Guarantee_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDegreeGuarantee = db.Degree_Guarantee.Where(p => p.Degree_Guarantee_ID.ToString() == Degree_Guarantee_ID).FirstOrDefault();
                    model = Mapper.Map<Degree_Guarantee, DegreeGuaranteeViewModel>(etDegreeGuarantee);
                }

            }
            return model;
        }

        // Get all data after search to fill grid
        public List<DegreeGuaranteeGridViewModel> GetListDegreeGuarantee(DegreeGuaranteeContentViewModel etDegreeGuaranteeContentViewModel, bool isAPI = true)
        {
            string[] messages = new string[2];
            messages[1] = GetListDegreeGuaranteeBuisnessLogic(etDegreeGuaranteeContentViewModel);

            List<DegreeGuaranteeGridViewModel> lst = new List<DegreeGuaranteeGridViewModel>();
            //  string message = GetListFitnessLevelBusinessLogic(etFitnessLevelContentViewModel);


            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_Get_Degree_Guarantee(
                    etDegreeGuaranteeContentViewModel.Degree_Guarantee_ID,
                    etDegreeGuaranteeContentViewModel.Degree_Guarantee_Name,
                    etDegreeGuaranteeContentViewModel.LANGUAGE_ID,
                    etDegreeGuaranteeContentViewModel.START_ROW,
                    etDegreeGuaranteeContentViewModel.END_ROW,
                    etDegreeGuaranteeContentViewModel.TOP,
                    etDegreeGuaranteeContentViewModel.IS_ACTIVE,
                    etDegreeGuaranteeContentViewModel.ENTRY_DATE_FROM,
                    etDegreeGuaranteeContentViewModel.ENTRY_DATE_TO,
                    etDegreeGuaranteeContentViewModel.MODIFICATION_DATE_FROM,
                    etDegreeGuaranteeContentViewModel.MODIFICATION_DATE_TO,
                    etDegreeGuaranteeContentViewModel.MODIFICATION_USER_ID,
                    etDegreeGuaranteeContentViewModel.ENTRY_USER_ID
                                       ).ToList();

                List<DegreeGuaranteeGridViewModel> Countries = Mapper.Map<List<USP_Get_Degree_Guarantee_Result>, List<DegreeGuaranteeGridViewModel>>(listdb);
                lst = Countries;
            };
            //if (isAPI)
            //    SessionContent.Container.FitnessLevelAPI.FitnessLevels = lst;
            //else
            //    SessionContent.Container.FitnessLevel.FitnessLevels = lst;



            return lst;

        }

        // Insert to table  and to by language Or Update if an ID is given
        public string[] SaveDegreeGuarantee(DegreeGuaranteeViewModel etDegreeGuaranteeViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveDegreeGuaranteeBuisnessLogic(etDegreeGuaranteeViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            Degree_Guarantee etDegreeGuarantee = new Degree_Guarantee();
            Degree_Guarantee_By_Language etDegreeGuaranteeByLanguageDb = new Degree_Guarantee_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etDegreeGuaranteeViewModel.Degree_Guarantee_ID.ToString() != "0" && etDegreeGuaranteeViewModel.Degree_Guarantee_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            //if (SessionContent.Container.Login.UserID != null)
                            //{
                            //    // Set the entry user id and the modification user id.

                            //    etDegreeGuaranteeViewModel.ENTRY_USER_ID = etDegreeGuaranteeViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            //}
                            var etDegreeGuaranteedb = db.Degree_Guarantee.Where(p => p.Degree_Guarantee_ID.ToString() == etDegreeGuaranteeViewModel.Degree_Guarantee_ID.ToString()).FirstOrDefault();
                            etDegreeGuarantee = Mapper.Map<DegreeGuaranteeViewModel, Degree_Guarantee>(etDegreeGuaranteeViewModel);

                            if (etDegreeGuaranteedb != null)
                            {
                                //if (SessionContent.Container.Login.UserID != null)
                                //{
                                //    etDegreeGuaranteeViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                //}
                                string[] properties = new string[16];

                                int count = -1;

                                if (etDegreeGuaranteeViewModel.Degree_Guarantee_Name != null)
                                {
                                    count++;
                                    properties[count] = "Degree_Guarantee_Name";
                                }

                                if (etDegreeGuaranteeViewModel.Is_Active != null)
                                {
                                    count++;
                                    properties[count] = "IS_ACTIVE";
                                }
                     

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etDegreeGuaranteedb, etDegreeGuarantee, true,
                                       properties
                                      );
                                etDegreeGuaranteedb.Modification_Date = DateTime.Now;

                                etDegreeGuaranteeByLanguageDb = db.Degree_Guarantee_By_Language.Where(p => p.Degree_Guarantee_Id == etDegreeGuarantee.Degree_Guarantee_ID && p.Language_Id.ToString() == "1").FirstOrDefault();
                                Degree_Guarantee_By_Language etDegreeGuaranteeByLanguage = new Degree_Guarantee_By_Language();
                                etDegreeGuaranteeByLanguage = etDegreeGuaranteeByLanguageDb;
                                etDegreeGuaranteeByLanguage.Name = etDegreeGuarantee.Degree_Guarantee_Name;
                                etDegreeGuaranteeByLanguage.Is_Active = etDegreeGuarantee.Is_Active;
                                UtilityComponent.Utilities.MergeObject(etDegreeGuaranteeByLanguageDb, etDegreeGuaranteeByLanguage, true,
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
                            etDegreeGuarantee = Mapper.Map<DegreeGuaranteeViewModel, Degree_Guarantee>(etDegreeGuaranteeViewModel);
                            etDegreeGuarantee.Entry_Users_ID= new Guid(SessionContent.Container.Login.UserID.ToString());
                            etDegreeGuarantee.Entry_Date = DateTime.Now;

                            //if (SessionContent.Container.Login.UserID != null)
                            //{
                            //    etDegreeGuarantee.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            //}
                            db.Degree_Guarantee.Add(etDegreeGuarantee);
                            db.SaveChanges();


                            Degree_Guarantee_By_Language erTranslation = new Degree_Guarantee_By_Language();
                            erTranslation.Name = etDegreeGuarantee.Degree_Guarantee_Name;
                            erTranslation.Degree_Guarantee_Id = etDegreeGuarantee.Degree_Guarantee_ID;
                            erTranslation.Language_Id = (int)Languages.English;
                            erTranslation.Is_Active = etDegreeGuarantee.Is_Active;
                            erTranslation.Entry_Users_Date = DateTime.Now;
                            //  erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            db.Degree_Guarantee_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etDegreeGuarantee.Degree_Guarantee_ID.ToString();
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

        public String[] DeleteDegreeGuarantee(DegreeGuaranteeViewModel etDegreeGuaranteeViewModel)
        {
            String[] messages = new string[2];
            messages[1] = GetListDegreeGuaranteeBusinessLogic(etDegreeGuaranteeViewModel);
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
                        var etDegreeGuaranteeByLanguage = db.Degree_Guarantee_By_Language.Where(p => p.Degree_Guarantee_Id == etDegreeGuaranteeViewModel.Degree_Guarantee_ID).ToList();
                        if (etDegreeGuaranteeByLanguage.Count > 0)
                        {
                            db.Degree_Guarantee_By_Language.RemoveRange(etDegreeGuaranteeByLanguage);
                        }
                        db.SaveChanges();

                        var etDegreeGuarantee = db.Degree_Guarantee.Where(p => p.Degree_Guarantee_ID == etDegreeGuaranteeViewModel.Degree_Guarantee_ID).FirstOrDefault();
                        if (etDegreeGuarantee != null)
                        {
                            db.Degree_Guarantee.Remove(etDegreeGuarantee);
                        }
                        db.SaveChanges();
                        messages[0] = "s";
                        messages[1] = etDegreeGuarantee.Degree_Guarantee_ID.ToString();

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
        public String GetListDegreeGuaranteeBusinessLogic(DegreeGuaranteeViewModel etDegreeGuaranteeViewModel)
        {

            string message = string.Empty;

            // if .....

            return message;
        }

        public string GetListDegreeGuaranteeBuisnessLogic(DegreeGuaranteeContentViewModel etDegreeGuaranteeContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveDegreeGuaranteeBuisnessLogic(DegreeGuaranteeViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods
        public void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<Degree_Guarantee, DegreeGuaranteeViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_Get_Degree_Guarantee_Result, DegreeGuaranteeGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<DegreeGuaranteeViewModel, Degree_Guarantee>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<DegreeGuaranteeGridViewModel, USP_Get_Degree_Guarantee_Result>()
               .IgnoreAllNonExisting();

            #endregion


        }
        #endregion







    }
}
