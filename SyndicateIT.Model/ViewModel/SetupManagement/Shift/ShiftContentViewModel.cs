using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Shift
{
   public class ShiftContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }

        public int SHIFT_ID { get; set; } = -1;
        public string SHIFT_NAME { get; set; } = "";
        public int LANGUAGE_ID { get; set; } = 1;
        public bool IS_ACTIVE { get; set; } = true;
        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;
        public DateTime ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; } = "";
        public DateTime Modification_Date { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;

        public Boolean IS_REFRESH { get; set; }
    }
}
