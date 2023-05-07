using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.DegreeGuarantee
{
  public  class DegreeGuaranteeGridViewModel:USP_Get_Degree_Guarantee_Result
    {
        public String DegreeGuarantee_EncryptId { get; set; } = "";
    }
}
