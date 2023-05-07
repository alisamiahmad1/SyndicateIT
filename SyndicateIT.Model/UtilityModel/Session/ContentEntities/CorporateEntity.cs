using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContentEntities
{
    [Serializable()]
    public class CorporateEntity
    {
        #region Properties

        #region Corporate Info

        public int ID { get; set; }

        public string Name { get; set; }

        public string LogoName { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string PhoneCode { get; set; }

        public string CurrencyCode { get; set; }

        public string Domain { get; set; }

        public string maxLengthCIF { get; set; }

        public string maxLengthMobile { get; set; }

        public string CustomerServicePhone { get; set; }

        public string CustomerServiceEmail { get; set; }

        public Nullable<bool> IsSecure { get; set; }

        public int BankID { get; set; }

        public string BaseOffset { get; set; }

        #endregion

        #endregion

        #region Constructor


        public CorporateEntity()
        {

        }

        #endregion

        #region Methods


        #endregion
    }
}
