using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SyndicateIT.Model.DomainModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SyndicateIT.Model.UtilityModel;
using SyndicateIT.Model.ViewModel.UserManagement;
using SyndicateIT.Models;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.DomainModel.RoleManagementDomainModel;
using SyndicateIT.Model.ViewModel.RoleManagement;


namespace SyndicateIT.Controllers
{
    /// <summary>
    /// Class UserManagementController.
    /// </summary>
    [Serializable]
    [Authorize]
    public class RoleManagementController : BaseController
    {
        #region Init

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagementController"/> class.
        /// </summary>
        public RoleManagementController()
        {

        }

        #endregion

        #region Action


        public ActionResult RoleManagement()
        {
            bool hasMenu = SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Administration" && p.Action == "RoleManagement") != null && SessionContent.Container.Login.MenuPage.MenuItems.Where(p => p.MenuItemType == "Administration" && p.Action == "RoleManagement").Count() > 0 ? true : false;
            if (hasMenu == false)
                return View("~/Views/Manage/Error.cshtml");
            SessionContent.Container.RoleManagement = new Model.UtilityModel.Session.ContainerEntities.RoleManagement.RoleManagement();
            return View("Index", new RoleManagementDomainModel().RoleManagementContent());
        }

        public ActionResult RoleManagement_Read([DataSourceRequest] DataSourceRequest request)
        {
            bool isRefresh = SessionContent.Container.RoleManagement.ListRoleManagement != null ? false : true;
            var l_LimitCodeDefinition = new RoleManagementDomainModel().GetRoleManagementList(isRefresh);
            return Json(l_LimitCodeDefinition.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RoleManagement_Create([DataSourceRequest] DataSourceRequest request, GridRoleManagementViewModel role)
        {


            string message = string.Empty;
            bool isSuccess = true;
            string id = "";
            if (role != null && ModelState.IsValid)
            {
                isSuccess = new RoleManagementDomainModel().CreateRoleManagement(role, out id, out message);
            }

            if (isSuccess)
            {
                role.Id = id;
                return Json(new[] { role }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("grid_error", message);
                return Json(new[] { role }.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RoleManagement_Update([DataSourceRequest] DataSourceRequest request, GridRoleManagementViewModel role)
        {
            string message = string.Empty;
            bool isSuccess = true;
            if (role != null && ModelState.IsValid)
            {
                isSuccess = new RoleManagementDomainModel().UpdateRoleManagement(role, out message);
            }

            if (isSuccess)
            {

                return Json(new[] { role }.ToDataSourceResult(request, ModelState));

            }
            else
            {
                ModelState.AddModelError("grid_error", message);
                // var model = new RoleManagementDomainModel().GetRoleManagement(role);
                return Json(new[] { role }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RoleManagement_Destroy([DataSourceRequest] DataSourceRequest request, GridRoleManagementViewModel role)
        {
            string message = string.Empty;
            bool isSuccess = true;
            if (role != null && ModelState.IsValid)
            {
                isSuccess = new RoleManagementDomainModel().UpdateRoleManagement(role, out message);
            }

            if (isSuccess)
            {

                return Json(new[] { role }.ToDataSourceResult(request, ModelState));

            }
            else
            {
                ModelState.AddModelError("grid_error", message);
                var model = new RoleManagementDomainModel().GetRoleManagement(role);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }



        #endregion

        #region Methode




        #endregion
    }
}
