/**
 * RecoverController.js Version 1.0
 * @abstract Controller for recover password page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 07/07/2018
 */
Elysium.App.Controllers.Areas.Auth.RecoverController = function (arguments) {
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
    // PRevious Html
    var PreviousHtml = "";
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /**
     * Parsley configuration
     */
    var ParsleyConfiguration = {
        errorsContainer: function (parsleyField) {
            if (parsleyField.$element.attr("name") == "Email")
                return $("[data-elysium-register-email-error]");
            return parsleyField;
        }
    }
    /**
     * @name EnableUI
     * @abstract Method to enable UI
     */
    var EnableUI = function (enable) {
        if (enable) {
            $(Attr.UI.Input.Email).prop('disabled', !enable);
            $(Attr.UI.Button.Submit).prop('disabled', !enable);
            $(Attr.UI.Button.Submit).html(PreviousHtml);
        }
        else {
            $(Attr.UI.Input.Email).prop('disabled', !enable);
            $(Attr.UI.Button.Submit).prop('disabled', !enable);
            PreviousHtml = $(Attr.UI.Button.Submit).html();
            $(Attr.UI.Button.Submit).html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }
    /**
     * @name OnSuccessValidation
     * @abstract Event fired when form success
     */
    var OnSuccessValidation = function (e) {
        $(Attr.UI.Label.RecoverError).hide();
        if (grecaptcha.getResponse() == "") {
            $(Attr.UI.Label.RecoverError).text(i18next.t("Ryusei.Recover.EmptyCaptcha"));
            $(Attr.UI.Label.RecoverError).show();
            return false;
        }
        EnableUI(false);
        var params = {
            Email: $(Attr.UI.Input.Email).val().trim(),
            CaptchaToken: grecaptcha.getResponse()
        }
        RegisterService.RequestResetPassword(params).then(
            function (response) {
                if (response.Error == false) {
                    $(Attr.UI.Panel.Recover).hide();
                    $(Attr.UI.Panel.Success).show();
                }
                else {
                    $(Attr.UI.Label.RecoverError).text(i18next.t(response.Message));
                    $(Attr.UI.Label.RecoverError).show();
                    EnableUI(true);
                }
            },
            function (xhr) {
                if (xhr.responseText != undefined) {
                    $(Attr.UI.Label.RecoverError).text(i18next.t(xhr.responseText));
                    $(Attr.UI.Label.RecoverError).show();
                    EnableUI(true);
                }
                else {
                    $(Attr.UI.Label.RecoverError).text(i18next.t("Ryusei.Recover.UnexpectedError"));
                    $(Attr.UI.Label.RecoverError).show();
                    EnableUI(true);
                }
            }
        );
        return false;
    }
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
        $(Attr.UI.Panel.Success).hide();
        // Disable loader
        $('[data-toggle="tooltip"]').tooltip();
        $(".preloader").fadeOut();
        // Check if the user is logged or no
        if (localStorage.getItem("Elysium.Ryusei.Security.JWT") !== null)
            window.location.href = Elysium.AppHost + "/Home";
        // Hide elements
        $(Attr.UI.Label.RecoverError).hide();
        // Subscribe to event for form validation
        ParsleyForm = $(Attr.UI.Form.Selector).parsley(ParsleyConfiguration).on('form:submit', OnSuccessValidation);
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