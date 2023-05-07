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
    public class FilesContentDetailsViewModel : ViewModelBase
    {
        #region Properties    

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string TitleURL { get; set; }

        public string FileType { get; set; }      

        public string ClassTitle { get; set; }
      
        public FileDetailsViewModel FileDetails { get; set; }

        public Guid FileId { get { return SessionContent.Container.Files.CurrentFilesID; } }

        #endregion

        #region Constructor

        public FilesContentDetailsViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
            FileDetails = new FileDetailsViewModel();
        }


        #endregion

    }
}
