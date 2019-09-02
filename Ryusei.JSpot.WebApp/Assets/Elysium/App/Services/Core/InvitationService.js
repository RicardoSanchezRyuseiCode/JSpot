/**
 * @name InvitationService
 * @abstract Service class to use functionality of Core information
 * @author Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
 * @CreationDate 2019-08-23
 */
Elysium.App.Services.Core.InvitationService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Core/api/Invitation';
    /**********************************************/
    /*                   Methods                  */
    /**********************************************/

    var GetCurrent = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Check Access        
        var url = Api + '/GetCurrent';
        $.ajax({
            // tipo de llamado
            type: 'GET',
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
                Authorization: Elysium.GetBearerToken()
            },
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        // return promise
        return deferred.promise();
    };

    var GetByEventId = function (eventId) {
        // Define promise
        var deferred = new $.Deferred();
        // Check Access        
        var url = Api + '/GetByEventId/' + eventId;
        $.ajax({
            // tipo de llamado
            type: 'GET',
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
                Authorization: Elysium.GetBearerToken()
            },
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        // return promise
        return deferred.promise();
    };
    
    var Accept = function (invitationId) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/Accept/' + invitationId;
        var type = 'POST';
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
                Authorization: Elysium.GetBearerToken()
            },
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // content type
            contentType: 'application/json; charset=utf-8',
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        // return promise
        return deferred.promise();
    };

    var Reject = function (invitationId) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/Reject/' + invitationId;
        var type = 'POST';
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
                Authorization: Elysium.GetBearerToken()
            },
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // content type
            contentType: 'application/json; charset=utf-8',
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        // return promise
        return deferred.promise();
    };


    var Import = function (listEmail, eventId) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/Import';
        var type = 'POST';
        var objJson = JSON.stringify({
            CollectionEmail: listEmail,
            EventId : eventId
        });
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
                Authorization: Elysium.GetBearerToken()
            },
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // content type
            contentType: 'application/json; charset=utf-8',
            // data
            data: objJson,
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        // return promise
        return deferred.promise();
    };

    /**********************************************/
    /*                 Encapsulate                */
    /**********************************************/
    return {
        GetCurrent: GetCurrent,
        GetByEventId: GetByEventId,
        Accept: Accept,
        Reject: Reject,
        Import: Import
    };
}());