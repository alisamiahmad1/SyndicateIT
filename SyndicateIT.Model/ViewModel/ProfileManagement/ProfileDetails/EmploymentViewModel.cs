using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
    [Serializable]
    public class EmploymentViewModel : ViewModelBase
    {
        #region Properties 

      
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }


        public string ClassTitle { get; set; }

        public string Title { get; set; }      

        public int User_Employment_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<int> JOB_ID { get; set; }
        public Nullable<int> SHIFT_ID { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public Nullable<int> DEPARTMENT_ID { get; set; }
        public Nullable<int> STATUS_ID { get; set; }
        public Nullable<int> LicenseNumber { get; set; }
        public string Grade { get; set; }
        public string Division { get; set; }
        public Nullable<int> EmployeeNumber { get; set; }
        public Nullable<int> HPD { get; set; }
        public Nullable<int> WD { get; set; }
        public Nullable<bool> IS_INTERNATIONAL { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTime> LicenseIssuingDate { get; set; }
        public Nullable<System.DateTime> Contract_Date { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
        public bool IsEdit
        {
            get
            {
                if (SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion && SessionContent.Container.Profiles.PersonalInformations != null && SessionContent.Container.Profiles.PersonalInformations.User_ID != null)
                    return true;
                else
                    return true;
            }
        }

        #endregion
    }
}
