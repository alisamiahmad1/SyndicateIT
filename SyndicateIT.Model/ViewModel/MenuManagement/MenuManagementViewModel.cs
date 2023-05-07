using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.MenuManagement
{
    [Serializable]
    public class MenuManagementViewModel : ViewModelBase
    {
        #region Properties

        public string RoleName { get; set; }
        public List<MenuItemsViewModel> Menus { get; set; }

        #endregion
    }
}
