using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.RelationTypes
{
    public class RelationTypesViewModel : Relation_Types
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String RelationTypesTitle
        {
            get
            {
                if (Relation_Type_ID != 0 && Relation_Type_ID.ToString() != "")
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
