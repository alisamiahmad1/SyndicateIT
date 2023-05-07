using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SyndicateIT.DataLayer.DataContext;
using WebGrease.Css.Extensions;

namespace SyndicateIT.Controllers
{
    public class DocumentController : Controller
    {
        SyndicatDataEntities db = new SyndicatDataEntities();
        public ActionResult Index()
        {
            var model = db.T_Document_Template.OrderByDescending(o => o.Id).ToList();
            return View(model);
        }
        public ActionResult Save(long? id)
        {
            T_Document_Template model;
            if (id.HasValue)
            {
                model = db.T_Document_Template.Find(id);
            }
            else model = new T_Document_Template();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(T_Document_Template model)
        {
            try
            {
                T_Document_Template dbModel;
                if (model.Id == 0)
                {
                    dbModel = new T_Document_Template();
                    db.T_Document_Template.Add(dbModel);
                }
                else
                {
                    dbModel = db.T_Document_Template.FirstOrDefault(p => p.Id == model.Id);
                }
                model = rplc(model);
                dbModel.Template = model.Template;
                dbModel.Executed_Template = model.Executed_Template;
                dbModel.NbInput = model.NbInput;
                dbModel.FileIndexes = model.FileIndexes;
                dbModel.MFileIndexes = model.MFileIndexes;
                dbModel.Name = model.Name;

                db.SaveChanges();
                return RedirectToAction("Form", new { id = dbModel.Id });
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }
        T_Document_Template rplc(T_Document_Template model, List<T_Form_Data> data = null)
        {
            string st = model.Template;
            if (data == null) data = new List<T_Form_Data>();
            int index;
            List<string> names = new List<string>();
            List<int> FileIndexes = new List<int>();
            List<int> MFileIndexes = new List<int>();
            var ar = Regex.Split(st, "{{");
            st = ar[0];
            string val;
            for (int i = 1; i < ar.Length; i++)
            {
                var br = Regex.Split(ar[i], "}}");

                var cr = (br[0] + ":::").Split(':');
                string name = cr[1].Trim(), value = cr[2].Trim(), attr = cr[3].Trim();
                if (name == "" || !names.Contains(name))
                {
                    names.Add(name);
                    index = names.Count - 1;
                }
                else index = names.IndexOf(name);

                var dt = data.FirstOrDefault(p => p.Index == index);
                if (dt != null) val = dt.Value;
                else val = "";

                switch (cr[0].Trim())
                {
                    case "r":
                        st += $"<input type='radio' name='inp{index}' id='t{i}' value='{value}' {(val == value ? "checked" : "")} {attr}/>{value}";
                        break;
                    case "c":
                        st += $"<input type='checkbox' name='inp{index}' id='t{i}' value='true' {(val == "true" ? "checked" : "")} {attr}/>";
                        break;
                    case "f":
                        FileIndexes.Add(index);
                        st += $"<input type='file' name='inp{index}' accept='image/*,application/pdf' id='t{i}' {attr}/> <b>{value}</b> ";
                        if (val != "")
                        {
                            st += $" || <a href='{Url.Content(val)}' target='_blanc'>view old file</a> | <input type='checkbox' value='true' name='inp{index}d' />check for delete this file || ";
                        }
                        break;
                    case "a":
                        st += $"<textarea name='inp{index}' id='t{i}' {attr}>{HttpUtility.HtmlEncode(val ?? value)}</textarea>";
                        break;
                    case "s":
                        var options = value.Split('-').ToList();
                        st += $"<select name='inp{index}' id='t{i}' {attr}>";
                        options.ForEach(f =>
                        {
                            f = f.Trim();
                            st += $"<option value='{HttpUtility.HtmlEncode(f)}' {(f == val ? "selected" : "")}>{HttpUtility.HtmlEncode(f)}</option>";
                        });
                        st += "</select>";
                        break;
                    case "g":
                        st += $"<input type='hidden' name='inp{index}' />";
                        st += "<div style=\"border:2px solid black;width:300px;margin:auto;\">";
                        st += $"<canvas  data-id='inp{index}' data-val='{val}' style=\"width:300px;height:200px;\" class=\"sgn\"></canvas>";
                        st+= "<button type=\"button\" class=\"button sgn-clear\" >Clear</button></div>";
                        break;
                    case "t":
                    default:
                        st += $"<input type='text' name='inp{index}' id='t{i}' value='{HttpUtility.HtmlEncode(val ?? value)}' {attr}/>";
                        break;
                }
                st += br[1];
            }
            model.Executed_Template = st;
            model.NbInput = names.Count;
            model.FileIndexes = string.Join(",", FileIndexes.Distinct());

            return model;
        }
        string St_Show(T_Document_Template model, List<T_Form_Data> data)
        {
            string st = model.Template;
            List<string> files = new List<string>();
            int index;
            List<string> names = new List<string>();
            var ar = Regex.Split(st, "{{");
            st = ar[0];
            string val = null;
            for (int i = 1; i < ar.Length; i++)
            {
                var br = Regex.Split(ar[i], "}}");

                var cr = (br[0] + ":::").Split(':');
                string name = cr[1].Trim(), value = cr[2].Trim(), attr = cr[3].Trim();
                if (name == "" || !names.Contains(name))
                {
                    names.Add(name);
                    index = names.Count - 1;
                }
                else index = names.IndexOf(name);

                val = data.FirstOrDefault(p => p.Index == index).Value;

                switch (cr[0].Trim())
                {
                    case "c":
                        st += (val == value ? " X " : " - ");
                        break;
                    case "a":
                        st += $"<p><b>{HttpUtility.HtmlEncode(val==""?"----------------":val)}</b></p>";
                        break;
                    case "g":
                        st += $"<img style='margin-bottom:-100px;' src='data:image/png;base64,{val}' />";
                        break;
                    case "r":
                        st += (val == value ? val : "");
                        break;
                    case "f":
                        if (val != "")
                        {
                            files.Add(Url.Content(val));
                            var aa = val.Split('.');
                            var nm = value + " file." + aa[aa.Length - 1];
                            st += $"<b> <a href='{Url.Content(val)}' target='_blanc'>view {value}</a> || <a href='{Url.Content(val)}' download='{nm}'>download {value}</a> </b>";
                        }
                        else st += $"<b>{value} not uploaded</b>";
                        break;
                    case "s":
                    case "t":
                    default:
                        st += $"<b>{HttpUtility.HtmlEncode(val == "" ? "----------------" : val)}</b>";
                        break;
                }
                st += br[1];
            }
            if (files.Count > 0)
            {
                st += "<br /><br /><h2><a href=\"javascript:downloadfiles(files);\">download all</a></h2>";
                st += $@"
                    <script src=""{Url.Content("~/")}Scripts/jquery-1.10.2.min.js""></script>
                    <script>
                    var files=['{string.Join("','",files)}'];
                  

                    var dnlnk = document.createElement(""a"");
                    dnlnk.style.display = 'none';
                    document.body.appendChild( dnlnk );

                    function downloadfiles(ff) {{
                        for(var i=0;i<ff.length;i++){{
                            setTimeout(function(url){{
                                dnlnk.setAttribute( 'href', url );
                                var ar = url.split('/');
                                dnlnk.setAttribute('download', ar[ar.length-1]);
                                dnlnk.click();
                            }},300*(i+1),ff[i]);
                        }}
                        return false;
                    }}
                </script>";
            }

            return st;
        }
        public ActionResult Forms(long id)
        {
            ViewData["m"] = db.T_Document_Template.Find(id);
            var model = db.T_Form.Where(p => p.Document_Template_Id == id).OrderByDescending(o => o.Id).ToList();
            return View(model);
        }
        public ActionResult Form(long id, long? form_id)
        {
            T_Document_Template model;
            if (form_id.HasValue)
            {
                var form = db.T_Form.Find(form_id);
                model = rplc(form.T_Document_Template, form.T_Form_Data.ToList());
            }
            else
            {
                model = db.T_Document_Template.Find(id);
            }
            ViewData["form_id"] = form_id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Form(long? form_id, long id)
        {

            T_Form dbModel;
            var model = db.T_Document_Template.Find(id);
            var FileIndexes = model.FileIndexes.Split(',').Where(p => p != "").Select(s => int.Parse(s)).ToList();
            if (form_id.HasValue)
            {
                dbModel = db.T_Form.Find(form_id);
                var res= db.T_Form_Data.Where(p => p.Form_id == dbModel.Id && !FileIndexes.Contains(p.Index)).ToList();
                db.T_Form_Data.RemoveRange(res);
                db.SaveChanges();
            }
            else
            {
                dbModel = new T_Form() { Document_Template_Id = id };
                db.T_Form.Add(dbModel);
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

                        val = "~/Content/forms/" + dbModel.Id + "/";
                        string path = Server.MapPath(val);
                        if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                        var ar = file.FileName.Split('.');
                        string name = key + '.' + ar[ar.Length - 1];
                        file.SaveAs(path + name);
                        val += name;
                    }
                    var old = dbModel.T_Form_Data.FirstOrDefault(p => p.Index == i);
                    if (old != null)
                    {
                        if (Request.Form[key + "d"] == "true" || (val != "" && old.Value != ""))
                        {
                            System.IO.File.Delete(Server.MapPath(old.Value));
                        }
                        else if (val == "") val = old.Value;
                        db.T_Form_Data.Remove(old);
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (Request.Form.AllKeys.Contains(key)) val = Request.Form[key];
                }
                dbModel.T_Form_Data.Add(new T_Form_Data
                {
                    Form_id = dbModel.Id,
                    Index = i,
                    Value = val
                });
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Show(long id)
        {
            var f = db.T_Form.Find(id);
            var st = St_Show(f.T_Document_Template, f.T_Form_Data.ToList());
            return Content(st);
        }
        public ActionResult Remove(long id)
        {
            var model = db.T_Document_Template.Find(id);
            db.T_Document_Template.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveForm(long id)
        {
            var model = db.T_Form.Find(id);
            model.T_Form_Data.Clear();
            db.SaveChanges();
            db.T_Form.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}