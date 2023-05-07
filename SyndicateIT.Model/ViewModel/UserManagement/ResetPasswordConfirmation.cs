using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.UserManagement
{
    [Serializable]
    public class ResetPasswordConfirmation : ViewModelBase
    {
        public string ClassTitle { get; set; }

        public string Title { get; set; }
    }
}
