/**
 * @name PaymentCard
 * @abstract Entitie to wrap the behavior of Payment Card
 * @author Ricardo Sanchez Romero (ricardosanchezromero@outlook.es)
 * @copyright Elysium 12/05/2018
 */
Elysium.UI.Entities.PaymentCard = function (arguments)
{
    /*******************************************/
    /*                 Atrributes              */
    /*******************************************/
    // arguments
    var attr = arguments;
    // Stripe client.
    var stripe = Stripe(Elysium.PaymentKey);
    // Elements.
    var elements = stripe.elements({
        locale: Elysium.Language
    });
    // Style
    var style = {
        base: {
            color: '#32325d',
            lineHeight: '18px',
            fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
            fontSmoothing: 'antialiased',
            fontSize: '16px',
            '::placeholder': {
                color: '#aab7c4'
            }
        },
        invalid: {
            color: '#fa755a',
            iconColor: '#fa755a'
        }
    };
    // Card element
    var card = "";
    /*******************************************/
    /*                 Methods                 */
    /*******************************************/
    /**
     * @name CreateToken
     * @abstract Method to create token of card
     */
    var CreateToken = function () {
        var deferred = new $.Deferred();
        stripe.createToken(card).then(function (result) {
            if (result.error) {
                // Inform the user if there was an error.
                var errorElement = document.getElementById('card-errors');
                errorElement.textContent = result.error.message;
                deferred.reject(result.error.message);
            } else {
                // Send the token to your server.
                deferred.resolve(result);
            }
        });
        return deferred.promise(); 
    }
    /**
     * @name IsValid
     * @abstract Method to validate the information of card formulary
     */
    var IsValid = function() {
        if(!$(attr["Selector"]).hasClass("StripeElement--complete")) {
            // Put the invalid class
            $(attr["Selector"]).addClass("StripeElement--invalid");
            // Inform the user if there was an error.
            var errorElement = document.getElementById('card-errors');
            errorElement.textContent = i18next.t('Plpp.UI.PaymentCard.Invalid');
            // return response
            return false;
        }
        else {
            // Inform the user if there was an error.
            var errorElement = document.getElementById('card-errors');
            errorElement.textContent = "";
            // return response
            return true;
        }
    }
    /**
     * @name Clean
     * @abstract MEthod to clean element
     */
    var Clean = function() {
        // destroy card
        card.destroy();
        // Clean errors
        var errorElement = document.getElementById('card-errors');
        errorElement.textContent = "";
        // Initialize again
        Initialize();
    }
    
    /**
     * @name SetLocale
     * @abstract Method to set localization of component
     */
    var SetLocale = function (strLocale)    
    {
        // Get language
        var lang = strLocale.split("-")[0];
        // destroy card
        card.destroy();
        // replace elements
        elements = stripe.elements({
            locale: lang
        });
        // Clean errors
        var errorElement = document.getElementById('card-errors');
        errorElement.textContent = "";
        // Initialize again
        Initialize();
    }
    /**
     * @name Initialize
     * @abstract Method to intialize the element
     */
    var Initialize = function () {
        // Create an instance of the card Element.
        card = elements.create('card', { style: style });
        // Add an instance of the card Element into the `card-element` <div>.
        card.mount(attr["Selector"]);
        // Handle real-time validation errors from the card Element.
        card.addEventListener('change', function (event) {
            var displayError = document.getElementById('card-errors');
            if (event.error) {
                displayError.textContent = event.error.message;
            } else {
                displayError.textContent = '';
            }
        });
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        CreateToken: CreateToken,
        IsValid: IsValid,
        Clean: Clean
    }
}