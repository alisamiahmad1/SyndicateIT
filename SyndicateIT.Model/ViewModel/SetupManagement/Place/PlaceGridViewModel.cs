using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Place
{
    public class PlaceGridViewModel : USP_GET_Place_Result
    {
        public String Place_EncryptId { get; set; }
    }
}
