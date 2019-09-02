/**
 * @name Pagination
 * @abstract Entitie to wrap the behavior of Table
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 04/08/2018
 */
Elysium.UI.Entities.Pagination = function (arguments) {
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    // Default arguments
    var Attr = arguments;
    // Default callback
    var Callback = null;
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name Update
     * @abstract Method to update pagination
     * @param totalPages Total quantity of pages
     * @param maxVisible Maximun quantity of total pages
     */
    var Update = function (totalPages, maxVisible) {
        $(Attr.selector).bootpag({ total: totalPages, maxVisible: maxVisible, page: 1 });
    }
    /**
     * @name OnChange
     * @abstract Event fired when page changed
     * @param {any} callback
     */
    var OnChange = function (callback) {
        Callback = callback;
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
        $(Attr.selector).bootpag(Attr).on("page", function (event, num) {
            if (Callback != null)
                Callback(event, num);
        });
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        Update: Update,
        OnChange: OnChange
    }
}