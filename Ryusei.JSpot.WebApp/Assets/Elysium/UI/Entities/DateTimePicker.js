/**
 * @name DateTimePicker
 * @abstract Entitie to wrap the behavior of Table
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 04/08/2018
 */
Elysium.UI.Entities.DateTimePicker = function (arguments) {
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    var Attr = arguments;
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name Enable
     * @abstract Method to enable/disable DateTimePicker
     */
    var Enable = function(value) {
        //if (value)
        //    $(Attr.selector).data("DateTimePicker").enable();
        //else
        //    $(Attr.selector).data("DateTimePicker").disable();
    }
    /**
     * @name Show
     * @abstract 
     */
    var Show = function () {
        //$(Attr.selector).data("DateTimePicker").show();
    }
    /**
     * @name Hide
     * @abstract Method to hide datetime picker
     */
    var Hide = function () {
        //$(Attr.selector).data("DateTimePicker").hide();
    }
    /**
     * @name SetDate
     * @abstract Method to set date
     * @param {any} momenDate
     */
    var SetDate = function (momenDate) {
        $(Attr.selector).bootstrapMaterialDatePicker('setDate', momenDate);
    }
    /**
     * @name SetMinDate
     * @param {any} momentDate
     */
    var SetMinDate = function (momentDate) {
        $(Attr.selector).bootstrapMaterialDatePicker('setMinDate', momentDate);
    }
    /**
     * @name SetMinDate
     * @param {any} momentDate
     */
    var SetMaxDate = function (momentDate) {
        $(Attr.selector).bootstrapMaterialDatePicker('setMaxDate', momentDate);
    }
    /**
     * @name OnChangeEvt
     * @param {any} callback
     */
    var OnChangeEvt = function (callback) {
        $(Attr.selector).on("change", callback);
    }
    /**
     * @name SetLocale
     * @abstract Method to set locale
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize
     */
    var Initialize = function () {
        $(Attr.selector).bootstrapMaterialDatePicker(Attr.options);
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        Enable: Enable,
        Show: Show,
        Hide: Hide,
        OnChangeEvt: OnChangeEvt,
        SetDate: SetDate,
        SetMinDate: SetMinDate,
        SetMaxDate: SetMaxDate
    }
}