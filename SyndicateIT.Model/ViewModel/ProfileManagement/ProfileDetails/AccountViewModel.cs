using SyndicateIT.Model.UtilityModel.Session;
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
    public class AccountViewModel : ViewModelBase
    {
        #region Properties 

        public int Account_ID { get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public int AccountTypeID { get; set; }
        public int CountryID { get; set; }
        public int StatusID { get; set; }

        public string Country { get; set; }
        public int Status { get; set; }
        public int CurrencyID { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankNumber { get; set; }
        public string IBAN { get; set; }
        public string RountingNumber { get; set; }
        public string Description { get; set; }
        public Nullable<System.Boolean> IsCurrentDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }

        public List<AccountViewModel> Accounts { get; set; }

        public bool IsEdit
        {
            get
            {
                if ((SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.MyProfile) ||SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion && SessionContent.Container.Profiles.PersonalInformations != null && SessionContent.Container.Profiles.PersonalInformations.User_ID != null)
                    return true;
                else
                    return false;
            }
        }

        public AccountViewModel()
        {
            Accounts = new List<AccountViewModel>();
        }

        #endregion
    }
}
