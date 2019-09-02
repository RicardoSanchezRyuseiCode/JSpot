/**
 * I18n.js Version 1.0
 * @abstract Clase utilitaria para manejo de lenguaje 
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright HI 30/06/2017
 */
Elysium.App.Globals.ValidatorGlobals = function (arguments)
{
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name PasswordStrength
     * @abstract Method to register password strength function
     */
    var PasswordStrength = function (){
        window.Parsley
            .addValidator('passwordStrength', {
                requirementType: 'string',
                validateString: function (value, requirement) {
                    return /((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\#\%\@\!\*\(\(\)\\\|\°\¡\?¿$"&/¬'+=.-~])).{10,250}/.test(value);
                }
            });
    }
    /**
     * @name Initialize
     * @abstract Method to initlie the controller
     */
    var Initialize = function () {
        PasswordStrength();
        
        var parseRequirement = function (requirement) {
            if (isNaN(+requirement))
                return parseFloat(jQuery(requirement).val());
            else
                return +requirement;
        };

        // Greater than validator
        window.Parsley.addValidator('gt', {
            validateString: function (value, requirement) {
                return parseFloat(value) > parseRequirement(requirement);
            },
            priority: 32
        });

        // Greater than or equal to validator
        window.Parsley.addValidator('gte', {
            validateString: function (value, requirement) {
                return parseFloat(value) >= parseRequirement(requirement);
            },
            priority: 32
        });

        // Less than validator
        window.Parsley.addValidator('lt', {
            validateString: function (value, requirement) {
                return parseFloat(value) < parseRequirement(requirement);
            },
            priority: 32
        });

        // Less than or equal to validator
        window.Parsley.addValidator('lte', {
            validateString: function (value, requirement) {
                return parseFloat(value) <= parseRequirement(requirement);
            },
            priority: 32
        });

    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize
    }
}