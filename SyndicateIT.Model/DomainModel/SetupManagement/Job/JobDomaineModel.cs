using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.SetupManagement.Job;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Job
{
    public class JobDomainModel : DomainModelBase
    {
        public JobContentViewModel GetJobContent()
        {
            JobContentViewModel model = new JobContentViewModel();
            model.Navigation = GetNavigationList("list", "Job");
            model.Title = "Job";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public JobViewModel GetJobViewModel(String Job_ID)
        {
            JobViewModel model = new JobViewModel();
            string title = string.Empty;
            if (Job_ID != null && Job_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etJob = db.JOBs.Where(p => p.JOB_ID.ToString() == Job_ID).FirstOrDefault();
                    model = Mapper.Map<JOB, JobViewModel>(etJob);
                }
                title = "Edit Job";
            }
            else
            {
                title = "New Job";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public JobViewModel GetJobViewModel(JobViewModel model)
        {
            string title = string.Empty;
            if (model.JOB_ID != null && model.JOB_ID > 0)
            {
                title = "Edit Job";
            }
            else
            {
                title = "New Job";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


            public List<JobGridViewModel> GetListJob(JobContentViewModel etJobContentViewModel, bool isAPI = true)
            {
                string[] messages = new string[2];
                messages[1] = GetListJobBuisnessLogic(etJobContentViewModel);

                List<JobGridViewModel> lst = new List<JobGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etJobContentViewModel.JOB_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                    var listdb = db.USP_GET_JOB(
                       etJobContentViewModel.JOB_ID,
                        etJobContentViewModel.JOB_NAME,
                        etJobContentViewModel.LANGUAGE_ID,
                        etJobContentViewModel.IS_ACTIVE,
                        etJobContentViewModel.START_ROW,
                        etJobContentViewModel.END_ROW,
                        etJobContentViewModel.TOP,
                        etJobContentViewModel.ENTRY_DATE,
                        etJobContentViewModel.ENTRY_USER_ID,
                        etJobContentViewModel.MODIFICATION_USER_ID,
                        etJobContentViewModel.Modification_Date
                        ).ToList();

                    List<JobGridViewModel> Jobs = Mapper.Map<List<USP_GET_JOB_Result>, List<JobGridViewModel>>(listdb);

                    lst = Jobs;
                };

                return lst;

            }

        public String[] DeleteJob(JobViewModel etJobViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteJobBuisnessLogic(etJobViewModel);
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
                        var etJobByLanguage = db.JOB_BY_LANGUAGE.Where(p => p.JOB_ID == etJobViewModel.JOB_ID).ToList();
                        if (etJobByLanguage.Count > 0)
                        {
                            db.JOB_BY_LANGUAGE.RemoveRange(etJobByLanguage);
                        }
                        db.SaveChanges();
                        var etJob = db.JOBs.Where(p => p.JOB_ID == etJobViewModel.JOB_ID).FirstOrDefault();
                        if (etJob != null)
                        {
                            db.JOBs.Remove(etJob);
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

        public string[] SaveJob(JobViewModel etJobViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveJobBuisnessLogic(etJobViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            JOB etJob = new JOB();
            JOB_BY_LANGUAGE etJobByLanguageDb = new JOB_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etJobViewModel.JOB_ID != 0 && etJobViewModel.JOB_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etJobViewModel.ENTRY_USER_ID = etJobViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etJobdb = db.JOBs.Where(p => p.JOB_ID.ToString() == etJobViewModel.JOB_ID.ToString()).FirstOrDefault();
                            etJob = Mapper.Map<JobViewModel, JOB>(etJobViewModel);

                            if (etJob != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etJobViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etJobViewModel.JOB_NAME != null)
                                {
                                    count++;
                                    properties[count] = "JOB_NAME";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etJobdb, etJob, true,
                                      properties
                                     );
                                etJobdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etJobByLanguageDb = db.JOB_BY_LANGUAGE.Where(p => p.JOB_ID == etJob.JOB_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                JOB_BY_LANGUAGE etJobByLanguage = new JOB_BY_LANGUAGE();
                                etJobByLanguage = etJobByLanguageDb;
                                etJobByLanguage.JOB_NAME = etJob.JOB_NAME;
                                etJobByLanguage.IS_ACTIVE = etJob.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etJobByLanguageDb, etJobByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etJob = Mapper.Map<JobViewModel, JOB>(etJobViewModel);

                            etJob.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etJob.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.JOBs.Add(etJob);
                            db.SaveChanges();
                            JOB_BY_LANGUAGE erTranslation = new JOB_BY_LANGUAGE();
                            erTranslation.JOB_NAME = etJob.JOB_NAME;
                            erTranslation.JOB_ID = etJob.JOB_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etJob.IS_ACTIVE;
                            db.JOB_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etJob.JOB_ID.ToString();
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

        public string DeleteJobBuisnessLogic(JobViewModel etJob)
        {
            string message = string.Empty;
            if (etJob.JOB_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListJobBuisnessLogic(JobContentViewModel etJobContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveJobBuisnessLogic(JobViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Job", NavigationUrl = Utilities.GetUrl("Index", "Job", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<JOB, JobViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_JOB_Result, JobGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<JobViewModel, JOB>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<JobGridViewModel, USP_GET_JOB_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }

}
