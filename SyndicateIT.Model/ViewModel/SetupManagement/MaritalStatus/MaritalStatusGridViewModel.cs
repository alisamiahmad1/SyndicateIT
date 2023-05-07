using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.MaritalStatus
{
    public class MaritalStatusGridViewModel : USP_GET_Marital_Status_Result
    {

        public String MaritalStatus_EncryptId { get; set; } = "";
    }
}
