using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.DegreeGuarantee
{
   public class DegreeGuaranteeViewModel:Degree_Guarantee
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String DegreeGuaranteeTitle
        {
            get
            {
                if (Degree_Guarantee_ID.ToString() != "0" && Degree_Guarantee_ID.ToString() != "")
                {
                    return TypeAction.Edit + "DegreeGuarantee";
                }
                else
                {
                    return TypeAction.Add + "DegreeGuarantee";
                }
            }
        }
    }
}
