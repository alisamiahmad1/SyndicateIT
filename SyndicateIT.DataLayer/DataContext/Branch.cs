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
    using System.Collections.Generic;
    
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public Nullable<long> MobileNumber { get; set; }
        public string Email { get; set; }
        public Nullable<int> Country { get; set; }
        public Nullable<int> City { get; set; }
        public string Address { get; set; }
        public Nullable<System.Guid> User_Id { get; set; }
        public Nullable<System.DateTime> RecordDate { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
