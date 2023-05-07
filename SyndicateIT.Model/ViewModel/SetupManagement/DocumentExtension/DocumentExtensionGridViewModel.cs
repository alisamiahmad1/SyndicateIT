using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.DocumentExtension
{
    public class DocumentExtensionGridViewModel : USP_GET_Document_Extension_Result
    {
        public String DocumentExtension_EncryptId { get; set; } = "";
    }
}
