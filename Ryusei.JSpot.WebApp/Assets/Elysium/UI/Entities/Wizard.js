/**
 * @name Wizard
 * @abstract Entitie to wrap the behavior of Wizard element
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 12/05/2018
 */
Elysium.UI.Entities.Wizard = function (arguments)
{
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    var attr = arguments;
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name EnableValidation
     * @abstract Method to enable validation in wizard form
     */
    var EnableValidation = function(formSelector) {
        $(attr["Selector"]).on('leaveStep', function (e, anchorObject, stepNumber, stepDirection) {
            if (stepDirection == "backward") {
                $(attr["Selector"]).closest(formSelector).parsley().reset();
                return true;
            }
		    var res = $(attr["Selector"]).closest(formSelector).parsley().validate('step-' + (stepNumber + 1));
            return res;
	    });
	    $(attr["Selector"]).keypress(function( event ) {
		    if (event.which == 13 ) {
			    $(attr["Selector"]).smartWizard('next');
		    }
	    });
    }      
    /**
     * @name OnStepChanged
     * @param {any} callback
     */
    var OnStepChanged = function (callback) {
        $(attr["Selector"]).on("showStep", function (e, anchorObject, stepNumber, stepDirection) {
            callback(e, anchorObject, stepNumber, stepDirection);
        });
    }
    /**
     * @name Reset
     * @abstract Method to reset wizard
     */
    var Reset = function () {
        $(attr["Selector"]).smartWizard("reset");
    }
    /**
     * @name SetLocale
     * @abstract Method to set localization of component
     */
    var SetLocale = function (strLocale)
    {
        $(attr["Selector"] + " .sw-btn-prev").text(i18next.t('Elysium.UI.Wizard.PreviousButton'));
        $(attr["Selector"] + " .sw-btn-next").text(i18next.t('Elysium.UI.Wizard.NextButton'));
    }
    /**
     * @name Initialize
     * @abstract Method to intialize the element
     */
    var Initialize = function ()
    {
        $(attr["Selector"]).smartWizard({
            selected: 0,
            theme: 'dots',
            transitionEffect: 'fade',
            useURLhash: false,
            showStepURLhash: false,
            keyNavigation: false,
            anchorSettings: {
                anchorClickable: false
            },
            toolbarSettings: {
                toolbarPosition: 'bottom',
                showPreviousButton: false
            },
            lang: { 
                next: i18next.t('Elysium.UI.Wizard.NextButton'),
                previous: i18next.t('Elysium.UI.Wizard.PreviousButton')
            }
        });
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        EnableValidation: EnableValidation,
        OnStepChanged: OnStepChanged,
        Reset: Reset
    }
}