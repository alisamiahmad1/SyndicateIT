using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Helper;
using SyndicateIT.Model.UtilityModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.DomainModel.Menu
{
    [Serializable]
    public class MenuDomainModel : DomainModelBase
    {
        #region Public Methods    

        public List<MenuViewModel> GetMenuItem()
        {
            List<MenuViewModel> lst = new List<MenuViewModel>();
            MenuViewModel etMenuViewModel = new MenuViewModel();

            using (var db = new SyndicatDataEntities())
            {

                List<MenuViewModel> lstMenu = (from menuItems in db.MenuItems
                                               join menuItemRoles in db.MenuItemRoles on menuItems.ID equals menuItemRoles.MenuItem_ID
                                               join role in db.AspNetRoles on menuItemRoles.Role_ID equals role.Id
                                               join userRole in db.AspNetUserRoles on role.Id equals userRole.RoleId
                                               where userRole.UserId == SessionContent.Container.Login.UserGuidID.ToString()
                                               select
                                               new MenuViewModel
                                               {
                                                   Action = menuItems.Action,
                                                   Controller = menuItems.Controller,
                                                   MenuDescription = menuItems.Description,
                                                   IsMenuItem = menuItems.IsMenuItem,
                                                   MenuItemOrder = menuItems.MenuItemOrder,
                                                   MenuItemIndex = menuItems.MenuItemIndex,
                                                   MenuLevel = menuItems.MenuItemLevel,
                                                   ID = menuItems.ID,
                                                   MenuItemType = menuItems.MenuType,
                                                   IsSecure = SessionContent.Current.Corporate.IsSecure,
                                               }).Distinct<MenuViewModel>().ToList();


                if (lstMenu.Count > 0 && lstMenu != null)
                    lst = lstMenu;



            }
            lst = lst.ToList();
            return lst;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
