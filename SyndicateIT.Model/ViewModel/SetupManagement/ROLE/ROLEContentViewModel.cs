using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.ROLE
{
    public class ROLEContentViewModel
    {

        public List<NavigationViewModel> Navigation { get; set; }
        public Boolean IS_REFRESH { get; set; }
        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }
        public int LANGUAGE_ID { get; set; } = 1;

        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;

        public bool IS_ACTIVE { get; set; } = true;
        public DateTime ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; } = "";
        public DateTime MODIFICATION_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;

        public string Role_ID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string ROLE_NAME { get; set; } = "";
        public int Company_ID { get; set; } = -1;

    }
}
