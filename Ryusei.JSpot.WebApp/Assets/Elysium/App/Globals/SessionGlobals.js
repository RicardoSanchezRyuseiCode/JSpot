/**
 * SessionGlobals.js Version 1.0
 * @abstract  Global controller for session management
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 30/06/2017
 */
Elysium.App.Globals.SessionGlobals = function (arguments) {
    /*******************************************/
    /*                  Services               */
    /*******************************************/
    var BearerService = Elysium.App.Services.Auth.BearerService;
    var UserService = Elysium.App.Services.Auth.UserService;
    /*******************************************/
    /*                  Attributes             */
    /*******************************************/
    // Attributes
    var attr = arguments;
    // Broadcast Channel
    var BrdcstCh = null;
    // Id of thread
    var ThreadId = null;
    // Previous HTML
    var PreviousHtml = "";
    /*******************************************/
    /*                  Methods                */
    /*******************************************/
    /**
     * @name Logout
     * @abstract 
     */
    var Logout = function () {
        localStorage.removeItem("Elysium.Ryusei.WelcomeMessage");
        localStorage.removeItem("Elysium.Ryusei.Security.JWT");
        localStorage.removeItem("Elysium.Ryusei.Security.JWT.ExpirationDate");
        BrdcstCh.SendCommand({ Instruction: 'SessionGlobal.Logout' });
        window.location.href = "/Auth/Login";
    }
    /**
     * @name EnableUI
     * @abstract Method to enable/disable ui while request is doing
     */
    var EnableUI = function (enable) {
        if (enable) {
            $("[data-elysium-session-refresh]").prop('disabled', !enable);
            $("[data-elysium-session-logout]").prop('disabled', !enable);
            $("[data-elysium-session-refresh]").html(PreviousHtml);
        }
        else {
            $("[data-elysium-session-refresh]").prop('disabled', !enable);
            $("[data-elysium-session-logout]").prop('disabled', !enable);
            PreviousHtml = $("[data-elysium-session-refresh]").html();
            $("[data-elysium-session-refresh]").html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }
    /**
     * @name Refresh
     * @abstract Method to refresh session for user
     */
    var Refresh = function () {
        // Hide error
        $("[data-elysium-session-error]").hide();
        // Disable UI
        EnableUI(false);
        // Get profile
        var tokenInformation = Elysium.GetProfileUser();
        // Make request to refresh token
        BearerService.RefreshToken(tokenInformation.refresh_token).then(
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
                // Hide modal
                $("[data-elysium-session-modal]").modal("hide");
                // Enable UI
                EnableUI(true);
                // Send command
                BrdcstCh.SendCommand({ Instruction: 'SessionGlobal.Refresh' });
                
            },
            function () {
                // Show error
                $("[data-elysium-session-error]").show();
                // Enabel UI
                EnableUI(true);
            }
        );
    }
    /**
     * @name CheckAccess
     * @abstract Method to check session time
     */
    var CheckSessionTime = function () {
        // Get expiration time stamp
        var expirationTimeStamp = Number(localStorage.getItem("Elysium.Ryusei.Security.JWT.ExpirationDate"));
        // Get current time stamp
        var currentTimeStamp = (new Date()).getTime();
        // Chek if is valid
        if (currentTimeStamp > expirationTimeStamp) {
            Logout();
        }
        // Check tolerance time of session
        else if ((expirationTimeStamp - currentTimeStamp) <= 180000) {
            // Ask for refresh token
            $("[data-elysium-session-modal]").modal("show");
        }
    }
    /**
     * @name SetProfileInformation
     * @abstract Method to set profile information of user
     */
    var SetProfileInformation = function () {
        // Get profile
        var profileInfo = Elysium.GetProfileUser();
        // Set username
        $("[data-elysium-user-profile-name]").text(profileInfo.Name + " " + profileInfo.Lastname);
        // Set position
        $("[data-elysium-user-profile-position]").text(profileInfo.Job);
    }
    /**
     * @name SetProfilePhoto
     * @abstract Method to set profile photo
     */
    var SetProfilePhoto = function () {
        UserService.DownloadProfilePicture().then(
            function (response) {
                if (!response.Error) {
                    if(response.Message == "")
                        return;
                    // complete base 64 image
                    var imgBase64 = response.Message;
                    // Set profile img
                    $("[data-elysium-user-profile-photo]").attr('src', imgBase64);
                }
                else {
                    // Show warning notification
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                }
            }, 
            function(xhr) {
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }    
        );
    }
    /**
     * @name ReceiveCommand
     * @abstract Method to receive command
     * @param {any} command
     */
    var ReceiveCommand = function (command) {
        switch (command.Instruction)
        {
            case 'SessionGlobal.Refresh': {
                $("[data-elysium-session-modal]").modal("hide");
                break;
            }
            case 'SessionGlobal.Logout': {
                window.location.href = "/Auth/Login";
                break;
            }
            case 'SessionGlobal.UpdateProfileInformation': {
                SetProfileInformation();
                break;
            }
            case 'SessionGlobal.UpdateProfilePhoto': {
                SetProfilePhoto();
                break;
            }
        }
    }
    /**
     * @name AccessIsValid
     * @abstract Method to check 
     */
    var AccessIsValid = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Check the access
        UserService.GetCurrent().then(
            function (currentUser) {
                if (currentUser.IsValidated) {
                    if (localStorage.getItem("Elysium.Ryusei.WelcomeMessage") == null) {
                        localStorage["Elysium.Ryusei.WelcomeMessage"] = "true";
                        Elysium.UI.Entities.Notification.Info({ text: i18next.t('Jspot.App.SessionGlobal.Welcome'), time: Elysium.NotificationTime });
                    }
                    deferred.resolve();
                }
                else {
                    $("#page-loader").hide();
                    Elysium.UI.Entities.MsgBox.Warning({
                        text: i18next.t("Jspot.App.SessionGlobal.NotValidated"), callback: function () {
                            Logout();
                        }
                    });
                    deferred.reject();
                }   
            },
            function (xhr) {
                $("#page-loader").hide();
                if (xhr.responseText != undefined) {
                    Elysium.UI.Entities.MsgBox.Warning({
                        text: i18next.t(xhr.responseText), callback: function () {
                            Logout();
                        }
                    });
                }
                else {
                    Elysium.UI.Entities.MsgBox.Warning({
                        text: i18next.t('Jspot.App.SessionGlobal.AccessIsValid.UnexpectedError'), callback: function () {
                            Logout();
                        }
                    });
                }
                deferred.reject();
            }
        );
        // return promise
        return deferred.promise();
    }
    /**
     * @name Initialize
     * @abstract Method to intiialize SessionGlobal controller
     */
    var Initialize = function () {
        // Check if we have token
        if (localStorage.getItem("Elysium.Ryusei.Security.JWT") == null)
            window.location.href = "/Auth/Login"; 
        // Suscribe to broadcast chanel
        BrdcstCh = Elysium.GlobalObj["BrdcstChGlobal"];
        BrdcstCh.OnCommandReceive("SessionGlobal", ReceiveCommand);
        // Check if session is valid
        CheckSessionTime();
        // Check if we have access to application
        AccessIsValid().then(
            function () {
                // Enable set Interval
                ThreadId = setInterval(CheckSessionTime, (60000));
                // Add event to stop interval
                window.addEventListener("beforeunload", function (e) {
                    clearInterval(ThreadId);
                });
                // Set Profile information
                SetProfileInformation();
                //  Set profile photo
                SetProfilePhoto();
                // Hide error msg
                $("[data-elysium-session-error]").hide();
                // Bind refresh event
                $("[data-elysium-session-refresh]").click(Refresh);
                // Bind logout event
                $("[data-elysium-session-logout]").click(Logout);
                // Execution event
                if (attr.OnInitializeEnd !== undefined) {
                    attr.OnInitializeEnd(true);
                }
            }
        );
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetProfileInformation: SetProfileInformation,
        SetProfilePhoto: SetProfilePhoto,
        Logout: Logout
    }
}