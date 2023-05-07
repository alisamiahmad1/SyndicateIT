using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Kaddaa
{
    public class KaddaaViewModel :DataLayer.DataContext.Kaddaa
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String GenderTitle
        {
            get
            {
                if (STP_Kaddaa_ID != 0 && STP_Kaddaa_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Kaddaa";
                }
                else
                {
                    return TypeAction.Add + "Kaddaa";
                }
            }
        }
    }
}
