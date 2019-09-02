/**
 * MyProfileController.js Version 1.0
 * @abstract Controller for MyProfile
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 08/07/2018
 */
Elysium.App.Controllers.Areas.Auth.MyProfileController = function (arguments) {
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var UserService = Elysium.App.Services.Auth.UserService;
    var RegisterService = Elysium.App.Services.Auth.RegisterService;
    var CarService = Elysium.App.Services.Core.CarService;
    /*******************************************************************************/
    /*                                  Attributes                                 */
    /*******************************************************************************/
    // Arguments
    var Attr = arguments;
    // Spinner
    var ISpinner = Elysium.Implements(new Elysium.UI.Entities.Spinner({
        image: Elysium.SpinnerImage,
        bg_color: Elysium.SpinnerBackground
    }), [Elysium.UI.Interfaces.ISpinner]);
    // IFormUser
    var IFormProfile = Elysium.Implements(new Elysium.UI.Entities.Form(Attr.UI.Userform), [Elysium.UI.Interfaces.IForm]);
    // IFormPassword
    var IFormPassword = Elysium.Implements(new Elysium.UI.Entities.Form(Attr.UI.PasswordForm), [Elysium.UI.Interfaces.IForm]);
    // Profile form
    var ProfileForm = null;
    // Current User
    var CurrentUser = null;
    // Previous Htl
    var PreviousHtml = "";
    // Password Form
    var PasswordForm = null;
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /**
     * @name GetCurrentUser
     * @abstract Method to get current user
     */
    var GetCurrentUser = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        UserService.GetCurrent().then(
            function (user) {
                // Set current user data
                CurrentUser = user;
                // resolve the promise
                deferred.resolve();
            },
            function (xhr) {
                Elysium.Directives.RequestError.ThrowXhr(xhr);
                // reject the promise
                deferred.reject();
            }
        );
        // return the promise
        return deferred.promise();
    }
    /**
     * @name SetUserInformation
     * @abstract Method to set user information
     */
    var SetUserInformation = function () {
        $(Attr.UI.Username).text(CurrentUser.Name + " " + CurrentUser.Lastname);
        $(Attr.UI.Useremail).text(CurrentUser.Email);
        $(Attr.UI.Userphone).text(CurrentUser.Phone);
        $(Attr.UI.Usercompany).text(CurrentUser.Company);
        $(Attr.UI.Userjob).text(CurrentUser.Job);
    }
    /**
     * @name VisibleInfoFields
     * @abstract Method to set visible info fields
     * @param {any} isVisible
     */
    var VisibleInfoFields = function (isVisible) {
        if (isVisible)
            $(Attr.UI.InfoFields).show();
        else
            $(Attr.UI.InfoFields).hide();
    }
    /**
     * @name VisibleEditFields
     * @abstract Method to set visible edit fields
     * @param {any} isVisible
     */
    var VisibleEditFields = function (isVisible) {
        if (isVisible)
            $(Attr.UI.EditFields).show();
        else
            $(Attr.UI.EditFields).hide();
    }
    /**
     * @name ProfileFormConfig
     * @abstract Configuration of parsley
     */
    var ProfileFormConfig = {
        errorsContainer: function (parsleyField) {
            if (parsleyField.$element.attr("name") == "Name")
                return $("[data-elysium-jspot-myprofile-name-error]");
            if (parsleyField.$element.attr("name") == "Lastname")
                return $("[data-elysium-jspot-myprofile-lastname-error]");
            if (parsleyField.$element.attr("name") == "Email")
                return $("[data-elysium-jspot-myprofile-email-error]");
            if (parsleyField.$element.attr("name") == "Phone")
                return $("[data-elysium-jspot-myprofile-phone-error]");
            if (parsleyField.$element.attr("name") == "Company")
                return $("[data-elysium-jspot-myprofile-company-error]");
            if (parsleyField.$element.attr("name") == "Job")
                return $("[data-elysium-jspot-myprofile-job-error]");
            return parsleyField;
        }
    }
    /**
     * @name ShowEditInformation
     * @abstract Method to show edit information
     */
    var ShowEditInformation = function () {
        // hide info fields
        VisibleInfoFields(false);
        // Set information on form
        IFormProfile.SetValues(CurrentUser);
        // Reset profile form
        ProfileForm.reset()
        // show edit fields
        VisibleEditFields(true);
    }
    /**
     * @name HideEditInformation
     * @abstrat Method to hide edit information
     */
    var HideEditInformation = function () {
        // hide info fields
        VisibleEditFields(false);
        // Reset profile form
        ProfileForm.reset()
        // Set information on form
        IFormProfile.Clean();
        // show edit fields
        VisibleInfoFields(true);
    }
    /**
     * @name EnableUI
     * @abstract Method to enable ui
     */
    var EnableUI = function (enable) {
        $(Attr.UI.Userform).find("input").prop('disabled', !enable);
        $(Attr.UI.Userform).find("button").prop('disabled', !enable);
        if (enable) {
            $(Attr.UI.Userform).find("button[type=submit]").html(PreviousHtml);
        }
        else {
            PreviousHtml = $(Attr.UI.Userform).find("button[type=submit]").html();
            $(Attr.UI.Userform).find("button[type=submit]").html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }
    /**
     * @name OnSuccessProfile
     * @abstract Method to process after validation success
     */
    var OnSuccessProfile = function () {
        // Get user data
        var user = IFormProfile.GetValues();
        // Disable UI
        EnableUI(false);
        // Update user information
        UserService.Update(user).then(
            function (response) {
                // Check if there was an error
                if (response.Error) {
                    // Enable UI
                    EnableUI(true);
                    // Show error information
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                }
                GetCurrentUser().then(
                    function () {
                        // Enable
                        EnableUI(true);
                        // Hide
                        HideEditInformation();
                        // Set user information
                        SetUserInformation();
                        // Show success mesage
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t("Jspot.MyProfile.UpdateSuccess"), time: Elysium.NotificationTime });
                    }
                )
            },
            function (error) {
                // Enable UI
                EnableUI(true);
                // Manage the error
                if (error.responseJSON != undefined) {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseJSON.error_description), time: Elysium.NotificationTime });
                }
                else if (error.responseText != undefined) {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseText), time: Elysium.NotificationTime });
                }
                else {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t("Jspot.MyProfile.UpdateUserError"), time: Elysium.NotificationTime });
                }
            }
        );
        return false;
    }
    /*****************************************/
    /*                  Password             */
    /*****************************************/
    /**
     * @name VisibleInfoPasswordFields
     * @abstract Method to set visible info fields
     * @param {any} isVisible
     */
    var VisibleInfoPasswordFields = function (isVisible) {
        if (isVisible)
            $(Attr.UI.InfoPasswordField).show();
        else
            $(Attr.UI.InfoPasswordField).hide();
    }
    /**
     * @name VisibleInfoPasswordFields
     * @abstract Method to set visible edit fields
     * @param {any} isVisible
     */
    var VisibleEditPasswordFields = function (isVisible) {
        if (isVisible)
            $(Attr.UI.EditPasswordField).show();
        else
            $(Attr.UI.EditPasswordField).hide();
    }
    /**
     * @name ProfileFormConfig
     * @abstract Configuration of parsley
     */
    var PasswordFormConfig = {
        errorsContainer: function (parsleyField) {
            if (parsleyField.$element.attr("name") == "Password")
                return $("[data-elysium-jspot-myprofile-password-error]");
            if (parsleyField.$element.attr("name") == "ConfirmPassword")
                return $("[data-elysium-jspot-myprofile-confirmpassword-error]");
            return parsleyField;
        }
    }
    /**
     * @name ShowEditPasswordInformation
     * @abstract Method to show edit information
     */
    var ShowEditPasswordInformation = function () {
        // hide info fields
        VisibleInfoPasswordFields(false);
        // Reset profile form
        PasswordForm.reset()
        // show edit fields
        VisibleEditPasswordFields(true);
    }
    /**
     * @name HideEditPasswordInformation
     * @abstrat Method to hide edit information
     */
    var HideEditPasswordInformation = function () {
        // hide info fields
        VisibleEditPasswordFields(false);
        // Reset profile form
        PasswordForm.reset()
        // Set information on form
        IFormPassword.Clean();
        // show edit fields
        VisibleInfoPasswordFields(true);
    }
    /**
     * @name EnablePasswordUI
     * @abstract Method to enable ui
     */
    var EnablePasswordUI = function (enable) {
        $(Attr.UI.PasswordForm).find("input").prop('disabled', !enable);
        $(Attr.UI.PasswordForm).find("button").prop('disabled', !enable);
        if (enable) {
            $(Attr.UI.PasswordForm).find("button[type=submit]").html(PreviousHtml);
        }
        else {
            PreviousHtml = $(Attr.UI.PasswordForm).find("button[type=submit]").html();
            $(Attr.UI.PasswordForm).find("button[type=submit]").html('<i class="fas fa-cog fa-spin fa-lg"></i>');
        }
    }
    /**
     * @name OnSuccessPassword
     * @abstract Method to manage 
     */
    var OnSuccessPassword = function () {
        // Get user data
        var userPassword = IFormPassword.GetValues();
        // Disable UI
        EnablePasswordUI(false);
        // Update user information
        UserService.UpdatePassword(userPassword.Password).then(
            function (response) {
                // Check if there was an error
                if (response.Error) {
                    // Enable UI
                    EnablePasswordUI(true);
                    // Show error information
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                }
                // Enable
                EnablePasswordUI(true);
                // Hide
                HideEditPasswordInformation();
                // Show success mesage
                Elysium.UI.Entities.Notification.Success({ text: i18next.t("Jspot.MyProfile.UpdatePasswordSuccess"), time: Elysium.NotificationTime });
            },
            function (error) {
                // Enable UI
                EnablePasswordUI(true);
                // Manage the error
                if (error.responseJSON != undefined) {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseJSON.error_description), time: Elysium.NotificationTime });
                }
                else if (error.responseText != undefined) {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseText), time: Elysium.NotificationTime });
                }
                else {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t("Jspot.MyProfile.UpdatePasswordError"), time: Elysium.NotificationTime });
                }
            }
        );
        return false;
    }
    /*****************************************/
    /*               Close account           */
    /*****************************************/
    /**
     * @name CloseAccount
     * @abstract Method to close account
     */
    var CloseAccount = function () {
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Jspot.MyProfile.CloseAccountDialog.Title'),
            i18next.t('Jspot.MyProfile.CloseAccountDialog.Description'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Send request
                RegisterService.Deactivate(CurrentUser.UserId).then(
                    function (response) {
                        if (!response.Error) {
                            $("[data-elysium-session-logout]").trigger("click");
                        }
                        else {
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                        }
                    },
                    function (xhr) {
                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                    }
                );
            }
        );
    }
    /****************************************/
    /*              Profile picture         */
    /****************************************/
    /**
     * @name GetProfilePicture
     * @abstract Method to get profile picture
     */
    var GetProfilePicture = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Download  profile picture
        UserService.DownloadProfilePicture().then(
            function (response) {
                if (!response.Error) {
                    // complete base 64 image
                    var imgBase64 = response.Message;
                    if (imgBase64 != "") {
                        // Set profile img
                        $(Attr.UI.ProfilePicture).attr('src', imgBase64);
                    }
                    // resolve
                    deferred.resolve();
                }
                else {
                    // Show warning notification
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                    // Deferred reject
                    deferred.reject();
                }
            },
            function (xhr) {
                Elysium.Directives.RequestError.ThrowXhr(xhr);
                deferred.reject();
            }
        );
        // return the promise
        return deferred.promise();
    }
    /**
     * @name HandlerProgressUpload
     * @abstract Method to handle the process of file upload
     */
    var HandlerProgressUpload = function (e) {
        if (e.lengthComputable) {
            ISpinner.UpdateText(".profile-header", "Uploading (" + Math.round((e.loaded * 100) / e.total) + "%)");
        } else {
            console.log("Hola Mundo");
        }
    }
    /**
     * @name UploadProfilePicture
     * @abstract Method to upload profile picture
     */
    var UploadProfilePicture = function () {
        // Read input file
        var file = event.target.files[0];
        // Check type of the file
        if (!file.type.match(/image.*/)) {
            Elysium.UI.Entities.Notification.Warning({ text: i18next.t('Jspot.MyProfile.IncorrectImageFile'), time: Elysium.NotificationTime });
            return;
        }
        if (file.size >= Elysium.ProfilePictureSize) {
            Elysium.UI.Entities.Notification.Warning({ text: i18next.t('Jspot.MyProfile.MinimunSize'), time: Elysium.NotificationTime });
            return;
        }
        ISpinner.Show(Attr.UI.MainPanel);
        var formData = new FormData();
        formData.append('ProfileImage', file);
        // Upload the loco
        UserService.UploadProfilePicture(formData, HandlerProgressUpload).then(
            function (response) {
                if (response.Error) {
                    // Hide Spinner
                    ISpinner.Hide(".profile-header");
                    // Show warning notification
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                    // Return
                    return;
                }
                GetProfilePicture().then(function () {
                    // Set new profile photo
                    Elysium.GlobalObj["SessionGlobal"].SetProfilePhoto();
                    // Update in other tabs
                    Elysium.GlobalObj["BrdcstChGlobal"].SendCommand({ Instruction: 'SessionGlobal.UpdateProfilePhoto' });
                    // Hide Spinner
                    ISpinner.Hide(Attr.UI.MainPanel);
                    // Show success notification
                    Elysium.UI.Entities.Notification.Success({ text: i18next.t("Jspot.MyProfile.UploadProfilePhotoSuccess"), time: Elysium.NotificationTime });

                });
            },
            function (xhr) {
                // Hide spinner
                ISpinner.Hide(Attr.UI.MainPanel);
                // remove canvas from body
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
    }
    /**
     * @name ShowDialogInputPhoto
     * @abstract Method to show picture selection
     */
    var ShowDialogInputPhoto = function () {
        $(Attr.UI.ProfilePictureForm).trigger("reset");
        $(Attr.UI.ProfilePictureInput).trigger('click');
    }
    /*********************************/
    /*               CAR             */
    /*********************************/
    var CurrentCars = [];
    var IFormCar = Elysium.Implements(new Elysium.UI.Entities.Form(Attr.UI.CarForm), [Elysium.UI.Interfaces.IForm]);
    var ParsleyCar = null;

    /**
     * @name SetCollectionCars
     * @abstract Method  to set collection cars
     * @param {any} collectionCars
     */
    var SetCollectionCars = function (collectionCars) {
        $(Attr.UI.CarList).html("");
        // Loop in cars
        collectionCars.forEach(function (car, index, array) {
            var carHtml =   '<div class="d-flex flex-row comment-row mt-0">' +
                                '<div class="p-2"><img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/car-icon.png" alt="user" width="50" class="rounded-circle"></div>' +
                                '<div class="comment-text w-100">' +
                                    '<h6 class="font-medium">' + car.Model + ' - ' + car.Brand + '</h6>' +
                                    '<span class="mb-3 d-block"></span>' +
                                    '<div class="comment-footer">' +
                                        '<span class="label label-rounded label-primary" style="cursor:pointer" data-elysium-car-edit="' + car.CarId + '">Editar</span>' +
                                        '<span class="label label-rounded label-danger" style="cursor:pointer" data-elysium-car-delete="' + car.CarId + '">Eliminar</span>' +
                                    '</div>' +
                                '</div>' +
                            '</div>';

            $(Attr.UI.CarList).append(carHtml);
        });
    }
    /**
     * @name GetCars
     * @abstract Method to get cars
     */
    var GetCars = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        CarService.GetCurrent().then(
            function (collectionCars) {
                CurrentCars = collectionCars;
                // Set current events
                SetCollectionCars(collectionCars);
                // resolve the promise
                deferred.resolve();
            },
            function (xhr) {
                Elysium.Directives.RequestError.ThrowXhr(xhr);
                // reject the promise
                deferred.reject();
            }
        );
        // return the promise
        return deferred.promise();
    }
    /**
     * @name RefreshCar
     * @abstract Method to refresh car in window
     */
    var RefreshCar = function () {
        // Show spinner
        ISpinner.Show(Attr.UI.CarPanel);
        // Hide funtion
        var hide = function () { ISpinner.Hide(Attr.UI.CarPanel) };
        // Get 
        GetCars().then(hide, hide);
    }
    /**
     * @name CarNew
     * @abstract Method to show car panel
     */
    var CarNew = function () {
        // Hide panel
        $(Attr.UI.CarPanel).hide();
        // Show form panel
        $(Attr.UI.CarPanelForm).show();
    }
    /**
     * @name CarCancel
     * @abstract Method to car cancel
     */
    var CarCancel = function () {
        // Clean form
        IFormCar.Clean();
        // Clean pasrley
        ParsleyCar.reset();
        // Show form panel
        $(Attr.UI.CarPanelForm).hide();
        // Hide panel
        $(Attr.UI.CarPanel).show();
    }
    /**
     * @name CarFormOnSuccess
     * @abstract Method to execute when validation success
     */
    var CarFormOnSuccess = function () {
        // Get car information
        var car = IFormCar.GetValues();
        // Show spinner
        ISpinner.Show(Attr.UI.CarPanelForm);
        // Send information to server
        CarService.Save(car).then(
            function (response) {
                if (response.Error) {
                    // Hide spinner
                    ISpinner.Hide(Attr.UI.CarPanelForm);
                    // Show error information
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                    // return
                    return;
                }
                // Hide spinner
                ISpinner.Hide(Attr.UI.CarPanelForm);
                // Show success information
                Elysium.UI.Entities.Notification.Success({ text: i18next.t("El auto se ha guardado correctamente"), time: Elysium.NotificationTime });
                // Tirgger cacel
                CarCancel();
                // Refreh car
                RefreshCar();
            },
            function (xhr) {
                // Hide spinner
                ISpinner.Hide(Attr.UI.CarPanelForm);
                // Show error
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
        return false;
    }
    /**
     * @name CarEdit
     * @abstract Method to edit car
     */
    var CarEdit = function () {
        var carId = $(this).attr('data-elysium-car-edit');
        var car = CurrentCars.filter(function (car) {
            return car.CarId == carId;
        })[0];
        IFormCar.SetValues(car);
        CarNew();
    }
    /**
     * @name CarDelete
     * @abstract Metho to delete car
     */
    var CarDelete = function () {
        var carId = $(this).attr('data-elysium-car-delete');
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Eliminar auto'),
            i18next.t('¿Desea eliminar este auto?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Show spinner
                ISpinner.Show(Attr.UI.CarPanel);
                // Send information to server
                CarService.Deactivate(carId).then(
                    function (response) {
                        if (response.Error) {
                            // Hide spinner
                            ISpinner.Hide(Attr.UI.CarPanel);
                            // Show error information
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                            // return
                            return;
                        }
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.CarPanel);
                        // Show success information
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t("El auto se ha eliminado correctamente"), time: Elysium.NotificationTime });
                        // Refreh car
                        RefreshCar();
                    },
                    function (xhr) {
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.CarPanel);
                        // Show error
                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                    }
                );
            }
        );
    }
    /**
     * @name SearchCar
     * @abstract Method to search car
     */
    var SearchCar = function () {
        var value = $(this).val().trim().toUpperCase();

        if (value !== "") {
            var cars = CurrentCars.filter(function (car) {
                return (car.Model.toUpperCase() + ' - ' + car.Brand.toUpperCase()).indexOf(value) >= 0;
            });
        }
        else
            cars = CurrentCars;
        // Set collection cars
        SetCollectionCars(cars);
    }

    /*********************************/
    /*            Initialize         */
    /*********************************/

    /**
     * @name SetLocale
     * @abstract Method to set local of controller
     * @param {any} strLocale
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize the controller
     */
    var Initialize = function () {
        // Show spinner
        ISpinner.Show(Attr.UI.MainPanel);
        // Define hide function
        var hide = function () { ISpinner.Hide(Attr.UI.MainPanel); };
        // Update localization
        Elysium.GlobalObj["i18nGlobal"].Refresh(Attr.UI.MainPanel, function () {
            // Hide edit fields
            VisibleEditFields(false);
            VisibleEditPasswordFields(false);
            // Get information current user
            GetCurrentUser().then(
                function () { 
                    // Get profile photo
                    GetProfilePicture().then(
                        function () {

                            GetCars().then(function () {
                                // Set user information
                                SetUserInformation();
                                // Bind edit events
                                $(Attr.UI.Usereditbtn).click(ShowEditInformation);
                                $(Attr.UI.Usercancelbtn).click(HideEditInformation);
                                ProfileForm = $(Attr.UI.Userform).parsley(ProfileFormConfig).on('form:submit', OnSuccessProfile);
                                // Bind password events
                                $(Attr.UI.Passwordeditbtn).click(ShowEditPasswordInformation);
                                $(Attr.UI.Passwordcancelbtn).click(HideEditPasswordInformation);
                                PasswordForm = $(Attr.UI.PasswordForm).parsley(PasswordFormConfig).on('form:submit', OnSuccessPassword);
                                // Bind close account
                                $(Attr.UI.CloseAccountBtn).click(CloseAccount);
                                // Bind Profile Photo
                                $(Attr.UI.ProfilePicture).click(ShowDialogInputPhoto);
                                $(Attr.UI.ProfilePictureInput).change(UploadProfilePicture);


                                $(Attr.UI.CarInputSearch).keyup(SearchCar);
                                $(Attr.UI.CarNewButton).click(CarNew);
                                $(Attr.UI.CarCancelButton).click(CarCancel);
                                $(Attr.UI.CarList).on("click", "[data-elysium-car-edit]", CarEdit);
                                $(Attr.UI.CarList).on("click", "[data-elysium-car-delete]", CarDelete);
                                $(Attr.UI.CarPanelForm).hide();

                                ParsleyCar = $(Attr.UI.CarForm).parsley().on('form:submit', CarFormOnSuccess);


                                // Initialize scroller
                                $('.message-center, .customizer-body, .scrollable').perfectScrollbar({
                                    wheelPropagation: !0
                                });
                                // Hide loader
                                hide();

                            }, hide);

                           
                    }, hide);
                }, hide
            );
        });
    }
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        SetLocale: SetLocale,
        Initialize: Initialize
    }
}