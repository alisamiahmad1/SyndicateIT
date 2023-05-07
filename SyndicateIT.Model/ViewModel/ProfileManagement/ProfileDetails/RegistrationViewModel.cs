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
    public class RegistrationViewModel : ViewModelBase
    {
        #region Properties 
        public int Registration_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<bool> CurrentRegistration { get; set; }
        public Nullable<bool> HasRegistration { get; set; }
        public Nullable<bool> HasCardIssued { get; set; }
        public Nullable<bool> HasCardDelivered { get; set; }
        public string CardNumber { get; set; }
        public string TypeSpecialty { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<System.Guid> Class_ID { get; set; }
        public Nullable<int> Division_ID { get; set; }
        public Nullable<System.DateTime> From { get; set; }
        public Nullable<System.DateTime> To { get; set; }
        public Nullable<System.Guid> PromotionFromClass { get; set; }
        public Nullable<System.Guid> PromotionToClass { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }     
        public SchoolHistoryViewModel SchoolHistory { get; set; }
        public List<SchoolHistoryViewModel> SchoolHistories { get; set; }

        public bool IsEdit
        {
            get
            {
                if (SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion && SessionContent.Container.Profiles.PersonalInformations != null && SessionContent.Container.Profiles.PersonalInformations.User_ID != null)
                    return true;
                else
                    return false;
            }
        }
        public RegistrationViewModel()
        {
            SchoolHistories = new List<SchoolHistoryViewModel>();
            SchoolHistory = new SchoolHistoryViewModel();
        }

        #endregion
    }
}
