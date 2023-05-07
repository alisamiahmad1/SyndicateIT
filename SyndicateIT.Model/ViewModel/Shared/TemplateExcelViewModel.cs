using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.Shared
{
    [Serializable]
    public class TemplateExcelViewModel : ViewModelBase
    {

        #region Properties   

        public string Title { get; set; }

        public List<string> HeaderNames { get; set; }

        public List<ListViewModel> HeaderValues { get; set; }

        #endregion

        #region Constructor

        public TemplateExcelViewModel()
        {
            HeaderNames = new List<string>();
            HeaderValues = new List<ListViewModel>();
            Title = string.Empty;
        }


        #endregion
    }
}
