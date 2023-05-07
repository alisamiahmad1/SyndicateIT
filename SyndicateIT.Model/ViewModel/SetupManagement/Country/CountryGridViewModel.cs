using SyndicateIT.DataLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Country
{
    public class CountryGridViewModel : USP_GET_Country_Result
    {
        public String Country_EncryptId { get; set; } = "";
    }
}
