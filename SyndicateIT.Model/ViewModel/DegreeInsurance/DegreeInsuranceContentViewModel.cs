using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.DegreeInsurance
{
    public class DegreeInsuranceContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }

        public int DegreeInsurance_Id { get; set; } = -1;
        public string DegreeInsurance_By_Language_Name { get; set; } = "";
        public int Is_Active { get; set; } = -1;
        
        public int Entry_User_Id { get; set; } = -1;
        public DateTime ENTRY_DATE_FROM { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public DateTime ENTRY_DATE_TO { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public DateTime MODIFICATION_DATE_FROM { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public DateTime MODIFICATION_DATE_TO { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public int Modificartion_Id { get; set; } = -1;
        public string ENTRY_USER_NAME { get; set; } = "";
        public string MODIFICATION_USER_NAME { get; set; } = "";
        public int languageID { get; set; } = -1;
        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;
        public int Owner_Id { get; set; } = -1;

    }
}
