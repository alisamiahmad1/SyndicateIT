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
    
    public partial class Company_Addresses
    {
        public int Company_Add_ID { get; set; }
        public Nullable<int> Country_ID { get; set; }
        public Nullable<int> Kaddaa_ID { get; set; }
        public Nullable<int> Region_ID { get; set; }
        public Nullable<int> Place_ID { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Note { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Company Company1 { get; set; }
        public virtual Kaddaa Kaddaa { get; set; }
        public virtual Place Place { get; set; }
        public virtual Region Region { get; set; }
    }
}
