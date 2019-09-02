/**
 * RevolutionarySlider.js Version 1.0
 * @abstract Class for encapsulate the functionality of revolutionary slider
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 15/03/2018
 */
Elysium.UI.Entities.RevolutionarySlider = function (arguments)
{
    /*******************************************************************************/
    /*                                   Attributes                                */
    /*******************************************************************************/
    var revapi1014 = null;
    /*******************************************************************************/
    /*                                   Methods                                   */
    /*******************************************************************************/
    /**
     * @name Refresh
     * @abstract Method to refresh the slider
     */
    var Refresh = function ()
    {
        revapi1014.revredraw();
    }
    /**
     * @name SetLocale
     * @abstract Method to change the language of controller
     * @param strLocale String of language for example es-MX
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize the controller
     */
    var Initialize = function ()
    {
        revapi1014 = $("#rev_slider_1014_1").show().revolution({
            sliderType: "standard",
            jsFileLocation: "revolution/js/",
            sliderLayout: "fullscreen",
            dottedOverlay: "none",
            delay: 9000,
            navigation: {
              keyboardNavigation: "off",
              keyboard_direction: "horizontal",
              mouseScrollNavigation: "off",
              mouseScrollReverse: "default",
              onHoverStop: "off",
              touch: {
                touchenabled: "on",
                swipe_threshold: 75,
                swipe_min_touches: 1,
                swipe_direction: "horizontal",
                drag_block_vertical: false
              },
              arrows: {
                style: "hermes",
                enable: true,
                hide_onmobile: true,
                hide_under: 768,
                hide_onleave: false,
                left: {
                  h_align: "left",
                  v_align: "center",
                  h_offset: 0,
                  v_offset: 0
                },
                right: {
                  h_align: "right",
                  v_align: "center",
                  h_offset: 0,
                  v_offset: 0
                }
              }
            },
            responsiveLevels:[1240,1024,778,778],
            gridwidth:[1240,1024,778,480],
            gridheight:[600,500,400,300],
            lazyType: 'smart',
            scrolleffect: {
              fade: "on",
              grayscale: "on",
              on_slidebg: "on",
              on_parallax_layers: "on",
              direction: "top",
              multiplicator_layers: "1.4",
              tilt: "10",
              disable_on_mobile: "off",
            },
            parallax: {
              type: "scroll",
              origo: "slidercenter",
              speed: 400,
              levels: [5, 10, 15, 20, 25, 30, 35, 40, 45, 46, 47, 48, 49, 50, 51, 55],
            },
            //shadow: 0,
            spinner: "off",
            stopLoop: 'off',
            stopAfterLoops: -1,
            stopAtSlide: -1,
            shuffle: "off",
            autoHeight: "off",
            fullScreenAutoWidth: "off",
            fullScreenAlignForce: "off",
            fullScreenOffsetContainer: "",
            fullScreenOffset: "0px",
            hideThumbsOnMobile: "off",
            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLilmit: 0,
            debugMode: false,
            fallbacks: {
              simplifyAll: "off",
              nextSlideOnWindowFocus: "off",
              disableFocusListener: false,
            }
        });
        RsTypewriterAddOn($, revapi1014);
    }
    /*******************************************************************************/
    /*                                Encapsulation                                */
    /*******************************************************************************/
    return {
        SetLocale: SetLocale,
        Initialize: Initialize,
        Refresh: Refresh
    }
}