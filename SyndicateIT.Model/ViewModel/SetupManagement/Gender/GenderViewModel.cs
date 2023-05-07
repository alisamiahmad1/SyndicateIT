using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Gender
{
    public class GenderViewModel : GENDER
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String GenderTitle
        {
            get
            {
                if (GENDER_ID != 0 && GENDER_ID.ToString() != "")
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
