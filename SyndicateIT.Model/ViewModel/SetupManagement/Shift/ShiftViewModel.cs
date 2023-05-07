using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Shift
{
 public   class ShiftViewModel:SHIFT
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String CountryTitle
        {
            get
            {
                if (SHIFT_ID != 0 && SHIFT_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Shift";
                }
                else
                {
                    return TypeAction.Add + "Shift";
                }
            }
        }
    }
}
