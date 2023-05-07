using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement
{
    
    [Serializable]
    public class ProfilesSearchViewModel : ViewModelBase
    {
        #region Properties 

        public string SearchCycle { get; set; }
        public string SearchClass { get; set; }
        public string SearchDivision { get; set; }     
        public DateTime SearchFromDate { get; set; }
        public DateTime SearchToDate { get; set; }     
        public string SearchFirstName { get; set; }   
        public string SearchLastName { get; set; }
        public UserType UserType { get; set; }
        public bool IsLoadProfilesContent
        {
            get
            {
                return SessionContent.Container.Profiles.IsLoadProfilesContent;
            }
        }

        public int UserTypeID { get; set; }

        public ProfilesSearchViewModel()
        {
            SearchCycle = "";

            SearchClass = "";

            SearchDivision = "";

        }

        #endregion
    }
}
