
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SyndicateIT.Model.UtilityModel
{
    [Serializable]
    public class ApplicationStore
    {
        #region Properties    

        #region Lists
        
        #region Assigned Branch

        private List<SelectListItem> assignedBranchList;

        public List<SelectListItem> AssignedBranchList
        {
            get { return Utilities.CloneList(assignedBranchList); }
            set { assignedBranchList = value; }
        }

        #endregion 

        #region Role List

        private List<SelectListItem> roleList;

        public List<SelectListItem> RoleList
        {
            get { return Utilities.CloneList(roleList); }
            set { roleList = value; }
        }

        #endregion

        #region User List

        private List<SelectListItem> userList;

        public List<SelectListItem> UserList
        {
            get { return Utilities.CloneList(userList); }
            set { userList = value; }
        }

        #endregion


        #region RoleCode List

        private List<SelectListItem> roleCodeList;

        public List<SelectListItem> RoleCodeList
        {
            get { return Utilities.CloneList(roleCodeList); }
            set { roleCodeList = value; }
        }

        #endregion     

        #region Currency

        private List<SelectListItem> currencyList;

        public List<SelectListItem> CurrencyList
        {
            get { return Utilities.CloneList(currencyList); }
            set { currencyList = value; }
        }

        #endregion

        #endregion

        #endregion

        #region Constructor


        /// <summary>
        /// Initializes a new instance of ApplicationStore
        /// </summary>
        /// <param name="culture">Culture</param>
        public ApplicationStore()
        {

        }

        #endregion
    }
}
