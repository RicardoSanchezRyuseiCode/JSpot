/**
 * @name RegisterService
 * @abstract Service class to use functionality of Auth information
 */
Elysium.App.Services.Auth.UserService = (function () {
    /**********************************************/
    /*                   Variables                */
    /**********************************************/
    var Api = Elysium.ApiGateway + '/Auth/api/User';
    /**********************************************/
    /*                   Methods                  */
    /**********************************************/

    var GetCurrent = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Check Access        
        var url = Api + '/GetCurrentUser';
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
    
    var Update = function (user) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/Update';
        var type = 'PUT';
        var objJson = JSON.stringify(user);
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

    var UpdatePassword = function (password) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/UpdatePassword';
        var type = 'PUT';
        var objJson = JSON.stringify(password);
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

    var UploadProfilePicture = function (formData, handlerProgress) {
        // Define promise
        var deferred = new $.Deferred();
        // Get type and url
        var url = Api + '/UploadProfilePicture';
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
            // content type
            contentType: false,
            // process data
            processData: false,
            // progress
            xhr: function () {
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) {
                    myXhr.upload.addEventListener('progress', handlerProgress, false);
                }
                return myXhr;
            },
            // data
            data: formData,
            // Success
            success: function (data, textStatus, jqXHR) {
                deferred.resolve(data);
            },
            // Error
            error: function (xhr, status, error) {
                deferred.reject(xhr);
            }
        });
        // return promise
        return deferred.promise();
    };

    var DownloadProfilePicture = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Check Access        
        var url = Api + '/DownloadProfilePicture';
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

    
    var DownloadProfilePictureById = function (userId) {
        // Define promise
        var deferred = new $.Deferred();
        // Check Access        
        var url = Api + '/DownloadProfilePictureById/' + userId;
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
    }

    /**********************************************/
    /*                 Encapulation               */
    /**********************************************/
    return {
        GetCurrent: GetCurrent,
        Update: Update,
        UpdatePassword: UpdatePassword,
        UploadProfilePicture: UploadProfilePicture,
        DownloadProfilePicture: DownloadProfilePicture,
        DownloadProfilePictureById: DownloadProfilePictureById

    };
}());