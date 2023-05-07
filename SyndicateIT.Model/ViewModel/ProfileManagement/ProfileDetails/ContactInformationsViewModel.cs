using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
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
    public class ContactInformationsViewModel : ViewModelBase
    {
        #region Properties 
        public int User_Contact_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public Nullable<int> COUNTRY_ID { get; set; }
        public Nullable<int> STATE_ID { get; set; }
        public Nullable<int> REGION_ID { get; set; }
        public Nullable<int> CITY_ID { get; set; }
        public string TownShip { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string POBox { get; set; }
        public string Fax { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }
        public Nullable<int> Flag { get; set; }

        public int SecondUser_Contact_ID { get; set; }
        public Nullable<System.Guid> SecondUser_ID { get; set; }
        public Nullable<int> SecondCOUNTRY_ID { get; set; }
        public Nullable<int> SecondSTATE_ID { get; set; }
        public Nullable<int> SecondREGION_ID { get; set; }
        public Nullable<int> SecondCITY_ID { get; set; }
        public string SecondTownShip { get; set; }
        public string SecondStreet { get; set; }
        public string SecondBuilding { get; set; }
        public string SecondFloor { get; set; }
        public string SecondPOBox { get; set; }
        public string SecondFax { get; set; }
        public Nullable<bool> SecondIS_ACTIVE { get; set; }
        public Nullable<System.DateTimeOffset> SecondENTRY_DATE { get; set; }
        public Nullable<System.Guid> SecondENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> SecondMODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> SecondMODIFICATION_DATE { get; set; }
        public Nullable<int> SecondFlag { get; set; }

        public string Title { get; set; }

        public string ClassTitle { get; set; }

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
