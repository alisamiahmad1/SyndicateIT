using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Corporate
{
    public class CorporateContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }
        public Boolean IS_REFRESH { get; set; }
        public string Title { get; set; }
        public int LANGUAGE_ID { get; set; } = 1;
        public string ClassTitle { get; set; }
        public int ID { get; set; } = -1;
        public string Name { get; set; } = "";
        public string LogoName { get; set; } = "";
        public string CountryCode { get; set; } = "";
        public string PhoneCode { get; set; } = "";
        public string CurrencyCode { get; set; } = "";
        public string Domain { get; set; } = "";
        public string CountryName { get; set; } = "";
        public string CustomerServicePhone { get; set; } = "";
        public string CustomerServiceEmail { get; set; } = "";
        public bool IsSecure { get; set; } = true;
        public int MaxLengthCIF { get; set; } = -1;
        public int MaxLengthMobile { get; set; } = -1;
        public int BankID { get; set; } = -1;
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
