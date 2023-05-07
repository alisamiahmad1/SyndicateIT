using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.SetupManagement.Department;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Department
{
    public class DepartmentDomainModel : DomainModelBase
    {
        public DepartmentContentViewModel GetDepartmentContent()
        {
            DepartmentContentViewModel model = new DepartmentContentViewModel();
            model.Navigation = GetNavigationList("list", "Department");
            model.Title = "Department";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public DepartmentViewModel GetDepartmentViewModel(String Department_ID)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            string title = string.Empty;
            if (Department_ID != null && Department_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etDepartment = db.DEPARTMENTs.Where(p => p.DEPARTMENT_ID.ToString() == Department_ID).FirstOrDefault();
                    model = Mapper.Map<DEPARTMENT, DepartmentViewModel>(etDepartment);
                }
                title = "Edit Department";
            }
            else
            {
                title = "New Department";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public DepartmentViewModel GetDepartmentViewModel(DepartmentViewModel model)
        {
            string title = string.Empty;
            if (model.DEPARTMENT_ID != null && model.DEPARTMENT_ID > 0)
            {
                title = "Edit Department";
            }
            else
            {
                title = "New Department";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


            public List<DepartmentGridViewModel> GetListDepartment(DepartmentContentViewModel etDepartmentContentViewModel, bool isAPI = true)
            {
                string[] messages = new string[2];
                messages[1] = GetListDepartmentBuisnessLogic(etDepartmentContentViewModel);

                List<DepartmentGridViewModel> lst = new List<DepartmentGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etDepartmentContentViewModel.DEPARTMENT_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                    var listdb = db.USP_GET_DEPARTMENT(
                       etDepartmentContentViewModel.DEPARTMENT_ID,
                        etDepartmentContentViewModel.DEPARTMENT_NAME,
                        etDepartmentContentViewModel.LANGUAGE_ID,
                        etDepartmentContentViewModel.IS_ACTIVE,
                        etDepartmentContentViewModel.START_ROW,
                        etDepartmentContentViewModel.END_ROW,
                        etDepartmentContentViewModel.TOP,
                        etDepartmentContentViewModel.ENTRY_DATE,
                        etDepartmentContentViewModel.ENTRY_USER_ID,
                        etDepartmentContentViewModel.MODIFICATION_USER_ID,
                        etDepartmentContentViewModel.Modification_Date
                        ).ToList();

                    List<DepartmentGridViewModel> Departments = Mapper.Map<List<USP_GET_DEPARTMENT_Result>, List<DepartmentGridViewModel>>(listdb);

                    lst = Departments;
                };

                return lst;

            }

        public String[] DeleteDepartment(DepartmentViewModel etDepartmentViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteDepartmentBuisnessLogic(etDepartmentViewModel);
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
                        var etDepartmentByLanguage = db.DEPARTMENT_BY_LANGUAGE.Where(p => p.DEPARTMENT_ID == etDepartmentViewModel.DEPARTMENT_ID).ToList();
                        if (etDepartmentByLanguage.Count > 0)
                        {
                            db.DEPARTMENT_BY_LANGUAGE.RemoveRange(etDepartmentByLanguage);
                        }
                        db.SaveChanges();
                        var etDepartment = db.DEPARTMENTs.Where(p => p.DEPARTMENT_ID == etDepartmentViewModel.DEPARTMENT_ID).FirstOrDefault();
                        if (etDepartment != null)
                        {
                            db.DEPARTMENTs.Remove(etDepartment);
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

        public string[] SaveDepartment(DepartmentViewModel etDepartmentViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveDepartmentBuisnessLogic(etDepartmentViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DEPARTMENT etDepartment = new DEPARTMENT();
            DEPARTMENT_BY_LANGUAGE etDepartmentByLanguageDb = new DEPARTMENT_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etDepartmentViewModel.DEPARTMENT_ID != 0 && etDepartmentViewModel.DEPARTMENT_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etDepartmentViewModel.ENTRY_USER_ID = etDepartmentViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etDepartmentdb = db.DEPARTMENTs.Where(p => p.DEPARTMENT_ID.ToString() == etDepartmentViewModel.DEPARTMENT_ID.ToString()).FirstOrDefault();
                            etDepartment = Mapper.Map<DepartmentViewModel, DEPARTMENT>(etDepartmentViewModel);

                            if (etDepartment != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etDepartmentViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etDepartmentViewModel.DEPARTMENT_NAME != null)
                                {
                                    count++;
                                    properties[count] = "DEPARTMENT_NAME";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etDepartmentdb, etDepartment, true,
                                      properties
                                     );
                                etDepartmentdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etDepartmentByLanguageDb = db.DEPARTMENT_BY_LANGUAGE.Where(p => p.DEPARTMENT_ID == etDepartment.DEPARTMENT_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                DEPARTMENT_BY_LANGUAGE etDepartmentByLanguage = new DEPARTMENT_BY_LANGUAGE();
                                etDepartmentByLanguage = etDepartmentByLanguageDb;
                                etDepartmentByLanguage.DEPARTMENT_NAME = etDepartment.DEPARTMENT_NAME;
                                etDepartmentByLanguage.IS_ACTIVE = etDepartment.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etDepartmentByLanguageDb, etDepartmentByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etDepartment = Mapper.Map<DepartmentViewModel, DEPARTMENT>(etDepartmentViewModel);

                            etDepartment.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etDepartment.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.DEPARTMENTs.Add(etDepartment);
                            db.SaveChanges();
                            DEPARTMENT_BY_LANGUAGE erTranslation = new DEPARTMENT_BY_LANGUAGE();
                            erTranslation.DEPARTMENT_NAME = etDepartment.DEPARTMENT_NAME;
                            erTranslation.DEPARTMENT_ID = etDepartment.DEPARTMENT_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etDepartment.IS_ACTIVE;
                            db.DEPARTMENT_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etDepartment.DEPARTMENT_ID.ToString();
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

        public string DeleteDepartmentBuisnessLogic(DepartmentViewModel etDepartment)
        {
            string message = string.Empty;
            if (etDepartment.DEPARTMENT_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListDepartmentBuisnessLogic(DepartmentContentViewModel etDepartmentContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveDepartmentBuisnessLogic(DepartmentViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Department", NavigationUrl = Utilities.GetUrl("Departments", "Department", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DEPARTMENT, DepartmentViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_DEPARTMENT_Result, DepartmentGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<DepartmentViewModel, DEPARTMENT>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<DepartmentGridViewModel, USP_GET_DEPARTMENT_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }

}
