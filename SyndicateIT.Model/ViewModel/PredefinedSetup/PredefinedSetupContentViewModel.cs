using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.PredefinedSetup
{
   
    [Serializable]
    public class PredefinedSetupContentViewModel : ViewModelBase
    {
        #region Properties    

         
        public string Title { get; set; }

        public string ClassTitle { get; set; }

       



        #endregion

        #region Constructor

        public PredefinedSetupContentViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
        

        }

        #endregion

    }
}
