using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
    public class DependentContentViewModel
    {
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }

        public int User_Relation_ID { get; set; } = -1;
        public string User_ID { get; set; } ="";
        public string Relative_ID { get; set; } = "";
        public Nullable<int> Relation_Type_ID { get; set; } = -1;
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Mobile { get; set; } = "";
        public Nullable<System.DateTime> DateOfBirth { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public Nullable<bool> HasEmergencyContact { get; set; } = false;
        public Nullable<bool> IsMemberOfSchool { get; set; } = false;
        public Nullable<bool> IS_ACTIVE { get; set; } = true;
        public Nullable<System.DateTime> ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public string ENTRY_USER_ID { get; set; } ="";
        public string MODIFICATION_USER_ID { get; set; } ="";
        public Nullable<System.DateTime> MODIFICATION_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
    }
}
