using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Corporate
{
    public class CorporateGridViewModel : USP_GET_Corporate_Result
    {
        public String Corporate_EncryptId { get; set; } = "";
    }
}
