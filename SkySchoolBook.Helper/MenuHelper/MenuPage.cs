using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Helper
{
    [Serializable()]
    public class MenuPage
    {
        #region Properties


        public string Text { get; set; }


        public string CallBackFunction { get; set; }

        public List<MenuViewModel> MenuItems { get; set; }


        public List<MenuItem> Menus
        {
            get
            {
                return GetMenuItems(MenuItems);
            }
        }

        #endregion

        #region Methods


        private List<MenuItem> GetMenuItems(List<MenuViewModel> lstMenuItems)
        {
            //Filter from the returned menus, all the parent (Top level) menus with level 1
            int LEVEL_ONE = 1;
            List<MenuItem> menuItemList = (from menus in lstMenuItems
                                           where menus.MenuLevel == LEVEL_ONE
                                           orderby menus.MenuItemOrder
                                           select new MenuItem()
                                           {
                                               MenuID = menus.ID,
                                               MenuDescription = menus.MenuDescription,
                                               Controller = menus.Controller,
                                               Action = menus.Action,
                                               MenuOrder = menus.MenuItemOrder,
                                               MenuIndex = menus.MenuItemIndex,
                                               MenuLevel = menus.MenuLevel,
                                               IsMenuItem = menus.IsMenuItem,
                                               MenuTypeID = menus.MenuTypeID,
                                               MenuSubList = null,
                                               MenuClass = menus.MenuLevel == LEVEL_ONE && menus.MenuItemOrder == LEVEL_ONE ? "Active" : "",
                                               IsSecure = ConfigurationManager.AppSettings["IsSecure"] != "" ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSecure"]) : (bool?)null,
                                           }).ToList();
            for (int i = 0; i < menuItemList.Count; i++)
            {
                if (string.IsNullOrEmpty(menuItemList[i].Controller))
                {
                    GetChildMenuItems(menuItemList[i], lstMenuItems);
                }
            }
            menuItemList = menuItemList.Distinct().ToList();
            return menuItemList;
        }


        private void GetChildMenuItems(MenuItem menuItem, List<MenuViewModel> lstMenuItems)
        {
            //Get Direct child menu items            
            List<MenuViewModel> lstChildNodes = (from menus in lstMenuItems
                                                 where menus.MenuItemIndex.StartsWith(menuItem.MenuIndex + ".") &&
                                                 menus.MenuLevel == (menuItem.MenuLevel + 1) &&
                                                 menus.IsMenuItem
                                                 orderby menus.MenuItemOrder
                                                 select menus).ToList();
            MenuItem newMenuItem = null;
            //Fill Child Menu Items
            for (int i = 0; i < lstChildNodes.Count; i++)
            {
                newMenuItem = new MenuItem()
                {
                    MenuID = lstChildNodes[i].ID,
                    MenuDescription = lstChildNodes[i].MenuDescription,
                    Controller = lstChildNodes[i].Controller,
                    Action = lstChildNodes[i].Action,
                    MenuOrder = lstChildNodes[i].MenuItemOrder,
                    MenuIndex = lstChildNodes[i].MenuItemIndex,
                    IsMenuItem = lstChildNodes[i].IsMenuItem,
                    MenuSubList = null
                };

                if (menuItem.MenuSubList == null)
                    menuItem.MenuSubList = new List<MenuItem>();

                menuItem.MenuSubList.Add(newMenuItem);
                //Recursivly fill child nodes
                if (string.IsNullOrEmpty(lstChildNodes[i].Controller))
                    GetChildMenuItems(newMenuItem, lstMenuItems);
            }
        }


        #endregion


    }
}
