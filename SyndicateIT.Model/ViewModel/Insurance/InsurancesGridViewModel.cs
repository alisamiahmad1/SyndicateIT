using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.Insurance
{
   public class InsurancesGridViewModel: USP_Insurance_Result
    {
        public String Insurances_EncryptId { get; set; } = "";
    }
}
