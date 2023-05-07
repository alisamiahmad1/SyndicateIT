using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Branches
{
    public class BranchesGridViewModel : USP_GET_Branches_Result
    {
        public String Branches_EncryptId { get; set; } = "";
    }
}
