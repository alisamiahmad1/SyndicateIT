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
    
    public partial class T_Document_Form_Sub_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Document_Form_Sub_Category()
        {
            this.T_Document_Form_Template = new HashSet<T_Document_Form_Template>();
        }
    
        public int Id { get; set; }
        public int Document_Form_Category_Id { get; set; }
        public string Title { get; set; }
    
        public virtual T_Document_Form_Category T_Document_Form_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Document_Form_Template> T_Document_Form_Template { get; set; }
    }
}
