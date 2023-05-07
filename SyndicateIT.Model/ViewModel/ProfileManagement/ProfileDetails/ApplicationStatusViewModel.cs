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
    public class ApplicationStatusViewModel : ViewModelBase
    {
        #region Properties 
        public System.Guid ApplicationStatus_Id { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public bool HasStatus { get; set; }
        public string ApplicationStatus_Description { get; set; }
        public string Role { get; set; }   
        public bool HasAccepted { get; set; }
        public string ApplicationName { get; set; }
        public bool IsEdit
        {
            get
            {
                if ((SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion || SessionContent.Container.Profiles.SourceID == (int)FileType.EnrolmentCommitte || SessionContent.Container.Profiles.SourceID == (int)FileType.FinancialCommitte) && SessionContent.Container.Profiles.PersonalInformations != null && SessionContent.Container.Profiles.PersonalInformations.User_ID != null)
                    return true;
                else
                    return false;
            }
        }

        public string Status
        {
            get
            {
                if (HasStatus == true)
                    return "Accepted";
                else
                    return "Rejected";
            }
        }

        public string StatusClass
        {
            get
            {
                if (HasStatus == true)
                    return "center-block padding-5 label label-success";
                else
                    return "center-block padding-5 label label-danger";
            }
        }

        public Nullable<System.DateTime> Modification_Date { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<System.Guid> Entry_Users_Id { get; set; }
        public Nullable<System.Guid> Modification_Users_Id { get; set; }
        public Nullable<System.Boolean> IsActive { get; set; }
        public List<ApplicationStatusViewModel> ApplicationStatusHistories { get; set; }

        

        #endregion
    }
}
