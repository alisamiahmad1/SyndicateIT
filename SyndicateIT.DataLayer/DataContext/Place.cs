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
    
    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Place()
        {
            this.Company_Addresses = new HashSet<Company_Addresses>();
        }
    
        public int Place_ID { get; set; }
        public string Place_Name { get; set; }
        public string Place_Description { get; set; }
        public Nullable<int> Country_ID { get; set; }
        public Nullable<int> Kaddaa_ID { get; set; }
        public Nullable<int> Region_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company_Addresses> Company_Addresses { get; set; }
    }
}
