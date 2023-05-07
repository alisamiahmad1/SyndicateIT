using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
   public class AssignmentContentViewModel
    {
        public Nullable<int> user_Course_ID { get; set; } = -1;
        public Nullable<int> Cycle_ID { get; set; } = -1;
        public Nullable<int> division_ID { get; set; } = -1;
        public string course_ID { get; set; } = "";
        public string class_ID { get; set; } = "";
        public string user_ID { get; set; } = "";
        public  Nullable<bool> is_Active { get; set; } = true;
        public Nullable<int> sTART_ROW { get; set; } = -1;
        public  Nullable<int> eND_ROW { get; set; } = -1; public  Nullable<int> top { get; set; } = -1;
        public Nullable<System.DateTime> entry_Date { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public  string entry_User_ID { get; set; } = "";
        public string modification_User_ID { get; set; } = "";
      public   Nullable<System.DateTime> modification_Date { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
    }
}
