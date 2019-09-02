/**
 * Elysium.Tools.Strings.js Version 1.0
 * @abstract Clase utilitaria para operaciones de cadena
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright  07/03/2017
 */
Elysium.Types.Image = (function () {
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    
    var ToBlob = function (image) {
        var BASE64_MARKER = ';base64,';
        if (image.indexOf(BASE64_MARKER) == -1) {
            var parts = image.split(',');
            var contentType = parts[0].split(':')[1];
            var raw = parts[1];
            return new Blob([raw], { type: contentType });
        }
        var parts = image.split(BASE64_MARKER);
        var contentType = parts[0].split(':')[1];
        var raw = window.atob(parts[1]);
        var rawLength = raw.length;
        var uInt8Array = new Uint8Array(rawLength);
        for (var i = 0; i < rawLength; ++i) {
            uInt8Array[i] = raw.charCodeAt(i);
        }
        return new Blob([uInt8Array], { type: contentType });
    };
    
    var Scale = function (imageUrl, maxDimension) {
        // Define deferred object
        var deferred = new $.Deferred();
        // define new image
        var image = new Image();
        // bind onload
        image.onload = function (imageEvent) {
            // Create canvas
            var canvas = document.createElement('canvas');
            // Define image max size
            var max_size = maxDimension;
            // Get image width
            var width = image.width;
            // Get image height
            var height = image.height;
            // Check the size and get new dimensiones
            if (width > height) {
                if (width > max_size) {
                    height *= max_size / width;
                    width = max_size;
                }
            } else {
                if (height > max_size) {
                    width *= max_size / height;
                    height = max_size;
                }
            }
            // Set canvas size
            canvas.width = width;
            canvas.height = height;
            // Set image
            canvas.getContext('2d').drawImage(image, 0, 0, width, height);
            // Get url of image
            var newImage = canvas.toDataURL('image/jpeg');
            // resolve promise
            deferred.resolve(newImage);
        };
        image.src = imageUrl;
        // return promise
        return deferred.promise();
    };
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        Scale: Scale,
        ToBlob: ToBlob
    };
}());