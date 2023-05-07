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
    public class DependentViewModel : ViewModelBase
    {
        #region Properties


      

        [Display(Name = "Title")]
        public string Title { get; set; }

        public int User_Relation_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<System.Guid> Relative_ID { get; set; }
        public Nullable<int> Relation_Type_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> HasEmergencyContact { get; set; }
        public Nullable<bool> IsMemberOfSchool { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }

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

        public List<DependentViewModel> Dependents { get; set; }

        public string Relation_NAME {
            get; set;
        }
        #endregion
        public string FullName() {
            return FirstName + " " + LastName; 

        }
        public DependentViewModel()
        {
            Dependents = new List<DependentViewModel>();
        }
    }

  
    
}
