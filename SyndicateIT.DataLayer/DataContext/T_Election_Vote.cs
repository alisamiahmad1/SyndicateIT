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
    
    public partial class T_Election_Vote
    {
        public string Vote_Key { get; set; }
        public string Signature { get; set; }
        public long Candidate_Id { get; set; }
        public int Election_Id { get; set; }
    
        public virtual T_Candidate T_Candidate { get; set; }
        public virtual T_Election T_Election { get; set; }
    }
}