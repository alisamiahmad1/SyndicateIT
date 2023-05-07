using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
   
    [Serializable]
    public class SystemPrivilegesViewModel : ViewModelBase
    {
        #region Properties    
        public bool HasLoginUser{ get; set; } 
        public bool HasEmail { get; set; }       
        public string FirstName { get; set; }     
        public string LastName { get; set; }    
        public string Email { get; set; }      
        public string MobileNumber { get; set; }    
        public Guid UserId { get; set; }
        public string Type { get; set; }              
        public bool Enable { get; set; }   
        public string Username { get; set; }           
        public string RoleID { get; set; }
        public string RoleName{ get; set; }      
        public DateTime CreationDate { get; set; }        
        public string Password { get; set; }      
        public string ConfirmPassword { get; set; }

        #endregion

    }

}
