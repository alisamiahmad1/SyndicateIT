using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.FileManagement
{
      public class FilesViewModel : ViewModelBase
    {
        #region Properties       

        /// <summary>
        /// Gets or sets the partial name of the view.
        /// </summary>
        /// <value>
        /// The partial name of the view.
        /// </value>
        public string PartialViewName { get; set; }

        /// <summary>
        /// Gets or sets the partial view name model.
        /// </summary>
        /// <value>
        /// The partial view name model.
        /// </value>
        public object PartialViewNameModel { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        #endregion

        #region construction 


        public FilesViewModel()
        {

        }

        #endregion
    }
}
