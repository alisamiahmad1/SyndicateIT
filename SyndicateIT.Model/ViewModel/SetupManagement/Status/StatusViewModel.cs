using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Status
{
    public class StatusViewModel : DataLayer.DataContext.Status
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String StatusTitle
        {
            get
            {
                if (Status_ID != 0 && Status_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Status";
                }
                else
                {
                    return TypeAction.Add + "Status";
                }
            }
        }
    }
}
