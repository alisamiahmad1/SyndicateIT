using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.User_Relation
{
    public class User_RelationContentViewModel
    {
        public int User_Relation_ID { get; set; } = -1;
        public string User_ID { get; set; } = "";
        public string Relative_ID { get; set; } = "";
        public int Relation_Type_ID { get; set; } = -1;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Mobile { get; set; } = "";
        public bool HasEmergency { get; set; } = true;
        public bool IsMember { get; set; } = true;
        public int Language_Id { get; set; } = -1;
        public Boolean IS_ACTIVE { get; set; } = true;
        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int Top { get; set; } = -1;
        public Nullable<System.DateTime> ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; } = "";
        public Nullable<System.DateTime> Modification_Date { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public Nullable<System.DateTime> DateOfBirth { get; set; } = Convert.ToDateTime("1/1/1900").Date;
        public Boolean IS_REFRESH { get; set; } = true;

    }
}
