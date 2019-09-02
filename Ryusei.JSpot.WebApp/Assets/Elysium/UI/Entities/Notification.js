/**
 * ysium.MsgBox.js Version 2.0
 * @abstract Definicion de la funcionalidad general para Message Box
 * @author Ricardo Sanchez Romero, rsr_master@hotmail.com
 * @copyright Faurecia 08/01/2016
 */
Elysium.UI.Entities.Notification = (function (/* Argumentos */) {
    /*******************************************/
    /*                 Constans                */
    /*******************************************/
    Noty.overrideDefaults({
        layout: 'topRight',
        theme: 'nest',
        closeWith: ['button'],
        progressBar: true,
        animation: {
            open: 'animated fadeInRight',
            close: 'animated fadeOutRight'
        }
    });
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name Info
     * @abstract Method to show an information notification
     * @param { text : 'text', time: '1000'  } options
     */
    var Info = function (options) {
        var message = '<strong>@@notificationType</strong> <br> @@message';
        message = Elysium.Types.Strings.ReplaceAll(message, '@@notificationType', i18next.t('Elysium.UI.Notification.Info'));
        message = Elysium.Types.Strings.ReplaceAll(message, '@@message', options.text);
        new Noty({
            type: 'info',
            text: message,
            timeout: options.time
        }).show();
    }
    /**
     * @name Alert
     * @abstract Method to show an alert notification
     * @param { text: 'text', time: '1000' } options
     */
    var Alert = function (options) {
        var message = '<strong>@@notificationType</strong> <br> @@message';
        message = Elysium.Types.Strings.ReplaceAll(message, '@@notificationType', i18next.t('Elysium.UI.Notification.Alert'));
        message = Elysium.Types.Strings.ReplaceAll(message, '@@message', options.text);
        new Noty({
            type: 'alert',
            text: message,
            timeout: options.time
        }).show();
    }
    /**
     * @name Alert
     * @abstract Method to show a success notification
     * @param { text: 'text', time: '1000' } options
     */
    var Success = function (options) {
        var message = '<strong>@@notificationType</strong> <br> @@message';
        message = Elysium.Types.Strings.ReplaceAll(message, '@@notificationType', i18next.t('Elysium.UI.Notification.Success'));
        message = Elysium.Types.Strings.ReplaceAll(message, '@@message', options.text);
        new Noty({
            type: 'success',
            text: message,
            timeout: options.time
        }).show();
    }
    /**
     * @name Warning
     * @abstract Method to show a warning notification
     * @param { text: 'text', time: '1000' } options
     */
    var Warning = function (options) {
        var message = '<strong>@@notificationType</strong> <br> @@message';
        message = Elysium.Types.Strings.ReplaceAll(message, '@@notificationType', i18next.t('Elysium.UI.Notification.Warning'));
        message = Elysium.Types.Strings.ReplaceAll(message, '@@message', options.text);
        new Noty({
            type: 'warning',
            text: message,
            timeout: options.time
        }).show();
    }
    /**
     * @name Error
     * @abstract Method to show an error notification
     * @param { text: 'text', time: '1000' } options
     */
    var Error = function (options) {
        var message = '<strong>@@notificationType</strong> <br> @@message';
        message = Elysium.Types.Strings.ReplaceAll(message, '@@notificationType', i18next.t('Elysium.UI.Notification.Error'));
        message = Elysium.Types.Strings.ReplaceAll(message, '@@message', options.text);
        new Noty({
            type: 'error',
            text: message,
            timeout: options.time
        }).show();
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Info: Info,
        Alert: Alert,
        Success: Success,
        Warning: Warning,
        Error: Error
    }
}());