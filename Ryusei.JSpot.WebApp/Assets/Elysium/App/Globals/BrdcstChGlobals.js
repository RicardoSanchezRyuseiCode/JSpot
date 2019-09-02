/**
 * BrdcstChGlobals.js Version 1.0
 * @abstract Global object to manage communication between tabs
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 22/06/2018
 */
Elysium.App.Globals.BrdcstChGlobals = function (arguments) {
    /*******************************************/
    /*                  Attributes             */
    /*******************************************/
    var BrdcstChKey = "Elysium.Ryusei.BrdcstCh.Command";
    var BrdcstChCallbacks = [];
    /*******************************************/
    /*                  Methods                */
    /*******************************************/
    /**
     * @name SendCommand
     * @abstract Method to send command to suscribed elements
     */
    var SendCommand = function (command) {
        localStorage.setItem(BrdcstChKey, JSON.stringify(command));
        localStorage.removeItem(BrdcstChKey);
    }
    /**
     * @name OnCommandReceive
     * @abstract Suscribe to event when is fired
     * @param {any} key
     * @param {any} callback
     */
    var OnCommandReceive = function (key, callback) {
        BrdcstChCallbacks[key] = callback;
    }
    /**
     * @name ReceiveCommand
     * @abstract Method to receiva a command a proppagate to suscried elements
     * @param {any} event
     */
    var ReceiveCommand = function (event) {
        // Ignore other keys
        if (event.originalEvent.key != BrdcstChKey) return;
        // Parse command
        var command = JSON.parse(event.originalEvent.newValue);
        // Ignore empty commands
        if (!command) return; 
        // Fire events
        for (var key in BrdcstChCallbacks) {
            BrdcstChCallbacks[key](command);
        }
    }
    /**
     * @name Initialize
     * @abstract Method to initialize the BroadcastChanel controller
     */
    var Initialize = function () {
        // Bind to event
        $(window).on('storage', ReceiveCommand);
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SendCommand: SendCommand,
        OnCommandReceive: OnCommandReceive
    }
}