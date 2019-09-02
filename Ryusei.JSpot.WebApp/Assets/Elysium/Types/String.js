/**
 * Elysium.Tools.Strings.js Version 1.0
 * @abstract Clase utilitaria para operaciones de cadena
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright  07/03/2017
 */
Elysium.Types.Strings = (function () {
    /**
     * ReplaceAll
     * Metodo para realizar el rempleazo de una ocurrencia en toda la cadena
     * @param string Cadena en la cual se reemplazara el valor
     * @param oldS Cadena a buscar para reemplazar
     * @param newS Cadena a nueva a colocar
     */
    var ReplaceAll = function (string, oldS, newS) {
        var re = new RegExp(oldS, 'g');
        string = string.replace(re, newS);
        return string;
    }
    /**
     * ToCurrency
     * Metodo para convetir una cadena con formaro 1234 a formato de moneda
     * @param value Cadena a convertir
     * @param dSeparator Simbolo separador con decimales
     * @param nSeparator Simbolo separador de numeros
     * @param cSign Simbolo de moneda
     */
    var ToCurrency = function (value, dSeparator, nSeparator, cSign) {
        var price = Number(value).toFixed(2).replace(/./g, function (c, i, a) {
            return i && c !== dSeparator && ((a.length - i) % 3 === 0) ? nSeparator + c : c;
        });
        return cSign + price;
    }
    /**
     * Encapsulamiento
     */
    return {
        ReplaceAll: ReplaceAll,
        ToCurrency: ToCurrency
    }
}());