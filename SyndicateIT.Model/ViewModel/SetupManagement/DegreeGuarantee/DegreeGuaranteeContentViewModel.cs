using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.DegreeGuarantee
{
    public class DegreeGuaranteeContentViewModel
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }

        public int LANGUAGE_ID { get; set; } = 1;

        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;

        public string Degree_Guarantee_ID { get; set; } = "";
        public string Degree_Guarantee_Name { get; set; } = "";
        public DateTime Modification_Date { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public DateTime Entry_Date { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public bool Is_Active { get; set; } = true;
        public string Entry_Users_ID { get; set; } = "";
        public string Modification_Users_ID { get; set; } = "";
    }
}
