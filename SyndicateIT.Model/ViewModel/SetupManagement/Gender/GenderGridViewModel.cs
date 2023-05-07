using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
namespace SyndicateIT.Model.ViewModel.SetupManagement.Gender
{
    public class GenderGridViewModel :USP_GET_Gender_Result
    {
        public String Gender_EncryptId { get; set; } = "";
    }
}
