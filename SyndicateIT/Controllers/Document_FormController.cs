using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Controllers
{
    public class Document_FormController : Controller
    {
        private SyndicatDataEntities db = new SyndicatDataEntities();

        private void ViewData_Lst()
        {
            ViewBag.Document_Form_Category_Id = new SelectList(db.T_Document_Form_Category.Where(p => p.T_Document_Form_Sub_Category.Count > 0).OrderBy(o => o.Title), "Id", "Title");

            ViewBag.Document_Form_Sub_Category_Id = new SelectList(db.T_Document_Form_Sub_Category.OrderBy(o => o.Title), "Id", "Title", "T_Document_Form_Category.Title");
            ViewBag.Document_Form_Template_Id = new SelectList(db.T_Document_Form_Sub_Category.OrderBy(o => o.Title), "Id", "Name", "T_Document_Form_Sub_Category.Title");

        }
        public ActionResult Index()
        {
            ViewData_Lst();
            return View();
        }
        public ActionResult Create(long? id)
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
            T_Document_Form model = new T_Document_Form()
            {
                Document_Form_Template_Id = id.Value,
                T_Document_Form_Template = t_Document_Form_Template
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Document_Form_Template_Id,Title,Description")] T_Document_Form t_Document_Form)
        {
            if (ModelState.IsValid)
            {
                db.T_Document_Form.Add(t_Document_Form);
                db.SaveChanges();
                SaveForm_Data(t_Document_Form.Document_Form_Template_Id,t_Document_Form);
                return RedirectToAction("Index");
            }
            return View(t_Document_Form);
        }
        private void SaveForm_Data(long template_id, T_Document_Form form,bool edit=false)
        {
            var model = db.T_Document_Form_Template.Find(template_id);
            var FileIndexes = model.FileIndexes.Split(',').Where(p => p != "").Select(s => int.Parse(s)).ToList();
            if (edit)
            {
               var res = db.T_Document_Form_Data.Where(p => p.Form_id == form.Id && !FileIndexes.Contains(p.Index)).ToList();
                db.T_Document_Form_Data.RemoveRange(res);
                db.SaveChanges();
            }
            for (int i = 0; i < model.NbInput; i++)
            {
                string val = "";
                string key = "inp" + i;
                if (FileIndexes.Contains(i) && Request.Files.AllKeys.Contains(key))
                {
                    var file = Request.Files[key];
                    if (file.ContentLength > 0)
                    {

                        val = "~/Content/forms/" + form.Id + "/";
                        string path = Server.MapPath(val);
                        if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                        var ar = file.FileName.Split('.');
                        string name = key + '.' + ar[ar.Length - 1];
                        file.SaveAs(path + name);
                        val += name;
                    }
                    var old = db.T_Document_Form_Data.FirstOrDefault(p => p.Index == i&&p.Form_id==form.Id);
                    if (old != null)
                    {
                        if (Request.Form[key + "d"] == "true" || (val != "" && old.Value != ""))
                        {
                            System.IO.File.Delete(Server.MapPath(old.Value));
                        }
                        else if (val == "") val = old.Value;
                        db.T_Document_Form_Data.Remove(old);
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (Request.Form.AllKeys.Contains(key)) val = Request.Form[key];
                }
                db.T_Document_Form_Data.Add(new T_Document_Form_Data
                {
                    Form_id = form.Id,
                    Index = i,
                    Value = val
                });
            }
            db.SaveChanges();
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form t_Document_Form = db.T_Document_Form.Find(id);
            if (t_Document_Form == null)
            {
                return HttpNotFound();
            }
            ViewBag.Document_Form_Template_Id = new SelectList(db.T_Document_Form_Template, "Id", "Name", t_Document_Form.Document_Form_Template_Id);
            return View(t_Document_Form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Document_Form_Template_Id,Title,Description")] T_Document_Form t_Document_Form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_Document_Form).State = EntityState.Modified;
                db.SaveChanges();
                SaveForm_Data(t_Document_Form.Document_Form_Template_Id, t_Document_Form,true);
                return RedirectToAction("Index");
            }
            ViewBag.Document_Form_Template_Id = new SelectList(db.T_Document_Form_Template, "Id", "Name", t_Document_Form.Document_Form_Template_Id);
            return View(t_Document_Form);
        }
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form t_Document_Form = db.T_Document_Form.Find(id);
            if (t_Document_Form == null)
            {
                return HttpNotFound();
            }
            return View(t_Document_Form);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            T_Document_Form t_Document_Form = db.T_Document_Form.Find(id);
            db.T_Document_Form.Remove(t_Document_Form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Publish(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Document_Form model = db.T_Document_Form.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Is_Publish = true;
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
