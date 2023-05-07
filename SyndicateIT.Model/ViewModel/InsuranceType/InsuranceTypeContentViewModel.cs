using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.InsuranceType
{
    public class InsuranceTypeContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }

        public string Type_Insurance_Id { get; set; } = "";
        public string Type_Insurance_Name { get; set; } = "";

        public int Is_Active { get; set; } = -1;


        public string Entry_User_Id { get; set; } = "";
        public DateTime Entry_User_Date { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public string Modification_User_Id { get; set; } = "";

        public DateTime Modification_User_Date { get; set; } = Convert.ToDateTime("1/1/1900").Date;



        public int Languages_Id { get; set; } = -1;


    }
}
