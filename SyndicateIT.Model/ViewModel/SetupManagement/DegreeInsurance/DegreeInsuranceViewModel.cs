using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.DegreeInsurance
{
    public class DegreeInsuranceViewModel : Degree_Insurance
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String DegreeInsuranceTitle
        {
            get
            {
                if (Degree_Insurance_Id.ToString() != "0" && Degree_Insurance_Id.ToString() != "")
                {
                    return TypeAction.Edit + "Gender";
                }
                else
                {
                    return TypeAction.Add + "Gender";
                }
            }
        }
    }
}
