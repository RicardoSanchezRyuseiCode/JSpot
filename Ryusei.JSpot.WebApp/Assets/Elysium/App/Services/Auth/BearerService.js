/**
 * @name BearerService
 * @abstract Service class to use functionality of Auth information
 */
Elysium.App.Services.Auth.BearerService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Auth';
    /**********************************************/
    /*                   Methods                  */
    /**********************************************/

    var RequestToken = function (userCredentials) {
        var deferred = new $.Deferred();
        $.ajax({
            // tipo de llamado
            type: 'POST',
            // url del llamado a ajax
            url: Api + '/token',
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // content type
            contentType: 'application/x-www-form-urlencoded',
            // data
            data: {
                grant_type: 'password',
                username: userCredentials.Email,
                password: userCredentials.Password
            },
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        return deferred.promise();
    };
    
    var RequestImpersonationToken = function (userCredentials) {
        var deferred = new $.Deferred();
        $.ajax({
            // tipo de llamado
            type: 'POST',
            // url del llamado a ajax
            url: Api + '/token',
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // content type
            contentType: 'application/x-www-form-urlencoded',
            // data
            data: {
                grant_type: 'password',
                username: userCredentials.Email,
                password: userCredentials.Password,
                impersonation: userCredentials.Impersonation
            },
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        return deferred.promise();
    };
    
    var RefreshToken = function (tokenId) {
        var deferred = new $.Deferred();
        $.ajax({
            // tipo de llamado
            type: 'POST',
            // url del llamado a ajax
            url: Api + '/token',
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "json",
            // content type
            contentType: 'application/x-www-form-urlencoded',
            // data
            data: {
                grant_type: 'refresh_token',
                refresh_token: tokenId
            },
            // funcion en caso de exito
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        return deferred.promise();
    };
    /**********************************************/
    /*                 Encapulation               */
    /**********************************************/
    return {
        RequestToken: RequestToken,
        RequestImpersonationToken: RequestImpersonationToken,
        RefreshToken: RefreshToken
    };
}());