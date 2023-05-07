using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Divisiion
{
   public class DivisionContentViewModel
    {
        public AlertViewModel Alert { get; set; }
        public Boolean IS_REFRESH { get; set; }
        public string Title { get; set; }
        public string ClassTitle { get; set; } = "";
        public List<NavigationViewModel> Navigation { get; set; }
        public int Division_ID { get; set; } = -1;
        public int Cycle_ID { get; set; } = -1;
        public int Language_ID { get; set; } = -1;
        public string Division_Name { get; set; } = "";
        public string Class_ID { get; set; } = "";
        public bool IS_ACTIVE { get; set; } = true;
        public Nullable<System.DateTime> ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; }= "";
        public Nullable<System.DateTime> MODIFICATION_DATE { get; set; }= Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
    
    }
}
