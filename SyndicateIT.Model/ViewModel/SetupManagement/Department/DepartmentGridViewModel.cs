using SyndicateIT.DataLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Department
{
   public  class DepartmentGridViewModel: USP_GET_DEPARTMENT_Result
    {
        public String DEPARTMENT_EncryptId { get; set; } = "";
    }
}
