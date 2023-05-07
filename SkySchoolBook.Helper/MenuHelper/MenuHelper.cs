using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace SyndicateIT.Helper
{
    [Serializable()]
    public static class MenuHelper
    {

        /// <summary>
        /// The Helper that will be called from the view to build a accordion 
        /// </summary>
        /// <param name="helper">The html helper</param>
        /// <param name="accordions">The list of menues</param>
        /// <param name="classTab">The style of the menu</param>
        /// <returns></returns>
        public static string Menu(this HtmlHelper helper, MenuPage menuPages)
        {
            var scriptWriter = new StringBuilder();
            var writer = new HtmlTextWriter(new StringWriter());

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "nav");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            var menu = menuPages.Menus.ToList();
            for (int i = 0; i < menu.Count; i++)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, menu[i].MenuClass);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, menu[i].MenuTypeID.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, menu[i].Url);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, menu[i].MenuIClass);
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, menu[i].MenuSpanClass);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(menu[i].MenuDescription);
                writer.RenderEndTag();
                writer.RenderEndTag();
                if (menu[i].HasSubmenu == true)
                {
                    var subMenu = menu[i].MenuSubList.ToList();
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, menu[i].MenuSubClass);
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, menu[i].MenuTypeID.ToString() + "UL");
                    writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    for (int j = 0; j < subMenu.Count; j++)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, subMenu[j].Controller);
                        writer.AddAttribute(HtmlTextWriterAttribute.Id, subMenu[j].Action);
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, subMenu[j].Url);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, subMenu[j].MenuSubSpanClass);
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(subMenu[j].MenuDescription);
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            #region JavaScript
            //Add the script to the According.
            writer.Write("<script type=\"text/javascript\">$(document).ready(function(){ " + scriptWriter.ToString() + " " + WritePagerScript() + " });</script>");
            #endregion


            return writer.InnerWriter.ToString();

        }

        /// <summary>
        /// write Script 
        /// </summary>
        /// <param name="scriptWriter">Script Writer</param>
        /// <param name="classTab">class Tab</param>
        private static string WritePagerScript()
        {
            StringBuilder scriptWriter = new StringBuilder();
            //scriptWriter.AppendLine("$('.hidden-xs').click(selectProfile);");
            //scriptWriter.AppendLine("$('.toggle-min .fa-bars').click(toggleBar);");
            //scriptWriter.AppendLine("$('#nav li').click(selectNavigation);");        
            return scriptWriter.ToString();
        }
    }
}
