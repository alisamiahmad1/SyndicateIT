using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement
{
   
    [Serializable]
    public class ProfilesContentDetailsViewModel : ViewModelBase
    {
        #region Properties    

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string TitleURL { get; set; }

        public UserType UserType { get; set; }

        public int SourceId { get; set; }

        public string ClassTitle { get; set; }

        public string DataEntryOperatorStatus { get; set; }
        public string EntolmentCommitteStatus { get; set; }
        public string FinancialCommitteStatus { get; set; }
        public string DataEntryOperatorClassCss
        {
            get
            {

                if (DataEntryOperatorStatus == "Not Received")
                    return "txt-color-teal";

                else if (DataEntryOperatorStatus == "Approved")
                    return "txt-color-greenDark";

                else if (DataEntryOperatorStatus == "Rejected")
                    return ".txt-color-red";

                else if (DataEntryOperatorStatus == "Pending")
                    return "txt-color-orangeDark";

                return "";
            }

        }
        public string EntolmentCommitteClassCss
        {
            get
            {

                if (EntolmentCommitteStatus == "Not Received")
                    return "txt-color-teal";

                else if (EntolmentCommitteStatus == "Rejected")
                    return ".txt-color-red";

                else if (EntolmentCommitteStatus == "Approved")
                    return "txt-color-greenDark";

                else if (EntolmentCommitteStatus == "Pending")
                    return "txt-color-orangeDark";

                return "";
            }

        }
        public string FinancialCommitteClassCss
        {
            get
            {

                if (FinancialCommitteStatus == "Not Received")
                    return "txt-color-teal";

                else if (FinancialCommitteStatus == "Approved")
                    return "txt-color-greenDark";

                else if (FinancialCommitteStatus == "Rejected")
                    return ".txt-color-red";


                else if (FinancialCommitteStatus == "Pending")
                    return "txt-color-orangeDark";

                return "";
            }

        }

        public string ClassCss
        {
            get
            {

                if (SessionContent.Container.Profiles.CurrentProfilesID != null && SessionContent.Container.Profiles.CurrentProfilesID.ToString() != "")
                    return "";
                else
                    return "pace";
            }

        }


        public ProfilesSearchViewModel ProfilesSearch { get; set; }

        public ProfilesViewModel Profiles { get; set; }


        public Guid ProfileId { get { return SessionContent.Container.Profiles.CurrentProfilesID; } }

        #endregion

        #region Constructor

        public ProfilesContentDetailsViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
            ProfilesSearch = new ProfilesSearchViewModel();
            Profiles = new ProfilesViewModel();
        }


        #endregion

    }
}
