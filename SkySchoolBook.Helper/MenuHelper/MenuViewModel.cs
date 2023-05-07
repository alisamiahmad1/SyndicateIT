using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Helper
{
    [Serializable]
    public class MenuViewModel
    {
        #region Properties

        public int ID { get; set; }

        public string MenuDescription { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int MenuItemOrder { get; set; }

        public string MenuItemIndex { get; set; }

        public int MenuLevel { get; set; }

        public bool IsMenuItem { get; set; }

        public string MenuItemType { get; set; }

        public Nullable<bool> IsSecure { get; set; }

        public MenuType MenuTypeID
        {
            get
            {
                if (MenuItemType == "Administration")
                    return MenuType.Administration;
                else if (MenuItemType == "Profiles")
                    return MenuType.Profiles;
                else if (MenuItemType == "Files")
                    return MenuType.Files;
                else if (MenuItemType == "Reports")
                    return MenuType.Reports;
                if (MenuItemType == "SchoolSettings")
                    return MenuType.SchoolSettings;
                else if (MenuItemType == "Services")
                    return MenuType.Services;
                else if (MenuItemType == "Channels")
                    return MenuType.Channels;
                else if (MenuItemType == "Queues")
                    return MenuType.Queues;
                else if (MenuItemType == "Financial")
                    return MenuType.Financial;
                else if (MenuItemType == "Exam")
                    return MenuType.Exam;
                else if (MenuItemType == "StudentGrades")
                    return MenuType.StudentGrades;
                else if (MenuItemType == "StudentLevels")
                    return MenuType.StudentLevels;
                else if (MenuItemType == "Dashboard")
                    return MenuType.Dashboard;
                else if (MenuItemType == "Setup")
                    return MenuType.Setup;
                else if (MenuItemType == "Outlook")
                    return MenuType.Outlook;
                else if (MenuItemType == "PredefinedSetup")
                    return MenuType.PredefinedSetup;
                else
                    return MenuType.Administration;

            }
        }

        #endregion
    }
}
