using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Controllers
{
    public class Document_Form_CategoryController : Controller
    {
        private SyndicatDataEntities db = new SyndicatDataEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search([Bind(Include = "Title")] T_Document_Form_Category content)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.T_Document_Form_Category.AsQueryable();

            if (!string.IsNullOrWhiteSpace(content.Title)) query = query.Where(p => p.Title.Contains(content.Title.Trim()));
            var model = query.OrderBy(o => o.Title).Select(s => new { s.Id,s.Title, Sub_Categories=s.T_Document_Form_Sub_Category.Count}).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Document_Form_Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] T_Document_Form_Category t_Document_Form_Category)
        {
            if (ModelState.IsValid)
            {
                db.T_Document_Form_Category.Add(t_Document_Form_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Document_Form_Category);
        }

        // GET: Document_Form_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Category t_Document_Form_Category = db.T_Document_Form_Category.Find(id);
            if (t_Document_Form_Category == null)
            {
                return HttpNotFound();
            }
            return View(t_Document_Form_Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] T_Document_Form_Category t_Document_Form_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Document_Form_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Document_Form_Category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Category t_Document_Form_Category = db.T_Document_Form_Category.Find(id);
            if (t_Document_Form_Category == null)
            {
                return HttpNotFound();
            }
            return View(t_Document_Form_Category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Document_Form_Category t_Document_Form_Category = db.T_Document_Form_Category.Find(id);
            db.T_Document_Form_Category.Remove(t_Document_Form_Category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
