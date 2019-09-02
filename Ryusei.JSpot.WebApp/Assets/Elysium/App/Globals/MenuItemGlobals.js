/**
 * MenuItemGlobals.js Version 1.0
 * @abstract Global object to manage menu of page
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright Elysium 28/06/2018
 */
Elysium.App.Globals.MenuItemGlobals = function (arguments) {
    /*******************************************/
    /*                  Services               */
    /*******************************************/
    var MenuItemService = Elysium.App.Services.Auth.MenuItemService;
    /*******************************************/
    /*                 Attributes              */
    /*******************************************/
    // Attributes
    var Attr = arguments;
    // ISpinner
    var ISpinner = Elysium.Implements(new Elysium.UI.Entities.Spinner({
        image: Elysium.SpinnerImage,
        bg_color: "#f5f7fa"
    }), [Elysium.UI.Interfaces.ISpinner]);
    // Initial Assing
    var InitialAssign = false;
    // Dicc Names
    var DiccNames = [];
    /*******************************************/
    /*                  Methods                */
    /*******************************************/
    /**
     * @name DisableRightClick
     * @abstrat Method to disable right click on menu panel
     */
    var DisableRightClick = function () {
        $('#sidebar').bind('contextmenu', function (e) {
            return false;
        });
    }
    /**
     * @name GenerateMenuItemLang
     * @abstract Generate menu item lang
     * @param {any} value
     */
    var GenerateMenuItemLang = function (value) {
        var newLangKey = "Menu_" + (+ new Date() + "" + (Math.floor(Math.random() * 101)) + "" + (Math.floor(Math.random() * 101)));
        for (var lang in i18nLocalResources) {
            if (i18nLocalResources[lang].translation.Elysium["Menu"] == undefined) {
                i18nLocalResources[lang].translation.Elysium["Menu"] = {};
            }
        }
        listLangValue = value.split(";")
        listLangValue.forEach(function (langValue, langValueIndex, langValueArray) {
            var aux = langValue.split("::");
            var lang = aux[0];
            var value = aux[1];
            i18nLocalResources[lang].translation.Elysium["Menu"][newLangKey] = value;
        });
        return "Elysium.Menu." + newLangKey;
    }
    /**
     * @name RenderContent
     * @abstract Method to render content
     */
    var RenderContent = function (listMenuItem) {
        // Define result
        var html = "";
        // loop in list items
        for (var i = 0; i < listMenuItem.length; i++) {
            var menuItemView = listMenuItem[i];
            // Check if is a single item or have more items
            if (menuItemView.ListMenuItem != null && menuItemView.ListMenuItem.length > 0) {
                var MultipleElement =  '<li class="sidebar-item">' +
                                            '<a @@Initial class="sidebar-link has-arrow waves-effect waves-dark" href="javascript:void(0)" aria-expanded="false">' +
                                                '<i class="@@icon"></i><span class="hide-menu" data-i18n="@@name"></span>' +
                                            '</a>' +
                                            '<ul aria-expanded="false" class="collapse first-level">' +
                                                '@@SubMenu' +
                                            '</ul>' +
                                        '</li>';
                // Check if is single element
                if (menuItemView.MenuItem.IsInitial == true && InitialAssign == false) {
                    MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@Initial', 'data-elysium-menu-items-initial-view');
                    InitialAssign == true;
                }
                else {
                    MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@Initial', '');
                }
                // Set URL of menu
                MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@URL', Elysium.AppHost + '/' +  menuItemView.MenuItem.URL);
                // Set icon
                MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@icon', menuItemView.MenuItem.Icon);
                // Set name
                if (DiccNames[menuItemView.MenuItem.MainName] == undefined) {
                    MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@name', GenerateMenuItemLang(menuItemView.MenuItem.MainName));

                    DiccNames[menuItemView.MenuItem.MainName] = 'USED';
                }
                else {
                    MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@name', GenerateMenuItemLang(menuItemView.MenuItem.AltName));
                }
                // Set submenu
                MultipleElement = Elysium.Types.Strings.ReplaceAll(MultipleElement, '@@SubMenu', RenderContent(menuItemView.ListMenuItem));
                // Add to final html
                html = html + "" + MultipleElement;
            }
            else {
                var SingleElement = '<li class="sidebar-item">' +
                                        '<a data-elysium-menu-items-islink @@Initial class="sidebar-link waves-effect waves-dark sidebar-link" href="@@URL" aria-expanded="false">' +
                                            '<i class="@@icon"></i><span class="hide-menu" data-i18n="@@name"></span>' +
                                        '</a>' +
                                    '</li>';
                 // Check if is single element
                if (menuItemView.MenuItem.IsInitial == true && InitialAssign == false) {
                    SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@Initial', 'data-elysium-menu-items-initial-view');
                    InitialAssign == true;
                }
                else {
                    SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@Initial', '');
                }
                // Set URL of menu
                SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@URL', Elysium.AppHost + '/' + menuItemView.MenuItem.URL);
                // Set icon
                SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@icon', menuItemView.MenuItem.Icon);
                // Set name
                if (DiccNames[menuItemView.MenuItem.MainName] == undefined) {
                   SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@name', GenerateMenuItemLang(menuItemView.MenuItem.MainName));

                    DiccNames[menuItemView.MenuItem.MainName] = 'USED';
                }
                else {
                   SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@name', GenerateMenuItemLang(menuItemView.MenuItem.AltName));

                    SingleElement = Elysium.Types.Strings.ReplaceAll(SingleElement, '@@name', menuItemView.MenuItem.AltName);
                }
                // Add to final html
                html = html + "" + SingleElement;
            }
        }
        // return result
        return html;
    }
    /**
     * @name DrawMenu
     * @abstract Method to draw menu
     */
    var DrawMenu = function (listMenuItem) {
        // Add content
        $("[data-elysium-menu-items]").append(RenderContent(listMenuItem));
    }
    /**
     * @name OpenInitialView
     * @abstract Method to open intial view
     */
    var OpenInitialView = function() {
        if ($('[data-elysium-menu-items-initial-view]').length > 1) {
            $($('[data-elysium-menu-items-initial-view]')[0]).click()
        }
        else {
            $('[data-elysium-menu-items-initial-view]').click();
        }

    }
    /**
     * @name RenderMenu
     * @abstract MEthod to render user menu
     */
    var RenderMenu = function () {
        // Define promise
        var deferred = new $.Deferred();
        // Request user menu
        MenuItemService.GetUserMenu().then(
            function (listMenuItem) {
                // Draw the menu according with the user menu
                DrawMenu(listMenuItem);
                // Open initial view
                OpenInitialView();
                // resolve promise
                deferred.resolve();
            },
            function (xhr) {
                Elysium.Directives.RequestError.ThrowXhr(xhr);
                deferred.reject();
            }
        );
        // return promise
        return deferred.promise();
    }

    /**
     * @name LoadPage
     * @abstrat Method to load page
     */
    var LoadPage = function (href) {
        //if($("#content").hasClass("content-full-width"))
        //    $("#content").removeClass("content-full-width");
        $("#content").html("<div id=\"whileLoadingPage\" style=\"height:500px\"></div>");
        ISpinner.Show("#content");
        $.ajax({
            // tipo de llamado
            type: 'GET',
            headers: {
                Authorization: Elysium.GetBearerToken()
            },
            // url del llamado a ajax
            url: href,
            // obtener siempre los datos mas actualizados
            cache: false,
            // tipo de datos de regreso
            dataType: "html",
            // funcion en caso de exito
            success: function (html, textStatus, jqXHR) {
                $("#content").html(html);
            },
            // funcion en caso de error
            error: function (xhr, status, error) {
                switch (xhr.status) {
                    case 401: LoadPage(Elysium.AppHost + "/Error/Error401"); break;
                    case 404: LoadPage(Elysium.AppHost + "/Error/Error404"); break;
                    case 500: LoadPage(Elysium.AppHost + "/Error/Error500"); break;
                }
            }
        });
    }
    /**
     * @name AssignActiveClass
     * @abstract Method to assign active clas
     * @param {any} element
     */
    var AssignActiveClass = function (element) {
        // Get grand parent element
        var grandParentElement = $(element).parent().parent();
        // Get parent element
        var parentElement = $(element).parent();
        // Remove active from grand parent element
        $(grandParentElement).children("a").removeClass("active");
        // Set active
        $(element).addClass("active");
        // Check if is submenu
        if ($(grandParentElement).hasClass('sub-menu')) {
            // recall function with a element
            AssignActiveClass($(grandParentElement).parent().children("a"));
        }
    }
    /**
     * @name ManageLinks
     * @abstrat Method to manage link of the pase
     */
    var ManageLinks = function () {
        var manager = function (event) {
            // Prevent Default
            event.preventDefault();
            // Get attribute is link
            var attr = $(this).attr('data-elysium-menu-items-islink');
            // Check if element is link
            if (typeof attr !== typeof undefined && attr !== false) {
                // Check if is current active link
                if ($(this).parent().hasClass("active"))
                    return;
                // Clean active class
                $("[data-elysium-menu-items]").find("li").removeClass("active");
                // Asign active class
                AssignActiveClass(this);
                // Get path to load
                var href = $(this).attr("href");
                // Add to history
                history.pushState("[data-i18n='" + $(this).children("span").attr("data-i18n")  + "']"  , null, null);
                // Load page
                LoadPage(href);
            }
        }
        $("[data-elysium-menu-items]").on('click', "a", manager);
        $("body").on('click', '.elysium-outside-menu', manager);
    }
    /**
     * @name Load
     * @abstract Method to load page
     */
    var Load = function (URL) {
        var historyUrl = Elysium.AppHost + "/" + URL;
        if (URL.indexOf(Elysium.AppHost) >= 0) {
            var historyUrl = URL;
        }
        history.pushState(historyUrl, null, null);
        LoadPage(historyUrl);
    }

    /**
     * @name ManageHistory
     * @abstract Method to manager history
     */
    var ManageHistory = function () {
        window.addEventListener('popstate', function (e) {
            var location = e.state;
            if (location != null) {
                if (location.indexOf("data-i18n") >= 0)
                    $(location).click();
                else
                    Load(location);
            } else {
                window.history.back();
            }
        });
    }
    /**
     * @name Initialize
     * @abstract Method to initialize Global controller
     */
    var Initialize = function () {
        ISpinner.Show("[data-elysium-menu-items]");
        DisableRightClick();
        ManageLinks();
        ManageHistory();
        RenderMenu().then(
            function () {
                if (Attr.OnInitializeEnd !== undefined) {
                    Attr.OnInitializeEnd();
                }
                ISpinner.Hide("[data-elysium-menu-items]");
            },
            function () {
                ISpinner.Hide("[data-elysium-menu-items]");
            }
        );
    }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        Load: Load
    }
}