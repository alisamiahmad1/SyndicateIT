//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyndicateIT.DataLayer.DataContext
{
    using System;
    
    public partial class USP_GET_User_Relations_Result
    {
        public int User_Relation_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<System.Guid> Relative_ID { get; set; }
        public Nullable<int> Relation_Type_ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> HasEmergencyContact { get; set; }
        public Nullable<bool> IsMemberOfSchool { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
