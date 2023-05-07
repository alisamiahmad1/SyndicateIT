using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;


namespace SyndicateIT.Model.ViewModel.SetupManagement.Corporate
{
    public class CorporateViewModel : DataLayer.DataContext.Corporate
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String CorporateTitle
        {
            get
            {
                if (ID != 0 && ID.ToString() != "")
                {
                    return TypeAction.Edit + "Corporate";
                }
                else
                {
                    return TypeAction.Add + "Corporate";
                }
            }
        }
    }
}
