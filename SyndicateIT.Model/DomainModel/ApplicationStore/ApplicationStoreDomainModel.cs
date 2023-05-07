
using SyndicateIT.Model.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.DomainModel
{
    [Serializable]
    public class ApplicationStoreDomainModel
    {
        #region Public Methods

        public ApplicationStore Initialize()
        {
            ApplicationStore store = new ApplicationStore();
            using (var db = new SyndicatDataEntities())
            {
                store.CurrencyList = GetCurrencyList(db);
                store.RoleList = GetRoleList(db);
                store.RoleCodeList = GetRoleCodeList(db);
                store.UserList = GetUserList(db);
            }

            return store;
        }

        #endregion

        #region Private Method

        private List<SelectListItem> GetRoleCodeList(SyndicatDataEntities db)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            var dbList = db.AspNetRoles.ToList();
            for (int i = 0; i < dbList.Count; i++)
            {
                lst.Add(new SelectListItem()
                {
                    Text = dbList[i].Name,
                    Value = dbList[i].Id.ToString(),
                });
            }



            return lst;
        }

        private List<SelectListItem> GetRoleList(SyndicatDataEntities db)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            var dbList = db.AspNetRoles.Where(p => p.Name != "SuperAdministrator").ToList();
            for (int i = 0; i < dbList.Count; i++)
            {
                lst.Add(new SelectListItem()
                {
                    Text = dbList[i].Name,
                    Value = dbList[i].Id.ToString(),
                });
            }

            return lst;
        }

        private List<SelectListItem> GetUserList(SyndicatDataEntities db)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            var dbList = (from u in db.AspNetUsers
                          join r in db.AspNetUserRoles on u.Id equals r.UserId
                          where r.RoleId != "fc185540-fd2a-4cf8-b7d0-425308e00b66" || r.RoleId != "5787df52-cf95-4a27-a9d6-602610489c85"
                          select new
                          {
                              u.FirstName,
                              u.LastName,
                              u.UserId,
                          }).ToList();

            for (int i = 0; i < dbList.Count; i++)
            {
                lst.Add(new SelectListItem()
                {
                    Text = dbList[i].FirstName + " " + dbList[i].LastName,
                    Value = dbList[i].UserId.ToString(),
                });
            }

            return lst;
        }


        private List<SelectListItem> GetCurrencyList(SyndicatDataEntities db)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Text = "USD",
                Value = "840"
            });
            lst.Add(new SelectListItem()
            {
                Text = "LBP",
                Value = "422"
            });
            lst.Add(new SelectListItem()
            {
                Text = "AED",
                Value = "784"
            });
            return lst;
        }

        #endregion
    }
}
