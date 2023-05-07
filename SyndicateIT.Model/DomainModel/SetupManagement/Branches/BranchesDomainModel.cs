using AutoMapper;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.SetupManagement.Branches;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Branches
{
    
        public class BranchesDomainModel : DomainModelBase
    {

        public BranchesContentViewModel GetBranchesContent()
        {
            BranchesContentViewModel model = new BranchesContentViewModel();
            model.Navigation = GetNavigationList("list", "Branch");
            model.Title = "Branch";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public BranchesViewModel GetBranchesViewModel(String Branches_ID)
        {
            BranchesViewModel model = new BranchesViewModel();
            string title = string.Empty;
            if (Branches_ID != null && Branches_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etBranches = db.Branches.Where(p => p.BranchId.ToString() == Branches_ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.Branch, BranchesViewModel>(etBranches);
                }
                title = "Edit Branch";
            }
            else
            {
                title = "New Branch";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public BranchesViewModel GetBranchesViewModel(BranchesViewModel model)
        {
            string title = string.Empty;
            if (model.BranchId != null && model.BranchId > 0)
            {
                title = "Edit Branch";
            }
            else
            {
                title = "New Branch";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<BranchesGridViewModel> GetListBranches(BranchesContentViewModel etBranchesContentViewModel, bool isAPI = true)
        {
            string[] messages = new string[2];
            messages[1] = GetListBranchesBuisnessLogic(etBranchesContentViewModel);

            List<BranchesGridViewModel> lst = new List<BranchesGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etBranchesContentViewModel.BranchId.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Branches(etBranchesContentViewModel.BranchId, etBranchesContentViewModel.Name, etBranchesContentViewModel.MobileNumber, etBranchesContentViewModel.Email, etBranchesContentViewModel.Country, etBranchesContentViewModel.City, etBranchesContentViewModel.Address, etBranchesContentViewModel.User_Id, etBranchesContentViewModel.RecordDate, etBranchesContentViewModel.RowVersion, etBranchesContentViewModel.language, etBranchesContentViewModel.IS_ACTIVE, etBranchesContentViewModel.START_ROW, etBranchesContentViewModel.END_ROW, etBranchesContentViewModel.TOP, etBranchesContentViewModel.ENTRY_DATE, etBranchesContentViewModel.ENTRY_USER_ID, etBranchesContentViewModel.MODIFICATION_USER_ID, etBranchesContentViewModel.MODIFICATION_DATE).ToList();
                List<BranchesGridViewModel> Branchess = Mapper.Map<List<USP_GET_Branches_Result>, List<BranchesGridViewModel>>(listdb);

                lst = Branchess;
            };

            return lst;

        }

        public String[] DeleteBranches(BranchesViewModel etBranchesViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteBranchesBuisnessLogic(etBranchesViewModel);
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
                        var etBranchesByLanguage = db.Branches_By_Language.Where(p => p.BranchId == etBranchesViewModel.BranchId).ToList();
                        if (etBranchesByLanguage.Count > 0)
                        {
                            db.Branches_By_Language.RemoveRange(etBranchesByLanguage);
                        }
                        db.SaveChanges();
                        var etBranches = db.Branches.Where(p => p.BranchId == etBranchesViewModel.BranchId).FirstOrDefault();
                        if (etBranches != null)
                        {
                            db.Branches.Remove(etBranches);
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

        public string[] SaveBranches(BranchesViewModel etBranchesViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveBranchesBuisnessLogic(etBranchesViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.Branch etBranches = new DataLayer.DataContext.Branch();
            Branches_By_Language etBranchesByLanguageDb = new Branches_By_Language();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etBranchesViewModel.BranchId != 0 && etBranchesViewModel.BranchId.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etBranchesViewModel.ENTRY_USER_ID = etBranchesViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etBranchesdb = db.Branches.Where(p => p.BranchId.ToString() == etBranchesViewModel.BranchId.ToString()).FirstOrDefault();
                            etBranches = Mapper.Map<BranchesViewModel, DataLayer.DataContext.Branch>(etBranchesViewModel);

                            if (etBranches != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etBranchesViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[18];

                                int count = -1;

                                if (etBranchesViewModel.Name != null)
                                {
                                    count++;
                                    properties[count] = "Name";
                                }

                                if (etBranchesViewModel.MobileNumber != null)
                                {
                                    count++;
                                    properties[count] = "MobileNumber";
                                }

                                if (etBranchesViewModel.Email != null)
                                {
                                    count++;
                                    properties[count] = "Email";
                                }

                                if (etBranchesViewModel.Country != null)
                                {
                                    count++;
                                    properties[count] = "Country";
                                }

                                if (etBranchesViewModel.City != null)
                                {
                                    count++;
                                    properties[count] = "City";
                                }
                                if (etBranchesViewModel.Address != null)
                                {
                                    count++;
                                    properties[count] = "Address";
                                }
                                if (etBranchesViewModel.User_Id != null)
                                {
                                    count++;
                                    properties[count] = "User_Id";
                                }
                                if (etBranchesViewModel.RecordDate != null)
                                {
                                    count++;
                                    properties[count] = "RecordDate";
                                }
                                if (etBranchesViewModel.RowVersion != null)
                                {
                                    count++;
                                    properties[count] = "RowVersion";
                                }





                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etBranchesdb, etBranches, true,
                                      properties
                                     );
                                etBranchesdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etBranchesByLanguageDb = db.Branches_By_Language.Where(p => p.BranchId == etBranches.BranchId && p.Language_ID == 1).FirstOrDefault();
                                Branches_By_Language etBranchesByLanguage = new Branches_By_Language();
                                etBranchesByLanguage = etBranchesByLanguageDb;
                                etBranchesByLanguage.Name = etBranches.Name;
                                etBranchesByLanguage.IS_ACTIVE = etBranches.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etBranchesByLanguageDb, etBranchesByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etBranches = Mapper.Map<BranchesViewModel, DataLayer.DataContext.Branch>(etBranchesViewModel);

                            etBranches.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etBranches.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Branches.Add(etBranches);
                            db.SaveChanges();
                            Branches_By_Language erTranslation = new Branches_By_Language();
                            erTranslation.Name = etBranches.Name;
                            erTranslation.BranchId = etBranches.BranchId;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etBranches.IS_ACTIVE;
                            db.Branches_By_Language.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etBranches.BranchId.ToString();
                        //   savedSuccessfully = true;
                        scope.Complete();

                    }
                }

                catch (Exception ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.InnerException.Message;
                }

            }
            return messages;

        }

        public string DeleteBranchesBuisnessLogic(BranchesViewModel etBranches)
        {
            string message = string.Empty;
            if (etBranches.BranchId == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListBranchesBuisnessLogic(BranchesContentViewModel etBranchesContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveBranchesBuisnessLogic(BranchesViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Branch", NavigationUrl = Utilities.GetUrl("index", "Branches", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.Branch, BranchesViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Branches_Result, BranchesGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<BranchesViewModel, DataLayer.DataContext.Branch>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<BranchesGridViewModel, USP_GET_Branches_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
