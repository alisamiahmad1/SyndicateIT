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
    public class FilesContentViewModel : ViewModelBase
    {
        #region Properties    

        public string Title { get; set; }

        public string FileType { get; set; }

        public string ClassTitle { get; set; }

        public FilesSearchViewModel FilesSearch { get; set; }

        public FilesViewModel Files { get; set; }


        public Guid FilesId { get { return SessionContent.Container.Files.CurrentFilesID; } }

        public string FileTypeName
        {
            get
            {
                if (FileType == SyndicateIT.UtilityComponent.FileType.DataEntryOperator.ToString())
                    return " list of members  pending";
                else if (FileType == SyndicateIT.UtilityComponent.FileType.EnrolmentCommitte.ToString())
                    return "list of members  approved by Data Entry Operator";
                else if (FileType == SyndicateIT.UtilityComponent.FileType.FinancialCommitte.ToString())
                    return "list of members  approved by Enrolment Committe";
                else
                    return "";
            }
        }

        public string CommitteName
        {
            get
            {
                if (FileType == SyndicateIT.UtilityComponent.FileType.DataEntryOperator.ToString())
                    return " Data Entry Operator";
                else if (FileType == SyndicateIT.UtilityComponent.FileType.EnrolmentCommitte.ToString())
                    return "Enrolment Committe";
                else if (FileType == SyndicateIT.UtilityComponent.FileType.FinancialCommitte.ToString())
                    return "Financial Committe";
                else
                    return "";
            }
        }

        #endregion

        #region Constructor

        public FilesContentViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
            FilesSearch = new FilesSearchViewModel();
            Files = new FilesViewModel();
        }


        #endregion

    }
}
