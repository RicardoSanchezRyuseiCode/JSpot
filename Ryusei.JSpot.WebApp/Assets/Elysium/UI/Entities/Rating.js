/**
 * @name Table
 * @abstract Entitie to wrap the behavior of Table
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 04/08/2018
 */
Elysium.UI.Entities.Rating = function (arguments) {
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    var Attr = arguments;
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name Disable
     * @abstract Method to disable or enable control
     */
    var Disable = function(enable) {
        $(Attr.selector).barrating('readonly', enable);
    }
    /**
     * @name SetValue
     * @abstract Method to set value of controller
     */
    var SetValue = function (value) {
        $(Attr.selector).barrating('set', value);
    }
    /**
     * @name GetValue
     * @abstract Method to get value of rating
     */
    var GetValue = function () {
        return $(Attr.selector).val();
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
        $(Attr.selector).barrating({
            theme: 'css-stars'
        });
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        SetValue: SetValue,
        GetValue: GetValue,
        Disable: Disable
    }

}