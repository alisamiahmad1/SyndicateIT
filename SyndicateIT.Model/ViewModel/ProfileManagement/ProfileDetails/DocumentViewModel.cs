using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.User_Documents;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
       [Serializable]
    public class DocumentViewModel : Tbl_User_Documents
    {
        #region Properties    
        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string PhoneCode
        {
            get
            {
                return SessionContent.Current.Corporate.PhoneCode;
            }
        }


        public string CountryCode
        {
            get
            {
                return SessionContent.Current.Corporate.CountryCode;
            }
        }

        public List<User_DocumentsViewModel> ListUser_Documents { get; set; } = new List<User_DocumentsViewModel>();

        public bool IsEdit
        {
            get
            {
                if (SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.MyProfile || (SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion && SessionContent.Container.Profiles.PersonalInformations != null && SessionContent.Container.Profiles.PersonalInformations.User_ID != null))
                    return true;
                else
                    return false;
            }
        }

        #endregion

    }
}
