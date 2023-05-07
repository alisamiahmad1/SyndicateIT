using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Divisiion
{
   public class DivisionGridViewModel
    {
        public int Division_ID { get; set; }
        public string Division_Name { get; set; }
        public string Division_EncryptId { get; set; }
        
        public Nullable<System.Guid> Class_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
