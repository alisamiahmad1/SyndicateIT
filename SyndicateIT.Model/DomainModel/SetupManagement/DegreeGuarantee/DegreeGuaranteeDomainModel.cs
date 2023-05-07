using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;
using SyndicateIT.Model.ViewModel.Shared;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.SetupManagement.DegreeGuarantee;

namespace SyndicateIT.Model.DomainModel.SetupManagement.DegreeGuarantee
{
    public class DegreeGuaranteeDomainModel : DomainModelBase
    {

        public DegreeGuaranteeContentViewModel GetDegreeGuaranteeContent()
        {
            DegreeGuaranteeContentViewModel model = new DegreeGuaranteeContentViewModel();
            model.Navigation = GetNavigationList("list", "Degree Guarantee");
            model.Title = "Degree Guarantee";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public DegreeGuaranteeViewModel GetDegreeGuaranteeViewModel(String DegreeGuarantee_ID)
        {
            DegreeGuaranteeViewModel model = new DegreeGuaranteeViewModel();
            string title = string.Empty;
            if (DegreeGuarantee_ID != null && DegreeGuarantee_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDegreeGuarantee = db.Degree_Guarantee.Where(p => p.Degree_Guarantee_ID.ToString() == DegreeGuarantee_ID).FirstOrDefault();
                    model = Mapper.Map<Degree_Guarantee, DegreeGuaranteeViewModel>(etDegreeGuarantee);
                }
                title = "Edit Degree Guarantee";
            }
            else
            {
                title = "New Degree Guarantee";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public DegreeGuaranteeViewModel GetDegreeGuaranteeViewModel(DegreeGuaranteeViewModel model)
        {
            string title = string.Empty;
            if (model.Degree_Guarantee_ID.ToString() != null && model.Degree_Guarantee_ID.ToString() != "0")
            {
                title = "Edit Degree Guarantee";
            }
            else
            {
                title = "New Degree Guarantee";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<DegreeGuaranteeGridViewModel> GetListDegreeGuarantee(DegreeGuaranteeContentViewModel etDegreeGuaranteeContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListDegreeGuaranteeBuisnessLogic(etDegreeGuaranteeContentViewModel);

            List<DegreeGuaranteeGridViewModel> lst = new List<DegreeGuaranteeGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etDegreeGuaranteeContentViewModel.Degree_Guarantee_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {
                var listDb = db.Degree_Guarantee.ToList();

                for (int i = 0; i < listDb.Count; i++)
                {
                    lst.Add(new DegreeGuaranteeGridViewModel()
                    {
                        Degree_Guarantee_Name = listDb[i].Degree_Guarantee_Name,
                        Degree_Guarantee_ID = listDb[i].Degree_Guarantee_ID

                    });
                }              
            };

            return lst;

        }

        public String[] DeleteDegreeGuarantee(DegreeGuaranteeViewModel etDegreeGuaranteeViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteDegreeGuaranteeBuisnessLogic(etDegreeGuaranteeViewModel);
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

        public string[] SaveDegreeGuarantee(DegreeGuaranteeViewModel etDegreeGuaranteeViewModel)
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
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etDegreeGuaranteeViewModel.Entry_Users_ID = etDegreeGuaranteeViewModel.Modification_Users_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etDegreeGuaranteedb = db.Degree_Guarantee.Where(p => p.Degree_Guarantee_ID.ToString() == etDegreeGuaranteeViewModel.Degree_Guarantee_ID.ToString()).FirstOrDefault();
                            etDegreeGuarantee = Mapper.Map<DegreeGuaranteeViewModel, Degree_Guarantee>(etDegreeGuaranteeViewModel);

                            if (etDegreeGuarantee != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etDegreeGuaranteeViewModel.Modification_Users_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etDegreeGuaranteeViewModel.Degree_Guarantee_Name != null)
                                {
                                    count++;
                                    properties[count] = "Degree_Guarantee_Name";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etDegreeGuaranteedb, etDegreeGuarantee, true,
                                      properties
                                     );
                                etDegreeGuaranteedb.Modification_Date = DateTime.Now;

                                etDegreeGuaranteeByLanguageDb = db.Degree_Guarantee_By_Language.Where(p => p.Degree_Guarantee_Id == etDegreeGuarantee.Degree_Guarantee_ID && p.Language_Id == 1).FirstOrDefault();
                                Degree_Guarantee_By_Language etDegreeGuaranteeByLanguage = new Degree_Guarantee_By_Language();
                                etDegreeGuaranteeByLanguage = etDegreeGuaranteeByLanguageDb;
                               
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
                            etDegreeGuarantee = Mapper.Map<DegreeGuaranteeViewModel, Degree_Guarantee>(etDegreeGuaranteeViewModel);

                            etDegreeGuarantee.Entry_Date = DateTime.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etDegreeGuarantee.Entry_Users_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Degree_Guarantee.Add(etDegreeGuarantee);
                            db.SaveChanges();
                            Degree_Guarantee_By_Language erTranslation = new Degree_Guarantee_By_Language();
                           
                            erTranslation.Degree_Guarantee_Id = etDegreeGuarantee.Degree_Guarantee_ID;
                            erTranslation.Language_Id = (int)Languages.English;
                            erTranslation.Entry_Users_Date = DateTime.Now;
                            erTranslation.Entry_Users_Id = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.Is_Active = etDegreeGuarantee.Is_Active;
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
                    messages[1] = ex.Message;
                }

            }
            return messages;

        }

        public string DeleteDegreeGuaranteeBuisnessLogic(DegreeGuaranteeViewModel etDegreeGuarantee)
        {
            string message = string.Empty;
            if (etDegreeGuarantee.Degree_Guarantee_ID.ToString() == "-1")
            {
                message = "there is no record";
            }
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
                model.Add(new NavigationViewModel() { NavigationName = "DegreeGuarantee", NavigationUrl = Utilities.GetUrl("DegreeGuarantees", "DegreeGuarantee", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
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
            base.InitializeMapper();
        }
        #endregion
    }
}
