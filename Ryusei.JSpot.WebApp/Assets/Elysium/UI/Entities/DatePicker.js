/**
 * @name Table
 * @abstract Entitie to wrap the behavior of Table
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 04/08/2018
 */
Elysium.UI.Entities.DatePicker = function (arguments) {
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    var Attr = arguments;
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
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
        $(Attr.selector).datepicker(arguments);
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale
    }

}