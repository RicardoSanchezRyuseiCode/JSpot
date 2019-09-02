/**
 * @name ContactService
 * @abstract Service class to use functionality of contact information
 */
Elysium.App.Services.WorldService = (function () {

    var Api = 'https://geo-battuta.net/api';
    /****************************************************/
    /*                      Methods                     */
    /****************************************************/
    
    /*var GetCountries = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Make ajax request
        $.ajax({
            // tipo de llamado
            type: 'GET',
            // url del llamado a ajax
            url: Api + "/GetCountries",
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

    var GetStates = function (countryCode) {
        // Define promise
        var deferred = new $.Deferred();
        // URL
        var url = Api + "/GetStatesByCountryCode/" + countryCode;
        // Make ajax request
        $.ajax({
            // tipo de llamado
            type: 'GET',
            // url del llamado a ajax
            url: url,
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
    };*/
    
    var GetCities = function (countryCode, state) {
        // Define promise
        var deferred = new $.Deferred();
        // URL
        var url = Api + '/city/' + countryCode + '/search/?region=' + state + '&key=fe67a1e57634b9805cb8245fe795a0a1';
        // Make ajax request
        $.ajax({
            // tipo de llamado
            type: 'GET',
            // url del llamado a ajax
            url: url,
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "jsonp",
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
    /****************************************************/
    /*                   Encapsulation                  */
    /****************************************************/
    return {
        //GetCountries: GetCountries,
        //GetStates: GetStates,
        GetCities: GetCities
    };
}());