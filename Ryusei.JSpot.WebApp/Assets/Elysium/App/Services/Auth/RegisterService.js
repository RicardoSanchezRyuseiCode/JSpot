/**
 * @name RegisterService
 * @abstract Service class to use functionality of Auth information
 */
Elysium.App.Services.Auth.RegisterService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Auth/api/Register';

    /**********************************************/
    /*                   Methods                  */
    /**********************************************/

    var CreateNewUser = function (user) {
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/CreateNewUser';
        var type = 'POST';
        var objJson = JSON.stringify(user);
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
            // Add authorization header
            headers: {
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


    var ValidateNewUser = function (token) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/ValidateNewUser';
        var type = 'POST';
        var objJson = JSON.stringify(token);
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
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


    var RequestResetPassword = function (requestResetPasswordPrm) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/RequestResetPassword';
        var type = 'POST';
        var objJson = JSON.stringify(requestResetPasswordPrm);
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
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

    var ResetPassword = function (resetPasswordPrm) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/ResetPassword';
        var type = 'POST';
        var objJson = JSON.stringify(resetPasswordPrm);
        $.ajax({
            // tipo de llamado
            type: type,
            // url del llamado a ajax
            url: url,
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


    var Deactivate = function (userId) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/DeactivateUser/' + userId;
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

    /**********************************************/
    /*                 Encapulation               */
    /**********************************************/
    return {
        CreateNewUser: CreateNewUser,
        ValidateNewUser: ValidateNewUser,
        RequestResetPassword: RequestResetPassword,
        ResetPassword: ResetPassword,
        Deactivate: Deactivate
    };

}());