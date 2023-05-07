
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.UserManagement;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.DomainModel.UserManagement
{
    [Serializable]
    public class UserManagementDomainModel : DomainModelBase 
    {
        #region Public Methods

        public UserManagementContentViewModel UserManagementContent()
        {
            UserManagementContentViewModel model = new UserManagementContentViewModel();
            model.Navigation = GetNavigationList("list", "User Management");
            model.Title = "User Management";
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";

            return model;
        }

        public UserViewModel GetAddUserViewModel(string title)
        {
            UserViewModel model = new UserViewModel();
            model.Navigation = GetNavigationList("user", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";

            return model;
        }
        public EditUserViewModel GetEditUserViewModel(string title)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.Navigation = GetNavigationList("user", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";

            return model;
        }

        public ResetPasswordConfirmation GetResetPasswordConfirmation(string title)
        {
            ResetPasswordConfirmation model = new ResetPasswordConfirmation();
            model.Navigation = GetNavigationList("reset", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";

            return model;
        }

        public UserViewModel GetUserViewModel(string title)
        {
            UserViewModel model = new UserViewModel();
            model.Navigation = GetNavigationList("user", title);
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.Title = title;
            model.ClassTitle = "fa fa-lg fa-fw fa-windows";

            return model;
        }
        #endregion

        #region Private Methods


        public List<NavigationViewModel> GetNavigationList(string type, string title)
        {
            var model = new List<NavigationViewModel>();

            if (type == "list")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Administration", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
            else if (type == "user")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Administration", NavigationUrl = Utilities.GetUrl("Index", "UserManagement", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
            else if (type == "reset")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Administration", NavigationUrl = Utilities.GetUrl("Index", "UserManagement", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = "Reset Password" });
            }
            return model;
        }


        #endregion
    }
}
