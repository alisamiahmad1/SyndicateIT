using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.MenuManagement
{
    [Serializable]
    public class MenuItemRoleViewModel : ViewModelBase
    {
        #region Properties  

        public string ParentItemId { get; set; }
        public string ChildItemId { get; set; }
        public string RoleId { get; set; }

        #endregion
    }
}
