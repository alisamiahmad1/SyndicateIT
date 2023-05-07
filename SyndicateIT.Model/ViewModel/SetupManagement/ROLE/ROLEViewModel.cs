using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.ROLE
{
    public class ROLEViewModel : DataLayer.DataContext.ROLE
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String ROLETypesTitle
        {
            get
            {
                if (Role_ID.ToString() != "0" && Role_ID.ToString() != "")
                {
                    return TypeAction.Edit + "RelationTypes";
                }
                else
                {
                    return TypeAction.Add + "RelationTypes";
                }
            }
        }
    }
}
