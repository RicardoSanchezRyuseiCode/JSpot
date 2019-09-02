using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace Ryusei.JSpot.WebApp
{
    /// <summary>
    /// Name: AsIsBundleOrderer
    /// Description: Method to order the files in bundle scripts
    /// </summary>
    internal class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
    /// <summary>
    /// Name: BundleExtensions
    /// Description: Method to force order of files
    /// </summary>
    internal static class BundleExtensions
    {
        public static Bundle ForceOrdered(this Bundle sb)
        {
            sb.Orderer = new AsIsBundleOrderer();
            return sb;
        }
    }

    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*************************************************************************/
            /*                          Xtreme Style Bundle                          */
            /*************************************************************************/
            bundles.Add(new StyleBundle("~/Assets/Xtreme/Style").Include(
                      "~/Assets/Xtreme/libs/fullcalendar/dist/fullcalendar.min.css",
                      "~/Assets/Xtreme/extra-libs/calendar/calendar.css",
                      "~/Assets/Xtreme/libs/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css",
                      "~/Assets/Xtreme/libs/select2/dist/css/select2.min.css",
                      "~/Assets/Xtreme/libs/datatables.net-bs4/css/dataTables.bootstrap4.css",
                      
                      "~/Assets/Xtreme/dist/css/style.min.css",
                      "~/Assets/Xtreme/libs/parsley/src/parsley.css",
                      "~/Assets/Xtreme/libs/noty/noty.css",
                      "~/Assets/Xtreme/libs/jquery-smart-wizard/dist/css/smart_wizard.min.css",
                      "~/Assets/Xtreme/libs/jquery-smart-wizard/dist/css/smart_wizard_theme_dots.min.css",
                      "~/Assets/Xtreme/libs/leaflet/leaflet.css"
                      ).ForceOrdered());
            /*************************************************************************/
            /*                          Xtreme Script Bundle                         */
            /*************************************************************************/
            bundles.Add(new ScriptBundle("~/Assets/Xtreme/Script/BaseBottom").Include(
                        "~/Assets/Xtreme/libs/jquery/dist/jquery.min.js",
                        "~/Assets/Xtreme/extra-libs/taskboard/js/jquery.ui.touch-punch-improved.js",
                        "~/Assets/Xtreme/extra-libs/taskboard/js/jquery-ui.min.js",
                        "~/Assets/Xtreme/libs/popper.js/dist/umd/popper.min.js",
                        "~/Assets/Xtreme/libs/bootstrap/dist/js/bootstrap.min.js",
                        "~/Assets/Xtreme/libs/parsley/dist/parsley.js",
                        "~/Assets/Xtreme/libs/i18next/i18next.min.js",
                        "~/Assets/Xtreme/libs/i18next/jquery-i18next.min.js",
                        "~/Assets/Xtreme/libs/noty/noty.js",
                        "~/Assets/Xtreme/dist/js/app.min.js",
                        "~/Assets/Xtreme/dist/js/app.init.js",
                        "~/Assets/Xtreme/dist/js/app-style-switcher.js",
                        "~/Assets/Xtreme/libs/perfect-scrollbar/dist/perfect-scrollbar.jquery.min.js",
                        "~/Assets/Xtreme/extra-libs/sparkline/sparkline.js",
                        "~/Assets/Xtreme/dist/js/waves.js",
                        "~/Assets/Xtreme/dist/js/sidebarmenu.js",
                        "~/Assets/Xtreme/dist/js/custom.min.js",
                        "~/Assets/Xtreme/libs/bootstrap-sweetalert/sweetalert.min.js",
                        "~/Assets/Xtreme/libs/moment/min/moment.min.js",
                        "~/Assets/Xtreme/libs/moment/locale/es.js",
                        "~/Assets/Xtreme/libs/fullcalendar/dist/fullcalendar.min.js",
                        "~/Assets/Xtreme/libs/fullcalendar/dist/locale-all.js",
                        "~/Assets/Xtreme/libs/jquery-smart-wizard/src/js/jquery.smartWizard.js",
                        "~/Assets/Xtreme/libs/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker-custom.js",
                        "~/Assets/Xtreme/libs/leaflet/leaflet.js",
                        "~/Assets/Xtreme/libs/select2/dist/js/select2.full.min.js",
                        "~/Assets/Xtreme/libs/select2/dist/js/select2.min.js",
                        "~/Assets/Xtreme/libs/sheetJS/xlsx.full.min.js",
                        "~/Assets/Xtreme/extra-libs/DataTables/datatables.min.js"
                        ).ForceOrdered());
            /*************************************************************************/
            /*                          Ryusei Style Bundle                          */
            /*************************************************************************/
            bundles.Add(new StyleBundle("~/Assets/Ryusei/Style").Include(
                      "~/Assets/Ryusei/css/RyuseiStyle.css"
                      ).ForceOrdered());
            /*************************************************************************/
            /*                              Elysium Bundle                           */
            /*************************************************************************/
            bundles.Add(new ScriptBundle("~/Assets/Elysium/Scripts").Include(
                // Configuration
                "~/Assets/Elysium/Elysium.js",
                // Types
                "~/Assets/Elysium/Types/String.js",
                "~/Assets/Elysium/Types/Image.js",
                // UI Interfaces
                "~/Assets/Elysium/UI/Interfaces/IRevolutionarySlider.js",
                "~/Assets/Elysium/UI/Interfaces/IForm.js",
                "~/Assets/Elysium/UI/Interfaces/IWizard.js",
                "~/Assets/Elysium/UI/Interfaces/IPaymentCard.js",
                "~/Assets/Elysium/UI/Interfaces/IMsgBox.js",
                "~/Assets/Elysium/UI/Interfaces/INotification.js",
                "~/Assets/Elysium/UI/Interfaces/ISpinner.js",
                "~/Assets/Elysium/UI/Interfaces/ITable.js",
                "~/Assets/Elysium/UI/Interfaces/IDatePicker.js",
                "~/Assets/Elysium/UI/Interfaces/IDateTimePicker.js",
                "~/Assets/Elysium/UI/Interfaces/IDatePickerRange.js",
                "~/Assets/Elysium/UI/Interfaces/IPagination.js",
                "~/Assets/Elysium/UI/Interfaces/IRating.js",
                "~/Assets/Elysium/UI/Interfaces/IReportViewer.js",
                "~/Assets/Elysium/UI/Interfaces/IStylesheet.js",
                "~/Assets/Elysium/UI/Interfaces/ICalendar.js",
                // UI Elements
                "~/Assets/Elysium/UI/Entities/RevolutionarySlider.js",
                "~/Assets/Elysium/UI/Entities/Form.js",
                "~/Assets/Elysium/UI/Entities/Wizard.js",
                "~/Assets/Elysium/UI/Entities/PaymentCard.js",
                "~/Assets/Elysium/UI/Entities/MsgBox.js",
                "~/Assets/Elysium/UI/Entities/Notification.js",
                "~/Assets/Elysium/UI/Entities/Spinner.js",
                "~/Assets/Elysium/UI/Entities/Table.js",
                "~/Assets/Elysium/UI/Entities/DatePicker.js",
                "~/Assets/Elysium/UI/Entities/DateTimePicker.js",
                "~/Assets/Elysium/UI/Entities/DatePickerRange.js",
                "~/Assets/Elysium/UI/Entities/Pagination.js",
                "~/Assets/Elysium/UI/Entities/Rating.js",
                "~/Assets/Elysium/UI/Entities/ReportViewer.js",
                "~/Assets/Elysium/UI/Entities/Stylesheet.js",
                "~/Assets/Elysium/UI/Entities/Calendar.js",
                // Directives
                "~/Assets/Elysium/Directives/RequestError.js",
                // Services
                "~/Assets/Elysium/App/Services/WorldService.js",
                "~/Assets/Elysium/App/Services/Auth/BearerService.js",
                "~/Assets/Elysium/App/Services/Auth/RegisterService.js",
                "~/Assets/Elysium/App/Services/Auth/UserService.js",
                "~/Assets/Elysium/App/Services/Auth/MenuItemService.js",

                "~/Assets/Elysium/App/Services/Core/AddressService.js",
                "~/Assets/Elysium/App/Services/Core/AssistantService.js",
                "~/Assets/Elysium/App/Services/Core/CarService.js",
                "~/Assets/Elysium/App/Services/Core/CarImageService.js",
                "~/Assets/Elysium/App/Services/Core/CommentService.js",
                "~/Assets/Elysium/App/Services/Core/DepartmentService.js",
                "~/Assets/Elysium/App/Services/Core/EventService.js",
                "~/Assets/Elysium/App/Services/Core/EventGroupService.js",
                "~/Assets/Elysium/App/Services/Core/EventGroupDepartmentService.js",
                "~/Assets/Elysium/App/Services/Core/InvitationService.js",
                "~/Assets/Elysium/App/Services/Core/ParticipantService.js",
                "~/Assets/Elysium/App/Services/Core/PassengerService.js",
                "~/Assets/Elysium/App/Services/Core/TransportService.js",
                "~/Assets/Elysium/App/Services/Core/DepartmentService.js",
                "~/Assets/Elysium/App/Services/Core/UserDepartmentService.js",
                // Controller Interface
                "~/Assets/Elysium/App/Interfaces/IController.js",
                // Controllers
                "~/Assets/Elysium/App/Controllers/Areas/HomeController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Auth/LoginController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Auth/RegisterController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Auth/ValidateUserController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Auth/RecoverController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Auth/ResetPasswordController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Auth/MyProfileController.js",
                "~/Assets/Elysium/App/Controllers/Areas/Core/EventController.js",
                // Globals
                "~/Assets/Elysium/App/Globals/BrdcstChGlobals.js",
                "~/Assets/Elysium/App/Globals/I18nGlobals.js",
                "~/Assets/Elysium/App/Globals/ValidatorGlobals.js",
                "~/Assets/Elysium/App/Globals/SessionGlobals.js",
                "~/Assets/Elysium/App/Globals/MenuItemGlobals.js"
            ).ForceOrdered());
            /*************************************************************************/
            /*                                  I18N                                 */
            /*************************************************************************/
            bundles.Add(new ScriptBundle("~/Assets/i18n/Scripts").Include(
               "~/Assets/I18n/i18n.js",
               "~/Assets/I18n/es-MX.js"
           ).ForceOrdered());
        }
    }
}
