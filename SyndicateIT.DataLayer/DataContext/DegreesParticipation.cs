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
    
    public partial class DegreesParticipation
    {
        public System.Guid DegreesParticipations_ID { get; set; }
        public Nullable<int> Cycle_ID { get; set; }
        public Nullable<System.Guid> Class_ID { get; set; }
        public Nullable<int> Division_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public System.DateTime PeriodDate { get; set; }
        public Nullable<int> Reason_ID { get; set; }
        public Nullable<int> Percentage { get; set; }
        public Nullable<bool> Is_Published { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
