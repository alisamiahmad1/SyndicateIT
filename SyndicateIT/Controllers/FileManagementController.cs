using SkySchoolBook.Model.DomainModel.ProfileManagement;
using SyndicateIT.Controllers;
using SyndicateIT.Model.DomainModel.ProfileManagement;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.FileManagement;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateIT.Controllers
{
    public class FileManagementController : BaseController
    {
        public ActionResult EnrolmentCommitte()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Files" && p.Action == "EnrolmentCommitte") != null && SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Files" && p.Action == "EnrolmentCommitte").Count() > 0 ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");

            @ViewBag.FileType = "EnrolmentCommitte";
            SessionContent.Container.Files = new Model.UtilityModel.Session.ContainerEntities.Files.Files();
            SessionContent.Container.Files.FileType = FileType.EnrolmentCommitte;
            var model = new FileManagementDomainModel().GetFiles(FileType.EnrolmentCommitte);
            return View("~/Views/FileManagement/Index.cshtml", model);
        }

        public ActionResult FinancialCommitte()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Files" && p.Action == "FinancialCommitte") != null && SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Files" && p.Action == "FinancialCommitte").Count() > 0 ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            @ViewBag.FileType = "FinancialCommitte";
            SessionContent.Container.Files = new Model.UtilityModel.Session.ContainerEntities.Files.Files();
            SessionContent.Container.Files.FileType = FileType.FinancialCommitte;
            var model = new FileManagementDomainModel().GetFiles(FileType.FinancialCommitte);
            return View("~/Views/FileManagement/Index.cshtml", model);
        }

              
        public ActionResult LoadFilesContent()
        {
            SessionContent.Container.Files.IsLoadFilesContent = true;
            return PartialView("~/Views/FileManagement/Files/_FilesContentPartial.cshtml", new FileManagementDomainModel().FilesContent(SessionContent.Container.Files.FilesSearch.FileType));
        }

        #region File List 

        public ActionResult LoadFilesList(string searchFileType= "")
        {
            AlertType alertType = AlertType.Success;
            string message = string.Empty;
            bool isSuccess = true;
            SessionContent.Container.Files.IsLoadFilesContent = false;
            SessionContent.Container.Files.ListFiles = null;
            var model = new FileManagementDomainModel().GetListFileViewModel(searchFileType,  out message, out isSuccess, out alertType);
            if (isSuccess)
            {
                  return PartialView("~/Views/FileManagement/Files/_GridFilesPartial.cshtml", model);
            }
            else
            {
                var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
                return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);
            }

        }

        #endregion

        #region File Details 

        public ActionResult LoadFilesContentDetails(string fileId, string fileType)
        {

            if (fileType == FileType.EnrolmentCommitte.ToString())
                SessionContent.Container.Files.FileType = FileType.EnrolmentCommitte;
            else if (fileType == FileType.FinancialCommitte.ToString())
                SessionContent.Container.Files.FileType = FileType.FinancialCommitte;           

            if (fileId != null)
            {
                SessionContent.Container.Files.CurrentFilesID = new Guid(fileId);
            }
            return PartialView("~/Views/FileManagement/Files/_FilesContentDetailsPartial.cshtml", new FileManagementDomainModel().GetFilesContentDetailsViewModel(fileId));
        }
                
        public ActionResult LoadFileDocument()
        {
            var model = new ProfileManagementDomainModel().GetDocumentViewModel(SessionContent.Container.Files.CurrentFilesID);
            return PartialView("~/Views/FileManagement/Files/Details/_FileDocumentPartial.cshtml", model);
        }

        #endregion


    }
}
