using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel
{
    public class ViewModelBase
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

        #endregion



    }
}