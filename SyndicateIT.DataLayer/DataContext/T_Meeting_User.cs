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
    
    public partial class T_Meeting_User
    {
        public System.Guid User_Id { get; set; }
        public long Meeting_Id { get; set; }
        public bool Is_Joined { get; set; }
        public Nullable<System.DateTime> Joined_At { get; set; }
    
        public virtual T_Meeting T_Meeting { get; set; }
    }
}
