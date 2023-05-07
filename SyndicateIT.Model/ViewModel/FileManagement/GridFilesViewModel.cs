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
    public class GridFilesViewModel : ViewModelBase
    {
        #region Properties 


        public Guid ProfileID { get; set; }
              
        public DateTime DateBirth { get; set; }

         public bool Enabled { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateBirthString
        {
            get
            {
                return DateBirth.ToString(Constants.DATETIMEFORMAT);
            }
        }

       
        #endregion
    }
}
