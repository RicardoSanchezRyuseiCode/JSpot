/**
 * EventController.js Version 1.0
 * @abstract Controller for MyProfile
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 08/07/2018
 */
Elysium.App.Controllers.Areas.Core.EventController = function (arguments) {
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var UserService = Elysium.App.Services.Auth.UserService;
    var EventService = Elysium.App.Services.Core.EventService;
    var AddressService = Elysium.App.Services.Core.AddressService;
    var EventGroupService = Elysium.App.Services.Core.EventGroupService;
    var ParticipantService = Elysium.App.Services.Core.ParticipantService;
    var UserDepartmentService = Elysium.App.Services.Core.UserDepartmentService;
    var DepartmentService = Elysium.App.Services.Core.DepartmentService;
    var AssistantService = Elysium.App.Services.Core.AssistantService;
    var InvitationService = Elysium.App.Services.Core.InvitationService;
    var CarService = Elysium.App.Services.Core.CarService;
    var TransportService = Elysium.App.Services.Core.TransportService;
    var PassengerService = Elysium.App.Services.Core.PassengerService;
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
    // Event
    var CurrentUser = null;
    var CurrentEvent = null;
    var CurrentAddress = null;
    var CurrentEventGroups = [];
    var CurrentEventGroup = null;
    var CurrentParticipants = [];
    var CurrentUserDepartments = [];
    var CurrentDepartments = [];
    var CurrentAssistants = [];
    var CurrentInvitations = [];
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /******************************/
    /*           User            */
    /******************************/
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
    /******************************/
    /*           Event            */
    /******************************/
    /**
     * @name SetEventInformation
     * @abstract Method to set event information
     */
    var SetEventInformation = function () {
        // Set event information
        $(Attr.UI.EventName).text(CurrentEvent.Name);
        $(Attr.UI.EventDescription).text(CurrentEvent.Description);
        // StartDate
        var date = moment.utc(CurrentEvent.StartDate, "YYYY-MM-DDTHH:mm:ss")
        date = date.local();
        $(Attr.UI.EventStartDate).text(date.format("DD/MM/YYYY h:mm:ss a"));
        // EndDate
        date = moment.utc(CurrentEvent.EndDate, "YYYY-MM-DDTHH:mm:ss")
        date = date.local();
        $(Attr.UI.EventEndDate).text(date.format("DD/MM/YYYY h:mm:ss a"));
    }
    /**
     * @name GetCurrentEvent
     * @abstract Method to get current event
     */
    var GetCurrentEvent = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        EventService.GetById(Attr.EventId).then(
            function (event) {
                // Set current user data
                CurrentEvent = event;
                // Set event information
                SetEventInformation();
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
    /******************************/
    /*           Address          */
    /******************************/
    /**
     * @Name CreateMap
     * @abstract Method to create map with address information
     * @param {any} latitude
     * @param {any} longitude
     */
    var CreateMap = function (latitude, longitude) {
        // Create map
        var AddressMap = L.map(Attr.UI.AddressMap).setView([latitude, longitude], 14);
        // Add container to map
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1Ijoicnl1c2VpY29kZSIsImEiOiJjanpyc25ra2QwenpvM2JvYmZ0anBueTB3In0.OwWCVr1Tp-NQA5rJMDMSdw', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox.streets',
            accessToken: 'pk.eyJ1Ijoicnl1c2VpY29kZSIsImEiOiJjanpyc25ra2QwenpvM2JvYmZ0anBueTB3In0.OwWCVr1Tp-NQA5rJMDMSdw'
        }).addTo(AddressMap);
        // Add marker to map
        L.marker([latitude, longitude]).addTo(AddressMap);
    }
    /**
     * @name SetAddress
     * @abstract Method to set address
     */
    var SetAddress = function () {
        $(Attr.UI.AddressStreet).text(CurrentAddress.Street);
        $(Attr.UI.AddressNumbers).text(CurrentAddress.ExternalNumber + " " + CurrentAddress.InternalNumber);
        $(Attr.UI.AddressNeighborhood).text(CurrentAddress.Neighborhood);
        $(Attr.UI.AddressCountry).text(CurrentAddress.City + "," + CurrentAddress.State + "," + CurrentAddress.Country);
        $(Attr.UI.AddressZipCode).text(CurrentAddress.ZipCode);
        CreateMap(CurrentAddress.Latitude, CurrentAddress.Longitude);
    }
    /**
     * @name GetAddressByEventId
     * @abstract Method to get address by event id
     */
    var GetAddressByEventId = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        AddressService.GetByEventId(Attr.EventId).then(
            function (address) {
                // Set current user data
                CurrentAddress = address;
                // Set event information
                SetAddress();
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
    /******************************/
    /*           Grupos           */
    /******************************/

    /**
     * @name SetEventGroups
     * @abstract Method to set information of groups
     */
    var SetEventGroups = function () {
        $(Attr.UI.EventGroupList).html("");
        // Loop in collection
        CurrentEventGroups.forEach(function (eventGroup, index, array) {
            $(Attr.UI.EventGroupList).append(
                '<li class="media">' +
                '<button data-elysium-event-eventgroup-eventid="' + eventGroup.EventGroupId + '" type="button" class="btn btn-success btn-circle" style="margin-right: 15px;"><i class="fa fa-cubes"></i> </button>' +
                '<div class="media-body">' +
                '<h5 class="mt-0 mb-1">' + eventGroup.Name + '</h5>' +
                '' + eventGroup.Description +
                '</div>' +
                '</li>' +
                '<hr>'
            );
        });
    }
    /**
     * @name GetGroupByUserIdEventId
     * @abstract Method to GetGroupByUserIdEventId
     */
    var GetGroupByUserIdEventId = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        EventGroupService.GetByUserIdEventId(CurrentUser.UserId, CurrentEvent.EventId).then(
            function (collectionEventGroups) {
                // Set current user data
                CurrentEventGroups = collectionEventGroups;
                // Set event information
                SetEventGroups();
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
     * @name SetEventGroupInformation
     * @abstract Method to set event group information
     */
    var SetEventGroupInformation = function (eventGroup) {
        $(Attr.UI.EventGroupName).text(eventGroup.Name);
        $(Attr.UI.EventGroupDescription).text(eventGroup.Description);
        var date = moment.utc(eventGroup.StartDate, "YYYY-MM-DDTHH:mm:ss")
        date = date.local();
        $(Attr.UI.EventGroupStartDate).text(date.format("DD/MM/YYYY h:mm:ss a"));
        var date = moment.utc(eventGroup.EndDate, "YYYY-MM-DDTHH:mm:ss")
        date = date.local();
        $(Attr.UI.EventGroupEndDate).text(date.format("DD/MM/YYYY h:mm:ss a"));
        $(Attr.UI.EventGroupCapacity).text(eventGroup.Capacity);
    }
    /**
     * @name RenderParticipant
     * @abstract Method to render participant
     * @param {any} index
     * @param {any} deferred
     */
    var RenderParticipant = function (index, deferred) {
        // Request image
        UserService.DownloadProfilePictureById(CurrentParticipants[index].User.UserId).then(
            function (image) {
                var participantHtml = '<div class="d-flex flex-row comment-row">' +
                    '<div class="p-2"><img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg" alt="user" width="50" class="rounded-circle"></div>' +
                    '<div class="comment-text active w-100">' +
                    '<h6 class="font-medium">' + CurrentParticipants[index].User.Name + ' ' + CurrentParticipants[index].User.Lastname + '</h6>' +
                    '<span class="mb-3 d-block">' + CurrentParticipants[index].User.Email + '</span>' +
                    '</div>' +
                    '</div>';
                CurrentParticipants[index].User["Image"] = Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg';
                if (!image.Error && image.Message != "") {
                    // Get Row templae
                    participantHtml =
                        '<div class="d-flex flex-row comment-row">' +
                        '<div class="p-2"><img src="' + image.Message + '" alt="user" width="50" class="rounded-circle"></div>' +
                        '<div class="comment-text active w-100">' +
                        '<h6 class="font-medium">' + CurrentParticipants[index].User.Name + ' ' + CurrentParticipants[index].User.Lastname + '</h6>' +
                        '<span class="mb-3 d-block">' + CurrentParticipants[index].User.Email + '</span>' +
                        '</div>' +
                        '</div>';

                    CurrentParticipants[index].User["Image"] = image.Message;
                }
                // Add to list
                $(Attr.UI.ParticipantList).append(participantHtml);
                index = index + 1;
                if (index < CurrentParticipants.length)
                    RenderParticipant(index, deferred);
                else
                    deferred.resolve();
            }
        );
    }
    /**
     * @name SetParticipants
     * @abstract Method to set participants
     */
    var SetParticipants = function (deferred) {
        // Clean list
        $(Attr.UI.ParticipantList).html("");
        // Get free spots
        var freeSpots = CurrentEventGroup.Capacity - CurrentParticipants.length;
        if (freeSpots == 0)
            // Hide button if already full
            $(Attr.UI.EventGroupSignIn).hide();
        else
            $(Attr.UI.EventGroupSignIn).show();
        // Set the free spots
        $(Attr.UI.EventGroupSpots).text(freeSpots);
        // Check if user is assigned to the group
        for (var i = 0; i < CurrentParticipants.length; i++) {
            if (CurrentUser.UserId == CurrentParticipants[i].User.UserId) {
                $(Attr.UI.EventGroupSignIn).hide();
                break;
            }
        }
        // Check if we have elements
        if (CurrentParticipants.length > 0) {
            // Render elements with pictures
            var index = 0;
            RenderParticipant(index, deferred);
        }
        else
            deferred.resolve();
    }
    /**
     * @name GetParticipantsByEventGroupId
     * @abstract Method to get assistants by event group id
     * @param {any} eventGroupId
     */
    var GetParticipantsByEventGroupId = function (eventGroupId) {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        ParticipantService.GetByEventGroupId(eventGroupId).then(
            function (collectionParticipants) {
                // Set current user data
                CurrentParticipants = collectionParticipants;
                // Set event information
                SetParticipants(deferred);
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
     * @name SetEventGroupById
     * @abstract Method to set the informatio of group id
     */
    var SetEventGroupById = function () {
        // Get id
        eventGroupId = $(this).attr("data-elysium-event-eventgroup-eventid");
        // Hide function
        var hide = function () { ISpinner.Hide(Attr.UI.EventGroupPanelDetails); };
        // Show panel
        $(Attr.UI.EventGroupPanelDetails).show();
        // Show spinner
        ISpinner.Show(Attr.UI.EventGroupPanelDetails);
        // Click tabk
        $(Attr.UI.EventGroupDetailsPanel).click();
        // Request information
        EventGroupService.GetById(eventGroupId).then(
            function (eventGroup) {
                // Set event information
                SetEventGroupInformation(eventGroup);
                // Set current event group
                CurrentEventGroup = eventGroup;
                // Get the participants to group
                GetParticipantsByEventGroupId(eventGroupId).then(function () {
                    // Hide Spinner
                    ISpinner.Hide(Attr.UI.EventGroupPanelDetails);
                }, hide);
            },
            function (xhr) {
                // Hide Spinner
                ISpinner.Hide(Attr.UI.EventGroupPanelDetails);
                // Throw error
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
    }
    /**
     * @name SearchParticipan
     * @abstract Method to search participant from list
     */
    var SearchParticipant = function () {
        var searchValue = $(this).val().trim();
        var collectionParticipant = CurrentParticipants;
        if (searchValue != "") {
            collectionParticipant = CurrentParticipants.filter(function (participant) {
                return (participant.User.Name + ' ' + participant.User.Lastname).toUpperCase().indexOf(searchValue.toUpperCase()) >= 0 || participant.User.Email.toUpperCase().indexOf(searchValue.toUpperCase()) >= 0;
            });
        }
        $(Attr.UI.ParticipantList).html("");
        collectionParticipant.forEach(function (participant, index, array) {
            var participantHtml =
                '<div class="d-flex flex-row comment-row">' +
                '<div class="p-2"><img src="' + participant.User.Image + '" alt="user" width="50" class="rounded-circle"></div>' +
                '<div class="comment-text active w-100">' +
                '<h6 class="font-medium">' + participant.User.Name + ' ' + participant.User.Lastname + '</h6>' +
                '<span class="mb-3 d-block">' + participant.User.Email + '</span>' +
                '</div>' +
                '</div>';

            $(Attr.UI.ParticipantList).append(participantHtml);
        });
    }

    /**
     * @name SignInGroup
     * @abstract Method to sign in user to event
     */
    var SignInGroup = function () {
        // Get ids
        var participant = {
            UserId: CurrentUser.UserId,
            EventGroupId: CurrentEventGroup.EventGroupId
        }
        // Show dialog
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Inscribirse'),
            i18next.t('¿Desea inscribirse a este grupo?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Hide spinner
                ISpinner.Show(Attr.UI.EventGroupPanelDetails);
                // SignIn
                ParticipantService.Create(participant).then(
                    function (response) {
                        // Check if there was an error
                        if (response.Error) {
                            // Hide spinner
                            ISpinner.Hide(Attr.UI.EventGroupPanelDetails);
                            // Show error information
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                            // return
                            return;
                        }
                        //  Hide spinner
                        ISpinner.Hide(Attr.UI.EventGroupPanelDetails);
                        // Show success message
                        Elysium.UI.Entities.Notification.Success({ text: "Se ha registrado correctamente en el curso", time: Elysium.NotificationTime });
                        // Refresh the information
                        $('[data-elysium-event-eventgroup-eventid=' + CurrentEventGroup.EventGroupId + ']').trigger('click');

                    },
                    function (xhr) {
                        // Hide Spinner
                        ISpinner.Hide(Attr.UI.EventGroupPanelDetails);
                        // Throw error
                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                    }
                );
            }
        );
    }
    /******************************/
    /*       User-Department      */
    /******************************/
    /**
     * @name GetUserDepartment
     * @abstract Method to get userdepartments
     */
    var GetUserDepartment = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        UserDepartmentService.GetByUserIdEventId(CurrentUser.UserId, CurrentEvent.EventId).then(
            function (collectionUserDepartments) {
                // Set current user data
                CurrentUserDepartments = collectionUserDepartments;
                // resolve promise
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
    /******************************/
    /*           Department       */
    /******************************/
    /**
     * @name SetDepartments
     * @abstract MEthod to set departments
     */
    var SetDepartments = function () {
        // Loop collection
        CurrentDepartments.forEach(function (department, index, array) {
            // Add element to select
            $(Attr.UI.AreaSelect).append(new Option(department.Name, department.DepartmentId));
        });
    }
    /**
     * @name GetDepartments
     * @abstract Method to get departments
     */
    var GetDepartments = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        DepartmentService.GetByEventId(CurrentEvent.EventId).then(
            function (collectionDepartments) {
                // Set current user data
                CurrentDepartments = collectionDepartments;
                // Set departments
                SetDepartments();
                // resolve promise
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
     * @name ValidateDepartment
     * @abstract Method to validate is user already select a department
     */
    var ValidateDepartment = function () {
        if (CurrentUserDepartments.length <= 0) {
            // Show department panel
            $(Attr.UI.AreaPanel).show();
            $(Attr.UI.EventGroupPanel).hide();
        }
        else {
            // Hide panel
            $(Attr.UI.AreaPanel).hide();
            $(Attr.UI.EventGroupPanel).show();
        }
    }
    /**
     * @name AreaFormOnSuccess
     * @abstract Method on success form
     */
    var AreaFormOnSuccess = function () {
        // Create userDepartment object
        var userDepartmentPrm = {
            UserId: CurrentUser.UserId,
            EventId: CurrentEvent.EventId,
            CollectionDepartmentId: $(Attr.UI.AreaSelect).val()
        };
        // Show dialog
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Seleccionar area'),
            i18next.t('¿Las areas seleccionadas son correctas?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Hide spinner
                ISpinner.Show(Attr.UI.AreaPanel);
                // SignIn
                UserDepartmentService.Create(userDepartmentPrm).then(
                    function (response) {
                        // Check if there was an error
                        if (response.Error) {
                            // Hide spinner
                            ISpinner.Hide(Attr.UI.AreaPanel);
                            // Show error information
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                            // return
                            return;
                        }
                        GetUserDepartment().then(
                            function () {
                                GetGroupByUserIdEventId().then(
                                    function () {
                                        // Hide details
                                        $(Attr.UI.EventGroupPanelDetails).hide();
                                        //  Hide spinner
                                        ISpinner.Hide(Attr.UI.AreaPanel);
                                        // Validate Department
                                        ValidateDepartment();
                                        // Show success message
                                        Elysium.UI.Entities.Notification.Success({ text: "Se han asignado las áreas selecciondas", time: Elysium.NotificationTime });
                                    },
                                    function (xhr) {
                                        // Hide Spinner
                                        ISpinner.Hide(Attr.UI.AreaPanel);
                                        // Throw error
                                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                                    }
                                );
                            },
                            function (xhr) {
                                // Hide Spinner
                                ISpinner.Hide(Attr.UI.AreaPanel);
                                // Throw error
                                Elysium.Directives.RequestError.ThrowXhr(xhr);
                            }
                        );
                    },
                    function (xhr) {
                        // Hide Spinner
                        ISpinner.Hide(Attr.UI.AreaPanel);
                        // Throw error
                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                    }
                );
            }
        );
        return false;
    }
    /******************************/
    /*        Assistants          */
    /******************************/
    /**
     * @name RenderAssistants
     * @abstract Method to render assistanst
     * @param {any} index
     * @param {any} deferred
     */
    var RenderAssistants = function (index, deferred) {
        // Request image
        UserService.DownloadProfilePictureById(CurrentAssistants[index].User.UserId).then(
            function (image) {
                // Define label
                var label = '<span class="label label-rounded label-primary" style="cursor:pointer" data-elysium-event-assistant-addowner="' + CurrentAssistants[index].User.UserId + '" >Agregar Organizador</span>';
                if (CurrentAssistants[index].IsOwner)
                    label = '<span class="label label-rounded label-success" style="cursor:pointer" data-elysium-event-assistant-removeowner="' + CurrentAssistants[index].User.UserId + '" >Organizador</span>';
                var assistantHtml =
                    '<div class="d-flex flex-row comment-row">' +
                    '<div class="p-2"><img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg" alt="user" width="50" class="rounded-circle"></div>' +
                    '<div class="comment-text active w-100">' +
                    '<h6 class="font-medium">' + CurrentAssistants[index].User.Name + ' ' + CurrentAssistants[index].User.Lastname + '</h6>' +
                    '<span class="mb-3 d-block">' + CurrentAssistants[index].User.Email + '</span>' +
                    '<div class="comment-footer">' +
                    label +
                    '</div>' +
                    '</div>' +
                    '</div><br/>';
                CurrentAssistants[index].User["Image"] = Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg';
                if (!image.Error && image.Message != "") {
                    // Get Row templae
                    assistantHtml =
                        '<div class="d-flex flex-row comment-row">' +
                        '<div class="p-2"><img src="' + image.Message + '" alt="user" width="50" class="rounded-circle"></div>' +
                        '<div class="comment-text active w-100">' +
                        '<h6 class="font-medium">' + CurrentAssistants[index].User.Name + ' ' + CurrentAssistants[index].User.Lastname + '</h6>' +
                        '<span class="mb-3 d-block">' + CurrentAssistants[index].User.Email + '</span>' +
                        '<div class="comment-footer">' +
                        label +
                        '</div>' +
                        '</div>' +
                        '</div><br/>';
                    CurrentAssistants[index].User["Image"] = image.Message;
                }
                // Add to list
                $(Attr.UI.AssistantList).append(assistantHtml);
                index = index + 1;
                if (index < CurrentAssistants.length)
                    RenderAssistants(index, deferred);
                else
                    deferred.resolve();
            }
        );
    }
    /**
     * @name SetAssistants
     * @param {any} deferred
     */
    var SetAssistants = function (deferred) {
        // Clean list
        $(Attr.UI.AssistantList).html("");
        // Set quantity of assistants
        $(Attr.UI.AssistantQuantity).text(CurrentAssistants.length);
        // Check if we have elements
        if (CurrentAssistants.length > 0) {
            // Render elements with pictures
            var index = 0;
            RenderAssistants(index, deferred);
        }
        else
            deferred.resolve();
    }
    /** 
     *  @name ValidateAssistantOwner
     *  @abstract Method to validate assistant owner
     */
    var ValidateAssistantOwner = function () {

        var owner = CurrentAssistants.filter(function (assistan) {
            return assistan.UserId == CurrentUser.UserId && assistan.IsOwner == true;
        });

        if (owner.length == undefined || owner.length == null || owner.length <= 0) {
            $('[href="#assistantpanel"]').parent().hide();
            $('[href="#invitationspanel"]').parent().hide();

        }
    }
    /**
     * @name GetAssistants
     * @abstract Method to get assistants
     */
    var GetAssistants = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        AssistantService.GetByEventId(CurrentEvent.EventId).then(
            function (collectionAssistants) {
                // Set current user data
                CurrentAssistants = collectionAssistants;
                // Validate assistants owner
                ValidateAssistantOwner();
                // Set event information
                SetAssistants(deferred);
                
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
     * @name SearchAssistant
     * @abstract Method to serch assistant
     */
    var SearchAssistant = function () {
        var searchValue = $(this).val().trim();
        var collectionAssistants = CurrentAssistants;
        if (searchValue != "") {
            collectionAssistants = CurrentAssistants.filter(function (assistant) {
                return (assistant.User.Name + ' ' + assistant.User.Lastname).toUpperCase().indexOf(searchValue.toUpperCase()) >= 0 || assistant.User.Email.toUpperCase().indexOf(searchValue.toUpperCase()) >= 0;
            });
        }
        $(Attr.UI.AssistantList).html("");
        collectionAssistants.forEach(function (assistant, index, array) {
            // Define label
            var label = '<span class="label label-rounded label-primary" style="cursor:pointer" data-elysium-event-assistant-addowner="' + assistant.User.UserId + '" >Agregar Organizador</span>';
            if (assistant.IsOwner)
                label = '<span class="label label-rounded label-success" style="cursor:pointer" data-elysium-event-assistant-removeowner="' + assistant.User.UserId + '" >Organizador</span>';
            var assistantHtml =
                '<div class="d-flex flex-row comment-row">' +
                '<div class="p-2"><img src="' + assistant.User.Image + '" alt="user" width="50" class="rounded-circle"></div>' +
                '<div class="comment-text active w-100">' +
                '<h6 class="font-medium">' + assistant.User.Name + ' ' + assistant.User.Lastname + '</h6>' +
                '<span class="mb-3 d-block">' + assistant.User.Email + '</span>' +
                '<div class="comment-footer">' +
                label +
                '</div>' +
                '</div>' +
                '</div><br/>';
            $(Attr.UI.AssistantList).append(assistantHtml);
        });
    }

    /**
     * @name UpdateAssistant
     * @abstract Method to update assistant
     * @param {any} userId
     * @param {any} eventId
     */
    var UpdateAssistant = function (userId, eventId, isOwner) {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        AssistantService.Update({ UserId: userId, EventId: eventId, IsOwner: isOwner }).then(
            function (response) {
                if (response.Error) {
                    // show warning message
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                    // reject
                    deferred.reject();
                    // return
                    return;
                }
                Elysium.UI.Entities.Notification.Success({ text: "El asistente se ha modificado correctamente", time: Elysium.NotificationTime });
                // resolve promise
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
     * @name AddOwner
     * @abstract Method to add owner
     */
    var AddOwner = function (event) {
        // Show dialog
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Agregar organizador(a)'),
            i18next.t('¿Desea agregar esta persona como organizadora del evento?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Get id  of owner to add
                var userId = $(event.target).attr("data-elysium-event-assistant-addowner");
                // Show spinner
                ISpinner.Show(Attr.UI.AssistantPanel);
                var hide = function () { ISpinner.Hide(Attr.UI.AssistantPanel); };
                // Hide assistant
                UpdateAssistant(userId, CurrentEvent.EventId, true).then(
                    function () {
                        // Update Assistant
                        GetAssistants().then(
                            hide, hide
                        );
                    }, hide
                );
            }
        );
    }
    /**
     * @name RemoveOwner
     * @abstract Method to remove owner
     */
    var RemoveOwner = function (event) {
        // Show dialog
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Retirar organizador'),
            i18next.t('¿Desea que esta persona deje de ser organizadora del evento?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Get id  of owner to add
                var userId = $(event.target).attr("data-elysium-event-assistant-removeowner");
                // Show spinner
                ISpinner.Show(Attr.UI.AssistantPanel);
                var hide = function () { ISpinner.Hide(Attr.UI.AssistantPanel); };
                // Hide assistant
                UpdateAssistant(userId, CurrentEvent.EventId, false).then(
                    function () {
                        // Update Assistant
                        GetAssistants().then(
                            hide, hide
                        );
                    }, hide
                );
            }
        );
    }
    /******************************/
    /*         Invitations        */
    /******************************/
    /**
     * @name ProfileFormConfig
     * @abstract Configuration of parsley
     */
    var InvitationFormConfig = {
        errorsContainer: function (parsleyField) {
            return $("[data-elysium-event-invitations-error]");
        }
    }

    /**
     * @name RenderInvitations
     * @abstract Method to render invitations
     * @param {any} index
     * @param {any} deferred
     */
    var RenderInvitations = function (index, deferred) {
    }
    /**
     * @name SetInvitations
     * @abstract Method to set invitations
     */
    var SetInvitations = function () {
        // Clean html
        $(Attr.UI.InvitationList).html("");

        var pending = 0; 
        var accepted = 0;
        var rejected = 0;

        // Loop to render elements
        CurrentInvitations.forEach(function (invitation, index, array) {
            // Define date
            var date = moment.utc(invitation.SendDate, "YYYY-MM-DDTHH:mm:ss")
            date = date.local();
            // Define status
            var status = '<span class="label label-rounded label-primary">Pendiente</span>';
            if (invitation.ResponseDate != null)
                if (invitation.Answer) {
                    status = '<span class="label label-rounded label-success">Aceptada</span>';
                    accepted++;
                }
                else {
                    status = '<span class="label label-rounded label-danger">Rechazada</span>';
                    rejected++;
                }
            else
                pending++;
            // Set invitation
            var invitationHtml =
                '<div class="d-flex flex-row comment-row mt-0">' +
                    '<div class="p-2"><img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg" alt="user" width="50" class="rounded-circle"></div>' +
                    '<div class="comment-text w-100">' +
                        '<h6 class="font-medium">' + invitation.Email + '</h6>' +
                        '<span class="mb-3 d-block"></span>' +
                        '<div class="comment-footer">' +
                            '<span class="text-muted float-right">Fecha de envió - ' + date.format("DD/MM/YYYY h:mm:ss a") + '</span>' +
                            status +
                        '</div>' +
                    '</div>' +
                '</div>';
            // Append to hml
            $(Attr.UI.InvitationList).append(invitationHtml);
        });
        // Set counters
        $(Attr.UI.InvitationsPending).text(pending);
        $(Attr.UI.InvitationsRejected).text(rejected);
        $(Attr.UI.InvitationsAccepted).text(accepted);
    }
    /**
     * @name GetInvitations
     * @abstract Method to get invitations
     */
    var GetInvitations = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        InvitationService.GetByEventId(CurrentEvent.EventId).then(
            function (collectionInvitations) {
                // Set current user data
                CurrentInvitations = collectionInvitations;
                // Set event information
                SetInvitations();
                // resolve promise
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
     * @name SearchInvitation
     * @abstract Method to search invitation in list
     */
    var SearchInvitation = function () {
        var searchValue = $(this).val().trim();
        var collectionInvitation = CurrentInvitations;
        if (searchValue != "") {
            collectionInvitation = CurrentInvitations.filter(function (invitation) {
                return invitation.Email.toUpperCase().indexOf(searchValue.toUpperCase()) >= 0;
            });
        }
        $(Attr.UI.InvitationList).html("");
        collectionInvitation.forEach(function (invitation, index, array) {
            // Define date
            var date = moment.utc(invitation.SendDate, "YYYY-MM-DDTHH:mm:ss")
            date = date.local();
            // Define status
            var status = '<span class="label label-rounded label-primary">Pendiente</span>';
            if (invitation.ResponseDate != null)
                if (invitation.Answer)
                    status = '<span class="label label-rounded label-success">Aceptada</span>';
                else
                    status = '<span class="label label-rounded label-danger">Rechazada</span>';
            // Set invitation
            var invitationHtml =
                '<div class="d-flex flex-row comment-row mt-0">' +
                    '<div class="p-2"><img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg" alt="user" width="50" class="rounded-circle"></div>' +
                    '<div class="comment-text w-100">' +
                        '<h6 class="font-medium">' + invitation.Email + '</h6>' +
                        '<span class="mb-3 d-block"></span>' +
                        '<div class="comment-footer">' +
                            '<span class="text-muted float-right">Fecha de envió - ' + date.format("DD/MM/YYYY h:mm:ss a") + '</span>' +
                            status +
                        '</div>' +
                    '</div>' +
                '</div>';
            // Append to hml
            $(Attr.UI.InvitationList).append(invitationHtml);
        });
    }
    /**
     * @name SetFileName
     * @abstract Method to set file name 
     */
    var SetFileName = function () {
        // Check if we have file
        if (event.target.files.length > 0) {
            // Read input file
            var file = event.target.files[0];
            $(Attr.UI.InvitationLabel).text(file.name);
        }
        else 
            $(Attr.UI.InvitationLabel).text("Escoger archivo");
    }
    /**
     * @name InvitiationFormOnSuccess
     * @abstract Method to 
     */
    var InvitiationFormOnSuccess = function () {

        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Enviar invitaciones'),
            i18next.t('¿Desea enviar las invitaciones cargadas?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Get file
                var file = $(Attr.UI.InvitationInput).get(0).files[0];
                // Define reader
                var reader = new FileReader();
                reader.onload = function (e) {
                    var data
                    if (!e) {
                        data = reader.content;
                    }
                    else {
                        data = e.target.result;
                    }
                    // read book
                    var workbook = XLSX.read(data, { type: 'binary' });
                    // Get firs sheet
                    var first_sheet_name = workbook.SheetNames[0];
                    // Parse To JSON
                    var jsonData = XLSX.utils.sheet_to_json(workbook.Sheets[first_sheet_name], { header: ["Email"] });
                    if (jsonData != null && jsonData.length > 0) {
                        // Shift header
                        jsonData.shift();
                        // Check if we have data to import
                        if (jsonData != null && jsonData.length > 0) {
                            // Get data to submit
                            var data = jsonData;
                            // Show spinner
                            ISpinner.Show(Attr.UI.InvitationPanel);
                            var emails = [];
                            jsonData.forEach(function (email, index, array) {
                                emails.push(email.Email);
                            });
                            // Call the function
                            InvitationService.Import(emails, CurrentEvent.EventId).then(
                                function (response) {
                                    // Check the response
                                    if (response.Error) {
                                        // Show warning notification
                                        Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                                        // Hide spinner
                                        ISpinner.Hide(Attr.UI.InvitationPanel);
                                        // Return
                                        return;
                                    }
                                    GetInvitations().then(function () {
                                        // Show success notification
                                        Elysium.UI.Entities.Notification.Success({ text: "Se han enviado correctamente las invitaciones", time: Elysium.NotificationTime });
                                        // Clean all
                                        $(Attr.UI.InvitationLabel).text("Escoger archivo");
                                        $(Attr.UI.InvitationInput).val("");
                                        // Hide spinner
                                        ISpinner.Hide(Attr.UI.InvitationPanel);
                                    }, function () { ISpinner.Hide(Attr.UI.InvitationPanel); });
                                },
                                function (xhr) {
                                    // Hide spinner
                                    ISpinner.Hide(Attr.UI.InvitationPanel);
                                    // Show the notification error
                                    Elysium.Directives.RequestError.ThrowXhr(xhr);
                                }
                            );
                        }
                        else {
                            Elysium.UI.Entities.Notification.Success({ text: i18next.t("Plpp.MasterData.Part.Wizard.EmptyFile"), time: Elysium.NotificationTime });
                        }
                    }
                }
                reader.readAsBinaryString(file);
            }
        );

        return false;
    }
    /******************************/
    /*         Transport          */
    /******************************/
    var CurrentCars = [];
    var DepartureMap = null;
    var DepartureMarker = null;
    var ArriveMap = null;
    var ArriveMarker = null;
    var IDatePickerTransport = null;
    var TravelSense = false;
    var SexType = 0;
    var TransportForm = null;
    var IFormTransport = Elysium.Implements(new Elysium.UI.Entities.Form(Attr.UI.TransportForm), [Elysium.UI.Interfaces.IForm]);
    var CurrentGoPassenger = null;
    var CurrentReturnPassenger = null;
    var DetailsDepartureMap = null;
    var DetailsDepartureMarker = null;
    var DetailsArriveMap = null;
    var DetailsArriveMarker = null;
    var TableTransportGo = Elysium.Implements(new Elysium.UI.Entities.Table({
        selector: Attr.UI.TransportTableGo,
        responsive: true,
        paging: true,
        columnDefs: [
            {
                targets: [0],
                className: "dt-center",
                defaultContent: '<img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/car-icon.png" alt="user" width="50">'
                
            },
            {
                targets: [1],
                className: "",
                render: function (data, type, full, meta) {
                    return '<h6>' + full.Car.Model + '</h6> <small class="text-muted">' + full.Car.Brand + '</small>';
                }
            },
            {
                targets: [2],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    return '<span class="round" style="background:' + full.Car.Color + '">&nbsp;</span>';
                }
            },
            {
                targets: [3],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    if(full.IsFull)
                        return '<span class="round round-danger">&nbsp;</span>';
                    else 
                        return '<span class="round round-success">&nbsp;</span>';
                }
            },
            {
                targets: [5],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    var date = moment.utc(full.DepartureDate).local();
                    return date.format('DD/MM/YYYY h:mm:ss a');
                }
            },
            {
                targets: [6],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    if (CurrentGoPassenger != null) {

                        if (CurrentGoPassenger.Transport.TransportId == full.TransportId) {
                            return  '<div class="btn-group">' +
                                        '<button type="button" class="btn btn-success btn-icon btn-lg"><span class="far fa-check-circle"></span></button>' +
                                        '<button data-elysium-event-transport-table-details type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-list"></span></button>' +
                                    '</div>';
                        }
                        else {
                            return  '<div class="btn-group">' +
                                        '<button data-elysium-event-transport-table-details type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-list"></span></button>' +
                                    '</div>';
                        }

                    }
                    else {
                        return  '<div class="btn-group">' +
                                    '<button data-elysium-event-transport-table-signin type="button" class="btn btn-primary btn-icon btn-lg"><span class="fas fa-sign-in-alt"></span></button>' +
                                    '<button data-elysium-event-transport-table-details type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-list"></span></button>' +
                                '</div>';
                    }
                }
            }
        ],
        columns: [
            { data: "" },
            { data: "" },
            { data: "" },
            { data: "" },
            { data: "Description" },
            { data: "" },
            { data: "" }
        ]
    }), [Elysium.UI.Interfaces.ITable]);

    var TransportTableReturn = Elysium.Implements(new Elysium.UI.Entities.Table({
        selector: Attr.UI.TransportTableReturn,
        responsive: true,
        paging: true,
        columnDefs: [
            {
                targets: [0],
                className: "dt-center",
                defaultContent: '<img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/car-icon.png" alt="user" width="50">'

            },
            {
                targets: [1],
                className: "",
                render: function (data, type, full, meta) {
                    return '<h6>' + full.Car.Model + '</h6> <small class="text-muted">' + full.Car.Brand + '</small>';
                }
            },
            {
                targets: [2],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    return '<span class="round" style="background:' + full.Car.Color + '">&nbsp;</span>';
                }
            },
            {
                targets: [3],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    if (full.IsFull)
                        return '<span class="round round-danger">&nbsp;</span>';
                    else
                        return '<span class="round round-success">&nbsp;</span>';
                }
            },
            {
                targets: [5],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    var date = moment.utc(full.DepartureDate).local();
                    return date.format('DD/MM/YYYY h:mm:ss a');
                }
            },
            {
                targets: [6],
                className: "dt-center",
                render: function (data, type, full, meta) {
                    if (CurrentReturnPassenger != null) {

                        if (CurrentReturnPassenger.Transport.TransportId == full.TransportId) {
                            return '<div class="btn-group">' +
                                '<button type="button" class="btn btn-success btn-icon btn-lg"><span class="far fa-check-circle"></span></button>' +
                                '<button data-elysium-event-transport-table-details type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-list"></span></button>' +
                                '</div>';
                        }
                        else {
                            return '<div class="btn-group">' +
                                '<button data-elysium-event-transport-table-details type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-list"></span></button>' +
                                '</div>';
                        }

                    }
                    else {
                        return '<div class="btn-group">' +
                            '<button data-elysium-event-transport-table-signin type="button" class="btn btn-primary btn-icon btn-lg"><span class="fas fa-sign-in-alt"></span></button>' +
                            '<button data-elysium-event-transport-table-details type="button" class="btn btn-info btn-icon btn-lg"><span class="fas fa-list"></span></button>' +
                            '</div>';
                    }
                }
            }
        ],
        columns: [
            { data: "" },
            { data: "" },
            { data: "" },
            { data: "" },
            { data: "Description" },
            { data: "" },
            { data: "" }
        ]
    }), [Elysium.UI.Interfaces.ITable]);
    /**
     * @name SetCars
     * @abstract Method to set cars
     * @param {any} collectionCars
     */
    var SetCars = function (collectionCars) {
        // remove options from city
        $(Attr.UI.TransportCarId).find('option').remove();
        // Add select
        $(Attr.UI.TransportCarId).append(new Option("Seleccionar auto", "default"));
        // Loop to add options
        collectionCars.forEach(function (car, index, array) {
            $(Attr.UI.TransportCarId).append(new Option(car.Model + ' - ' + car.Brand, car.CarId));
        });
        // Set value
        $(Attr.UI.TransportCarId).val("default");
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
                // CurrentCars
                CurrentCars = collectionCars;
                // Set cars
                SetCars(CurrentCars);
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
     * @name CreateTransportMap
     * @abstract Method to create map
     * @param {any} latitude
     * @param {any} longitude
     */
    var CreateTransportMap = function (selector, latitude, longitude) {
        // Create map
        var map = L.map(selector).setView([latitude, longitude], 14);
        // Add container to map
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1Ijoicnl1c2VpY29kZSIsImEiOiJjanpyc25ra2QwenpvM2JvYmZ0anBueTB3In0.OwWCVr1Tp-NQA5rJMDMSdw', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox.streets',
            accessToken: 'pk.eyJ1Ijoicnl1c2VpY29kZSIsImEiOiJjanpyc25ra2QwenpvM2JvYmZ0anBueTB3In0.OwWCVr1Tp-NQA5rJMDMSdw'
        }).addTo(map);
        return map;
    }
    /**
     * @name SetPosition
     * @abstract Method to set position
     * @param {any} position
     */
    var SetTransportPosition = function (position) {
        DepartureMap = CreateTransportMap(Attr.UI.TransportDepartureMap, position.coords.latitude, position.coords.longitude);
        DepartureMap.on('click', AddMarkerDeparture);
        ArriveMap = CreateTransportMap(Attr.UI.TransportArriveMap, position.coords.latitude, position.coords.longitude);
        ArriveMap.on('click', AddMarkerArrive);
    }
    /**
     * @name EnableMap
     * @abstract Method to enable map
     */
    var EnableTransportMap = function () {
        DepartureMap = CreateTransportMap(Attr.UI.TransportDepartureMap, 19.42847, -99.12766);
        DepartureMap.on('click', AddMarkerDeparture);
        ArriveMap = CreateTransportMap(Attr.UI.TransportArriveMap, 19.42847, -99.12766);
        ArriveMap.on('click', AddMarkerArrive);
    }
    /**
     * @name AddMarkerDeparture
     * @abstract Method to add marker departure
     */
    var AddMarkerDeparture = function (ev) {
        // Check if we have previous marker
        if (DepartureMarker != null)
            DepartureMarker.remove()
        // Set marker
        DepartureMarker = L.marker(ev.latlng).addTo(DepartureMap);
    }
    /**
     * @name AddMarkerArrive
     * @abstract Method to add marker arrive
     */
    var AddMarkerArrive = function (ev) {
        // Check if we have previous marker
        if (ArriveMarker != null)
            ArriveMarker.remove()
        // Set marker
        ArriveMarker = L.marker(ev.latlng).addTo(ArriveMap);
    }
    /**
     * @name CreateDatePicker
     * @abstract Method to create date pickers
     */
    var CreateDatePicker = function () {
        // create date
        var startDate = moment.utc(CurrentEvent.StartDate, "YYYY-MM-DDTHH:mm:ss").local();
        var endDate = moment.utc(CurrentEvent.EndDate, "YYYY-MM-DDTHH:mm:ss").local();
        // Create StartDate Picker
        IDatePickerTransport = Elysium.Implements(new Elysium.UI.Entities.DateTimePicker({
            selector: Attr.UI.TransportDepartureDate,
            options: {
                format: 'DD-MM-YYYY h:mm:ss a',
                lang: 'es',
                time: true
            }
        }), [Elysium.UI.Interfaces.IDateTimePicker]);
        IDatePickerTransport.Initialize();
        IDatePickerTransport.SetDate(moment());
    }
    /**
     * @name AddGoTransport
     * @abstract Method to add transport
     */
    var AddGoTransport = function () {
        // Hide/Show panels
        $(Attr.UI.TransportPanel).hide();
        $(Attr.UI.TransportPanelForm).show();
        // Set travel sense
        TravelSense = true;
        // Set min date as end date of event
        var endDate = moment.utc(CurrentEvent.EndDate, "YYYY-MM-DDTHH:mm:ss").local();
        IDatePickerTransport.SetMaxDate(endDate);
        IDatePickerTransport.SetDate(moment());

        if (DepartureMap == null) {
            DepartureMap = CreateTransportMap(Attr.UI.TransportDepartureMap, CurrentAddress.Latitude, CurrentAddress.Longitude);
            DepartureMap.on('click', AddMarkerDeparture);
        }
        if (ArriveMap == null) {
            ArriveMap = CreateTransportMap(Attr.UI.TransportArriveMap, CurrentAddress.Latitude, CurrentAddress.Longitude);
            ArriveMap.on('click', AddMarkerArrive);
        }
        // Set marker
        ArriveMap.setView([CurrentAddress.Latitude, CurrentAddress.Longitude], 14);
        ArriveMarker = L.marker([CurrentAddress.Latitude, CurrentAddress.Longitude]).addTo(ArriveMap);
    }
    /**
     * @name AddReturnTransport
     */
    var AddReturnTransport = function () {
        // Hide/Show panels
        $(Attr.UI.TransportPanel).hide();
        $(Attr.UI.TransportPanelForm).show();
        // Set travel sense
        TravelSense = false;
        // Set min date
        var startDate = moment.utc(CurrentEvent.StartDate, "YYYY-MM-DDTHH:mm:ss").local();
        IDatePickerTransport.SetMinDate(startDate);
        IDatePickerTransport.SetDate(moment());

        if (DepartureMap == null) {
            DepartureMap = CreateTransportMap(Attr.UI.TransportDepartureMap, CurrentAddress.Latitude, CurrentAddress.Longitude);
            DepartureMap.on('click', AddMarkerDeparture);
        }
        if (ArriveMap == null) {
            ArriveMap = CreateTransportMap(Attr.UI.TransportArriveMap, CurrentAddress.Latitude, CurrentAddress.Longitude);
            ArriveMap.on('click', AddMarkerArrive);
        }
        // Set marker
        DepartureMap.setView([CurrentAddress.Latitude, CurrentAddress.Longitude], 14);
        DepartureMarker = L.marker([CurrentAddress.Latitude, CurrentAddress.Longitude]).addTo(DepartureMap);

    }
    /**
     * @name CancelAddTransport
     * @abstract Method to cancel add transport
     */
    var CancelAddTransport = function () {
        // Clean form
        IFormTransport.Clean();
        // Reset pasley
        TransportForm.reset();
        // Hide/Show panels
        $(Attr.UI.TransportPanel).show();
        $(Attr.UI.TransportPanelForm).hide();
        // Remove markers
        if (ArriveMarker != null)
            ArriveMarker.remove();
        if (DepartureMarker != null)
            DepartureMarker.remove();
    }
    /**
     * @name EnableValidators
     * @abstract Method to enable extra validators
     */
    var EnableValidators = function () {
        window.Parsley
            .addValidator('departuremarkerSet', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return DepartureMarker != null;
                }
            });

        window.Parsley
            .addValidator('arrivemarkerSet', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return ArriveMarker != null;
                }
            });

        window.Parsley
            .addValidator('selectCustom', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return value != 'default';
                }
            });
    }
    /**
     * @name TransportFormOnSuccess
     */
    var TransportFormOnSuccess = function () {
        // Get data
        var transport = IFormTransport.GetValues();
        // Departure date
        transport.DepartureDate = moment(transport.DepartureDate, "DD-MM-YYYY h:mm:ss a").format("YYYY-MM-DDTHH:mm:ss")
        // Add travel sense
        transport['TravelSense'] = TravelSense;
        // Add sextype
        transport['SexType'] = SexType;
        // Add event is
        transport['EventId'] = CurrentEvent.EventId;
        // Add departure latitude and longitude
        transport['DepartureLatitude'] = DepartureMarker.getLatLng().lat;
        transport['DepartureLongitude'] = DepartureMarker.getLatLng().lng;
        // Add arrive latitude and longitude
        transport['ArriveLatitude'] = ArriveMarker.getLatLng().lat;
        transport['ArriveLongitude'] = ArriveMarker.getLatLng().lng;
        // Add user
        transport['UserId'] = CurrentUser.UserId;
        // Show confirmation
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Agregar transporte'),
            i18next.t('¿Desea agregar este transporte para el evento?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Show spinner
                ISpinner.Show(Attr.UI.TransportPanelForm);
                // Send information to server
                TransportService.Create(transport).then(
                    function (response) {
                        if (response.Error) {
                            // Hide spinner
                            ISpinner.Hide(Attr.UI.TransportPanelForm);
                            // Show error information
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                            // return
                            return;
                        }
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.TransportPanelForm);
                        // Show success information
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t("El transporte se ha agregado correctamente"), time: Elysium.NotificationTime });
                        // Trigger cancel
                        CancelAddTransport();
                        // Refreh transport
                        if (TravelSense)
                            RefreshGoTransport();
                        else
                            RefreshReturnTransport();
                    },
                    function (xhr) {
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.TransportPanelForm);
                        // Show error
                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                    }
                );
            }
        );
        return false;
    }
    /**
     * @name SetCollectionTransport
     * @abstract Method to set collection transport
     */
    var SetCollectionTransport = function (collectionTransport) {
        if (TravelSense)
            TableTransportGo.SetData(collectionTransport);
        else
            TransportTableReturn.SetData(collectionTransport);
    }
    /**
     * @name GetTransport
     * @abstract Method to get transport
     */
    var GetTransport = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        TransportService.GetByEventId(CurrentEvent.EventId, TravelSense).then(
            function (collectionTransport) {
                // Set current events
                SetCollectionTransport(collectionTransport);
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
     * @name RefreshTransport
     * @abstract Method to refresh go transport
     */
    var RefreshGoTransport = function () {
        ISpinner.Show(Attr.UI.TransportPanel);
        var hide = function () { ISpinner.Hide(Attr.UI.TransportPanel); };
        TravelSense = true;
        GetSelectedTransport().then(function () {
            GetTransport().then(function () {
                ValidateAddCar();
                hide();
            }, hide);
        }, hide);
    }
    /**
     * @name RefreshReturnTransport
     * @abstract Method to refresh return transport
     */
    var RefreshReturnTransport = function () {
        ISpinner.Show(Attr.UI.TransportPanel);
        var hide = function () { ISpinner.Hide(Attr.UI.TransportPanel); };
        TravelSense = false;
        GetSelectedTransport().then(function () {
            GetTransport().then(function () {
                ValidateAddCar();
                hide();
            }, hide);
        }, hide);
    }
    /**
     * @name AddPassenger
     * @abstract Method to add passenger
     * @param {any} object
     * @param {any} row
     * @param {any} data
     * @param {any} event
     */
    var AddPassenger = function (object, row, data, event) {
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Seleccionar transporte'),
            i18next.t('¿Desea seleccionar este auto como su transporte?'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Show spinner
                ISpinner.Show(Attr.UI.TransportPanel);
                // Send information to server
                PassengerService.Create({ TransportId: data.TransportId, UserId: CurrentUser.UserId }).then(
                    function (response) {
                        if (response.Error) {
                            // Hide spinner
                            ISpinner.Hide(Attr.UI.TransportPanel);
                            // Show error information
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                            // return
                            return;
                        }
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.TransportPanel);
                        // Show success information
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t("Ahora es un pasajero de este transporte"), time: Elysium.NotificationTime });
                        // Refresh data
                        if (data.TravelSense)
                            RefreshGoTransport();
                        else
                            RefreshReturnTransport();
                    },
                    function (xhr) {
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.TransportPanel);
                        // Show error
                        Elysium.Directives.RequestError.ThrowXhr(xhr);
                    }
                );
            }
        );
    }
    /**
     * @name RenderParticipants
     * @abstract Method to render assistanst
     * @param {any} index
     * @param {any} deferred
     */
    var RenderParticipants = function (index, collectionPassenger, deferred) {
        // Request image
        UserService.DownloadProfilePictureById(collectionPassenger[index].User.UserId).then(
            function (image) {
                // Define label
                var passengerHtml =
                    '<div class="d-flex flex-row comment-row">' +
                        '<div class="p-2"><img src="' + Elysium.AppHost + '/Assets/Xtreme/images/users/1.jpg" alt="user" width="50" class="rounded-circle"></div>' +
                        '<div class="comment-text active w-100">' +
                            '<h6 class="font-medium">' + collectionPassenger[index].User.Name + ' ' + collectionPassenger[index].User.Lastname + '</h6>' +
                            '<span class="mb-3 d-block">' + collectionPassenger[index].User.Email + '</span>' +
                        '</div>' +
                    '</div><br/>';
                if (!image.Error && image.Message != "") {
                    // Get Row templae
                    passengerHtml =
                        '<div class="d-flex flex-row comment-row">' +
                            '<div class="p-2"><img src="' + image.Message + '" alt="user" width="50" class="rounded-circle"></div>' +
                            '<div class="comment-text active w-100">' +
                                '<h6 class="font-medium">' + collectionPassenger[index].User.Name + ' ' + collectionPassenger[index].User.Lastname + '</h6>' +
                                '<span class="mb-3 d-block">' + collectionPassenger[index].User.Email + '</span>' +
                            '</div>' +
                        '</div><br/>';
                }
                // Add to list
                $(Attr.UI.ModalTransportPassengers).append(passengerHtml);
                index = index + 1;
                if (index < collectionPassenger.length)
                    RenderParticipants(index, collectionPassenger, deferred);
                else
                    deferred.resolve(collectionPassenger);
            }
        );
    }
    /**
     * @name SetPassengers
     * @param {any} deferred
     */
    var SetPassengers = function (collectionPassengers, deferred) {
        // Clean list
        $(Attr.UI.ModalTransportPassengers).html("");
        // Check if we have elements
        if (collectionPassengers.length > 0) {
            // Render elements with pictures
            var index = 0;
            RenderParticipants(index, collectionPassengers, deferred);
        }
        else
            deferred.resolve(collectionPassenger);
    }
    /**
     * @name GetPassengersByTransportId
     * @param {any} transportId
     */
    var GetPassengersByTransportId = function (transportId) {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        PassengerService.GetByTransportId(transportId).then(
            function (collectionPassenger) {
                // Set current events
                SetPassengers(collectionPassenger, deferred);
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
     * @name DetailsTransport
     * @abstract Method to show details of transport
     * @param {any} object
     * @param {any} row
     * @param {any} data
     * @param {any} event
     */
    var DetailsTransport = function (object, row, data, event) {
        // reset tab
        $('[href="#detailslocation"]').click();
        // Show mdoal
        $(Attr.UI.ModalTransportDetail).modal('show');
        // Hide function
        var hide = function () { ISpinner.Hide(Attr.UI.ModalTransportDetail + ' .modal-dialog') };
        // Show spinner
        ISpinner.Show(Attr.UI.ModalTransportDetail + ' .modal-dialog');
        // Get
        GetPassengersByTransportId(data.TransportId).then(
            function (collectionPassenger) {
                // Set spots
                $(Attr.UI.ModalTransportSpots).text(data.Car.Spots);
                // Set free spots
                $(Attr.UI.ModalTransportSpotsFree).text(data.Car.Spots - collectionPassenger.length);

                $('[data-elysium-event-modal-driver]').text(data.Car.User.Name + ' ' + data.Car.User.Lastname);

                $('[data-elysium-event-modal-phone]').html('<a style="color: white;" href="tel:' + data.Car.User.Phone + '">' + data.Car.User.Phone + '</a>');

                // Set map information
                if (DetailsDepartureMap == null) {
                    DetailsDepartureMap = CreateTransportMap(Attr.UI.ModalTransportDepartureMap, data.DepartureLatitude, data.DepartureLongitude);
                } else {
                    DetailsDepartureMap.setView([data.DepartureLatitude, data.DepartureLongitude], 14);
                }
                if (DetailsArriveMap == null) {
                    DetailsArriveMap = CreateTransportMap(Attr.UI.ModalTransportArriveMap, data.ArriveLatitude, data.ArriveLongitude);
                } else {
                    DetailsArriveMap.setView([data.ArriveLatitude, data.ArriveLongitude], 14);
                }
                // Set markers
                if (DetailsDepartureMarker != null)
                    DetailsDepartureMarker.remove();
                DetailsDepartureMarker = L.marker([data.DepartureLatitude, data.DepartureLongitude]).addTo(DetailsDepartureMap);
                if (DetailsArriveMarker != null)
                    DetailsArriveMarker.remove();
                DetailsArriveMarker = L.marker([data.ArriveLatitude, data.ArriveLongitude]).addTo(DetailsArriveMap);
                // Hide
                hide();
            }, hide
        );
    }
    /**
     * @name GetSelectedTransport
     * @abstract Method to get selected transport
     */
    var GetSelectedTransport = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        PassengerService.GetByUserIdEventIdSense(CurrentUser.UserId, CurrentEvent.EventId, TravelSense).then(
            function (collectionPassenger) {
                // Set current events
                if (TravelSense)
                    CurrentGoPassenger = collectionPassenger;
                else
                    CurrentReturnPassenger = collectionPassenger;
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
     * @name ValidateAddCar
     * @abstract Method to validate add car
     */
    var ValidateAddCar = function () {
        if (CurrentGoPassenger != null)
            $(Attr.UI.TransportAddGo).hide();

        if (CurrentReturnPassenger != null)
            $(Attr.UI.TransportAddReturn).hide();
    }
    /******************************/
    /*        Initialize          */
    /******************************/
    /**
     * @name SetLocale
     * @abstract Method to set locale
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
            // Initialize table
            TableTransportGo.Initialize();
            TransportTableReturn.Initialize();
            // Get current user
            GetCurrentUser().then(function () {
                // Get information of event
                GetCurrentEvent().then(function () {
                    // Get userdepartments
                    GetUserDepartment().then(function () {
                        // Get departments
                        GetDepartments().then(function () {
                            // Get Address
                            GetAddressByEventId().then(function () {
                                // Get list of groups
                                GetGroupByUserIdEventId().then(function () {
                                    // Get invitations
                                    GetInvitations().then(function () {
                                        // Get cars
                                        GetCars().then(function () {
                                            // Travel sense
                                            TravelSense = true;
                                            // Get transport
                                            GetSelectedTransport().then(function () {
                                                // Get selected transport
                                                GetTransport().then(function () {
                                                    // Travel sense 
                                                    TravelSense = false;
                                                    GetSelectedTransport().then(function () {
                                                        // Get selected transport
                                                        GetTransport().then(function () {
                                                            // Enable maps
                                                            //EnableTransportMap();
                                                            // Get Assistants
                                                            GetAssistants();
                                                            // Validate department
                                                            ValidateDepartment();
                                                            // Bind events
                                                            $(Attr.UI.EventGroupList).on('click', 'button[data-elysium-event-eventgroup-eventid]', SetEventGroupById);
                                                            $(Attr.UI.EventGroupSignIn).click(SignInGroup);
                                                            $(Attr.UI.ParticipantSearchInput).keyup(SearchParticipant);
                                                            $(Attr.UI.AssistantInputSearch).keyup(SearchAssistant);
                                                            $(Attr.UI.InvitationSearchInput).keyup(SearchInvitation);
                                                            $(Attr.UI.AssistantList).on('click', '[data-elysium-event-assistant-addowner]', AddOwner);
                                                            $(Attr.UI.AssistantList).on('click', '[data-elysium-event-assistant-removeowner]', RemoveOwner);
                                                            $(Attr.UI.InvitationInput).change(SetFileName);
                                                            var AreaForm = $(Attr.UI.AreaForm).parsley().on('form:submit', AreaFormOnSuccess);
                                                            var InvitationForm = $(Attr.UI.InvitationForm).parsley(InvitationFormConfig).on('form:submit', InvitiationFormOnSuccess);
                                                            CreateDatePicker();
                                                            $(Attr.UI.TransportAddGo).click(AddGoTransport);
                                                            $(Attr.UI.TransportAddReturn).click(AddReturnTransport);
                                                            $(Attr.UI.TransportFormCancel).click(CancelAddTransport);
                                                            $(Attr.UI.TransportPanelForm).hide();
                                                            TransportForm = $(Attr.UI.TransportForm).parsley().on('form:submit', TransportFormOnSuccess);
                                                            TableTransportGo.OnEvent("click", '[data-elysium-event-transport-table-signin]', AddPassenger);
                                                            TableTransportGo.OnEvent("click", '[data-elysium-event-transport-table-details]', DetailsTransport);
                                                            TransportTableReturn.OnEvent("click", '[data-elysium-event-transport-table-signin]', AddPassenger);
                                                            TransportTableReturn.OnEvent("click", '[data-elysium-event-transport-table-details]', DetailsTransport);
                                                            EnableValidators();
                                                            ValidateAddCar();

                                                            // Hide panel
                                                            $(Attr.UI.EventGroupPanelDetails).hide();
                                                            // Start scroll
                                                            $('.message-center, .customizer-body, .scrollable').perfectScrollbar({
                                                                wheelPropagation: !0
                                                            });
                                                            // Hide Spinner
                                                            hide();
                                                        }, hide);
                                                    }, hide);
                                                }, hide);
                                            }, hide);
                                        }, hide);
                                    }, hide);
                                }, hide);
                            }, hide);
                        }, hide);
                    }, hide);
                }, hide);
            }, hide);
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