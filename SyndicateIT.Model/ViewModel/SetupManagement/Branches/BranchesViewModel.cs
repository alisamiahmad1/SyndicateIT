using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Branches
{
    public class BranchesViewModel : SyndicateIT.DataLayer.DataContext.Branch
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }

            
        public String BranchesTitle
        {
            get
            {
                if (BranchId != 0 && BranchId.ToString() != "")
                {
                    return TypeAction.Edit + "Branches";
                }
                else
                {
                    return TypeAction.Add + "Branches";
                }
            }
        }
    }
}
