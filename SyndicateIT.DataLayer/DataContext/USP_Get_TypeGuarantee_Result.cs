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
    
    public partial class USP_Get_TypeGuarantee_Result
    {
        public System.Guid Type_Guarantee_Id { get; set; }
        public string Type_Guarantee_By_Language_Name { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<System.Guid> Entry_Users_Id { get; set; }
        public Nullable<System.DateTimeOffset> Entry_Users_Date { get; set; }
        public Nullable<System.Guid> Modification_Users_Id { get; set; }
        public Nullable<System.DateTimeOffset> Modification_Users_Date { get; set; }
    }
}
