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
    
    public partial class USP_Get_Degree_Guarantee_Result
    {
        public System.Guid Degree_Guarantee_ID { get; set; }
        public string Degree_Guarantee_Name { get; set; }
        public string NAME { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<System.Guid> Entry_Users_ID { get; set; }
        public bool Is_Active { get; set; }
        public Nullable<System.DateTime> Modification_Date { get; set; }
        public Nullable<System.Guid> Modification_Users_ID { get; set; }
    }
}
