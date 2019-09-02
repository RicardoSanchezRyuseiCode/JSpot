/**
 * LoginController.js Version 1.0
 * @abstract Controller for login page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 27/05/2018
 */
Elysium.App.Controllers.Areas.Auth.LoginController = function (arguments)
{
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var BearerService = Elysium.App.Services.Auth.BearerService;
    /*******************************************************************************/
    /*                                  Attributes                                 */
    /*******************************************************************************/
    var attr = arguments;
    // ParsleyForm
    var ParsleyForm;
    // Broadcast Channel
    var BrdcstCh = null;
    // Form object
    var IForm = Elysium.Implements(new Elysium.UI.Entities.Form(attr.UI.Form.Selector), [Elysium.UI.Interfaces.IForm]);
    // Previous Html
    var PreviousHtml = "";
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /**
     * @name EnableUI
     * @abstract Method to enable UI
     */
    var EnableUI = function (enable) {
        if (enable) {
            $(attr.UI.Input.Email).prop('disabled', !enable);
            $(attr.UI.Input.Password).prop('disabled', !enable);
            $(attr.UI.Button.Submit).prop('disabled', !enable);
            $(attr.UI.Checkbox.RememberMe).prop('disabled', !enable);
            $(attr.UI.Button.Submit).html(PreviousHtml);
        }
        else {
            $(attr.UI.Input.Email).prop('disabled', !enable);
            $(attr.UI.Input.Password).prop('disabled', !enable);
            $(attr.UI.Button.Submit).prop('disabled', !enable);
            $(attr.UI.Checkbox.RememberMe).prop('disabled', !enable);
            PreviousHtml = $(attr.UI.Button.Submit).html();
            $(attr.UI.Button.Submit).html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }
    /**
     * @name OnSuccessValidation
     * @abstract Event fired when form success
     */
    var OnSuccessValidation = function (e) {
        // Get user credentials
        var userCredentials = IForm.GetValues();
        // Disable UI
        EnableUI(false);
        // Request token
        BearerService.RequestToken(userCredentials).then(
            function (response) {
                // Current date
                var date = new Date();
                // Expiration date
                var expirationDate = new Date();
                // calculate expiration date
                expirationDate.setTime(date.getTime() + (response.expires_in * 1000));
                // Store bearer token in local storage
                localStorage["Elysium.Ryusei.Security.JWT"] = JSON.stringify(response);
                localStorage["Elysium.Ryusei.Security.JWT.ExpirationDate"] = expirationDate.getTime();    
                // Check reminder
                if (typeof userCredentials['RememberMe'] !== 'undefined') {
                    localStorage["Elysium.Ryusei.Security.Login.Email"] = userCredentials.Email;
                    localStorage["Elysium.Ryusei.Security.Login.Password"] = userCredentials.Password;
                }
                else {
                    localStorage.removeItem("Elysium.Ryusei.Security.Login.Email");
                    localStorage.removeItem("Elysium.Ryusei.Security.Login.Password");
                }
                // Send command
                BrdcstCh.SendCommand({ Instruction: 'Login.Redirect' });
                // Redirect
                window.location.href = Elysium.AppHost + "/Home";
            },
            function (error) {
                // Enable UI
                EnableUI(true);
                // Manage the error
                if (error.responseJSON != undefined) {
                    $(attr.UI.Label.LoginError).text(i18next.t(error.responseJSON.error_description));
                    $(attr.UI.Label.LoginError).show();
                }
                else {
                    $(attr.UI.Label.LoginError).text(i18next.t("Ryusei.Login.Error.LoginUnavailable"));
                    $(attr.UI.Label.LoginError).show();
                }
            }
        );
        // prevent submit
        return false;
    }

    /**
     * @name: SetRememberValues
     * @abstract: Method to set remember values
     */
    function SetRememberValues() {
        // colocamos el usuario
        if (typeof localStorage["Elysium.Ryusei.Security.Login.Email"] == 'undefined')
            $(attr.UI.Input.Email).val('');
        else
            $(attr.UI.Input.Email).val(localStorage["Elysium.Ryusei.Security.Login.Email"].toLowerCase());
        // colocamos el password
        if (typeof localStorage["Elysium.Ryusei.Security.Login.Password"] == 'undefined')
            $(attr.UI.Input.Password).val('');
        else
            $(attr.UI.Input.Password).val(localStorage["Elysium.Ryusei.Security.Login.Password"]);
        $(attr.UI.Checkbox.RememberMe).prop('checked', true);
    }
    /**
     * @name ReceiveCommand
     * @abstract Method to receive command
     * @param {any} command
     */
    var ReceiveCommand = function (command) {
        switch (command.Instruction) {
            case 'Login.Redirect': {
                window.location.href = Elysium.AppHost + "/Home";
                break;
            }
        }
    }
    /**
     * @name ParsleyErrorDisplay
     * @abstract Method to set display error
     */
    var ParsleyErrorDisplay = {
        errorsContainer: function (parsleyField) {
            if (parsleyField.$element.attr("name") == "Email")
                return $("[data-elysium-login-email-error]");
            if (parsleyField.$element.attr("name") == "Password")
                return $("[data-elysium-login-password-error]");
            return parsleyField;
        }
    };
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
        $('[data-toggle="tooltip"]').tooltip();
        $(".preloader").fadeOut();
        if (localStorage.getItem("Elysium.Ryusei.Security.JWT") !== null)
            window.location.href = Elysium.AppHost + "/Home";
        // Hide initial elements
        $(attr.UI.Label.LoginError).hide();
        // Subscribe to event
        ParsleyForm = $(attr.UI.Form.Selector).parsley(ParsleyErrorDisplay).on('form:submit', OnSuccessValidation);
        // Set remember values
        SetRememberValues();
        // Suscribe to broadcast chanel
        BrdcstCh = Elysium.GlobalObj["BrdcstChGlobal"];
        BrdcstCh.OnCommandReceive("Login", ReceiveCommand);
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