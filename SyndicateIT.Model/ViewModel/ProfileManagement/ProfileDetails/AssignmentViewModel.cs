using SyndicateIT.Model.UtilityModel.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
    [Serializable]
   
    public class AssignmentViewModel : ViewModelBase
    {
        #region Properties 
        public int User_Course_ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<System.Guid> Class_ID { get; set; }
        public Nullable<int> Division_ID { get; set; }
        public Nullable<System.Guid> Course_ID { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }

        public List<JoinCourcesViewModel> JoinCources { get; set; }


        public AssignmentViewModel ()
        {
            JoinCources = new List<JoinCourcesViewModel>();
        }

        #endregion
    }
}
