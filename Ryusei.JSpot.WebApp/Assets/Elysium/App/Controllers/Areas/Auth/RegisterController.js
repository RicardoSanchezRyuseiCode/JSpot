/**
 * RegisterController.js Version 1.0
 * @abstract Controller for login page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 27/05/2018
 */
Elysium.App.Controllers.Areas.Auth.RegisterController = function (arguments)
{
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
    // Form object
    var IForm = Elysium.Implements(new Elysium.UI.Entities.Form(Attr.UI.Form), [Elysium.UI.Interfaces.IForm]);
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
            if (parsleyField.$element.attr("name") == "Name")
                return $("[data-elysium-login-name-error]");
            if (parsleyField.$element.attr("name") == "Lastname")
                return $("[data-elysium-login-lastname-error]");
            if (parsleyField.$element.attr("name") == "Email")
                return $("[data-elysium-login-email-error]");
            if (parsleyField.$element.attr("name") == "Phone")
                return $("[data-elysium-login-phone-error]");
            if (parsleyField.$element.attr("name") == "Company")
                return $("[data-elysium-login-company-error]");
            if (parsleyField.$element.attr("name") == "Job")
                return $("[data-elysium-login-job-error]");
            if (parsleyField.$element.attr("name") == "Password")
                return $("[data-elysium-login-password-error]");
            if (parsleyField.$element.attr("name") == "ConfirmPassword")
                return $("[data-elysium-login-confirmpassword-error]");
            return parsleyField;
        }
    }
    /**
     * @name EnableUI
     * @abstract Method to enable UI
     */
    var EnableUI = function (enable) {
        $(Attr.UI.Form).find("input").prop('disabled', !enable);
        if (enable) {
            $(Attr.UI.Form).find("button").html(PreviousHtml);
        }
        else {
            PreviousHtml = $(Attr.UI.Form).find("button").html();
            $(Attr.UI.Form).find("button").html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }
    /**
     * @name OnSuccessValidation
     * @abstract Method to save register of user
     */
    var OnSuccessValidation = function () {
        // Get user credentials
        var user = IForm.GetValues();
        // Disable UI
        EnableUI(false);
        // Save the information
        RegisterService.CreateNewUser(user).then(
            function (response) {
                if (response.Error) {
                    // Enable UI
                    EnableUI(true);
                    // Set and show error
                    $(attr.UI.Label.LoginError).text(i18next.t(error.responseJSON.error_description));
                    $(attr.UI.Label.LoginError).show();
                }
                // Hide register panel
                $(Attr.UI.RegisterPanel).hide();
                // Show success panel
                $(Attr.UI.SuccessPanel).show();
            },
            function (error) {
                // Enable UI
                EnableUI(true);
                // Manage the error
                if (error.responseJSON != undefined) {
                    $(Attr.UI.RegisterError).text(i18next.t(error.responseJSON.error_description));
                    $(Attr.UI.RegisterError).show();
                }
                else if (error.responseText != undefined) {
                    $(Attr.UI.RegisterError).text(i18next.t(error.responseText));
                    $(Attr.UI.RegisterError).show();
                }
                else {
                    $(Attr.UI.RegisterError).text(i18next.t("Ryusei.Register.Error"));
                    $(Attr.UI.RegisterError).show();
                }
            }
        );


        return false;
    }
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
        // Enable parsley
        ParsleyForm = $(Attr.UI.Form).parsley(ParsleyConfiguration).on('form:submit', OnSuccessValidation);
    }
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        SetLocale: SetLocale,
        Initialize: Initialize
    }
}