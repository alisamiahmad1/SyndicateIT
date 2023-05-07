using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Status
{
    public class StatusGridViewModel : USP_GET_Status_Result
    {
        public String Status_EncryptId { get; set; } = "";
    }
}
