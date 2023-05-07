using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.FileManagement
{
   
    [Serializable]
    public class FileDetailsViewModel : ViewModelBase
    {
        #region Properties    

        public string Street { get; set; }
        public string Place_Name { get; set; }
        public string Region_Name { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public System.Guid User_ID { get; set; }
        public string Class_Title { get; set; }
        public string Division_Name { get; set; }
        public string Cycle_Title { get; set; }
        public DateTime Date { get; set; }
        public string DateString  { get {return Date.ToString(Constants.DATEFORMAT); }  }
        public Guid FilesId { get { return SessionContent.Container.Files.CurrentFilesID; } }

        public bool IsActive { get; set; }

        #endregion

        #region Constructor

        public FileDetailsViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
            
        }


        #endregion

    }
}
