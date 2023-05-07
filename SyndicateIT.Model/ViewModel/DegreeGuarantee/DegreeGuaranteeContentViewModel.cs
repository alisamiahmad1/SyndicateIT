using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.DegreeGuarantee
{
  public  class DegreeGuaranteeContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }
        public Boolean IS_REFRESH { get; set; }
        public string Degree_Guarantee_ID { get; set; } = "";
        public int LANGUAGE_ID { get; set; } =-1;
        public string Degree_Guarantee_Name { get; set; } = "";
        public string NAME { get; set; } = "";
        public string ENTRY_USER_ID { get; set; } = "";
        public DateTime ENTRY_DATE_FROM { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public DateTime ENTRY_DATE_TO { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public DateTime MODIFICATION_DATE_FROM { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public DateTime MODIFICATION_DATE_TO { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public string MODIFICATION_USER_ID { get; set; } = "";
        public bool IS_ACTIVE { get; set; } = true;
        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;
    }
}
