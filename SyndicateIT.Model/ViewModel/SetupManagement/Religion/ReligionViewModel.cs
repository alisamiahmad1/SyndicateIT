using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Religion
{
   public  class ReligionViewModel: RELIGION
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }

        public String CountryTitle
        {
            get
            {
                if (Religion_ID != 0 && Religion_ID.ToString() != "")
                {
                    return TypeAction.Edit + "RELIGION";
                }
                else
                {
                    return TypeAction.Add + "RELIGION";
                }
            }
        }
    }
}