using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Controllers
{
    public class Document_Form_Sub_CategoryController : Controller
    {
        private SyndicatDataEntities db = new SyndicatDataEntities();
        private void ViewData_Lst(int? id = null)
        {
            ViewBag.Document_Form_Category_Id = new SelectList(db.T_Document_Form_Category.OrderBy(o => o.Title), "Id", "Title", id);
        }
        public ActionResult Index(int? cat_id)
        {
            ViewData_Lst(cat_id);
            return View();
        }
        [HttpPost]
        public ActionResult Search([Bind(Include = "Document_Form_Category_Id,Title")] T_Document_Form_Sub_Category content)
        {
            var query = db.T_Document_Form_Sub_Category.AsQueryable();
            if (!string.IsNullOrWhiteSpace(content.Title)) query = query.Where(p => p.Title.Contains(content.Title.Trim()));
            if (content.Document_Form_Category_Id > 0) query = query.Where(p => p.Document_Form_Category_Id == content.Document_Form_Category_Id);
            var model = query.OrderBy(o => o.Title).Select(s => new {
                s.Id, s.Title, Document_Form_Category = s.T_Document_Form_Category.Title,
                Document_Form_Templates=s.T_Document_Form_Template.Count
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(int? id)
        {
            ViewData_Lst(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Document_Form_Category_Id,Title")] T_Document_Form_Sub_Category t_Document_Form_Sub_Category)
        {
            if (ModelState.IsValid)
            {
                db.T_Document_Form_Sub_Category.Add(t_Document_Form_Sub_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData_Lst(t_Document_Form_Sub_Category.Document_Form_Category_Id);
            return View(t_Document_Form_Sub_Category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Sub_Category t_Document_Form_Sub_Category = db.T_Document_Form_Sub_Category.Find(id);
            if (t_Document_Form_Sub_Category == null)
            {
                return HttpNotFound();
            }
            ViewData_Lst(t_Document_Form_Sub_Category.Document_Form_Category_Id);
            return View(t_Document_Form_Sub_Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Document_Form_Category_Id,Title")] T_Document_Form_Sub_Category t_Document_Form_Sub_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Document_Form_Sub_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData_Lst(t_Document_Form_Sub_Category.Document_Form_Category_Id);
            return View(t_Document_Form_Sub_Category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Sub_Category t_Document_Form_Sub_Category = db.T_Document_Form_Sub_Category.Find(id);
            if (t_Document_Form_Sub_Category == null)
            {
                return HttpNotFound();
            }
            return View(t_Document_Form_Sub_Category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Document_Form_Sub_Category t_Document_Form_Sub_Category = db.T_Document_Form_Sub_Category.Find(id);
            db.T_Document_Form_Sub_Category.Remove(t_Document_Form_Sub_Category);
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
