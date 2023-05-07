using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
namespace SyndicateIT.Model.ViewModel.SetupManagement.PhoneType
{
    public class PhoneTypeGridViewModel : USP_GET_Phone_Type_Result
    {
        public String PhoneType_EncryptId { get; set; } = "";
    }
}
