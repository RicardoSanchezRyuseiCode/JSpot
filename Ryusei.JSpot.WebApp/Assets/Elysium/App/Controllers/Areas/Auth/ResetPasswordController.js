/**
 * ResetPasswordController.js Version 1.0
 * @abstract Controller for reset password page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 08/07/2018
 */
Elysium.App.Controllers.Areas.Auth.ResetPasswordController = function (arguments) {
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var RegisterService = Elysium.App.Services.Auth.RegisterService;
    /*******************************************************************************/
    /*                                  Attributes                                 */
    /*******************************************************************************/
    // Attributes
    var Attr = arguments;
    // Parsley
    var Parsley = null;
    // Previous Html
    var PreviousHtml = "";
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /**
     * Parsley configuration
     */
    var ParsleyConfiguration = {
        errorsContainer: function (parsleyField) {
            if (parsleyField.$element.attr("name") == "Password")
                return $("[data-elysium-reset-password-error]");
            if (parsleyField.$element.attr("name") == "ConfirmPassword")
                return $("[data-elysium-reset-confirmpassword-error]");
            return parsleyField;
        }
    }
    /**
     * @name EnableUI
     * @abstract Method to enable UI
     */
    var EnableUI = function (enable) {
        if (enable) {
            $(Attr.UI.Input.Password).prop('disabled', !enable);
            $(Attr.UI.Input.ConfirmPassword).prop('disabled', !enable);
            $(Attr.UI.Button.Submit).prop('disabled', !enable);
            $(Attr.UI.Button.Submit).html(PreviousHtml);
        }
        else {
            $(Attr.UI.Input.Password).prop('disabled', !enable);
            $(Attr.UI.Input.ConfirmPassword).prop('disabled', !enable);
            $(Attr.UI.Button.Submit).prop('disabled', !enable);
            PreviousHtml = $(Attr.UI.Button.Submit).html();
            $(Attr.UI.Button.Submit).html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }

    /**
     * @name OnSuccessValidation
     * @abstract Event fired when form is success
     */
    var OnSuccessValidation = function (e) {
        // Hide error message
        $(Attr.UI.Label.RecoverError).hide();
        // Disable UI
        EnableUI(false);
        // Get params
        var params = {
            Token: Attr.Constants.Token,
            Password: $(Attr.UI.Input.Password).val().trim()
        }
        // Send request
        RegisterService.ResetPassword(params).then(
            function (response) {
                if (response.Error == false) {
                    $(Attr.UI.Div.RequestPanel).hide();
                    $(Attr.UI.Div.SuccessPanel).show();
                }
                else {
                    $(Attr.UI.Label.Error).text(i18next.t(response.Message));
                    $(Attr.UI.Label.Error).show();
                    EnableUI(true);
                }
            },
            function (xhr) {
                if (xhr.responseText != undefined) {
                    $(Attr.UI.Label.Error).text(i18next.t(xhr.responseText));
                    $(Attr.UI.Label.Error).show();
                    EnableUI(true);
                }
                else {
                    $(Attr.UI.Label.Error).text(i18next.t("Ryusei.ResetPassword.UnexpectedError"));
                    $(Attr.UI.Label.Error).show();
                    EnableUI(true);
                }
            }
        );
        return false;
    }
    /**
     * @name SetLocale
     * @abstract Method to set locale of application
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize the controller
     */
    var Initialize = function () {
        // Disable loader
        $('[data-toggle="tooltip"]').tooltip();
        $(".preloader").fadeOut();
        // Check if the user is logged or no
        if (localStorage.getItem("Elysium.Ryusei.Security.JWT") !== null)
            window.location.href = Elysium.AppHost + "/Home";
        // Hide elements
        $(Attr.UI.Label.Error).hide();
        // Subscribe to event for form validation
        ParsleyForm = $(Attr.UI.Form.Selector).parsley(ParsleyConfiguration).on('form:submit', OnSuccessValidation);
        // Hide panel
        $(Attr.UI.Div.SuccessPanel).hide();
        // disable autocomplete
        $('input').attr('autocomplete', 'off');
    }
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        SetLocale: SetLocale,
        Initialize: Initialize
    }
}