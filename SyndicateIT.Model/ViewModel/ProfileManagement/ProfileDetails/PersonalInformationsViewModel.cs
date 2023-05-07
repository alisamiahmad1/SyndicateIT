using SyndicateIT.Model.UtilityModel.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
       [Serializable]
    public class PersonalInformationsViewModel 
    {
        #region Properties    
        public List<NavigationViewModel> Navigation { get; set; }
        public AlertViewModel Alert { get; set; }
        public string ClassTitle { get; set; }
        public string Title { get; set; } 
        public System.Guid User_ID { get; set; }
        public string FirstName { get; set; }
        public string MIddleName { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string FirstNameName_Ar { get; set; }
        public string MiddleName_Ar { get; set; }
        public string LastName_Ar { get; set; }
        public string MotherName_Ar { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }

        public Nullable<System.DateTime> Date_Of_Birth1 { get; set; }
        public Nullable<int> Genders_Id { get; set; }
        public Nullable<int> Country_ID { get; set; }
        public Nullable<int> Religions_ID { get; set; }
        public Nullable<int> Maritals_ID { get; set; }
        public Nullable<int> Subjects_ID { get; set; }
        public Nullable<int> Companys_ID { get; set; }
        public Nullable<System.Guid> Roles_ID { get; set; }
        public Nullable<System.Guid>  UserReference_ID { get; set; }
        public string ApplicationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string RegisteryNumber { get; set; }
        public string RegisteryPlace { get; set; }

        public string Facebook_ID { get; set; }
        public string Google_ID { get; set; }
        public string Photo { get; set; }
        public Nullable<int> Place_Of_Birth { get; set; }
        public string UserName { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<int> FirstNationality { get; set; }
        public Nullable<int> SecondNationality { get; set; }
        public Nullable<int> Bus_id { get; set; }
        public Nullable<int> PlaceId { get; set; }
        public Nullable<decimal> User_LONGITUDE { get; set; }
        public Nullable<decimal> User_LATITUDE { get; set; }
        public Nullable<bool> IS_BLACKLISTED { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
        public string AspNetUsers_Id { get; set; }
        public string linkedin { get; set; }
        public string twitter { get; set; }

        public Nullable<int> SpeakingProficiencyArabic { get; set; }
        public Nullable<int> ReadingProficiencyArabic { get; set; }
        public Nullable<int> WritingProficiencyArabic { get; set; }
        public Nullable<int> SpeakingProficiencyAnglais { get; set; }
        public Nullable<int> ReadingProficiencyAnglais { get; set; }
        public Nullable<int> WritingProficiencyAnglais { get; set; }
        public Nullable<int> SpeakingProficiencyFrench { get; set; }
        public Nullable<int> ReadingProficiencyFrench { get; set; }
        public Nullable<int> WritingProficiencyFrench { get; set; }

        public int SpeakingProficiencyArabicCount
        {
            get
            {
                if(SpeakingProficiencyArabic!= null)
                {
                    return Convert.ToInt32(SpeakingProficiencyArabic);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ReadingProficiencyArabicCount
        {
            get
            {
                if (ReadingProficiencyArabic != null)
                {
                    return Convert.ToInt32(ReadingProficiencyArabic);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int WritingProficiencyArabicCount
        {
            get
            {
                if (WritingProficiencyArabic != null)
                {
                    return Convert.ToInt32(WritingProficiencyArabic);
                }
                else
                {
                    return 0;
                }
            }
        }
        ////////////////////////////////////////
        public int SpeakingProficiencyAnglaisCount
        {
            get
            {
                if (SpeakingProficiencyAnglais != null)
                {
                    return Convert.ToInt32(SpeakingProficiencyAnglais);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ReadingProficiencyAnglaisCount
        {
            get
            {
                if (ReadingProficiencyAnglais != null)
                {
                    return Convert.ToInt32(ReadingProficiencyAnglais);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int WritingProficiencyAnglaisCount
        {
            get
            {
                if (WritingProficiencyAnglais != null)
                {
                    return Convert.ToInt32(WritingProficiencyAnglais);
                }
                else
                {
                    return 0;
                }
            }
        }

        ////////////////////////////////////////
        public int SpeakingProficiencyFrenchCount
        {
            get
            {
                if (SpeakingProficiencyFrench != null)
                {
                    return Convert.ToInt32(SpeakingProficiencyFrench);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ReadingProficiencyFrenchCount
        {
            get
            {
                if (ReadingProficiencyFrench != null)
                {
                    return Convert.ToInt32(ReadingProficiencyFrench);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int WritingProficiencyFrenchCount
        {
            get
            {
                if (WritingProficiencyFrench != null)
                {
                    return Convert.ToInt32(WritingProficiencyFrench);
                }
                else
                {
                    return 0;
                }
            }
        }

        public string UserType
        {
            get
            {
                return SessionContent.Container.Profiles.UserType.ToString();
            }
        }
        public string PhoneCode
        {
            get
            {
                return SessionContent.Current.Corporate.PhoneCode;
            }
        }
        public string CountryCode
        {
            get
            {
                return SessionContent.Current.Corporate.CountryCode;
            }
        }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string PhoneAlternateNumber { get; set; }

        public string MobileAlternateNumber { get; set; }

        public bool IsEdit
        {
            get
            {
                if (SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion || SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.MyProfile)
                    return true;
                else
                    return false;
            }
        }

        #endregion
    }
}
