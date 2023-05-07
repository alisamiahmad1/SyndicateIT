using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.User_Documents;

namespace SyndicateIT.Model.DomainModel.User_Documents
{
    public class User_DocumentsDomainModel : DomainModelBase
    {

        public List<User_DocumentsViewModel> GetListUser_Documents(Guid id)
        {
            List<User_DocumentsViewModel> lst = new List<User_DocumentsViewModel>();
            using (var db = new SyndicatDataEntities())
            {
                var lstDb = db.Tbl_User_Documents.Where(p => p.User_ID == id).ToList();
                lst = Mapper.Map<List<Tbl_User_Documents>, List<User_DocumentsViewModel>>(lstDb);
            }
            return lst;
        }

        public User_DocumentsContentViewModel GetUser_DocumentsContent()
        {
            User_DocumentsContentViewModel etUser_DocumentsContentViewModel = new User_DocumentsContentViewModel();
            return etUser_DocumentsContentViewModel;
        }

        public User_DocumentsViewModel GetUser_DocumentsViewModel(String User_Document_ID)
        {
            User_DocumentsViewModel model = new User_DocumentsViewModel();
            if (User_Document_ID != null && User_Document_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etUser_Document = db.Tbl_User_Documents.Where(p => p.User_Document_ID.ToString() == User_Document_ID).FirstOrDefault();
                    model = Mapper.Map<Tbl_User_Documents, User_DocumentsViewModel>(etUser_Document);
                }

            }
            return model;
        }

        // GETLIST Function
        public string GetListUser_DocumentsBuisnessLogic(User_DocumentsContentViewModel etUser_DocumentsContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public List<User_DocumentsGridViewModel> GetListUser_Documents(User_DocumentsContentViewModel etUser_DocumentsContentViewModel, bool isAPI = true)
        {
            string[] message = new string[2];
            message[1] = GetListUser_DocumentsBuisnessLogic(etUser_DocumentsContentViewModel);
            List<User_DocumentsGridViewModel> list = new List<User_DocumentsGridViewModel>();

            using (var db = new SyndicatDataEntities())
            {
                var Listdb = db.USP_GET_User_Documents(
                etUser_DocumentsContentViewModel.User_Document_ID,
                etUser_DocumentsContentViewModel.User_ID,
                etUser_DocumentsContentViewModel.Document_Extension_ID,

                    etUser_DocumentsContentViewModel.IS_ACTIVE,
                    etUser_DocumentsContentViewModel.START_ROW,
                    etUser_DocumentsContentViewModel.END_ROW,
                    etUser_DocumentsContentViewModel.Top,
                    etUser_DocumentsContentViewModel.ENTRY_DATE,
                    etUser_DocumentsContentViewModel.ENTRY_USER_ID,
                    etUser_DocumentsContentViewModel.MODIFICATION_USER_ID,
                    etUser_DocumentsContentViewModel.Modification_Date
                    ).ToList();
                List<User_DocumentsGridViewModel> User_Document = Mapper.Map<List<USP_GET_User_Documents_Result>, List<User_DocumentsGridViewModel>>(Listdb);
                list = User_Document;
            };
            return list;
        }

        // DELETE Function
        public string DeleteUser_DocumentsBuisnessLogic(User_DocumentsViewModel etUser_Documents)
        {
            string message = string.Empty;
            if (etUser_Documents.User_Document_ID.ToString() == null)
            {
                message = "There is no record";
            }
            return message;
        }

        public string[] DeleteUser_Documents(User_DocumentsViewModel etUser_DocumentsViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteUser_DocumentsBuisnessLogic(etUser_DocumentsViewModel);

            if (message[1] != null)
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
                        var etUser_Documents = db.Tbl_User_Documents.Where(p => p.User_Document_ID == etUser_DocumentsViewModel.User_Document_ID).FirstOrDefault();
                        if (etUser_Documents != null)
                        {
                            db.Tbl_User_Documents.Remove(etUser_Documents);
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
        public string SaveUser_DocumentsBuisnessLogic(User_DocumentsViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        public string[] SaveUser_Documents(User_DocumentsViewModel etUser_DocumentsViewModel, bool isAPI = true, string fname = "")
        {
            string[] messages = new string[2];
            messages[1] = SaveUser_DocumentsBuisnessLogic(etUser_DocumentsViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }

            Tbl_User_Documents etUser_Documents = new Tbl_User_Documents();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        {
                            // Need to add ENTRY_USER_ID 
                            //   etFitnessLevelViewModel.ENTRY_DATE = DateTime.Now;
                            // Need to add code to enter record in FitnessLevel_status with status id=1(pending)  
                            etUser_Documents = Mapper.Map<User_DocumentsViewModel, Tbl_User_Documents>(etUser_DocumentsViewModel);
                            var ar = etUser_Documents.Document_Path.Split('.');
                            var ext = ar[ar.Length - 1];
                            var existe = (from DocumentExtension in db.Document_Extension
                                          where DocumentExtension.Extension_Type == ext
                                          select DocumentExtension.Extension_Type
                                          )
                                          .ToList().Count > 0;

                            var pExtension = db.Document_Extension.Where(p => p.Extension_Type == ext).FirstOrDefault();
                            if (pExtension != null)
                            {
                                etUser_Documents.Document_Extension_ID = pExtension.Document_Ext_ID;
                            }

                            if (!existe)
                            {
                                messages[0] = "e";
                                messages[1] = "invalid Document Extension";
                                return messages;
                            }

                            //etUser_Documents.Document_Extension_ID  = 
                            etUser_Documents.ENTRY_DATE = DateTime.Now;
                            etUser_Documents.IS_ACTIVE = true;

                            if (SessionContent.Container.Login.UserID != null)
                            {
                                etUser_Documents.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Tbl_User_Documents.Add(etUser_Documents);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etUser_Documents.User_Document_ID.ToString();
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

            Mapper.CreateMap<Tbl_User_Documents, User_DocumentsViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_User_Documents_Result, User_DocumentsGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<User_DocumentsViewModel, Tbl_User_Documents>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<User_DocumentsGridViewModel, USP_GET_User_Documents_Result>()
               .IgnoreAllNonExisting();

            #endregion

            base.InitializeMapper();
        }

    }
}
