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
    
    public partial class T_Template_Variable
    {
        public long Id { get; set; }
        public long Document_Template_Id { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    }
}
