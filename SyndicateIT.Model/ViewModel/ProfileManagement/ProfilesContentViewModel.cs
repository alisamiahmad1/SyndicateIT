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
    public class ProfilesContentViewModel : ViewModelBase
    {
        #region Properties    

        public string Title { get; set; }

        public string UserType { get; set; }

        public string ClassTitle { get; set; }

        public ProfilesSearchViewModel ProfilesSearch { get; set; }

        public ProfilesViewModel Profiles { get; set; }


        public Guid ProfileId { get { return SessionContent.Container.Profiles.CurrentProfilesID; } }

        #endregion

        #region Constructor

        public ProfilesContentViewModel()
        {
            Navigation = new List<NavigationViewModel>();
            Alert = new AlertViewModel();
            ProfilesSearch = new ProfilesSearchViewModel();
            Profiles = new ProfilesViewModel();
        }


        #endregion

    }
}
