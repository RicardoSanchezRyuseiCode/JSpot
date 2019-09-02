/**
 * Elysium.Tools.Strings.js Version 1.0
 * @abstract Clase utilitaria para operaciones de cadena
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright  07/03/2017
 */
Elysium.Directives.RequestError = (function () {
    /**
     * @name ThrowXhr
     * @abstract Method to throw Xhr error
     * @param {any} xhr
     */
    var ThrowXhr = function (xhr) {

        if (xhr.status == 401) {
            Elysium.UI.Entities.Notification.Warning({ text: i18next.t("Herji.Auth.Unauthorized"), time: Elysium.NotificationTime });
        }
        else {
            if (typeof xhr.responseJSON != 'undefined' && xhr.responseJSON != null) {
                Elysium.UI.Entities.Notification.Error({ text: i18next.t(xhr.responseJSON.Message), time: Elysium.NotificationTime });
            }
            else if (typeof xhr.responseText != 'undefined' && xhr.responseText != null) {
                var message = xhr.responseText
                if (i18next.t(xhr.responseText) != "")
                    message = i18next.t(xhr.responseText);
                Elysium.UI.Entities.Notification.Error({ text: message, time: Elysium.NotificationTime });
            }
            else if (xhr.statusText != 'undefined' && xhr.statusText != null) {
                Elysium.UI.Entities.Notification.Error({ text: xhr.statusText, time: Elysium.NotificationTime });
            }
            else {
                Elysium.UI.Entities.Notification.Error({ text: 'Error in request', time: Elysium.NotificationTime });
            }
        }
    }
    /**
     * Encapsulamiento
     */
    return {
        ThrowXhr: ThrowXhr
    }
}());