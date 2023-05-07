using SyndicateIT.DataLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Shift
{
   public  class ShiftGridViewModel: USP_GET_SHIFT_Result
    {
        public String SHIFT_EncryptId { get; set; } = "";
    }
}
