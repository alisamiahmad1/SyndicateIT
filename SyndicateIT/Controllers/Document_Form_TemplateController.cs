using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Models;

namespace SyndicateIT.Controllers
{
    public class Document_Form_TemplateController : Controller
    {
        private SyndicatDataEntities db = new SyndicatDataEntities();


        private void ViewData_Lst(long? sub_cat_id = null, int? cat_id = null)
        {
            ViewBag.Document_Form_Category_Id = new SelectList(db.T_Document_Form_Category.Where(p => p.T_Document_Form_Sub_Category.Count > 0).OrderBy(o => o.Title), "Id", "Title", cat_id);

            ViewBag.Document_Form_Sub_Category_Id = new SelectList(db.T_Document_Form_Sub_Category.OrderBy(o => o.Title), "Id", "Title", "T_Document_Form_Category.Title", sub_cat_id);
        }
        public ActionResult Index(long? sub_cat_id)
        {
            ViewData_Lst(sub_cat_id);
            return View();
        }
        [HttpPost]
        public ActionResult Search([Bind(Include = "Document_Form_Category_Id,Document_Form_Sub_Category_Id,Name")] T_Document_Form_Template content, int? document_Form_Category_Id)
        {
            var query = db.T_Document_Form_Template.AsQueryable();
            if (!string.IsNullOrWhiteSpace(content.Name)) query = query.Where(p => p.Name.Contains(content.Name.Trim()));
            if (content.Document_Form_Sub_Category_Id > 0) query = query.Where(p => p.Document_Form_Sub_Category_Id == content.Document_Form_Sub_Category_Id);
            if (document_Form_Category_Id > 0) query = query.Where(p => p.T_Document_Form_Sub_Category.Document_Form_Category_Id == document_Form_Category_Id);
            var model = query.OrderBy(o => new { t1 = o.T_Document_Form_Sub_Category.T_Document_Form_Category.Title, t2 = o.T_Document_Form_Sub_Category.Title, o.Name }).Select(s => new
            {
                s.Id,
                s.Name,
                Document_Form_Sub_Category = s.T_Document_Form_Sub_Category.Title,
                Document_Form_Category = s.T_Document_Form_Sub_Category.T_Document_Form_Category.Title,
                Document_Forms = s.T_Document_Form.Count,
                Is_Publish = false
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            ViewData_Lst();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Template,Document_Form_Sub_Category_Id")] T_Document_Form_Template t_Document_Form_Template)
        {
            if (ModelState.IsValid)
            {
                t_Document_Form_Template = Document_Form_Model.Form_Template(t_Document_Form_Template, Url);
                db.T_Document_Form_Template.Add(t_Document_Form_Template);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData_Lst(t_Document_Form_Template.Document_Form_Sub_Category_Id);
            return View(t_Document_Form_Template);
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Template t_Document_Form_Template = db.T_Document_Form_Template.Find(id);
            if (t_Document_Form_Template == null)
            {
                return HttpNotFound();
            }
            ViewData_Lst(t_Document_Form_Template.Document_Form_Sub_Category_Id, t_Document_Form_Template.T_Document_Form_Sub_Category.Document_Form_Category_Id);
            return View(t_Document_Form_Template);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,Template,Document_Form_Sub_Category_Id")] T_Document_Form_Template t_Document_Form_Template)
        {
            if (ModelState.IsValid)
            {
                t_Document_Form_Template = Document_Form_Model.Form_Template(t_Document_Form_Template, Url);
                db.Entry(t_Document_Form_Template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData_Lst(t_Document_Form_Template.Document_Form_Sub_Category_Id, t_Document_Form_Template.T_Document_Form_Sub_Category.Document_Form_Category_Id);
            return View(t_Document_Form_Template);
        }
        public ActionResult ShowClientForm(long? id, bool contentOnly = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Template t_Document_Form_Template = db.T_Document_Form_Template.Find(id);
            if (t_Document_Form_Template == null)
            {
                return HttpNotFound();
            }
            if (contentOnly) return Content(t_Document_Form_Template.Executed_Template);
            return View(t_Document_Form_Template);
        }
        public ActionResult Publish(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Template t_Document_Form_Template = db.T_Document_Form_Template.Find(id);
            if (t_Document_Form_Template == null)
            {
                return HttpNotFound();
            }
            t_Document_Form_Template.Is_Publish = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form_Template t_Document_Form_Template = db.T_Document_Form_Template.Find(id);
            if (t_Document_Form_Template == null)
            {
                return HttpNotFound();
            }
            return View(t_Document_Form_Template);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_Document_Form_Template t_Document_Form_Template = db.T_Document_Form_Template.Find(id);
            db.T_Document_Form_Template.Remove(t_Document_Form_Template);
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
