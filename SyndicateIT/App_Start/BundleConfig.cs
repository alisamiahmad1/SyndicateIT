using System.Web;
using System.Web.Optimization;

namespace SyndicateIT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendoui").Include(
               "~/Scripts/kendo/2014.1.318/kendo.web.min.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap/bootstrap.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/speech").Include(
                     "~/Scripts/speech/voicecommand.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/appconfig").Include(
                   "~/Scripts/app.config.js"));

            bundles.Add(new ScriptBundle("~/bundles/appmin").Include(
                   "~/Scripts/app.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/smartchat").Include(
                     "~/Scripts/smart-chat-ui/smart.chat.ui.min.js",
                     "~/Scripts/smart-chat-ui/smart.chat.manager.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/notification").Include(
                     "~/Scripts/notification/SmartNotification.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/smartwidgets").Include(
                     "~/Scripts/smartwidgets/jarvis.widget.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/dragDopSchedule").Include(
                     "~/Scripts/DragDrop/header.js",
                     "~/Scripts/DragDrop/redips-drag-min.js",
                     "~/Scripts/DragDrop/scriptSchedule.js"));

            bundles.Add(new ScriptBundle("~/bundles/dragDopSchedule").Include(
                   "~/Scripts/DragDrop/redips-drag-min.js",
                   "~/Scripts/DragDrop/scriptSchedule.js"));


            bundles.Add(new ScriptBundle("~/bundles/DropDopDistributionStudents").Include(
                    "~/Scripts/DragDrop/redips-drag-min.js",
                    "~/Scripts/DragDrop/scriptDistributionStudents.js"));


            bundles.Add(new ScriptBundle("~/bundles/plugin").Include(
                    "~/Scripts/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                    "~/Scripts/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                    "~/Scripts/plugin/sparkline/jquery.sparkline.min.js",
                    "~/Scripts/plugin/jquery-validate/jquery.validate.min.js",
                    "~/Scripts/plugin/select2/select2.min.js",
                    "~/Scripts/plugin/bootstrap-slider/bootstrap-slider.min.js",
                    "~/Scripts/plugin/msie-fix/jquery.mb.browser.min.js",
                    "~/Scripts/plugin/fastclick/fastclick.min.js",
                     "~/Scripts/plugin/summernote/summernote.min.js"
                    ));


            bundles.Add(new StyleBundle("~/Content/Css/File").Include(                
                        "~/Content/File.css",
                         "~/Content/Dashboard.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                      "~/Content/kendo/2013.3.1324/kendo.common-bootstrap.min.css",
                      "~/Content/kendo/2013.3.1324/kendo.bootstrap.min.css",
                       "~/Content/bootstrap-dialog.css",
                        "~/Content/bootstrap-dialog.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/kendo/js").Include(

                   "~/Scripts/kendo/2013.3.1324/kendo.all.min.js",
                   "~/Scripts/kendo/2013.3.1324/kendo.aspnetmvc.min.js",
                    "~/Scripts/bootstrap-dialog.js"
                  ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.min.css",
                   "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/DropDopSchedule").Include(
                "~/Content/DropDopSchedule.css"));


            bundles.Add(new StyleBundle("~/Content/smartadmin").Include(
                "~/Content/smartadmin-production-plugins.min.css",
                "~/Content/smartadmin-production.min.css",
                "~/Content/smartadmin-skins.min.css"));


            bundles.Add(new StyleBundle("~/Content/DropDopSchedule").Include(
                "~/Content/DropDopSchedule.css"));

            bundles.Add(new StyleBundle("~/Content/DropDopDistributionStudents").Include(
               "~/Content/DropDopDistributionStudents.css"));

            bundles.Add(new StyleBundle("~/Content/rtl").Include(
                "~/Content/smartadmin-rtl.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
              "~/scripts/plugin/datatables/jquery.dataTables.min.js",
              "~/scripts/plugin/datatables/dataTables.colVis.min.js",
              "~/scripts/plugin/datatables/dataTables.tableTools.min.js",
              "~/scripts/plugin/datatables/dataTables.bootstrap.min.js",
              "~/scripts/plugin/datatable-responsive/datatables.responsive.min.js"
              ));

            bundles.Add(new StyleBundle("~/Content/demo").Include(
                "~/Content/demo.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryValidation").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"
          ));
            
            bundles.Add(new ScriptBundle("~/Scripts/translate").Include("~/Scripts/Views/SetupManagement/translation.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Country").Include("~/Scripts/Views/SetupManagement/Country.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Gender").Include("~/Scripts/Views/SetupManagement/Gender.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Department").Include("~/Scripts/Views/SetupManagement/Department.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Cycle").Include("~/Scripts/Views/SetupManagement/Cycle.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Job").Include("~/Scripts/Views/SetupManagement/Job.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Kaddaa").Include("~/Scripts/Views/SetupManagement/Kaddaa.js"));
            bundles.Add(new ScriptBundle("~/Scripts/MaritalStatus").Include("~/Scripts/Views/SetupManagement/MaritalStatus.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Bus").Include("~/Scripts/Views/SetupManagement/Bus.js"));
            bundles.Add(new ScriptBundle("~/Scripts/PhoneType").Include("~/Scripts/Views/SetupManagement/PhoneType.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Region").Include("~/Scripts/Views/SetupManagement/Region.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Nationality").Include("~/Scripts/Views/SetupManagement/Nationality.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Educationalsystem").Include("~/Scripts/Views/SetupManagement/Educationalsystem.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ROLE").Include("~/Scripts/Views/SetupManagement/ROLE.js"));
            bundles.Add(new ScriptBundle("~/Scripts/DocumentExtension").Include("~/Scripts/Views/SetupManagement/DocumentExtension.js"));
            bundles.Add(new ScriptBundle("~/Scripts/RelationTypes").Include("~/Scripts/Views/SetupManagement/RelationTypes.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Place").Include("~/Scripts/Views/SetupManagement/Place.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Subjects").Include("~/Scripts/Views/SetupManagement/Subjects.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Shift").Include("~/Scripts/Views/SetupManagement/Shift.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Status").Include("~/Scripts/Views/SetupManagement/Status.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Company").Include("~/Scripts/Views/SetupManagement/Company.js"));
            bundles.Add(new ScriptBundle("~/Scripts/CompanyType").Include("~/Scripts/Views/SetupManagement/CompanyType.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Corporate").Include("~/Scripts/Views/SetupManagement/Corporate.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Religion").Include("~/Scripts/Views/SetupManagement/Religion.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Branches").Include("~/Scripts/Views/SetupManagement/Branches.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Classes").Include("~/Scripts/Views/SetupManagement/Classes.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Agenda").Include("~/Scripts/Views/ServiceManagement/Agenda.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Courses").Include("~/Scripts/Views/SetupManagement/Courses.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Skills").Include("~/Scripts/Views/SetupManagement/Skills.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Division").Include("~/Scripts/Views/SetupManagement/Division.js"));
            bundles.Add(new ScriptBundle("~/Scripts/SkillCoefficient").Include("~/Scripts/Views/SetupManagement/SkillCoefficient.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Event").Include("~/Scripts/Views/SetupManagement/Event.js"));
            bundles.Add(new ScriptBundle("~/Scripts/EventType").Include("~/Scripts/Views/SetupManagement/EventType.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Agenda").Include("~/Scripts/Views/ServiceManagement/Agenda.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Term").Include("~/Scripts/Views/SetupManagement/Term.js"));
            bundles.Add(new ScriptBundle("~/Scripts/TermMonth").Include("~/Scripts/Views/SetupManagement/TermMonth.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Msg").Include("~/Scripts/Views/Msg/Msg.js"));

        }
    }
}
