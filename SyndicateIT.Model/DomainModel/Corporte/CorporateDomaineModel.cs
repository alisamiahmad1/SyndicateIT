using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SyndicateIT.Model.DomainModel.Corporte
{
    [Serializable]
    public class CorporateDomaineModel : DomainModelBase
    {
        #region Public Methods    

        public void SetSessionCoporate()
        {
            using (var db = new SyndicatDataEntities())
            {
                var etCorporate = db.Corporates.FirstOrDefault();
                SessionContent.Current.Corporate = new UtilityModel.Session.ContentEntities.CorporateEntity()
                {
                    ID = etCorporate.ID,
                    CountryCode = etCorporate.CountryCode,
                    CurrencyCode = etCorporate.CurrencyCode,
                    CustomerServiceEmail = etCorporate.CustomerServiceEmail,
                    CustomerServicePhone = etCorporate.CustomerServicePhone,
                    Domain = etCorporate.Domain,
                    LogoName = etCorporate.LogoName,
                    Name = etCorporate.Name,
                    PhoneCode = etCorporate.PhoneCode,
                    CountryName = etCorporate.CountryName,
                    maxLengthCIF = etCorporate.MaxLengthCIF.ToString(),
                    maxLengthMobile = etCorporate.MaxLengthMobile.ToString(),
                    BankID = (int)etCorporate.BankID,
                    IsSecure = ConfigurationManager.AppSettings["IsSecure"] != "" ? Convert.ToBoolean(ConfigurationManager.AppSettings["IsSecure"]) : (bool?)null,                    

                };
            }
        }

        #endregion

        #region Private Methods


        #endregion
    }
}
