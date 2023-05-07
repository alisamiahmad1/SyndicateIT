using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.MaritalStatus
{
    public class MaritalStatusViewModel : Marital_Status
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String MaritalStatus
        {
            get
            {
                if (Marital_Status_ID != 0 && Marital_Status_ID.ToString() != "")
                {
                    return TypeAction.Edit + "MaritalStatus";
                }
                else
                {
                    return TypeAction.Add + "MaritalStatus";
                }
            }
        }
    }
}
