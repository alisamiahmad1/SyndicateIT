using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SyndicateIT.Model.ViewModel.MenuManagement
{
    [Serializable]
    public class MenuManagementContentViewModel : ViewModelBase
    {
        #region Properties    


        public string Title { get; set; }


        public string ClassTitle { get; set; }

        public List<SelectListItem> RolesList { get; set; }



        #endregion

        #region Constructor

        public MenuManagementContentViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
        }


        #endregion
    }

}
