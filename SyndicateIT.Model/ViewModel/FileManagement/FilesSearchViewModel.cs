using SyndicateIT.Model.UtilityModel.Session;
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
    public class FilesSearchViewModel : ViewModelBase
    {
        #region Properties 

        public string  SearchCycle { get; set; }
        public string SearchClass { get; set; }
        public string SearchDivision { get; set; }
        public int ServiceTypeID { get; set; }

        public FileType FileType { get; set; }

        public bool IsLoadFilesContent
        {
            get
            {
                return SessionContent.Container.Files.IsLoadFilesContent;
            }
        }

        public int FileTypeID { get; set; }

        public FilesSearchViewModel()
        {
            Alert = new Shared.AlertViewModel();
            SearchCycle = string.Empty;
            SearchClass = string.Empty;
            SearchDivision = string.Empty;
        }

        #endregion
    }
}
