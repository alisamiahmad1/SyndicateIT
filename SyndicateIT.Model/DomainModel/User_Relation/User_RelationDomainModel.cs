using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.User_Relation;

namespace SyndicateIT.Model.DomainModel.User_Relation
{
    class User_RelationDomainModel : DomainModelBase
    {
        public User_RelationContentViewModel GetUser_RelationContent()
        {
            User_RelationContentViewModel etUser_RelationContentViewModel = new User_RelationContentViewModel();
            return etUser_RelationContentViewModel;
        }

        public User_RelationViewModel GetUser_RelationViewModel(String User_Relation_ID)
        {
            User_RelationViewModel model = new User_RelationViewModel();
            if (User_Relation_ID != null && User_Relation_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etUser_Relation = db.TBL_User_Relations.Where(p => p.User_Relation_ID.ToString() == User_Relation_ID).FirstOrDefault();
                    model = Mapper.Map<TBL_User_Relations, User_RelationViewModel>(etUser_Relation);
                }

            }
            return model;
        }

        // GETLIST Function
        public string GetListUser_RelationBuisnessLogic(User_RelationContentViewModel etUser_RelationContentViewModel)
        {
            string message = string.Empty;
            return message;
        }
        public List<User_RelationGridViewModel> GetListUser_Relation(User_RelationContentViewModel etUser_RelationContentViewModel, bool isAPI = true)
        {
            string[] message = new string[2];
            message[1] = GetListUser_RelationBuisnessLogic(etUser_RelationContentViewModel);
            List<User_RelationGridViewModel> list = new List<User_RelationGridViewModel>();

            using (var db = new SyndicatDataEntities())
            {
                var Listdb = db.USP_GET_User_Relations(
                    etUser_RelationContentViewModel.User_Relation_ID,
                    etUser_RelationContentViewModel.User_ID,
                    etUser_RelationContentViewModel.Relative_ID,
                    etUser_RelationContentViewModel.Relation_Type_ID,
                    etUser_RelationContentViewModel.FirstName,
                    etUser_RelationContentViewModel.Email,etUser_RelationContentViewModel.Mobile,
                    etUser_RelationContentViewModel.HasEmergency,
                    etUser_RelationContentViewModel.DateOfBirth,
                    etUser_RelationContentViewModel.IsMember,
                    etUser_RelationContentViewModel.LastName,
                     etUser_RelationContentViewModel.IS_ACTIVE,
                    etUser_RelationContentViewModel.START_ROW,
                    etUser_RelationContentViewModel.END_ROW,
                    etUser_RelationContentViewModel.Top,
                    etUser_RelationContentViewModel.ENTRY_DATE,
                    etUser_RelationContentViewModel.ENTRY_USER_ID,
                    etUser_RelationContentViewModel.MODIFICATION_USER_ID,
                    etUser_RelationContentViewModel.Modification_Date
                    ).ToList();
                List<User_RelationGridViewModel> User_Relation = Mapper.Map<List<USP_GET_User_Relations_Result>, List<User_RelationGridViewModel>>(Listdb);
                list = User_Relation;
            };
            return list;
        }

        // DELETE Function
        public string DeleteUser_RelationBuisnessLogic(User_RelationViewModel etUser_Relation)
        {
            string message = string.Empty;
            if (etUser_Relation.User_Relation_ID.ToString() == null)
            {
                message = "There is no record";
            }
            return message;
        }
        public string[] DeleteUser_Relation(User_RelationViewModel etUser_RelationViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteUser_RelationBuisnessLogic(etUser_RelationViewModel);

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

                        // Delete From Original Table
                        var etUser_Relation = db.TBL_User_Relations.Where(p => p.User_Relation_ID == etUser_RelationViewModel.User_Relation_ID).FirstOrDefault();
                        if (etUser_Relation != null)
                        {
                            db.TBL_User_Relations.Remove(etUser_Relation);
                            db.SaveChanges();
                            message[0] = "s";
                            scope.Complete();
                            return message;
                        }
                        else
                        {
                            message[0] = "e";
                            scope.Dispose();
                            return message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.ToString();
                return message;
            }

        }

        // ADD Function
        public string SaveUser_RelationBuisnessLogic(User_RelationViewModel model)
        {
            string message = string.Empty;
            return message;
        }
        public string[] SaveUser_Relation(User_RelationViewModel etUser_RelationViewModel, bool isAPI = true)
        {
            string[] messages = new string[2];
            messages[1] = SaveUser_RelationBuisnessLogic(etUser_RelationViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            TBL_User_Relations etUser_Relation = new TBL_User_Relations();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etUser_RelationViewModel.User_Relation_ID!=0 && etUser_RelationViewModel.User_Relation_ID.ToString() != "")
                        {
                            
                            var etUser_Relationdb = db.TBL_User_Relations.Where(p => p.User_Relation_ID.ToString() == etUser_RelationViewModel.User_Relation_ID.ToString()).FirstOrDefault();                            

                            if (etUser_Relationdb != null)
                            {
                                etUser_Relation = Mapper.Map<User_RelationViewModel, TBL_User_Relations>(etUser_RelationViewModel);
                                
                                 if (SessionContent.Container.Login.UserID != null)
                                    {
                                        etUser_RelationViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                    }
                                   string[] properties = new string[8];

                                int count = -1;

                                if (etUser_RelationViewModel.User_ID != null)
                                {
                                    count++;
                                    properties[count] = "User_ID";
                                }

                                if (etUser_RelationViewModel.Relative_ID != null)
                                {
                                    count++;
                                    properties[count] = "Relative_ID";
                                }

                                if (etUser_RelationViewModel.Relation_Type_ID != null)
                                {
                                    count++;
                                    properties[count] = "Relation_Type_ID";
                                }
                                if (etUser_RelationViewModel.IS_ACTIVE != null)
                                {
                                    count++;
                                    properties[count] = "IS_ACTIVE";
                                }

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etUser_Relationdb, etUser_Relation, true, properties);
                                etUser_Relationdb.MODIFICATION_DATE = DateTimeOffset.Now;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            // Need to add ENTRY_USER_ID 
                            //   etFitnessLevelViewModel.ENTRY_DATE = DateTime.Now;
                            // Need to add code to enter record in FitnessLevel_status with status id=1(pending)  
                            etUser_Relation = Mapper.Map<User_RelationViewModel, TBL_User_Relations>(etUser_RelationViewModel);

                            etUser_Relation.ENTRY_DATE = DateTime.Now;
                           
                                if (SessionContent.Container.Login.UserID != null)
                                {
                                    etUser_Relation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }
                           
                            db.TBL_User_Relations.Add(etUser_Relation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etUser_Relation.User_Relation_ID.ToString();
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

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<TBL_User_Relations, User_RelationViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_User_Relations_Result, User_RelationGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<User_RelationViewModel, TBL_User_Relations>()
               
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<User_RelationGridViewModel, USP_GET_User_Relations_Result>()
               .IgnoreAllNonExisting();

            #endregion

            base.InitializeMapper();
        }

    }
}
