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
    
    public partial class T_Candidate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Candidate()
        {
            this.T_Election_Vote = new HashSet<T_Election_Vote>();
        }
    
        public long Id { get; set; }
        public string Full_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Election_Vote> T_Election_Vote { get; set; }
    }
}
