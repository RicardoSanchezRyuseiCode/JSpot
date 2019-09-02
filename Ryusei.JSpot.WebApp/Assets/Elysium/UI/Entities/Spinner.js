/**
 * Spinner.js Version 1.0
 * @abstract Clase utilitaria para el manejo de elemento loader
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright HI 12/03/2017
 * @params
 * {
     bg_color : '#000' // Default,
     opacity : 0.8 // Default [0 - 1],
     z_index : 1000 // Default,
     image :  '../../imgApp/loader/loader.GIF'
   } 
 */
Elysium.UI.Entities.Spinner = function (options) {
    /************************************************
     *       Asignacion de variables constructor    *
     ************************************************/
    // opciones
    var options = options;
    // selector
    var selector = 'body';
    // Id del loader
    var IdLoader = 'HI_UI_Entities_Spinner';
    // loader
    var loaderString = '<div id="' + IdLoader + '">' +
        '<div style="@Overlay">' +
        '<div align="center" style="@LoadingGif">' +
        '<img src="@Image" alt="Cargando..." /><br>' +
        '<p class="text-center"></p> ' +
        '</div>' +
        '</div>' +
        '</div>';
    // Estilo Overlar     
    var styleOverlay = 'position: absolute;' +
        'background-color: @bg_color;' +
        'top: 0%;' +
        'left: 0%;' +
        'width: 100%;' +
        'height: 100%;' +
        'opacity: @opacitySingle;' +
        '-moz-opacity: @opacitySingle;' +
        'filter: alpha(opacity=@opacityPlus);' +
        '-ms-filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=@opacityPlus);' +
        'z-index: @z-index;';
    // Estilo del Gif                   
    var styleLoadingGif = 'position: absolute;' +
        'top: 50%;' +
        'left: 50%;' +
        'transform: translate(-50%,-50%);';
    /************************************************
     *   Definicion de metodos propios de la clase  *
     ************************************************/
    /**
     * ReplaceOverlay
     * Metodo para realizar el reemplazo del estilo overlar
     */
    var ReplaceOverlay = function () {
        // Definimos valores por default
        var bg_color = '#000';
        var opacity = "0.8";
        var opacityPlus = "80";
        var z_index = "1000";
        // Realizamos remplazo en caso de venir en opciones
        if (typeof options.bg_color !== "undefined")
            bg_color = options.bg_color;
        if (typeof options.opacity !== "undefined") {
            var aux = Number(options.opacity);
            opacity = "" + aux;
            opacityPlus = "" + (aux * 100);
        }
        if (typeof options.z_index !== "undefined")
            z_index = "" + options.z_index;
        // Realizamos reemplazo de cadenas
        var overlay = Elysium.Types.Strings.ReplaceAll(styleOverlay, '@bg_color', bg_color);
        overlay = Elysium.Types.Strings.ReplaceAll(overlay, '@opacitySingle', opacity);
        overlay = Elysium.Types.Strings.ReplaceAll(overlay, '@opacityPlus', opacityPlus);
        overlay = Elysium.Types.Strings.ReplaceAll(overlay, '@z-index', z_index);
        return overlay;
    }
    /**
     * ReplaceLoadingGif
     * @abstract Metodo para realizar el remplazo de loading gif
     */
    var ReplaceLoadingGif = function () {
        return styleLoadingGif;
    }
    /**
     * ReplaceLoader
     * @abstract Metodo para armar el loader
     * @param overlay Cadena de estilos overlay
     * @param loadingGif Cadena de estilos loadinggif
     */
    var ReplaceLoader = function (overlay, loadingGif) {
        var img = '../../imgApp/loader/loader.GIF';
        if (typeof options.image !== "undefined")
            img = options.image;
        var loader = Elysium.Types.Strings.ReplaceAll(loaderString, '@Overlay', overlay);
        loader = Elysium.Types.Strings.ReplaceAll(loader, '@LoadingGif', loadingGif);
        loader = Elysium.Types.Strings.ReplaceAll(loader, '@Image', img);
        return loader;
    }
    /**
     * Show
     * @abstract Metodo para mostrar el loader
     * @param jqSelector selector de jquery
     */
    var Show = function (jqSelector) {
        // realizamos el remplazo de valores deacuerdo a las opciones dadas
        var overlay = ReplaceOverlay();
        var loadingGif = ReplaceLoadingGif();
        // realizamos remplazo general
        var loader = ReplaceLoader(overlay, loadingGif);
        // colocamor loader
        $(jqSelector).append(loader);
    }
    /**
     * Hide
     * Metodo para ocultar el loader
     */
    var Hide = function (jqSelector) {
        // Ocultamos loader
        $(jqSelector).find("#" + IdLoader).remove();
    }
    /**
     * UpdateText
     * @abstract Method to update the text of loader
     */
    var UpdateText = function (jqSelector, text) {
        $(jqSelector).find("#" + IdLoader).find("P").text(text);
    }
    /**
     * Encapsulado de la clase
     */
    return {
        Show: Show,
        Hide: Hide,
        UpdateText: UpdateText
    }
}