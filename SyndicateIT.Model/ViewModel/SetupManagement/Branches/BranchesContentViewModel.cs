using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Branches
{
    public class BranchesContentViewModel
    {
        public int language { get; set; } = 1;
        public Boolean IS_REFRESH { get; set; }
        public List<NavigationViewModel> Navigation { get; set; }
        public AlertViewModel Alert { get; set; }
        public string Title { get; set; }
        public string ClassTitle { get; set; }
        public int BranchId { get; set; } = -1;
        public string Name { get; set; } = "";
        public Nullable<long> MobileNumber { get; set; } = -1;
        public string Email { get; set; } = "";
        public string Country { get; set; } = "";
        public string City { get; set; } = "";
        public string Address { get; set; } = "";
        public int User_Id { get; set; } = -1;
        public DateTime RecordDate { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public byte[] RowVersion { get; set; }
        public bool IS_ACTIVE { get; set; } = true;
        public DateTime ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; } = "";
        public DateTime MODIFICATION_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;

        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;
    }
}
