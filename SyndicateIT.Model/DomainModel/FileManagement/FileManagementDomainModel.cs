using SyndicateDataEntities.Model.DomainModel;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.FileManagement;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SkySchoolBook.Model.DomainModel.ProfileManagement
{
    [Serializable]
    public class FileManagementDomainModel : DomainModelBase
    {
        #region Public Methods

        public FilesViewModel GetStudentFile(string userId)
        {
            var model = GetFilesContentDetailsViewModel(userId);
            model.FileType = FileType.FinancialCommitte.ToString();
            return new FilesViewModel()
            {
                PartialViewName = "~/Views/FileManagement/Files/_FilesContentDetailsPartial.cshtml",
                PartialViewNameModel = model,
                Title = "Student File",
            };
        }
        public FilesViewModel GetFiles(FileType fileType)
        {

            return new FilesViewModel()
            {
                PartialViewName = "~/Views/FileManagement/Files/_FilesContentPartial.cshtml",
                PartialViewNameModel = FilesContent(fileType),
                Title = GetTitle(fileType),
            };
        }
        public FilesContentViewModel FilesContent(FileType fileType)
        {
            FilesContentViewModel model = new FilesContentViewModel();
            SessionContent.Container.Files.FileType = fileType;
            model.Navigation = GetFilesNavigationList(fileType);
            model.Title = GetTitle(fileType);
            model.ClassTitle = "fa fa-lg fa-fw fa-list-alt";
            model.FileType = fileType.ToString();
            model.FilesSearch = new FilesSearchViewModel() { FileTypeID = (int)fileType };
            if (SessionContent.Container.Files.FilesSearch != null && SessionContent.Container.Files.IsLoadFilesContent == true)
            {
                model.FilesSearch.SearchCycle = SessionContent.Container.Files.FilesSearch.SearchCycle;
                model.FilesSearch.SearchClass = SessionContent.Container.Files.FilesSearch.SearchClass;
                model.FilesSearch.SearchDivision = SessionContent.Container.Files.FilesSearch.SearchDivision;
                model.FilesSearch.FileType = SessionContent.Container.Files.FilesSearch.FileType;
            }

            return model;
        }
        public List<ListFileViewModel> GetListStudentViewModel(int searchFileType,  out string message, out bool isSuccess, out AlertType alertType)
        {
            message = string.Empty;
            isSuccess = true;
            alertType = AlertType.Success;
            List<ListFileViewModel> model = new List<ListFileViewModel>();

            return model;
        }
        public List<ListFileViewModel> GetListFileViewModel(string searchFileType,  out string message, out bool isSuccess, out AlertType alertType)
        {
            message = string.Empty;
            isSuccess = true;
            alertType = AlertType.Success;
            List<ListFileViewModel> model = new List<ListFileViewModel>();
            if (SessionContent.Container.Files.FileType == FileType.EnrolmentCommitte)
            {
                return GetListFileStudents(searchFileType, out message, out isSuccess);
            }
            else if (SessionContent.Container.Files.FileType == FileType.FinancialCommitte)
            {
                return GetListFileStudents(searchFileType, out message, out isSuccess);
            }

            return model;
        }
        public List<ListFileViewModel> GetListFileStudents(string searchFileType,  out string message, out bool isSuccess)
        {
            message = string.Empty;
            isSuccess = false;
            List<ListFileViewModel> lst = new List<ListFileViewModel>();
            int committeId = 0;

            if (searchFileType == FileType.EnrolmentCommitte.ToString())
            {
                committeId = (int)FileType.DataEntryOperator;
            }
            else if (searchFileType == FileType.FinancialCommitte.ToString())
            {
                committeId = (int)FileType.EnrolmentCommitte;
            }
            try
            {
                using (var db = new SyndicatDataEntities())
                {


                    var users = (from u in db.Users
                                 let cs = db.TBL_ApplicationStatus
                                    .OrderByDescending(p => p.Entry_Date)
                                    .Where(cu => cu.Users_Id == u.User_ID)
                                    .FirstOrDefault()
                                 select new
                                 {
                                     u.User_ID,
                                     u.FirstName,
                                     u.LastName,
                                     u.MobileNumber,
                                     CommitteId = cs.Committe_Id,
                                     CommitteName = cs.Role,
                                     cs.HasStatus,
                                     u.ApplicationDate,
                                     u.ApplicationNumber,
                                     u.RegisteryNumber,

                                 });
       
         
                    var lstUser = users.Where(p => p.CommitteId == committeId && p.HasStatus == true).ToList();
                 
                    for (int i = 0; i < lstUser.Count; i++)
                    {
                        lst.Add(new ListFileViewModel()
                        {                           
                            ProfileID = lstUser[i].User_ID,
                            ProfileName = lstUser[i].FirstName + " " + lstUser[i].LastName,
                            MobileNumber = lstUser[i].MobileNumber,
                            CommitteName = lstUser[i].CommitteName,
                            ApplicationDate = Convert.ToDateTime(lstUser[i].ApplicationDate),
                            ApplicationNumber = lstUser[i].ApplicationNumber,
                            RegisteryNumber = lstUser[i].RegisteryNumber,
                        });
                    }
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
            }

            return lst;
        }

        #region Profile Details

        public FilesContentDetailsViewModel GetFilesContentDetailsViewModel(string fileId)
        {
            FileType fileType = SessionContent.Container.Files.FileType;
            FilesContentDetailsViewModel model = new FilesContentDetailsViewModel();
            model.Navigation = GetFilesDetailsNavigationList(fileType);
            model.Title = GetTitle(fileType);
            model.SubTitle = "File";
            model.ClassTitle = "fa fa-lg fa-fw fa-list-alt";
            model.TitleURL = GetTitleURL(fileType);
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.FileDetails = GetFileDetails(fileId);
            return model;
        }

        public FileDetailsViewModel GetFileDetails(string fileId)
        {
            FileDetailsViewModel etFileDetailsViewModel = new FileDetailsViewModel();
            using (var db = new SyndicatDataEntities())
            {
                var pUseRegistration = db.User_Registration.Where(p => p.User_ID.ToString() == fileId).FirstOrDefault();

                if (pUseRegistration != null)
                {
                    if (SessionContent.Container.Files.FilesSearch == null)
                        SessionContent.Container.Files.FilesSearch = new FilesSearchViewModel();

                   

                    //var etFileDb = db.USP_GET_Student(pUseRegistration.Cycle_ID, pUseRegistration.Class_ID.ToString(), fileId, pUseRegistration.Division_ID, -1).FirstOrDefault();
                    //if (etFileDb != null)
                    //{
                    //    etFileDetailsViewModel = new FileDetailsViewModel()
                    //    {
                    //        Class_Title = etFileDb.Class_Title,
                    //        Division_Name = etFileDb.Division_Name,
                    //        COUNTRY_NAME = etFileDb.COUNTRY_NAME,
                    //        Cycle_Title = etFileDb.Cycle_Title,
                    //        Email = etFileDb.Email,
                    //        FirstName = etFileDb.FirstName,
                    //        LastName = etFileDb.LastName,
                    //        MobileNumber = etFileDb.MobileNumber,
                    //        Place_Name = etFileDb.Place_Name,
                    //        Region_Name = etFileDb.Region_Name,
                    //        Street = etFileDb.Street,
                    //        User_ID = etFileDb.User_ID,
                    //        Date = Convert.ToDateTime(etFileDb.Date_Of_Birth),
                    //        IsActive = Convert.ToBoolean(etFileDb.IS_ACTIVE),
                    //    };
                    //}
                }
            }
            return etFileDetailsViewModel;
        }

     
        #endregion

        #endregion

        #region Private Methods

        private List<NavigationViewModel> GetFilesNavigationList(FileType type)
        {
            var model = new List<NavigationViewModel>();
            model.Add(new NavigationViewModel() { NavigationName = "Files", NavigationUrl = "" });
            model.Add(new NavigationViewModel() { NavigationName = GetTitle(type) });

            return model;
        }

        private List<NavigationViewModel> GetFilesDetailsNavigationList(FileType type)
        {
            var model = new List<NavigationViewModel>();
            string pNavigationUrl = string.Empty;

            pNavigationUrl = Utilities.GetUrl("Students", "FileManagement", SessionContent.Current.Corporate.IsSecure);
            model.Add(new NavigationViewModel() { NavigationName = "Files", NavigationUrl = "" });
            model.Add(new NavigationViewModel() { NavigationName = GetTitle(type), NavigationUrl = pNavigationUrl });
            model.Add(new NavigationViewModel() { NavigationName = GetFileDetailsTitle(type) });        

            return model;
        }

        private string GetFileDetailsTitle(FileType type)
        {
            if (FileType.EnrolmentCommitte == type)
                return "Members Details";
            else if (FileType.FinancialCommitte == type)
                return "Members Details";         
            else
                return "";

        }


        private string GetTitle(FileType type)
        {
            if (FileType.EnrolmentCommitte == type)
                return "Members File";
            else if (FileType.FinancialCommitte == type)
                return "Members File";           
            return "";

        }
        private string GetTitleURL(FileType type)
        {
            if (FileType.EnrolmentCommitte == type)
                return "/FileManagement/EnrolmentCommitte";
            else if (FileType.FinancialCommitte == type)
                return "/FileManagement/FinancialCommitte";
             else
                return "";

        }

        private List<SelectListItem> GetListMonth()
        {

            List<SelectListItem> lstItemMonth = new List<SelectListItem>();
            lstItemMonth.Add(new SelectListItem() { Value = "1", Text = "January" });
            lstItemMonth.Add(new SelectListItem() { Value = "2", Text = "February" });
            lstItemMonth.Add(new SelectListItem() { Value = "3", Text = "March" });
            lstItemMonth.Add(new SelectListItem() { Value = "4", Text = "April" });
            lstItemMonth.Add(new SelectListItem() { Value = "5", Text = "May" });
            lstItemMonth.Add(new SelectListItem() { Value = "6", Text = "June" });
            lstItemMonth.Add(new SelectListItem() { Value = "7", Text = "July" });
            lstItemMonth.Add(new SelectListItem() { Value = "8", Text = "August" });
            lstItemMonth.Add(new SelectListItem() { Value = "9", Text = "September" });
            lstItemMonth.Add(new SelectListItem() { Value = "10", Text = "October" });
            lstItemMonth.Add(new SelectListItem() { Value = "11", Text = "November" });
            lstItemMonth.Add(new SelectListItem() { Value = "12", Text = "December" });

            return lstItemMonth;

        }


        #endregion

    }
}
