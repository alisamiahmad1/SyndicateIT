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
    
    public partial class USP_GET_User_School_History_Result
    {
        public int SchoolHistory_ID { get; set; }
        public Nullable<System.DateTime> FromHistory { get; set; }
        public Nullable<System.DateTime> ToHistory { get; set; }
        public Nullable<System.Guid> ClassName { get; set; }
        public Nullable<int> StageName { get; set; }
        public string Location { get; set; }
        public string SchoolName { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}