﻿/**
 * @name DepartmentService
 * @abstract Service class to use functionality of Core information
 * @author Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
 * @CreationDate 2019-08-23
 */
Elysium.App.Services.Core.DepartmentService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Core/api/Department';
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

    /**********************************************/
    /*                 Encapsulate                */
    /**********************************************/
    return {
        GetByEventId: GetByEventId
    };
}());