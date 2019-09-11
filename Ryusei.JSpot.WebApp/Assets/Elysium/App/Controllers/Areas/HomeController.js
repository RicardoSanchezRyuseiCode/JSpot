/**
 * ResetPasswordController.js Version 1.0
 * @abstract Controller for reset password page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 08/07/2018
 */
Elysium.App.Controllers.Areas.HomeController = function (arguments) {
    /*******************************************************************************/
    /*                                   Services                                  */
    /*******************************************************************************/
    var UserService = Elysium.App.Services.Auth.UserService;
    var EventService = Elysium.App.Services.Core.EventService;
    var InvitationService = Elysium.App.Services.Core.InvitationService;
    var WorldService = Elysium.App.Services.WorldService;
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
    // Wizard element 
    var IWizard = Elysium.Implements(new Elysium.UI.Entities.Wizard({
        Selector: Attr.UI.EventWizard
    }), [Elysium.UI.Interfaces.IWizard]);
    // IFormEvent
    var IFormEvent = Elysium.Implements(new Elysium.UI.Entities.Form(Attr.UI.EventForm), [Elysium.UI.Interfaces.IForm]);
    // ICalendar
    var ICalendar = null;
    // EventForm
    var EventForm = null;
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/

    /************************************/
    /*         User Information         */
    /************************************/
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

    /************************************/
    /*                Event             */
    /************************************/

    /**
     * @name GetClassName
     * @abstract Method to get class name for background color
     */
    var GetClassName = function () {
        var random = Math.floor(Math.random() * (5 - 1 + 1)) + 1;
        switch (random) {
            case 1: return "bg-primary";
            case 2: return "bg-secondary";
            case 3: return "bg-success";
            case 4: return "bg-info";
            case 5: return "bg-warning";
            case 6: return "bg-danger";
            case 7: return "bg-light";
            case 8: return "bg-dark";
        }
    }
    /**
     * @name SetCollectionEvents
     * @abstract Method to SetCollectionEvents
     * @param {any} collectionEvents
     */
    var SetCollectionEvents = function (collectionEvents) {
        // Calendar Events
        var calendarEvents = [];
        // Loop in collection
        collectionEvents.forEach(function (event, index, array) {
            calendarEvents.push({
                id: event.EventId,
                title: event.Name,
                start: moment.utc(event.StartDate, "YYYY-MM-DDTHH:mm:ss").local(),
                end: moment.utc(event.EndDate, "YYYY-MM-DDTHH:mm:ss").local(),
                allDay: false,
                className: GetClassName()
            });
        });
        ICalendar.SetEvents(calendarEvents);
    }
    /**
     * @name GetCurrentEvents
     * @abstract Method to get current events
     */
    var GetCurrentEvents = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        EventService.GetCurrent().then(
            function (collectionEvents) {
                // Set current events
                SetCollectionEvents(collectionEvents);
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
     * @name ScheduleEvent
     * @abstract Method to schedule event
     * @param {any} date
     * @param {any} allDay
     * @param {any} jsEvent
     * @param {any} view
     */
    var ScheduleEvent = function (date, jsEvent, view) {
        // Set start date
        IDatePickerStartDate.SetDate(date);
        // Set end date
        IDatePickerEndDate.SetDate(date.add(1, 'days'));
        // Show Modal
        $(Attr.UI.ModalEvent).modal("show");
    }
    /**
     * @name ShowEventDetails
     * @abstract Method to show event details
     * @param {any} event
     */
    var ShowEventDetails = function (event) {
        // Show confirmation dialog
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Jspot.Home.DetailsDialog.Title'),
            i18next.t('Jspot.Home.DetailsDialog.Description'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Go to the other view
                Elysium.GlobalObj["MenuItemsGlobal"].Load("Core/Event/Index/" + event.id);

            }
        );
    }
    /************************************/
    /*             Invitation           */
    /************************************/
    /**
     * @name SetCollectionInvitations
     * @abstract Method to SetCollectionInvitations
     */
    var SetCollectionInvitations = function (collectionInvitations) {
        // loop in collection invitations
        collectionInvitations.forEach(function (invitation, index, array) {
            var date = moment.utc(invitation.Event.StartDate, "YYYY-MM-DDTHH:mm:ss");
            date = date.local();
            $(Attr.UI.InvitationList).append(
                '<div class="calendar-events mb-3" data-class="bg-info">' +
                '<strong>' + invitation.Event.Name + '</strong> <br/>' + date.format("DD-MM-YYYY h:mm a") +
                '<br />' +
                '<button type="button" class="btn btn-success btn-xs" data-elysium-jspot-home-invitation-accept="' + invitation.InvitationId + '"><i class="fas fa-check-circle"></i></button>' +
                '<button type="submit" class="btn btn-danger  btn-xs" data-elysium-jspot-home-invitation-reject="' + invitation.InvitationId + '"><i class="fas fa-trash"></i></button>' +
                '<hr />' +
                '</div>'
            );
        });
    }
    /**
     * @name GetCurrentInvitations
     * @abstract Method to get current invitation
     */
    var GetCurrentInvitations = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user data
        InvitationService.GetCurrent().then(
            function (collectionInvitations) {
                // Set current events
                SetCollectionInvitations(collectionInvitations);
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
     * @name AcceptInvitation
     * @abstract Method to accept invitation
     */
    var AcceptInvitation = function (eventFired) {
        var domElement = eventFired.currentTarget;
        var invitationId = $(eventFired.currentTarget).attr('data-elysium-jspot-home-invitation-accept');
        // Show spinner
        ISpinner.Show(Attr.UI.InvitationList);
        // Update user information
        InvitationService.Accept(invitationId).then(
            function (response) {
                // Check if there was an error
                if (response.Error) {
                    // Hide spinner
                    ISpinner.Hide(Attr.UI.InvitationList);
                    // Show error information
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                }
                GetCurrentEvents().then(
                    function () {
                        // Remove element from dom
                        $(domElement).parent().remove();
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.InvitationList);
                        // Show success mesage
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t("Jspot.Home.InvitationAcceptSuccess"), time: Elysium.NotificationTime });
                    }
                );
            },
            function (error) {
                // Hide spinner
                ISpinner.Hide(Attr.UI.InvitationList);
                // Manage the error
                if (error.responseJSON != undefined) {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseJSON.error_description), time: Elysium.NotificationTime });
                }
                else if (error.responseText != undefined) {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseText), time: Elysium.NotificationTime });
                }
                else {
                    Elysium.UI.Entities.Notification.Error({ text: i18next.t("Jspot.Home.InvitationAcceptError"), time: Elysium.NotificationTime });
                }
            }
        );
    }
    /**
     * @name RejectInvitation
     * @abstract Method to reject invitation
     */
    var RejectInvitation = function (eventFired) {
        var domElement = eventFired.currentTarget;
        var invitationId = $(eventFired.currentTarget).attr('data-elysium-jspot-home-invitation-reject');
        Elysium.UI.Entities.MsgBox.DialogQuestion(
            i18next.t('Jspot.Home.RejectInvitation.Title'),
            i18next.t('Jspot.Home.RejectInvitation.Description'),
            function () {
                // Close the dialog
                Elysium.UI.Entities.MsgBox.Close();
                // Hide spinner
                ISpinner.Show(Attr.UI.InvitationList);
                // Update user information
                InvitationService.Reject(invitationId).then(
                    function (response) {
                        // Check if there was an error
                        if (response.Error) {
                            // Hide spinner
                            ISpinner.Hide(Attr.UI.InvitationList);
                            // Show error information
                            Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                        }
                        // Remove element
                        $(domElement).parent().remove();
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.InvitationList);
                        // Show success mesage
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t("Jspot.Home.InvitationRejectSuccess"), time: Elysium.NotificationTime });
                    },
                    function (error) {
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.InvitationList);
                        // Manage the error
                        if (error.responseJSON != undefined) {
                            Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseJSON.error_description), time: Elysium.NotificationTime });
                        }
                        else if (error.responseText != undefined) {
                            Elysium.UI.Entities.Notification.Error({ text: i18next.t(error.responseText), time: Elysium.NotificationTime });
                        }
                        else {
                            Elysium.UI.Entities.Notification.Error({ text: i18next.t("Jspot.Home.InvitationRejectError"), time: Elysium.NotificationTime });
                        }
                    }
                );
            }
        );
    }
    /************************************/
    /*            EventWizard           */
    /************************************/
    var AddressMap = null;
    var AddressMapMarker = null;
    /**
     * @name EventFormConfig
     * @abstract Configuration of parsley
     */
    var EventFormConfig = {
        errorsContainer: function (parsleyField) {
            if (parsleyField.$element.attr("name") == "Address_Marker")
                "[data-elysium-jspot-home-event-marker]"
            if (parsleyField.$element.attr("name") == "CollectionDepartment_List")
                "[data-elysium-jspot-home-event-department-list-error]"
            return parsleyField;
        }
    }
    /**
     * @name OnStepChanged
     * @abstract Method to manage step change
     */
    var OnStepChanged = function (e, anchorObject, stepNumber, stepDirection) {
        switch (stepNumber) {
            case 1: {
                if (AddressMap == null) {
                    EnableMap();
                }
                break;
            }
            case 4: {
                SetRelationDepartmentEventGroup();
                break;
            }
        }
    }

    /**
     * @name EnableValidators
     * @abstract Method to enable validator
     */
    var EnableValidators = function () {
        window.Parsley
            .addValidator('selectRequired', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return value != "default";
                }
            });

        window.Parsley
            .addValidator('markerSet', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return AddressMapMarker != null;
                }
            });


        window.Parsley
            .addValidator('departmentOne', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return $(Attr.UI.DepartmentList).html().trim() != "";
                }
            });

        window.Parsley
            .addValidator('eventgroupOne', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return $(Attr.UI.EventGroupList).html().trim() != "";
                }
            });
    }
    /**
     * @name CleanModal
     * @abstract Method to clean modal
     */
    var CleanModal = function () {
        // Clean form 
        IFormEvent.Clean();
        // Clean parsley
        EventForm.reset();
        // Clean list
        $(Attr.UI.DepartmentList).html("");
        $(Attr.UI.EventGroupList).html("");
        $(Attr.UI.EventDepartmentList).html("");
        // Clean markes on map
        if (AddressMapMarker != null) {
            AddressMapMarker.remove();
        }
        AddressMapMarker = null;
        // Reset wizard
        IWizard.Reset();
    }
    /**
     * @name EventFormOnSuccess
     * @abstract Method executed when form is success
     */
    var EventFormOnSuccess = function () {
        // Get the raw event object
        var rawEvent = IFormEvent.GetValues();
        // Create the event object
        var objectEvent = {};
        // Add event 
        objectEvent['Event'] = rawEvent.Event;
        objectEvent['Event'].StartDate = moment(rawEvent.Event.StartDate, "DD-MM-YYYY h:mm a");
        objectEvent['Event'].EndDate = moment(rawEvent.Event.EndDate, "DD-MM-YYYY h:mm a");
        // Add Address
        objectEvent['Address'] = rawEvent.Address;
        objectEvent['Address']['Latitude'] = AddressMapMarker.getLatLng().lat;
        objectEvent['Address']['Longitude'] = AddressMapMarker.getLatLng().lng;
        objectEvent['Address'].City = rawEvent.Address.City.split(',')[2];
        // Add department collection
        objectEvent['CollectionDepartment'] = [];

        if (rawEvent.CollectionDepartment.Name.forEach != undefined) {
            rawEvent.CollectionDepartment.Name.forEach(function (departmentName, index, array) {
                objectEvent['CollectionDepartment'].push({
                    Name: departmentName.toUpperCase()
                });
            });
        }
        else {
            objectEvent['CollectionDepartment'].push({
                Name: rawEvent.CollectionDepartment.Name.toUpperCase()
            });
        }
        // Collection event group
        objectEvent['CollectionEventGroupCreatePrm'] = [];
        if (rawEvent.CollectionEventGroupCreatePrm.EventGroup.Name.forEach != undefined) {
            rawEvent.CollectionEventGroupCreatePrm.EventGroup.Name.forEach(function (eventGroupName, index, array) {
                // Create eventgroup
                var EventGroup = {
                    Name: rawEvent.CollectionEventGroupCreatePrm.EventGroup.Name[index].toUpperCase(),
                    Description: rawEvent.CollectionEventGroupCreatePrm.EventGroup.Description[index].toUpperCase(),
                    Capacity: Number(rawEvent.CollectionEventGroupCreatePrm.EventGroup.Capacity[index]),
                    StartDate: moment(rawEvent.CollectionEventGroupCreatePrm.EventGroup.StartDate[index], "DD-MM-YYYY h:mm a"),
                    EndDate: moment(rawEvent.CollectionEventGroupCreatePrm.EventGroup.EndDate[index], "DD-MM-YYYY h:mm a")
                };
                // Create collection department of event group
                var CollectionDepartment = [];
                var departmentNames = $($(Attr.UI.EventDepartmentList + " div.eventgroupdepartmentelement")[index]).find("select").val();
                departmentNames.forEach(function (departmentName, dindex, darray) {
                    CollectionDepartment.push({
                        Name: departmentName
                    });
                });
                // Add to colection
                objectEvent['CollectionEventGroupCreatePrm'].push({
                    EventGroup: EventGroup,
                    CollectionDepartment: CollectionDepartment
                });
            });
        }
        else {
            // Create eventgroup
            var EventGroup = {
                Name: rawEvent.CollectionEventGroupCreatePrm.EventGroup.Name.toUpperCase(),
                Description: rawEvent.CollectionEventGroupCreatePrm.EventGroup.Description.toUpperCase(),
                Capacity: Number(rawEvent.CollectionEventGroupCreatePrm.EventGroup.Capacity),
                StartDate: moment(rawEvent.CollectionEventGroupCreatePrm.EventGroup.StartDate, "DD-MM-YYYY h:mm a"),
                EndDate: moment(rawEvent.CollectionEventGroupCreatePrm.EventGroup.EndDate, "DD-MM-YYYY h:mm a")
            };
            // Create collection department of event group
            var CollectionDepartment = [];
            var departmentNames = $($(Attr.UI.EventDepartmentList + " div.eventgroupdepartmentelement")).find("select").val();

            if (departmentNames.forEach != undefined) {
                departmentNames.forEach(function (departmentName, dindex, darray) {
                    CollectionDepartment.push({
                        Name: departmentName
                    });
                });
            }
            else {
                CollectionDepartment.push({
                    Name: departmentNames
                });
            }
            // Add to colection
            objectEvent['CollectionEventGroupCreatePrm'].push({
                EventGroup: EventGroup,
                CollectionDepartment: CollectionDepartment
            });
        }
        // Show spinner
        ISpinner.Show(Attr.UI.ModalEvent + " .modal-dialog");
        // Create the event
        EventService.Create(objectEvent).then(
            function (response) {
                // Check if there was an error
                if (response.Error) {
                    // Hide spinner
                    ISpinner.Hide(Attr.UI.ModalEvent + " .modal-dialog");
                    // Show error information
                    Elysium.UI.Entities.Notification.Warning({ text: i18next.t(response.Message), time: Elysium.NotificationTime });
                }
                // Get current events
                GetCurrentEvents().then(
                    function () {
                        // Clean modal
                        CleanModal();
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.ModalEvent + " .modal-dialog");
                        // Close modal
                        $(Attr.UI.ModalEvent).modal("hide");
                        // Show success
                        Elysium.UI.Entities.Notification.Success({ text: i18next.t('Jspot.Home.EvenCreationSuccess'), time: Elysium.NotificationTime });
                    },
                    function () {
                        // Clean modal
                        CleanModal();
                        // Hide spinner
                        ISpinner.Hide(Attr.UI.ModalEvent + " .modal-dialog");
                        // Close modal
                        $(Attr.UI.ModalEvent).modal("hide");
                    });
            },
            function (xhr) {
                // Hide spinner
                ISpinner.Hide(Attr.UI.ModalEvent + " .modal-dialog");
                // Show error
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
        return false;
    }
    /************************************/
    /*            MAP Interaction       */
    /************************************/
    /**
     * @name AddMarker
     * @abstract Method to add marker to map
     * @param {any} ev
     */
    var AddMarker = function (ev) {
        // Check if we have previous marker
        if (AddressMapMarker != null)
            AddressMapMarker.remove()
        // Set marker
        AddressMapMarker = L.marker(ev.latlng).addTo(AddressMap);
    }
    /**
     * @name CreateMap
     * @abstract Method to create map
     * @param {any} latitude
     * @param {any} longitude
     */
    var CreateMap = function (latitude, longitude) {
        // Create map
        AddressMap = L.map(Attr.UI.EventAddressMap).setView([latitude, longitude], 14);
        // Add container to map
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1Ijoicnl1c2VpY29kZSIsImEiOiJjanpyc25ra2QwenpvM2JvYmZ0anBueTB3In0.OwWCVr1Tp-NQA5rJMDMSdw', {
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            maxZoom: 18,
            id: 'mapbox.streets',
            accessToken: 'pk.eyJ1Ijoicnl1c2VpY29kZSIsImEiOiJjanpyc25ra2QwenpvM2JvYmZ0anBueTB3In0.OwWCVr1Tp-NQA5rJMDMSdw'
        }).addTo(AddressMap);
        // Add event click to map
        AddressMap.on('click', AddMarker);
    }
    /**
     * @name SetPosition
     * @abstract Method to set poition
     */
    var SetPosition = function (position) {
        CreateMap(position.coords.latitude, position.coords.longitude);
    }
    /**
     * @name EnableMap
     * @abstract Method to enable map
     */
    var EnableMap = function () {
        //if (navigator.geolocation) {
        //    navigator.geolocation.getCurrentPosition(SetPosition);
        //} else {
            CreateMap(23.6345005, -102.5527878);
        //}
    }
    /************************************/
    /*             City events          */
    /************************************/
    var CollectionCities = [];
    /**
     * @name LoadCity
     * @abstract Method to load city
     */
    var LoadCity = function () {
        if ($(Attr.UI.AddressState).val() == "default")
            return;
        // Show spinner
        ISpinner.Show(Attr.UI.ModalEvent + " .modal-dialog");
        // remove options from city
        $(Attr.UI.AddressCity).find('option').remove();
        // Add select
        $(Attr.UI.AddressCity).append(new Option("Seleccionar ciudad", "default"));
        // Request user data
        WorldService.GetCities($(Attr.UI.AddressCountry).val(), $(Attr.UI.AddressState).val()).then(
            function (collectionCities) {
                // Set collection cities
                CollectionCities = collectionCities;
                // Loop to add to select
                collectionCities.forEach(function (city, index, array) {
                    $(Attr.UI.AddressCity).append(new Option(city.city, city.latitude + ',' + city.longitude + ',' + city.city));
                });
                // Set default
                $(Attr.UI.AddressCity).val("default");
                // Hide spinner
                ISpinner.Hide(Attr.UI.ModalEvent + " .modal-dialog");
            },
            function (xhr) {
                // Hide spinner
                ISpinner.Hide(Attr.UI.ModalEvent + " .modal-dialog");
                // Show error
                Elysium.Directives.RequestError.ThrowXhr(xhr);
            }
        );
    }
    /**
     * @name SetCityLocation
     * @abstract Method to SetCityLocation
     */
    var SetCityLocation = function () {
        if ($(Attr.UI.AddressCity).val() == "default")
            return;
        // Get value
        var value = $(Attr.UI.AddressCity).val();
        // split
        var splitValue = value.split(',');
        // Set latitude
        var latitude = Number(splitValue[0]);
        // Set longitude
        var longitude = Number(splitValue[1]);
        // Set position of map
        AddressMap.setView([latitude, longitude], 14);
    }
    /************************************/
    /*       Department Interaction     */
    /************************************/

    /**
     * @name AddDepartmentForm
     * @abstract Method to add department form
     */
    var AddDepartmentElement = function () {
        // Get the html fotm template
        var formTemplateHtml = $(Attr.UI.DepartmentTemplate).html();
        // Add the form template to container
        $(Attr.UI.DepartmentList).append(formTemplateHtml);
    }
    /**
     * @name RemoveDepartmentElement
     * @abstract Method to remove department element
     */
    var RemoveDepartmentElement = function () {
        // Get parent
        var form = $(this).parent().parent().parent().parent();
        // remove 
        $(form).remove();
    }
    /************************************/
    /*       EventGroup Interaction     */
    /************************************/

    /**
     * @name AddEventGroupElement
     * @abstract Method to AddEventGroupElement
     */
    var AddEventGroupElement = function () {
        // Get the html fotm template
        var formTemplateHtml = $(Attr.UI.EventGroupTemplate).html();
        // Add the form template to container
        $(Attr.UI.EventGroupList).append(formTemplateHtml);
        // Get event min date and max dat
        var selectedStartDate = moment($(Attr.UI.EventStartDate).val(), "DD-MM-YYYY h:mm a");
        var selectedEndDate = moment($(Attr.UI.EventEndDate).val(), "DD-MM-YYYY h:mm a");
        // Create StartDate Picker
        var startPicker = Elysium.Implements(new Elysium.UI.Entities.DateTimePicker({
            selector: $(Attr.UI.EventGroupList + " div.eventgroupelement").last().find("#CollectionEventGroupCreatePrm_EventGroup_StartDate"),
            options: {
                format: 'DD-MM-YYYY h:mm a',
                lang: 'es',
                minDate: selectedStartDate,
                maxDate: selectedEndDate,
                currentDate: selectedStartDate,
                time: true
            }
        }), [Elysium.UI.Interfaces.IDateTimePicker]);
        startPicker.Initialize();
        // Create EndDate Picker
        var endPicker = Elysium.Implements(new Elysium.UI.Entities.DateTimePicker({
            selector: $(Attr.UI.EventGroupList + " div.eventgroupelement").last().find("#CollectionEventGroupCreatePrm_EventGroup_EndDate"),
            options: {
                format: 'DD-MM-YYYY h:mm a',
                lang: 'es',
                minDate: selectedStartDate,
                maxDate: selectedEndDate,
                currentDate: selectedEndDate,
                time: true
            }
        }), [Elysium.UI.Interfaces.IDateTimePicker]);
        endPicker.Initialize();
    }
    /**
     * @name RemoveEventGroupElement
     * @abstract Method to remove event group element
     */
    var RemoveEventGroupElement = function () {
        // Get parent
        var form = $(this).parent().parent().parent().parent();
        // remove 
        $(form).remove();
    }

    /************************************/
    /*     EventGroup - Department      */
    /************************************/

    /**
     * @name SetRelationDepartmentEventGroup
     * @abstract Method to set relation of department event group
     */
    var SetRelationDepartmentEventGroup = function () {
        // We need to get departments name
        var collectionDepartmentName = [];
        var departmentList = $(Attr.UI.DepartmentList + " div.departmentelement").find("input[name=CollectionDepartment_Name]"); 
        for (var i = 0; i < departmentList.length; i++) {
            collectionDepartmentName.push($(departmentList[i]).val());
        }
        // We need to get the event group name
        var collectionEventGroupName = [];
        var eventGroupList = $(Attr.UI.EventGroupList + " div.eventgroupelement").find("input[name=CollectionEventGroupCreatePrm_EventGroup_Name]");
        for (var i = 0; i < eventGroupList.length; i++) {
            // Get template of elements
            var templateHtml = $(Attr.UI.EventDepartmentTemplate).html();
            // Add to html
            $(Attr.UI.EventDepartmentList).append(templateHtml);
            // Replace name
            $(Attr.UI.EventDepartmentList + " div.eventgroupdepartmentelement").last().find("input").val($(eventGroupList[i]).val().toUpperCase());
            // Get last element added
            var select = $(Attr.UI.EventDepartmentList + " div.eventgroupdepartmentelement").last().find("select");
            // Add options to select
            for (var j = 0; j < collectionDepartmentName.length; j++) {
                var departmentName = collectionDepartmentName[j];
                $(select).append(new Option(departmentName.toUpperCase(), departmentName.toUpperCase()));
            }
        }
    }
    /************************************/
    /*              Initialize          */
    /************************************/
    /**
     * @name SetLocale
     * @abstract Method to set locale
     * @param {any} strLocale
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize the cotnroller
     */
    var Initialize = function () {
        // Show spinner
        ISpinner.Show(Attr.UI.MainPanel);
        // Define hide function
        var hide = function () { ISpinner.Hide(Attr.UI.MainPanel); };
        // Update localization
        Elysium.GlobalObj["i18nGlobal"].Refresh(Attr.UI.MainPanel, function () {
            // Initialize the calendar
            ICalendar = Elysium.Implements(new Elysium.UI.Entities.Calendar({
                Selector: Attr.UI.EventCalendar,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                editable: false,
                locale: 'es',
                validRange: {
                    start: moment().format("YYYY-MM-DD")
                },
                eventClick: ShowEventDetails,
                dayClick: ScheduleEvent
            }), [Elysium.UI.Interfaces.ICalendar]);
            ICalendar.Initialize();
            // Initialize wizard
            IWizard.Initialize();
            // Enabel validation in wizard
            IWizard.EnableValidation(Attr.UI.EventForm);
            IWizard.OnStepChanged(OnStepChanged);

            // Initialize StartDate
            IDatePickerStartDate = Elysium.Implements(new Elysium.UI.Entities.DateTimePicker({
                selector: Attr.UI.EventStartDate,
                options: {
                    format: 'DD-MM-YYYY h:mm a',
                    lang: 'es',
                    minDate: moment(),
                    time: true
                }
            }), [Elysium.UI.Interfaces.IDateTimePicker]);
            IDatePickerStartDate.Initialize();
            // Initialize EndDate
            IDatePickerEndDate = Elysium.Implements(new Elysium.UI.Entities.DateTimePicker({
                selector: Attr.UI.EventEndDate,
                options: {
                    format: 'DD-MM-YYYY h:mm a',
                    lang: 'es',
                    minDate: moment(),
                    time: true
                }
            }), [Elysium.UI.Interfaces.IDateTimePicker]);
            IDatePickerEndDate.Initialize();
            // Get information current user
            GetCurrentUser().then(
                function () {
                    // Get Current event
                    GetCurrentEvents().then(
                        function () { 
                            // Get current invitations
                            GetCurrentInvitations().then(
                                function () {
                                    // Bind event for invitations
                                    $(Attr.UI.InvitationList).on('click', 'button[data-elysium-jspot-home-invitation-accept]', AcceptInvitation);
                                    $(Attr.UI.InvitationList).on('click', 'button[data-elysium-jspot-home-invitation-reject]', RejectInvitation);
                                    // Bind event when state change
                                    $(Attr.UI.AddressState).change(LoadCity);
                                    $(Attr.UI.AddressCity).change(SetCityLocation);
                                    // Bind department events
                                    $(Attr.UI.DepartmentAddElement).click(AddDepartmentElement);
                                    $(Attr.UI.DepartmentList).on('click', 'button' + Attr.UI.DepartmentDeleteElement , RemoveDepartmentElement);
                                    // Bind event group events
                                    $(Attr.UI.EventGroupAddElement).click(AddEventGroupElement);
                                    $(Attr.UI.EventGroupList).on('click', 'button' + Attr.UI.EventGroupDeleteElement, RemoveEventGroupElement);
                                    // Enable form validation
                                    EnableValidators();
                                    EventForm = $(Attr.UI.EventForm).parsley(EventFormConfig).on('form:submit', EventFormOnSuccess);
                                    // Bind close modal
                                    $(Attr.UI.ModalEvent).on('hide.bs.modal', function () {
                                        CleanModal();
                                    })
                                    // hide loader
                                    hide();
                                }, hide
                            );
                        }, hide 
                    );                    
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