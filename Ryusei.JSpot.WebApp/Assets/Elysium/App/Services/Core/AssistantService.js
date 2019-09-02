/**
 * @name AssistantService
 * @abstract Service class to use functionality of Core information
 * @author Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
 * @CreationDate 2019-08-23
 */
Elysium.App.Services.Core.AssistantService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Core/api/Assistant';
    /**********************************************/
    /*                   Methods                  */
    /**********************************************/

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


    var Update = function (assistant) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/Update';
        var type = 'PUT';
        var objJson = JSON.stringify(assistant);
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
    }


    /**********************************************/
    /*                 Encapsulate                */
    /**********************************************/
    return {
        GetByEventId: GetByEventId,
        Update: Update
    };
}());