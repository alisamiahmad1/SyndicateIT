using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Region
{
    public class RegionViewModel : DataLayer.DataContext.Region
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String RegionTitle
        {
            get
            {
                if (Region_ID != 0 && Region_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Region";
                }
                else
                {
                    return TypeAction.Add + "Region";
                }
            }
        }
    }
}
