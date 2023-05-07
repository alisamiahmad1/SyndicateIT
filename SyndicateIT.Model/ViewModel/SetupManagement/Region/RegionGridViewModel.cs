using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Region
{
    public class RegionGridViewModel : USP_GET_Region_Result
    {
        public String Region_EncryptId { get; set; } = "";
    }
}
