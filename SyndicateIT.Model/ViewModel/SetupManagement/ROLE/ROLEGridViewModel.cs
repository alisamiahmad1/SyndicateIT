using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.ROLE
{
    public class ROLEGridViewModel : USP_GET_ROLE_Result
    {
        public String ROLE_EncryptId { get; set; } = "";
    }
}
