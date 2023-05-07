using SyndicateIT.Model.DomainModel;
using SyndicateIT.Model.DomainModel.MenuManagement;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.MenuManagement;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateIT.Controllers
{
    [Serializable]
    public class MenuManagementController :  BaseController
    {
        public ActionResult MenuManagement()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Administration" && p.Action == "MenuManagement") != null && SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Administration" && p.Action == "MenuManagement").Count() > 0 ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            return View("Index", new MenuManagementDomainModel().GetMenuManagementContent());
        }

        public ActionResult MenuTemplatesEdit(string roleId)
        {  
             return PartialView("~/Views/MenuManagement/_MenuTemplates.cshtml", new MenuManagementDomainModel().MenuTemplatesEdit(roleId));            
        }      

        public ActionResult SaveMenuManagement(List<MenuItemRoleViewModel> currentTemplateRole)
        {
            string message = string.Empty;
            AlertType alertType = AlertType.Success;
            var model = new MenuManagementDomainModel().SaveMenuManagement(currentTemplateRole, out message, out alertType);
            var alertModel = new AlertViewModel() { HasAlert = true, AlertType = alertType, Message = message };
            return PartialView("~/Views/Shared/_AlertPartial.cshtml", alertModel);

        }

    }
}
