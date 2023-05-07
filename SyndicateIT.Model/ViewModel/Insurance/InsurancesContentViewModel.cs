using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.Insurance
{
  public  class InsurancesContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }
        public string Insurance_Id { get; set; } = "";
        public Nullable<System.DateTime> Entry_DateTo { get; set; }=  Convert.ToDateTime("1/1/1900 00:00:00 AM").Date;
        public Nullable<System.DateTime> Entry_DateFrom { get; set; } = Convert.ToDateTime("1/1/1900 00:00:00 AM").Date;
        public string Entry_User_Id { get; set; } = "";
        public Nullable<System.DateTime> MODIFICATION_DATEfrom { get; set; } = Convert.ToDateTime("1/1/1900 00:00:00 AM").Date;
        public Nullable<System.DateTime> MODIFICATION_DATETo { get; set; } = Convert.ToDateTime("1/1/1900 00:00:00 AM").Date;
        public string Modification_User_Id { get; set; } = "";
        public int IsActive { get; set; } = -1;
        public int HasInsurance { get; set; } = -1;
        public int HasGuarantee { get; set; } = -1;
        public string DegreeGuarantee_Id { get; set; } = "";
        public string DegreeInsurance_Id { get; set; } = "";
        public string TypeGuarantee_Id { get; set; } = "";
        public string User_Id { get; set; } = "";
        public string ENTRY_USER_NAME { get; set; } = "";
        public string MODIFICATION_USER_NAME { get; set; } = "";
    }
}
