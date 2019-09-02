/**
 * Calendar.js Version 1.0
 * @abstract Clase utilitaria 
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright HI 18/03/2017
 */
Elysium.UI.Entities.Calendar = function (arguments) {
    /*******************************************/
    /*                  Attributes             */
    /*******************************************/
    var Attr = arguments;
    var attrListEvents = [];
    /*******************************************/
    /*                  Methods                */
    /*******************************************/
    /**
     * @name SetLocale
     * @abstract Method to set the locale of component
     * @params strLocale locale string of the languaje to use
     */
    var SetLocale = function (strLocale) {
        $(Attr.Selector).fullCalendar('option', 'locale', 'es');
        SetEvents(attrListEvents);
    }
    /**
     * @name AddEvents
     * @abstract Method to add events to the calendar
     * @param listEvent [{  id, title, allDay, start, end, url, className, editable, startEditable, durationEditable, resourceEditable, rendering, overlap, constraint, source, color, backgroundColor, borderColor, textColor   }]
     */
    var SetEvents = function (listEvent) {
        attrListEvents = listEvent;
        // Remove all events
        $(Attr.Selector).fullCalendar('removeEvents');
        // Set the list of events
        $(Attr.Selector).fullCalendar('addEventSource', listEvent);
    }
    /**
     * @name GetEvents
     * @abstract Method to set a list of event sources
     * @return {  id, title, allDay, start, end, url, className, editable, startEditable, durationEditable, resourceEditable, rendering, overlap, constraint, source, color, backgroundColor, borderColor, textColor   }
     */
    var GetEvents = function () {
        return $(Attr.Selector).fullCalendar('getEventSources');
    }
    /**
     * @name GetCurrentDate
     * @abstract Method to get current date
     */
    var GetCurrentDate = function () {
        return $(Attr.Selector).fullCalendar('getDate');
    }
    /**
     * @name Initialize
     * @abstract Method to initialize the element
     */
    var Initialize = function () {
        // Create element
        $(Attr.Selector).fullCalendar(Attr);
        // Update lang
        $(Attr.Selector).fullCalendar('option', 'locale', 'es');
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        SetEvents: SetEvents,
        GetEvents: GetEvents,
        GetCurrentDate: GetCurrentDate
    }
}