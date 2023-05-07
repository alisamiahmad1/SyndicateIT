using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.PhoneType
{
    public class PhoneTypeViewModel : Phone_Type
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String PhoneTypeTitle
        {
            get
            {
                if (Phone_Type_ID != 0 && Phone_Type_ID.ToString() != "")
                {
                    return TypeAction.Edit + "PhoneType";
                }
                else
                {
                    return TypeAction.Add + "PhoneType";
                }
            }
        }
    }
}
