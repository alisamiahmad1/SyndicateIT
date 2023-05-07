using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.InsuranceType
{
   public class InsuranceTypeGridViewModel   : USP_Get_TypeInsurance_Result
    {

        public String InsuranceType_EncryptId { get; set; } = "";
    }
}
