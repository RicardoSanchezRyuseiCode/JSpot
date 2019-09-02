/**
 * Form.js Version 2.0
 * @abstract Wrapper for forms
 * @author Ricardo Sanchez Romero, rsr_master@hotmail.com
 * @copyright Faurecia 08/10/2016
 */
Elysium.UI.Entities.Form = function (formSelector) {
    /*****************************************/
    /*               Construct               */
    /*****************************************/
    var formSel = formSelector;
    /*****************************************/
    /*                Methods                */
    /*****************************************/
    /**
     * @name getValue
     * @abstract Method to get value
     */
    function getValue(name, value)
    {
        // Get element
        var attr = "";
        attr = $(formSel).find("[name=" + name + "]").attr("data-elysium-iform-uppercase");
        if (typeof attr !== typeof undefined && attr !== false) 
            return value.toUpperCase();
        attr = $(formSel).find("[name=" + name + "]").attr("data-elysium-iform-checkbox");
        if (typeof attr !== typeof undefined && attr !== false) 
            if(value == "on") return "true"; else return "false";
        return value;
        
    }
    /**
     * toSingleObject
     * Metodo para convertir el resultado de la serializacion de un formulario con el formato
     * { name: "name1" , value : "value1" }
     * a un formato
     * { name1 : "value1" }
     */
    function toSingleObject(array) {
        var object = {};
        $.each(array, function (i, field) {
            // comprobamos si se trata de un campo compuesto
            if (field.name.indexOf("_") >= 0) {
                // realizamos split
                var split = field.name.split("_");
                var aux = object;
                for (var i = 0; i < split.length - 1 ; i++) {
                    if (typeof aux[split[i]] == 'undefined')
                        aux[split[i]] = {};
                    aux = aux[split[i]];
                }
                if (field.value.constructor === Array) {
                    aux[split[split.length - 1]] = field.value;
                }
                else {
                    if (field.value.trim() === '')
                        aux[split[split.length - 1]] = null;
                    else
                        aux[split[split.length - 1]] = getValue(field.name, field.value);
                }
            }
            else {
                if (field.value.constructor === Array) {
                    object[field.name] = field.value;
                }
                else {
                    if (field.value.trim() === '')
                        object[field.name] = null;
                    else
                        object[field.name] = getValue(field.name, field.value);
                }
            }

        });
        return object;
    }

    function SearchValue(array,value)
    {
        var band = false;
        array.forEach(function (element, index, array) {
            if (element.name == value)
                band = true;
        });
        return band;
    }

    function GetRepeatedObjects(object)
    {
        var repeated = [];
        function CountProp(fieldName, obj)
        {
            var count = 0;
            $.each(obj, function (i, field) {
                if (field.name == fieldName)
                    count++;
            });
            return count;
        }
        $.each(object, function (i, field) {0
            if (CountProp(field.name, object) > 1)
            {
                if (!SearchValue(repeated, field.name))
                    repeated.push({ "name": field.name });
            }
        });
        return repeated;
    }

    function GetNonRepeatedObjects(object) {
        var nonRepeated = [];
        function CountProp(fieldName, obj) {
            var count = 0;
            $.each(obj, function (i, field) {
                if (field.name == fieldName)
                    count++;
            });
            return count;
        }
        $.each(object, function (i, field) {
            if (CountProp(field.name, object) == 1) {
                nonRepeated.push(field);
            }
        });
        return nonRepeated;
    }

    function JoinRepeated(array, original)
    {
        var objects = []
        array.forEach(function (element, a, index) {
            var o = original.filter(function (el) {
                if (el.name == element.name)
                {
                    return true;
                }
            });
            objects.push(JoinObject(o));
        });
        return objects;
    }

    function JoinObject(objects)
    {
        var object = {
            name: objects[0].name
        }
        
        var values = [];

        objects.forEach(function (element, index, array) {

            values.push(element.value);

        });

        object["value"] = values;

        return object
    }

    /**
     * @name: GetValues
     * @abstract: Method to get the values of form in object
     */
    function GetValues()
    {
        // serialize form
        var serialized = $(formSel).serializeArray();
        // get non repeated
        var nonRepeated = GetNonRepeatedObjects(serialized);
        // get repeated
        var repeated = GetRepeatedObjects(serialized);
        var joins = JoinRepeated(repeated, serialized);
        // Join repeated elements
        var values = nonRepeated.concat(joins);
        // values
        return toSingleObject(values);
    }
    /**
     * @name: SetValue
     * @abstract: Method to set the value to a field
     */
    function SetValue(element, value) {
        type = $(element).attr("data-type");
        switch (type) {
            
            case 'date':
                //$(element).val(kendo.format("{0:dd/MM/yyyy}", kendo.parseDate(value)));
                break;
            case 'autonumeric':
                $(element).autoNumeric('set', value);
                break;
            case 'combobox':
                //($(element).data("kendoComboBox")).value(value);
            case 'checkbox':
                $(element).prop('checked', value);
                break;
            default:
                $(element).val(value);
                break;
        }
    }
    /**
     * @name: AssingData
     * @description: Metodo para colocar la informacion de manera dinamica en el formulario
     */
    var AssingData = function (object, parent) {
        for (var key in object) {
            if (typeof object[key] == 'object') {
                if (Object.prototype.toString.call(object[key]) == '[object Date]') {
                    // armamos id
                    var id = parent + "" + key;
                    // obtenemos el elementos
                    var element = $(formSel).find("#" + id);
                    // colocamos dato
                    SetValue(element, object[key]);
                }
                else {
                    var next = "";
                    if (parent == '')
                        next = key + '_';
                    else {
                        next = parent + '' + key + '_';
                    }
                    AssingData(object[key], next);
                }
            }
            else {
                // armamos id
                var id = parent + "" + key;
                // obtenemos el elementos
                var element = $(formSel).find("#" + id);
                // colocamos dato
                SetValue(element, object[key]);
            }
        }
    }
    /**
     * @name: SetValues
     * @astract: Method to set the values of an object
     */
    function SetValues(object)
    {
        AssingData(object, '');
    }
    /**
     * DisableInputFieldsInForm
     * Metodo para deshabilitar el enter en los input de un formulario
     */
    var DisableInputFieldsInForm = function () {
        $(formSel).bind("keydown", function (event) {
            var code = event.keyCode || event.which;
            if (code == 13) {
                if ($(document.activeElement).prop("tagName") !== "TEXTAREA") {
                    event.preventDefault();
                    return false;
                }
            }
        });
    }
    /**
    * ExcludeStep
    * Metodo para excluir un elemento si es que se encuentra dentro del listado
    */
    var ExcludeStep = function (ElementsToJump) {
        if (typeof ElementsToJump === "undefined")
            return false;
        for (var i = 0 ; i < ElementsToJump.length; i++) {
            var element = ElementsToJump[i];
            switch (element.type) {
                case 'tagName': if ($(document.activeElement).prop("tagName").toLowerCase() === element.toCompare.toLowerCase()) {
                    return true;
                }
                    break;
                case 'id': if ($(document.activeElement).attr("id") === element.toCompare)
                {
                    return true;
                }
                    break;
            }
        }
        return false;
    }
    /**
     * EnterLikeTab
     * Metodo para cambiar el funcionamiento del enter por tab para un formulario en especifico
     * {
     *       disableEnterInInputs : // true o false default true
     *       EnableSubmitOnEnd: // true o false defautl true
     *       ReplaceSubmit: // function funcion en caso de querer remplazar el submit en el enter del ultimo control
     *       excludes: [
     *           { 
     *               type: // tagName or id
     *               toCompare: // cadena a comparar por ejemplo "TEXTAREA" o "myId" 
     *           }
     *       ]
     *  }
     */
    var EnableEnterTab = function (options) {
        // obtenemos el control al cual se asociara el evento
        var form = $(formSel);
        if (typeof options.disableEnterInInputs === "undefined") {
            DisableInputFieldsInForm(form);
        }
        else if (options.disableEnterInInputs === true) {
            DisableInputFieldsInForm(form);
        }
        if (typeof options.EnableSubmitOnEnd === "undefined") {
            options["EnableSubmitOnEnd"] = true;
        }
        // asociamos el evento al formulario
        $(form).bind("keyup", function (event) {
            if (event.which === 13) {
                if (!ExcludeStep(options.excludes)) {
                    event.preventDefault();
                    // obtenemos elementos que puedan obtener foco
                    var focusables = $(form).find(":focusable");
                    var visibles = [];
                    for (var i = 0; i < focusables.length; i++) {
                        var element = $(focusables[i]);
                        if ((typeof element.attr("tabindex") == "undefined" || "" + element.attr("tabindex") != "-1") && (typeof element.attr("id") !== "undefined") || element.prop("tagName") == "BUTTON" || element.hasClass("select2-selection")) {
                            visibles.push(focusables[i]);
                        }
                    }
                    var indexG = 0;
                    visibles.forEach(function (element, index, array) {
                        if (element == document.activeElement) {
                            indexG = index;
                            return;
                        }
                    });
                    if (indexG + 1 >= visibles.length && options.EnableSubmitOnEnd === true) {
                        if (typeof options.ReplaceSubmit !== "undefined") {
                            if (typeof options.ReplaceSubmit === "function") {
                                options.ReplaceSubmit();
                            }
                        }
                        else {
                            // si ya no hay siguiente control generamos un submit
                            $(form).submit();
                        }
                    }
                    else if (indexG + 1 >= visibles.length && options.EnableSubmitOnEnd === false) {
                        $(visibles[0]).focus();
                    }
                    else {
                        $(visibles[indexG + 1]).focus();
                    }
                    return false;
                }
            }
            else if (event.which === 9) {
                $(document.activeElement).focus();
            }
        });
    }
    /**
     * @name Clean
     * @abstract Method to clean the form, plugins are out of scope method
     */
    var Clean = function () {
        $(formSel)[0].reset();
    }
    /*****************************************/
    /*              Encapsulate              */
    /*****************************************/
    return {
        GetValues: GetValues,
        SetValues: SetValues,
        EnableEnterTab: EnableEnterTab,
        DisableEnterInInput: DisableInputFieldsInForm,
        Clean: Clean
    }
}