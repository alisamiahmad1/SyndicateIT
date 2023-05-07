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
    public class SchoolHistoryViewModel : ViewModelBase
    {
        #region Properties 
        public int SchoolHistory_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<System.DateTime> FromHistory { get; set; }
        public Nullable<System.DateTime> ToHistory { get; set; }
        public Nullable<System.Guid> ClassName { get; set; }
        public Nullable<int> CycleName { get; set; }
        public string Location { get; set; }
        public string SchoolName { get; set; }
        public string Description { get; set; }
        public string StageNameS { get; internal set; }
        public string ClassNameS { get; internal set; }

        #endregion
    }
}
