using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
  public  class PersonalInformationsContentViewModel
    {

        public System.Nullable<Guid>  User_ID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string FirstName { get; set; } = "";
        public string MIddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MotherName { get; set; } = "";
        public string FirstNameName_Ar { get; set; } = "";
        public string MiddleName_Ar { get; set; } = "";
        public string LastName_Ar { get; set; } = "";
        public string MotherName_Ar { get; set; } = "";
        public string Email { get; set; } = "";
        public Nullable<System.DateTime> Date_Of_Birth { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public Nullable<int> Genders_Id { get; set; } = -1;
        public Nullable<int> Country_ID { get; set; } = -1;
        public Nullable<int> Religions_ID { get; set; } = -1;
        public Nullable<int> Maritals_ID { get; set; } = -1;
        public Nullable<int> Subjects_ID { get; set; } = -1;
        public Nullable<int> Companys_ID { get; set; } = -1;
        public System.Nullable<Guid> Roles_ID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Facebook_ID { get; set; } = "";
        public string Google_ID { get; set; } = "";
        public string Photo { get; set; } = "";
        public Nullable<int> Place_Of_Birth { get; set; } = -1;
        public string IDNumber { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public Nullable<int> FirstNationality { get; set; } = -1;
        public Nullable<int> SecondNationality { get; set; } = -1;
        public Nullable<int> Bus_id { get; set; } = -1;
        public Nullable<int> PlaceId { get; set; } = -1;
        public Nullable<decimal> User_LONGITUDE { get; set; } = -1;
        public Nullable<decimal> User_LATITUDE { get; set; } = -1;
        public int IS_BLACKLISTED { get; set; } = -1;
        public string AspNetUsers_Id { get; set; } = "";
        public string linkedin { get; set; } = "";
        public string twitter { get; set; } = "";
        public Nullable<int> SpeakingProficiencyArabic { get; set; } = -1;
        public Nullable<int> ReadingProficiencyArabic { get; set; } = -1;
        public Nullable<int> WritingProficiencyArabic { get; set; } = -1;
        public Nullable<int> SpeakingProficiencyAnglais { get; set; } = -1;
        public Nullable<int> ReadingProficiencyAnglais { get; set; } = -1;
        public Nullable<int> WritingProficiencyAnglais { get; set; } = -1;
        public Nullable<int> SpeakingProficiencyFrench { get; set; } = -1;
        public Nullable<int> ReadingProficiencyFrench { get; set; } = -1;
        public Nullable<int> WritingProficiencyFrench { get; set; } = -1;
        public Nullable<int> IS_ACTIVE { get; set; } = -1;
        public Nullable<System.DateTime> ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; } = "";
        public Nullable<System.DateTime> MODIFICATION_DATE { get; set; } = Convert.ToDateTime("1/1/1900").Date;
    }
}
