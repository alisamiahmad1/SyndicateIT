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
    
    public partial class USP_Insurance_Result
    {
        public System.Guid Insurance_Id { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<System.Guid> Entry_Users_Id { get; set; }
        public Nullable<System.DateTime> Modification_Date { get; set; }
        public Nullable<System.Guid> Modification_Users_Id { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> HasInsurance { get; set; }
        public Nullable<bool> HasGuarantee { get; set; }
        public Nullable<System.Guid> DegreeGuarantee_Id { get; set; }
        public Nullable<System.Guid> DegreeInsurance_Id { get; set; }
        public Nullable<System.Guid> TypeGuarantee_Id { get; set; }
        public Nullable<System.Guid> Users_Id { get; set; }
        public string ENTRY_Users_NAME { get; set; }
        public string MODIFICATION_Users_NAME { get; set; }
    }
}