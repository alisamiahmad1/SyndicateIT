using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.Profile
{
    public class LoginResult
    {
        public string USER_ID { get; set; } = "";
        public string FULLNAME { get; set; } = "";
        public string ROLE_ID { get; set; } = "";
        public int DETAILS_FILLED { get; set; } = -1;
        public string STUDENT_TYPE_ID { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string FirstName_Ar { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string MiddleName_Ar { get; set; } = "";
        public string LastName { get; set; } = "";
        public string LastName_Ar { get; set; } = "";
        public string MotherName { get; set; } = "";
        public string MotherName_Ar { get; set; } = "";
        public string Profile_Url { get; set; } = "";

        public string ClassName { get; set; }
        public string DivisionName { get; set; }
        public string CycleName { get; set; }
        public int DivisionId { get; set; }
        public int CycleId { get; set; }
        public Guid ClassId { get; set; }
        public string Css { get; set; }
        public string ImageUrl { get; set; }
        public Boolean isparent { get; set; } = false;
    }
}
