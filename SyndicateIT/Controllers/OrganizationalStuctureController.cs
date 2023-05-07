using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateIT.Controllers
{
    public class OrganizationalStuctureController : Controller
    {
        // GET: OrganizationalStucture
        public ActionResult Index()
        {
            return View("~/Views/OrganizationalStucture/Index.cshtml");
        }

        public ActionResult Queue()
        {
            return View("~/Views/OrganizationalStucture/Queue.cshtml");
        }

    }
}