using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Helper;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.MenuManagement;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SyndicateIT.Model.DomainModel.MenuManagement
{
    [Serializable]
    public class MenuManagementDomainModel : DomainModelBase
    {
        #region Public Methods


        public MenuManagementContentViewModel GetMenuManagementContent()
        {
            MenuManagementContentViewModel model = new MenuManagementContentViewModel();
            model.RolesList = SessionContent.AppStore.RoleList;
            model.Navigation = GetMenuManagementContentNavigationList();
            model.Title = "privileges ";
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";
            return model;
        }
        public MenuManagementViewModel MenuTemplatesEdit(string roleId)
        {
            MenuManagementViewModel model = new MenuManagementViewModel();
            model.RoleName = SessionContent.AppStore.RoleList.Where(p => p.Value == roleId).FirstOrDefault().Text;
            model.Menus = GetMenus(roleId);
            return model;
        }

        public bool SaveMenuManagement(List<MenuItemRoleViewModel> currentTemplateRole, out string message, out AlertType alertType)
        {
            message = string.Empty;
            alertType = AlertType.Success;
            try
            {
                using (var db = new SyndicatDataEntities())
                {
                    List<int> parentItemIds = currentTemplateRole.Select(p => Convert.ToInt32(p.ParentItemId)).Distinct().ToList();
                    List<int> childItemIds = currentTemplateRole.Select(p => Convert.ToInt32(p.ChildItemId)).Distinct().ToList();
                    var roleId = currentTemplateRole.Select(p => p.RoleId).Distinct().FirstOrDefault();


                    var menuItemRoles = db.MenuItemRoles.Where(p => p.Role_ID == roleId).ToList();
                    for (int i = 0; i < menuItemRoles.Count(); i++)
                    {
                        var menuItemRole = menuItemRoles[i];
                        db.MenuItemRoles.Remove(menuItemRole);
                    }

                    List<int> items = new List<int>();
                    items.AddRange(parentItemIds);
                    items.AddRange(childItemIds);

                    for (int i = 0; i < items.Count(); i++)
                    {
                        MenuItemRole etMenuItemRole = new MenuItemRole
                        {
                            MenuItem_ID = items[i],
                            Role_ID = roleId,
                        };

                        db.MenuItemRoles.Add(etMenuItemRole);
                    }



                    db.SaveChanges();
                    message = " Menu Item  is successful";
                    alertType = AlertType.Success;

                    return true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                alertType = AlertType.Danger;
            }

            return false;
        }


        #endregion

        #region Private Methods


        public List<MenuItemsViewModel> GetMenus(string roleId)
        {
            var lstMenuItems = SessionContent.Container.Login.MenuPage;

            using (var db = new SyndicatDataEntities())
            {

                List<SelectListItem> menuRole = (from role in db.MenuItemRoles
                                                 where role.Role_ID == roleId
                                                 select new SelectListItem()
                                                 {
                                                     Value = role.Role_ID,
                                                     Text = role.MenuItem_ID.ToString(),
                                                 }).ToList();



                List<Helper.MenuItem> menuItems = (from menus in db.MenuItems
                                                              where menus.IsMenuItem == true
                                                              orderby menus.MenuItemOrder
                                                              select new Helper.MenuItem()
                                                              {
                                                                  MenuID = menus.ID,
                                                                  MenuDescription = menus.Description,
                                                                  MenuOrder = menus.MenuItemOrder,
                                                                  MenuIndex = menus.MenuItemIndex,
                                                                  MenuLevel = menus.MenuItemLevel,
                                                                  IsMenuItem = menus.IsMenuItem,
                                                                  Action = menus.Action,

                                                              }).ToList();





                int LEVEL_ONE = 1;
                List<MenuItemsViewModel> menuItemList = (from menus in menuItems
                                                         where menus.MenuLevel == LEVEL_ONE
                                                         orderby menus.MenuOrder
                                                         select new MenuItemsViewModel()
                                                         {
                                                             MenuID = menus.MenuID,
                                                             MenuDescription = menus.MenuDescription,
                                                             MenuAction = menus.Action,
                                                             RoleID = roleId,
                                                             MenuSubList = GetChildMenuItems(menus.MenuIndex, menuItems, roleId, menuRole),
                                                             HasMenu = menuRole.Where(p => p.Text == menus.MenuID.ToString()).FirstOrDefault()!= null? true :false,

                                                         }).ToList();


                return menuItemList;
            }
        }

        private List<MenuItemsViewModel> GetChildMenuItems(string MenuIndex, List<Helper.MenuItem> menuItems, string roleId, List<SelectListItem> menuRole)
        {
            List<MenuItemsViewModel> lst = new List<MenuItemsViewModel>();

            //Get Direct child menu items            
            List<Helper.MenuItem> lstChildNodes = (from menus in menuItems
                                                              where menus.MenuIndex.StartsWith(MenuIndex + ".") &&
                                                              menus.MenuLevel == 2 && menus.IsMenuItem == true
                                                              orderby menus.MenuOrder
                                                              select menus).ToList();
            MenuItemsViewModel newMenuItem = null;
            //Fill Child Menu Items
            for (int i = 0; i < lstChildNodes.Count; i++)
            {
                bool hasMenu = false;
                var roleMenu = menuRole.Where(p => p.Text == lstChildNodes[i].MenuID.ToString()).FirstOrDefault();
                if (roleMenu != null)
                    hasMenu = true;

                newMenuItem = new MenuItemsViewModel()
                {
                    MenuID = lstChildNodes[i].MenuID,
                    MenuDescription = lstChildNodes[i].MenuDescription,
                    RoleID = roleId,
                    HasMenu = hasMenu
                };

                lst.Add(newMenuItem);
            }

            return lst;
        }

        private List<NavigationViewModel> GetMenuManagementContentNavigationList()
        {
            var model = new List<NavigationViewModel>();
            model.Add(new NavigationViewModel() { NavigationName = "Administration", NavigationUrl = "" });
            model.Add(new NavigationViewModel() { NavigationName = "privileges" });

            return model;
        }




        #endregion

    }
}
