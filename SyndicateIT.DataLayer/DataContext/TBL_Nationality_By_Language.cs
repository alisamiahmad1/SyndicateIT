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
    
    public partial class TBL_Nationality_By_Language
    {
        public int Nationality_By_Language_ID { get; set; }
        public Nullable<int> Nationality_ID { get; set; }
        public string Nationality_Title { get; set; }
        public string Nationality_Description { get; set; }
        public Nullable<int> Language_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
