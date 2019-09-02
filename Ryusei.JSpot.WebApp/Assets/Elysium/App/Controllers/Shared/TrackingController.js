/**
 * TrackingController.js Version 1.0
 * @abstract Controller for MyProducts
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 17/11/2018
 */
Elysium.App.Controllers.Shared.TrackingController = function (arguments) {
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var UserDataService = Elysium.App.Services.CRM.UserDataService;
    /*******************************************************************************/
    /*                                  Attributes                                 */
    /*******************************************************************************/
    // Arguments
    var Attr = arguments;
    // Spinner
    var ISpinner = Elysium.Implements(new Elysium.UI.Entities.Spinner({
        image: Elysium.SpinnerImage,
        bg_color: Elysium.SpinnerBackground
    }), [Elysium.UI.Interfaces.ISpinner]);
    // DatePickerRange
    var DatePickerRange = null;
    // Table
    var TableTracking = Elysium.Implements(new Elysium.UI.Entities.Table({
        selector: "#TableSharedTracking",
        columnDefs: [
            {
                targets: [0, 1, 2],
                className: "dt-center"
            },
            {
                targets: [0],
                render: function (data, type, full, meta) {
                    return moment(data).format(Attr.CurrentCompanyConfiguration.DateFormat + " " + Attr.CurrentCompanyConfiguration.TimeFormat);
                }
            },
            {
                targets: 2,
                data: '',
                defaultContent:
                '<div class="btn-group">' +
                    '<button data-elysium-shared-tracking-table-user type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-user"></span></button>' +
                '</div>'
            }
        ],
        columns: [
            { data: "ModificationDate" },
            { data: "Operation" },
            { data: "" }
        ],
        order: [ [0, 'desc'] ]
    }), [Elysium.UI.Interfaces.ITable]);
    // Parsley
    var ParsleyDateRangeForm = null;
    // Parsley config
    var ParsleyDateRangeFormConfig = {
        errorsContainer: function (parsleyField) {
            return $("[data-elysium-shared-tracking-daterange-error]");
        }
    };
    // Element
    var ElementId = null;
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /**
     * @name CompanyPaymentsRangeSearchValidator
     * @abstract Method to add validator of date ranges
     */
    var DateRangeSearchValidator = function () {
        window.Parsley
            .addValidator('sharedTrackingRangeSearch', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    var startDate = moment($("#ModalSharedTracking .input-daterange").find('input[name="start"]').val(), Attr.CurrentCompanyConfiguration.DateFormat);
                    var endDate = moment($("#ModalSharedTracking .input-daterange").find('input[name="end"]').val(), Attr.CurrentCompanyConfiguration.DateFormat);
                    return Math.abs(moment.duration(startDate.diff(endDate)).asMonths()) <= 6;
                }
            });
    }
    /*********************************************************/
    /*                         Initialize                    */
    /*********************************************************/
    /**
     * @name Show
     * @abstract Method to show tracking modal
     */
    var Show = function (elementId) {
        ElementId = elementId;
        $("#ModalSharedTracking .input-daterange").find('input[name="start"]').val(moment().add(-1, 'months').format(Attr.CurrentCompanyConfiguration.DateFormat));
        $("#ModalSharedTracking .input-daterange").find('input[name="end"]').val(moment().add(1, 'days').format(Attr.CurrentCompanyConfiguration.DateFormat));
        $("#ModalSharedTracking").modal("show");
        $("[data-elysium-shared-tracking-form]").submit();
    }
    /**
     * @name GetTracking
     * @abstract Method to get tracking of elements
     */
    var GetTracking = function () {
        // Get dates
        var startDateStr = moment($("#ModalSharedTracking .input-daterange").find('input[name="start"]').val(), Attr.CurrentCompanyConfiguration.DateFormat).format("YYYY-MM-DDThh:mm:ss");
        var endDateStr = moment($("#ModalSharedTracking .input-daterange").find('input[name="end"]').val(), Attr.CurrentCompanyConfiguration.DateFormat).format("YYYY-MM-DDThh:mm:ss");
        TableTracking.SetData([]);
        // Show spinner 
        ISpinner.Show("#ModalSharedTracking .modal-dialog");
        // Request information
        Attr.Service.Get(ElementId, startDateStr, endDateStr).then(
            function (trackingElements) {
                // Set data
                TableTracking.SetData(trackingElements);
                // Hide Spinner
                ISpinner.Hide("#ModalSharedTracking .modal-dialog");
            },
            function (xhr) {
                // Hide spinner
                ISpinner.Hide("#ModalSharedTracking .modal-dialog");
                // Show the notification error
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
        return false;
    }
    /**
     * @name GetUserInformation
     * @abstract Method to get user information
     * @param {any} object
     * @param {any} row
     * @param {any} data
     * @param {any} event
     */
    var GetUserInformation = function (object, row, data, event) {
        // Request information
        UserDataService.GetById(data.UserDataId).then(
            function (userData) {
                if (userData != null) {
                    var parent = $(object).parent();
                    $(parent).html(userData.Email);
                }
                else {
                    var parent = $(object).parent();
                    $(parent).html(i18next.t("Plpp.Shared.Tracking.UserNotFound"));
                }
            },
            function (xhr) {
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
    }
    /**
     * @name UpdateService
     * @abstract Method to update service
     * @param {any} newService
     */
    var UpdateService = function (newService) {
        Attr.Service = newService;
    }
    /**
     * @name SetLocale
     * @abstract Method to set locale of controller
     */
    var SetLocale = function (strLocale) {
    }
    /**
     * @name Initialize
     * @abstract Method to initialize the controller 
     */
    var Initialize = function () {
        // Initialize date picker range
        DatePickerRange = Elysium.Implements(new Elysium.UI.Entities.DatePickerRange({
            selector: "#ModalSharedTracking .input-daterange",
            format: Attr.CurrentCompanyConfiguration.DateFormat.toLowerCase(),
            todayHighlight: true
        }), [Elysium.UI.Interfaces.IDatePickerRange]);
        DatePickerRange.Initialize();
        // Initialize table
        TableTracking.Initialize();
        TableTracking.OnEvent("click", "[data-elysium-shared-tracking-table-user]", GetUserInformation);
        // Add validator
        DateRangeSearchValidator();
        // Bind evends
        ParsleyDateRangeForm = $("[data-elysium-shared-tracking-form]").parsley(ParsleyDateRangeFormConfig).on('form:submit', GetTracking);
    }
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        Show: Show,
        UpdateService: UpdateService
    }
}