using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.Shared
{
    [Serializable]
    public class NavigationViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the name of the navigation.
        /// </summary>
        /// <value>
        /// The name of the navigation.
        /// </value>
        public string NavigationName { get; set; }

        /// <summary>
        /// Gets or sets the navigation URL.
        /// </summary>
        /// <value>
        /// The navigation URL.
        /// </value>
        public string NavigationUrl { get; set; }


        /// <summary>
        /// Gets a value indicating whether this instance has navigation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has navigation; otherwise, <c>false</c>.
        /// </value>
        public bool HasNavigation { get { return !string.IsNullOrEmpty(NavigationUrl); } }
    }
}
