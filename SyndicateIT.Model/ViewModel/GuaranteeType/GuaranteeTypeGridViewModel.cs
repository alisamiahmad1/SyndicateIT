using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.GuarantyType
{
 public   class GuaranteeTypeGridViewModel : USP_Get_TypeGuarantee_Result
    {
        public String GuaranteeType_EncryptId { get; set; } = "";
    }
}
