using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.UserManagement
{
    [Serializable]
    public class UserManagementContentViewModel : ViewModelBase
    {
        #region Properties    


        public string Title { get; set; }


        public string ClassTitle { get; set; }



        #endregion

        #region Constructor

        public UserManagementContentViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
        }


        #endregion
    }
}
