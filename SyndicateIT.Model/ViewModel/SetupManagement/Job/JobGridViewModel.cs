using SyndicateIT.DataLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Job
{
   public  class JobGridViewModel: USP_GET_JOB_Result
    {
        public String JOB_EncryptId { get; set; } = "";
    }
}
