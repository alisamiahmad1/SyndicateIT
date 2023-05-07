using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.DocumentExtension
{
    public class DocumentExtensionViewModel : DataLayer.DataContext.Document_Extension
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String DocumentExtensionTitle
        {
            get
            {
                if (Document_Ext_ID != 0 && Document_Ext_ID.ToString() != "")
                {
                    return TypeAction.Edit + "DocumentExtension";
                }
                else
                {
                    return TypeAction.Add + "DocumentExtension";
                }
            }
        }
    }
}
