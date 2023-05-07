using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Kaddaa
{
    public class KaddaaGridViewModel : USP_GET_Kaddaa_Result
    {

        public String Kaddaa_EncryptId { get; set; } = "";
    }
}
