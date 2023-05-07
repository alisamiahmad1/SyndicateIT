using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.RelationTypes
{
    public class RelationTypesGridViewModel : USP_GET_Relation_Types_Result
    {
        public String RelationTypes_EncryptId { get; set; } = "";
    }
}
