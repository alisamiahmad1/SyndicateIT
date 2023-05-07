using SyndicateIT.Helper;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Helper
{
    
    [Serializable()]
    public class MenuItem
    {
        /// <summary>
        /// Menu ID
        /// </summary>
        public int MenuID { get; set; }


        /// <summary>
        /// Menu Description
        /// </summary>
        public string MenuDescription { get; set; }

        /// <summary>
        /// Menu Controller
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Menu Action
        /// </summary>
        public string Action { get; set; }

        public string MenuClass { get; set; }

        public string MenuSubClass { get; set; }

        public string MenuIClass
        {
            get
            {
                switch (MenuTypeID)
                {
                    case MenuType.Administration:
                        return "fa fa-lg fa-fw fa-windows";
                    case MenuType.Profiles:
                        return "fa fa-lg fa-fw fa-pencil-square-o";
                    case MenuType.Files:
                        return "fa fa-lg fa-fw fa-list-alt";
                    case MenuType.Reports:
                        return "fa fa-lg fa-fw fa-bar-chart-o";
                    case MenuType.SchoolSettings:
                        return "fa fa-lg fa-fw fa-cube txt-color-blue";
                    case MenuType.Services:
                        return "fa fa-lg fa-fw fa-th";
                    case MenuType.Financial:
                        return "fa fa-lg fa-fw fa-shopping-cart";
                    case MenuType.Exam:
                        return "fa fa-lg fa-fw  fa-book txt-color-blue";
                    case MenuType.Queues:
                        return "fa fa-lg fa-fw fa-cube txt-color-blue";
                    case MenuType.Channels:
                        return "fa fa-lg fa-fw fa-stack-overflow";
                    case MenuType.StudentGrades:
                        return "fa fa-lg fa-fw fa-tasks";
                    case MenuType.StudentLevels:
                        return "fa fa-lg fa-fw fa-list";
                    case MenuType.Dashboard:
                        return "fa fa-lg fa-fw fa-home";
                    case MenuType.Setup:
                        return "fa fa-lg fa-fw fa-inbox";
                    case MenuType.PredefinedSetup:
                        return "fa fa-lg fa-fw fa fa-stack-exchange";
                    case MenuType.Outlook:
                        return "fa fa-lg fa-fw fa fa-envelope-o txt-color-blue";
                    default:
                        return null;
                }
            }
        }


        public string MenuBClass { get { return "collapse-sign"; } }
        public string MenuEmClass { get { return "fa fa-plus-square-o"; } }
        public string MenuSpanClass { get { return "menu-item-parent"; } }
        public string MenuSubSpanClass { get { return "menu-item-parent"; } }

        /// <summary>
        /// Menu Order
        /// </summary>
        public int MenuOrder { get; set; }

        /// <summary>
        /// Gets or sets the index of the menu.
        /// </summary>
        public string MenuIndex { get; set; }

        /// <summary>
        /// Menu Index Level
        /// </summary>
        public int? MenuLevel { get; set; }

        /// <summary>
        /// Menu Sub Items List
        /// </summary>
        public List<MenuItem> MenuSubList { get; set; }

        /// <summary>
        /// Get / Set Is Menu Item flag
        /// </summary>
        public bool IsMenuItem { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has submenu.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has submenu; otherwise, <c>false</c>.
        /// </value>
        public bool HasSubmenu { get { return !MenuSubList.IsNullOrEmpty(); } }

        public Nullable<bool> IsSecure { get; set; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        //public string Url { get { return HasSubmenu ? "#/ui" : Utilities.GetUrl(Action, Controller, null); } }    
        public string Url { get { return HasSubmenu ? "#/ui" : Utilities.GetUrl(Action, Controller, GetMenuRoutes(), IsSecure); } }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is redirect URL.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is redirect URL; otherwise, <c>false</c>.
        /// </value>
        public bool IsRedirectURL { get; set; }

        /// <summary>
        /// Gets or sets the application type ID.
        /// </summary>
        /// <value>
        /// The application type ID.
        /// </value>
        public MenuType MenuTypeID { get; set; }


        #region Methods

        /// <summary>
        /// Gets the menu routes.
        /// </summary>
        /// <returns></returns>
        public object GetMenuRoutes()
        {
            if (Controller == "Reports")
                return new { reset = Utilities.Encrypt("true", true) };

            return null;
        }

        #endregion
    }
}

