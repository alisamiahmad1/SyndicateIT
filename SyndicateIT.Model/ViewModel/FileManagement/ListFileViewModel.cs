using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.FileManagement
{
   
    [Serializable]
    public class ListFileViewModel : ViewModelBase
    {
        #region Properties 

        public Guid ProfileID { get; set; }
        public string  ProfileName { get; set; }
        public string MobileNumber { get; set; }
        public string CommitteName { get; set; }
        public string ApplicationNumber { get; set; }
        public string RegisteryNumber { get; set; }
        public DateTime ApplicationDate { get; set; }     
        public string Css { get; set; }
        public string ImageUrl { get; set; }

        #endregion
    }
}
