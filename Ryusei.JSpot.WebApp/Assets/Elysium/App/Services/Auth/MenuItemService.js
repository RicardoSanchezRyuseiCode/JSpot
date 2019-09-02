/**
 * @name RegisterService
 * @abstract Service class to use functionality of Auth information
 */
Elysium.App.Services.Auth.MenuItemService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Auth/api/MenuItem';
    /**********************************************/
    /*                   Methods                  */
    /**********************************************/

    var GetUserMenu = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/GetUserMenu';
        var type = 'GET';
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
                ApplicationId: Elysium.ApplicationId,
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
    /**********************************************/
    /*                 Encapulation               */
    /**********************************************/
    return {
        GetUserMenu: GetUserMenu
    };

}());