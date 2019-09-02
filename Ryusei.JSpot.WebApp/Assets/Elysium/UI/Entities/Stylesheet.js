/**
 * @name Stylesheet
 * @abstract Entitie to wrap the behavior of stylesheet
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 04/08/2018
 */
Elysium.UI.Entities.Stylesheet = function (arguments) {
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    var Attr = arguments;

    var HandsonStylesheet = null;
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name UpdateSettings
     * @abstract Method to update settings of stylesheet
     */
    var UpdateSettings = function (settings) {
        HandsonStylesheet.updateSettings(settings);
    }
    /**
     * @name UpdateRowsQuantity
     * @abstract Method to update rows quantity displyed
     * @param {any} rowsQuantity
     */
    var UpdateRowsQuantity = function (rowsQuantity) {
        // Get actual columns count
        var actualQuantity = HandsonStylesheet.countRows();
        //if (actualQuantity == 0) return;
        // Check is we need to remove or add
        if (rowsQuantity < actualQuantity) {
            // Get the different
            var difference = actualQuantity - rowsQuantity;
            // remove the rows
            HandsonStylesheet.alter('remove_row', actualQuantity, difference);
        }
        else if (rowsQuantity > actualQuantity) {
            // Get the different
            var difference = rowsQuantity - actualQuantity;
            // remove the rows
            HandsonStylesheet.alter('insert_row', actualQuantity, difference);
        }
    }
    /**
     * @name SetData
     * @abstract Method to set data in stylesheet
     */
    var SetData = function (data) {
        HandsonStylesheet.loadData(data);
    }
    /**
     * @name GetData
     * @abstract Method to get data of stylesheet
     * @param {any} data
     */
    var GetData = function () {
        return HandsonStylesheet.getData();        
    }
    /**
     * @name GetDataAtColumn
     * @param {any} columnName
     */
    var GetDataAtColumn = function (columnName) {
        return HandsonStylesheet.getDataAtProp(columnName);        
    }
    /**
     * @name CountRows
     * @abstract Method to count rows of table
     */
    var CountRows = function () {
        return HandsonStylesheet.countRows();        
    }
    /*******************************************/
    /*                  Events                 */
    /*******************************************/
    /**
     * @name AddEvent
     * @abstract Event fired after data is paste in table
     * @param {any} callback
     */
    var AddEvent = function (event, callback) {
        Handsontable.hooks.add(event, callback, HandsonStylesheet);
    }
    /*******************************************/
    /*              Initialization             */
    /*******************************************/
    /**
     * @name SetLocale
     * @abstract Method to set locale
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize
     */
    var Initialize = function () {
        HandsonStylesheet = new Handsontable(document.querySelector(Attr.Selector), Attr.Settings);
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        UpdateSettings: UpdateSettings,
        SetData: SetData,
        CountRows: CountRows,
        UpdateRowsQuantity: UpdateRowsQuantity,
        GetData: GetData,
        GetDataAtColumn: GetDataAtColumn,
        AddEvent: AddEvent
    }
}