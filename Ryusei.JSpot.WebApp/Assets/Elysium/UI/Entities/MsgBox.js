/**
 * Elysium.MsgBox.js Version 2.0
 * @abstract Definicion de la funcionalidad general para Message Box
 * @author Ricardo Sanchez Romero, rsr_master@hotmail.com
 * @copyright Faurecia 08/01/2016
 */
Elysium.UI.Entities.MsgBox = (function (/* Argumentos */) {
    /************************************************
     *       Asignacion de variables constructor    *
     ************************************************/
     /**
      * Tipos de mensaje
      */
     var Types = {
        Info: "Info",
        Warning: "Warning",
        Error: "Error",
        Success: "Success",
        Question : "Question"
     }
     /************************************************
     *   Definicion de metodos propios de la clase  *
     ************************************************/
     /**
      * CreateMsg 
      * @abstract Metodo para construir el mensaje a mostrar
      * @param textMsg Texto a mostrar en el mensaje
      * @param type Tipo de mensaje
      * @param time tiempo (milis) que se mostrar el mensaje si este valor viene en nulo
      * el mensaje se muestra sin cerrarse automaticamente
      * { time : 1000, text: 'hola', callback: 'hola' }
      */
     function CreateMsg(options,type) {
        var htmlFlag = false;
        if(/<[a-z][\s\S]*>/i.test(options.text))
            htmlFlag = true;
        switch(type) {
            case Types.Info: 
                if(typeof options.callback != "undefined" && options.callback != null) {
                    if(typeof options.time != "undefined" && options.time != null) { 
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Info'),
                            type : "info",
                            text : options.text,
                            timer: options.time,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(function (err) {
                            if (err == "timer") {
                                options.callback();
                            }
                        });
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Info'),
                            type : "info",
                            text : options.text,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(swal.noop);
                    }
                }
                else { 
                    if(typeof options.time != "undefined" && options.time != null) { 
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Info'),
                            type : "info",
                            text : options.text,
                            timer: options.time,
                            html : htmlFlag
                        }).catch(swal.noop);
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Info'),
                            type : "info",
                            text : options.text,
                            html : htmlFlag
                        }).catch(swal.noop);
                    }
                }                
                break;
            case Types.Warning:
                if(typeof options.callback != "undefined" && options.callback != null) {
                    if(typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Warning'),
                            type : "warning",
                            text : options.text,
                            timer: options.time,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(function (err) {
                            if (err == "timer") {
                                options.callback();
                            }
                        });
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Warning'),
                            type : "warning",
                            text : options.text,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(swal.noop);
                    }
                }
                else {
                    if(typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Warning'),
                            type : "warning",
                            text : options.text,
                            timer: options.time,
                            html : htmlFlag
                        }).catch(swal.noop);
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Warning'),
                            type : "warning",
                            text : options.text,
                            html : htmlFlag
                        }).catch(swal.noop);
                    }
                }
                break;
            case Types.Error:
                if(typeof options.callback != "undefined" && options.callback != null) {
                    if(typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Error'),
                            type : "error",
                            text : options.text,
                            timer: options.time,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(function (err) {
                            if (err == "timer") {
                                options.callback();
                            }
                        });
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Error'),
                            type : "error",
                            text : options.text,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(swal.noop);
                    }
                }
                else {
                    if(typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Error'),
                            type : "error",
                            text : options.text,
                            timer: options.time,
                            html : htmlFlag
                        });
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Error'),
                            type : "error",
                            text : options.text,
                            html : htmlFlag
                        }).catch(swal.noop);
                    }
                }
                break;
            case Types.Success:
                if(typeof options.callback != "undefined" && options.callback != null) {
                    if(typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type : "success",
                            text : options.text,
                            timer: options.time,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(function (err)
                        {
                            if (err == "timer")
                            {
                                options.callback();
                            }
                        });
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type : "success",
                            text : options.text,
                            allowEscapeKey : false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(swal.noop);
                    }
                }
                else {
                    if(typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type : "success",
                            text : options.text,
                            timer: options.time,
                            html : htmlFlag
                        }).catch(swal.noop);
                    } else {
                        swal({
                            title : i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type : "success",
                            text : options.text,
                            html : htmlFlag
                        }).catch(swal.noop);
                    }
                }
                break;
            case Types.Question:
                if (typeof options.callback != "undefined" && options.callback != null) {
                    if (typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title: i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type: "question",
                            text: options.text,
                            timer: options.time,
                            allowEscapeKey: false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(function (err) {
                            if (err == "timer") {
                                options.callback();
                            }
                        });
                    } else {
                        swal({
                            title: i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type: "question",
                            text: options.text,
                            allowEscapeKey: false,
                            html: htmlFlag,
                            preConfirm: function () {
                                return new Promise(function (resolve, reject) {
                                    options.callback(resolve, reject);
                                });
                            }
                        }).catch(swal.noop);
                    }
                }
                else {
                    if (typeof options.time != "undefined" && options.time != null) {
                        swal({
                            title: i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type: "question",
                            text: options.text,
                            timer: options.time,
                            html: htmlFlag
                        }).catch(swal.noop);
                    } else {
                        swal({
                            title: i18next.t('Elysium.UI.MsgBox.Titles.Success'),
                            type: "question",
                            text: options.text,
                            html: htmlFlag
                        }).catch(swal.noop);
                    }
                }
                break;
        }
     }
     /**
      * InfoMsg
      * @abstract Metodo para mostrar un mensaje de tipo info
      * Se usa una estructura de tres partes
      * { time : 1000, text: 'hola', callback: 'hola' }
      */
     function Info(options) {
        CreateMsg(options,Types.Info);
     }
     /**
      * WarningMsg
      * @abstract Metodo para mostrar mensaje de tipo warning
      * { time : 1000, text: 'hola', callback: 'hola' }
      */
     function Warning(options) {
        CreateMsg(options,Types.Warning);
     }
     /**
      * ErrorMsg
      * @abstract Metodo para mostrar mensaje de tipo Error
      * { time : 1000, text: 'hola', callback: 'hola' }
      */
     function Error(options) {
        CreateMsg(options,Types.Error);
     }     
     /**
      * SuccessMsg
      * @abstract Metodo para mostrar mensaje de tipo success
      * { time : 1000, text: 'hola', callback: 'hola' }
      */
     function Success(options) {
        CreateMsg(options,Types.Success);
     }
    /**
     * QuestionMsg
     * @abstract Metodo para mostrar un mensaje de tipo info
     * Se usa una estructura de tres partes
     * { time : 1000, text: 'hola', callback: 'hola' }
     */
     function Question(options) {
         CreateMsg(options, Types.Question);
     }
     /**
      * DialogInfo
      * @abstract Metodo para mostrar un mensaje de dialogo de tipo info
      */
     function DialogInfo(titleMsg,textMsg,callback) {
         swal({
             title: titleMsg,
             text: textMsg,
             type: 'info',
             showCancelButton: true,
             confirmButtonText: i18next.t('Elysium.UI.MsgBox.Buttons.Accept'),
             cancelButtonText: i18next.t('Elysium.UI.MsgBox.Buttons.Cancel'),
             allowEscapeKey: false,
             allowOutsideClick: false,
             showLoaderOnConfirm: true,
             customClass: 'slow-animation',
             preConfirm: function () {
                 return new Promise(function (resolve, reject) {
                     callback(resolve, reject);
                 });
             }
         }).catch(swal.noop);
     }     
     /**
      * DialogWarning
      * @abstract Metodo para mostrar un mensaje de dialogo de tipo warning 
      */
     function DialogWarning(titleMsg,textMsg,callback) {
         swal({
             title: titleMsg,
             text: textMsg,
             type: 'warning',
             showCancelButton: true,
             confirmButtonText: i18next.t('Elysium.UI.MsgBox.Buttons.Accept'),
             cancelButtonText: i18next.t('Elysium.UI.MsgBox.Buttons.Cancel'),
             allowEscapeKey: false,
             allowOutsideClick: false,
             showLoaderOnConfirm: true,
             customClass: 'slow-animation',
             preConfirm: function () {
                 return new Promise(function (resolve, reject) {
                     callback(resolve, reject);
                 });
             }
         }).catch(swal.noop);
     }
     /**
      * DialogQuestion
      * @abstract Metodo para mostrar un mensaje de dialogo de tipo warning 
      */
     function DialogQuestion(titleMsg, textMsg, callback, cancelCallback) {
         swal({
             title: titleMsg,
             text: textMsg,
             type: 'question',
             showCancelButton: true,
             confirmButtonText: i18next.t('Elysium.UI.MsgBox.Buttons.Accept'),
             cancelButtonText: i18next.t('Elysium.UI.MsgBox.Buttons.Cancel'),
             allowEscapeKey: false,
             allowOutsideClick: false,
             showLoaderOnConfirm: true,
             customClass: 'slow-animation',
             preConfirm: function () {
                 return new Promise(function (resolve, reject) {
                     callback(resolve, reject);
                 });
             }
         }).then(function () { }, function (dismiss) { cancelCallback(); }).catch(swal.noop);
     }
     /**
      * @name Close
      * @abstract Method to close the current open dialog 
      */
     function Close() {
         swal.close() 
     }
    /************************************************
     *         Encapsulamiento de la clase          *
     ************************************************/    
    return {
        Info : Info,
        Warning : Warning,
        Error : Error,
        Success: Success,
        Question: Question,
        DialogInfo : DialogInfo,
        DialogWarning: DialogWarning,
        DialogQuestion : DialogQuestion,
        Close: Close
    }
 }());