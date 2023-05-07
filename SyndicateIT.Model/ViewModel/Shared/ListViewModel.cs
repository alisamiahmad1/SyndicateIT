using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.Shared
{
    [Serializable]
    public class ListViewModel : ViewModelBase
    {

        #region Properties   

        public Dictionary<string, string> listData { get; set; }

        #endregion

        #region Constructor

        public ListViewModel()
        {
            listData = new Dictionary<string, string>();
        }


        #endregion
    }
}
