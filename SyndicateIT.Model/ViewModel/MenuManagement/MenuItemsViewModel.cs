using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.MenuManagement
{
    [Serializable]
    public class MenuItemsViewModel : ViewModelBase
    {
        #region Properties  


        public int MenuID { get; set; }

        public string MenuDescription { get; set; }

        public string MenuAction { get; set; }

        public List<MenuItemsViewModel> MenuSubList { get; set; }

        public bool HasMenu { get; set; }

        public string Checked
        {
            get
            {
                if (HasMenu)
                    return "checked='checked'";
                else
                    return "";
            }
        }

        public string RoleID { get; set; }

        #endregion

    }
}
