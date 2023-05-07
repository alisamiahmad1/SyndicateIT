using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.DomainModel.Insurance;
using SyndicateIT.Model.DomainModel.ProfileManagement;
using SyndicateIT.Model.DomainModel.User_Documents;
using SyndicateIT.Model.ProcessorModel.ProfileManagement;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.ProfileManagement;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.User_Documents;
using SyndicateIT.Models;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace SyndicateIT.Controllers
{
    [Serializable]
    [Authorize]
    public class TaskController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Views/Task/TaskManagement/Index.cshtml");
        }

        public ActionResult AddTask()
        {
            var model = new PersonalInformationsViewModel();
            return View("~/Views/Task/TaskManagement/AddTask.cshtml", model);
        }

        public ActionResult Queue()
        {
            return View("~/Views/Task/QueueManagement/Index.cshtml");
        }


    }
}
