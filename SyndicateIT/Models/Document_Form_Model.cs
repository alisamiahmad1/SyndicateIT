using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Models
{
    public class Document_Form_Model
    {
        public static string Show(T_Document_Template model, List<T_Form_Data> data, UrlHelper Url)
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
                        st += $"<p><b>{HttpUtility.HtmlEncode(val == "" ? "----------------" : val)}</b></p>";
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
                    var files=['{string.Join("','", files)}'];
                  

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
        public static T_Document_Form_Template Form_Template(T_Document_Form_Template model, UrlHelper Url, List<T_Form_Data> data = null)
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
                        st += "<button type=\"button\" class=\"button sgn-clear\" >Clear</button></div>";
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
    }
}