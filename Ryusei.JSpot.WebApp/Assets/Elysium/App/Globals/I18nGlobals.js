/**
 * I18n.js Version 1.0
 * @abstract Clase utilitaria para manejo de lenguaje 
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 22/06/2018
 */
Elysium.App.Globals.I18nGlobals = function (arguments) {
    /*******************************************/
    /*                  Attributes             */
    /*******************************************/
    var Attr = { ListControllers: [] }
    var Args = arguments;
    /*******************************************/
    /*                  Methods                */
    /*******************************************/
    /**
     * @name Refresh
     * @abstract Method to refresh the text of one part
     * @param strSection jquery selector of section
     * @callback Method to execute after load the language
     */
    var Refresh = function (strSection, callback) {
        i18next.changeLanguage(localStorage.getItem("Elysium.Ryusei.Language"), function () {
            $(strSection).localize();
            callback();
        });
    }
    /**
     * @name FullRefresh
     * @abstract Method to do a full refresh 
     */
    var FullRefresh = function (callback) {
        Refresh('body', function () {
            Attr.ListControllers.forEach(function (element, index, array) {
                element.SetLocale(localStorage.getItem("Elysium.Ryusei.Language"));
            });
            if (callback != undefined)
                callback();
        });
    }
    /**
     * @name UpdateControllers
     * @abstract Method to update the controllers for i18n support
     * @param listControllers list of controllers for i18n support
     */
    var UpdateControllers = function (listControllers) {
        Attr.ListControllers = listControllers;
    }
    /**
     * @name Initialize
     * @abstract Method to initialize the component
     */
    var Initialize = function () {
        i18next.init({
            lng: localStorage.getItem("Elysium.Ryusei.Language"), 
            resources: i18nLocalResources
        }, function (err, t) {
            // Initialize the plugin
            jqueryI18next.init(i18next, $);
            // Localize the body
            $('body').localize();
        });
        // Bind Event to change the language
        $("[data-elysium-i18n]").on(Args.Event, function () {
            Elysium.Language = $(this).data("elysium-i18n");
            localStorage["Elysium.Ryusei.Language"] = Elysium.Language;
            Refresh('body', function () {
                Attr.ListControllers.forEach(function (element, index, array) {
                    element.SetLocale(Elysium.Language);
                });
            });
        });
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        UpdateControllers: UpdateControllers,
        Refresh: Refresh,
        FullRefresh: FullRefresh
    }
}