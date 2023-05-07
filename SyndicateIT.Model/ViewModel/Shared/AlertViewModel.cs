using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.Shared
{
    [Serializable]
    public class AlertViewModel : ViewModelBase
    {

        public AlertType AlertType { get; set; }

        public string Message { get; set; }

        public bool HasAlert { get; set; }

        #region Constructor

        public AlertViewModel()
        {
            HasAlert = false;
        }


        #endregion 
    }
}
