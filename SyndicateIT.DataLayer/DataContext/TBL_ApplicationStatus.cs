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
    
    public partial class TBL_ApplicationStatus
    {
        public System.Guid ApplicationStatus_Id { get; set; }
        public Nullable<System.Guid> Users_Id { get; set; }
        public Nullable<int> Committe_Id { get; set; }
        public string Role { get; set; }
        public Nullable<bool> HasStatus { get; set; }
        public string ApplicationStatus_Description { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<System.Guid> Entry_Users_Id { get; set; }
        public Nullable<System.DateTime> Modification_Date { get; set; }
        public Nullable<System.Guid> Modification_Users_Id { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}