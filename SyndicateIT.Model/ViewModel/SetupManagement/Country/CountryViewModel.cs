using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Country
{
   public class CountryViewModel : COUNTRY
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String CountryTitle
        {
            get
            {
                if (COUNTRY_ID != 0 && COUNTRY_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Country";
                }
                else
                {
                    return TypeAction.Add + "Country";
                }
            }
        }
    }
}
