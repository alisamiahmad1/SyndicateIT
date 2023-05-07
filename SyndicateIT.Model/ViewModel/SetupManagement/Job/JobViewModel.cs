using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Job
{
 public   class JobViewModel : JOB
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String CountryTitle
        {
            get
            {
                if (JOB_ID != 0 && JOB_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Job";
                }
                else
                {
                    return TypeAction.Add + "Job";
                }
            }
        }
    }
}
