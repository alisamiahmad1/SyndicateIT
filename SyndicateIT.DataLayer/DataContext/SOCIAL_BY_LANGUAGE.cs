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
    
    public partial class SOCIAL_BY_LANGUAGE
    {
        public int Social_ID_By_Language { get; set; }
        public Nullable<int> Social_ID { get; set; }
        public Nullable<int> Language_ID { get; set; }
        public string Social_Type { get; set; }
        public string Social_Description { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
