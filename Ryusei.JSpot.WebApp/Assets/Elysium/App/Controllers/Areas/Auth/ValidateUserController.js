/**
 * RegisterController.js Version 1.0
 * @abstract Controller for login page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 27/05/2018
 */
Elysium.App.Controllers.Areas.Auth.ValidateUserController = function (arguments) {
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var RegisterService = Elysium.App.Services.Auth.RegisterService;
    /*******************************************************************************/
    /*                                  Attributes                                 */
    /*******************************************************************************/
    var Attr = arguments;
    // ParsleyForm
    var ParsleyForm;
    // Broadcast Channel
    var BrdcstCh = null;
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /*************************************/
    /*             Initialize            */
    /*************************************/
    /**
     * @name SetLocale
     * @abstract Method to set locale of controller
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize the controller
     */
    var Initialize = function () {
        // Hide success panel
        $(Attr.UI.SuccessPanel).hide();
        // Disable loader
        $('[data-toggle="tooltip"]').tooltip();
        $(".preloader").fadeOut();
        RegisterService.ValidateNewUser(Attr.Constants.Token).then(
            function (response) {
                if (!response.Error) {
                    // Hide Panel
                    $(Attr.UI.ValidatePanel).hide();
                    // Show success panel
                    $(Attr.UI.SuccessPanel).show();
                }
                else {
                    // Set text and show warning panel
                    Message = response.Message;
                    $(Attr.UI.ValidateError).html(i18next.t(response.Message));
                    $(Attr.UI.ValidateError).show();
                }
            },
            function () {
                // Hide panel
                $(Attr.UI.InfoPanel).hide();
                // Set text and show warning panel
                $(Attr.UI.ValidateError).html(i18next.t('Ryusei.ValidateUser.UnexpectedError'));
                $(Attr.UI.ValidateError).show();
            }
        );
    }
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        SetLocale: SetLocale,
        Initialize: Initialize
    }
}