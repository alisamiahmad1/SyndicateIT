using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.RoleManagement;
using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SyndicateIT.Model.DomainModel.RoleManagementDomainModel
{
    [Serializable]
    public class RoleManagementDomainModel : DomainModelBase
    {
        #region Public Methods


        public bool CreateRoleManagement(GridRoleManagementViewModel model, out string id, out string message)
        {
            message = string.Empty;
            id = "";
            if (model.Name != string.Empty && model.Description != string.Empty && model.Name != null && model.Description != null)
            {

                using (var db = new SyndicatDataEntities())
                {
                    try
                    {
                        var role = db.AspNetRoles.Where(p => p.Name == model.Name).FirstOrDefault();

                        if (role != null)
                        {
                            message = "The Role Name already exists.";
                            return false;


                        }
                        else
                        {
                            AspNetRole etAspNetRoles = new AspNetRole();
                            etAspNetRoles.Name = model.Name;
                            etAspNetRoles.Description = model.Description;
                            etAspNetRoles.Id = Guid.NewGuid().ToString();
                            db.AspNetRoles.Add(etAspNetRoles);
                            db.SaveChanges();
                            id = etAspNetRoles.Id;
                            SessionContent.AppStore.RoleList = GetRoleList(db);
                            SessionContent.AppStore.RoleCodeList = GetRoleCodeList(db);
                            db.Dispose();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        id = "";
                        return false;

                    }

                }
            }
            return true;
        }


        public GridRoleManagementViewModel GetRoleManagement(GridRoleManagementViewModel model)
        {
            GridRoleManagementViewModel etRole = new GridRoleManagementViewModel();
            using (var db = new SyndicatDataEntities())
            {
                var roleDb = db.AspNetRoles.Where(p => p.Id == model.Id).FirstOrDefault();

                if (roleDb != null)
                {
                    etRole = new GridRoleManagementViewModel()
                    {
                        Description = roleDb.Description,
                        Id = roleDb.Id,
                        Name = roleDb.Name,
                    };
                }
            }

            return etRole;

        }

        public bool UpdateRoleManagement(GridRoleManagementViewModel model, out string message)
        {
            message = string.Empty;
            if (model.Name != string.Empty && model.Description != string.Empty && model.Name != null && model.Description != null)
            {
                using (var db = new SyndicatDataEntities())
                {
                    try
                    {
                        var role = db.AspNetRoles.Where(p => p.Name == model.Name && p.Id != model.Id).FirstOrDefault();

                        if (role != null)
                        {
                            message = "The Role Name already exists.";
                            return false;


                        }
                        else
                        {
                            AspNetRole etAspNetRoles = new AspNetRole();
                            etAspNetRoles.Name = model.Name;
                            etAspNetRoles.Description = model.Description;
                            etAspNetRoles.Id = model.Id;
                            db.AspNetRoles.Attach(etAspNetRoles);
                            db.Entry(etAspNetRoles).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            SessionContent.AppStore.RoleList = GetRoleList(db);
                            SessionContent.AppStore.RoleCodeList = GetRoleCodeList(db);
                            db.Dispose();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return false;

                    }

                }
            }

            return true;

        }

        public List<GridRoleManagementViewModel> GetRoleManagementList(bool isRefresh)
        {
            List<GridRoleManagementViewModel> lst = new List<GridRoleManagementViewModel>();
            if (!isRefresh)
            {
                lst = SessionContent.Container.RoleManagement.ListRoleManagement;
            }
            else
            {
                using (var db = new SyndicatDataEntities())
                {

                    var rolesList = db.AspNetRoles.Where(p => p.Name != "SuperAdministrator").ToList();

                    if (rolesList != null && rolesList.Count > 0)
                    {
                        for (int i = 0; i < rolesList.Count; i++)
                        {
                            var model = new GridRoleManagementViewModel()
                            {
                                Description = rolesList[i].Description,
                                Name = rolesList[i].Name,
                                Id = rolesList[i].Id,
                            };

                            lst.Add(model);
                        }

                    }
                }
            }
            SessionContent.Container.RoleManagement.ListRoleManagement = lst;
            return lst;
        }

        public RoleManagementContentViewModel RoleManagementContent()
        {
            RoleManagementContentViewModel model = new RoleManagementContentViewModel();
            model.Navigation = GetRoleManagementContentNavigationList();
            model.Title = "Role Management ";
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";
            return model;
        }

        #endregion

        #region Private Methods


        private List<NavigationViewModel> GetRoleManagementContentNavigationList()
        {
            var model = new List<NavigationViewModel>();
            model.Add(new NavigationViewModel() { NavigationName = "Administration", NavigationUrl = "" });
            model.Add(new NavigationViewModel() { NavigationName = "Role Management" });

            return model;
        }


        private List<SelectListItem> GetRoleList(SyndicatDataEntities db)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            var dbList = db.AspNetRoles.Where(p => p.Name != "SuperAdministrator").ToList();
            for (int i = 0; i < dbList.Count; i++)
            {
                lst.Add(new SelectListItem()
                {
                    Text = dbList[i].Name,
                    Value = dbList[i].Id.ToString(),
                });
            }

            return lst;
        }

        private List<SelectListItem> GetRoleCodeList(SyndicatDataEntities db)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            var dbList = db.AspNetRoles.ToList();
            for (int i = 0; i < dbList.Count; i++)
            {
                lst.Add(new SelectListItem()
                {
                    Text = dbList[i].Name,
                    Value = dbList[i].Id.ToString(),
                });
            }

            return lst;
        }

        #endregion
    }
}