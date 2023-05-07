using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.UtilityModel.Session;

using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.ViewModel.User_Documents
{
    public class User_DocumentsViewModel : Tbl_User_Documents
    {
        public bool IsEditable
        {
            get
            {
                return SessionContent.Container.Login.UserRole == UserType.SuperAdministrator.ToString() || SessionContent.Container.Login.UserRole == UserType.Administrator.ToString();
            }
        }
    }
}
