var State = { "Initializing": 0, "Authenticated": 1, "Anonymous": 2 };



$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready
    console.log("document.ready");

});

$(window).load(function () {
    // executes when complete page is fully loaded, including all frames, objects and images
    console.log("window.loaded");

    //$.connection.hub.logging = true;

    var hubs = new function ()
    {
        var self = this;

        self.activities = $.connection.activities;
        self.catalogs = $.connection.catalogs;
    }

    hubs.activities.client.notify = function (message)
    {
        console.log(message);
        alert(message);

        //$.each(data, function () {
        //    $('#messages').append('<li>' + this + '</li>');
        //});
    }

    hubs.catalogs.client.catalog = function (catalog)
    {
        alert("CATALOG!");
        console.log(catalog);
        alert(catalog);
    }

    $.connection.hub.start().done(function () {

        //activities.server.notifyClients();

        $("#notifyButton").click(function () {
            hubs.activities.server.notifyClients();
        });

    });

});


// Onload event handler
$(function () {
    console.log("OnLoad...");

    document.getElementById('auth-loginlink').addEventListener('click', function () {
        FB.login(function (response) { console.log(response); }, { scope: 'email,user_about_me,friends_about_me' });
    });

    document.getElementById('auth-logoutlink').addEventListener('click', function () {
        FB.logout();
    });

    // Using jQuery for Ajax loading indicator.
    $(".loading").ajaxStart(function () {
        $(this).fadeIn();
    }).ajaxComplete(function () {
        $(this).fadeOut();
    }).ajaxError(function (event, request, settings) {
        //console.log(event);
        //console.log(request);
        //console.log(settings);

        var exception = null;

        var errorMessage = "Error";

        if (request.responseText && request.responseText[0] == "{") {
            // TODO: Figure out why the property is capitalized (Message), when it's not like this in the domain model.
            errorMessage = $.parseJSON(request.responseText).Message;
        }

        //ShowInformationDialog("There was a problem", request.statusText + " (" + request.status + "): " + errorMessage, "Close", "");

        if (request.status == 401) { // Access is denied
            // Perhaps show popup for Facebook-logo?
            return;
        }

        if (request.status === 0) {
            console.log('Not connect.\n Verify Network.');
        } else if (request.status == 404) {
            console.log('Requested page not found. [404]');
        } else if (request.status == 400) {
            console.log("System Failure: Error.");
            ShowInformationDialog("System Failure", "Unable to process the request. Please contact your system administrator.<br />" + errorMessage, "Close");
        } else if (request.status == 500) {
            console.log("System Failure: Unable to process the request.");
            console.log(request);

            ShowInformationDialog("System Failure", "Unable to process the request. Internal Server Error.<br />" + errorMessage, "Close");
        } else if (exception === 'parsererror') {
            ShowInformationDialog("System Failure", "Requested JSON parse failed.<br />Invalid response from server.<br />" + errorMessage, "Close");
        } else if (exception === 'timeout') {
            ShowInformationDialog("System Failure", "Time out error.<br />Please try again.<br />" + errorMessage, "Close");
        } else if (exception === 'abort') {
            console.log('Ajax request aborted.');
        } else {
            console.log('Uncaught Error.\n' + request.responseText);
            ShowInformationDialog("There was a problem", errorMessage, "Close");
        }
    });

    //$('img').live('dragstart', function (event) { event.preventDefault(); });
    $("img").on("dragstart", function (event) { event.preventDefault(); });

    $(document).on("dragstart", function () {
        return false;
    });

    // register onLoad event with anonymous function
    window.onload = function (e) {
        var evt = e || window.event,// define event (cross browser)
            imgs,                   // images collection
            i;                      // used in local loop
        // if preventDefault exists, then define onmousedown event handlers
        if (evt.preventDefault) {
            // collect all images on the page
            imgs = document.getElementsByTagName('img');
            // loop through fetched images
            for (i = 0; i < imgs.length; i++) {
                // and define onmousedown event handler
                imgs[i].onmousedown = disableDragging;
            }
        }
    };

    // disable image dragging
    function disableDragging(e) {
        e.preventDefault();
    }

    var ctrlDown = false;
    var ctrlKey = 17, vKey = 86, cKey = 67, aKey = 65;

    $(document).keydown(function (e) {
        if (e.keyCode == ctrlKey) ctrlDown = true;
    }).keyup(function (e) {
        if (e.keyCode == ctrlKey) ctrlDown = false;
    });

    var selectAll = false;

    $(document).keydown(function (e) {
        if (ctrlDown && (e.keyCode == aKey)) {
            selectAll = !selectAll;

            // TODO: We need to distinguish between catalog items and search result items.
            // Calling .click on everything does not work very well...

            if (selectAll) {
                $(".wrapper").addClass("selection");
                //$(".wrapper").click();
            }
            else {
                $(".wrapper").removeClass("selection");
                //$(".wrapper").click();
            }

            return false;
        }
        else if (ctrlDown && (e.keyCode == cKey)) {
            var img;
            var i = 0;

            i = $('.selection').length;

            //$('.wrapper .selected').each(function (index) {
            //    //alert(index + ': ' + $(this).text());
            //    i = index;
            //});

            alert("You selected " + i + " items to clipboard. These can now be pasted into another catalog.");
        };
    });

    // When clicking on the button close or the mask layer the popup closed
    $("#mask").on("click", function () {
        HideInformationDialog();

        return false;
    });

    //$('.wrapper').mouseover(function (source) {
    //    $(this).children(".description").fadeIn(300);
    //}).mouseout(function () {
    //    $(this).children(".description").fadeOut(100);
    //});

    $('.wrapper').on('mouseenter', function (source) {
        //$(this).children(".description").fadeIn(50);
        $(this).children(".description").show();
    }).on('mouseleave', function (source) {
        $(this).children(".description").hide();
        //$(this).children(".description").fadeOut(150);
    }).on('click', function (source) {
        if ($(this).hasClass("selection")) {
            $(this).removeClass("selection");
        }
        else {
            $(this).addClass("selection");
        }
    });

    if (jQuery.ui) {
        // UI loaded
        $("#slider").slider({
            value: 128,
            min: 32,
            max: 256,
            step: 32,
            slide: function (event, ui) {
                thumbnailSize = ui.value;
                ThumbnailRender();
            }
        });
    }

    setTimeout(function () {
        // Some corporations block Facebook URLs,
        // try to inform the user about potential issues if not authenticated within a certain time.

        VerifyFacebookAccess();
    }, 5000);

    if (window.applicationCache) {
        //applicationCache.addEventListener('updateready', function () {
        //    if (confirm('An update is available. Reload now?')) {
        //        window.location.reload();
        //    }
        //});

        window.applicationCache.addEventListener('updateready', function (e) {
            if (window.applicationCache.status == window.applicationCache.UPDATEREADY) {
                // Browser downloaded a new app cache.
                // Swap it in and reload the page to get the new hotness.
                window.applicationCache.swapCache();

                ShowUpdateNotification();

                // if (confirm('A new version of this site is available. Load it?')) {
                //     window.location.reload();
                // }
            } else {
                // Manifest didn't changed. Nothing new to server.
            }
        }, false);

        function handleCacheEvent(e) {
            console.log(e);
        }

        function handleCacheError(e) {
            console.log(e);
            //alert('Error: Cache failed to update!' + e);
        };

        var appCache = window.applicationCache;

        // Fired after the first cache of the manifest.
        appCache.addEventListener('cached', handleCacheEvent, false);

        // Checking for an update. Always the first event fired in the sequence.
        appCache.addEventListener('checking', handleCacheEvent, false);

        // An update was found. The browser is fetching resources.
        appCache.addEventListener('downloading', handleCacheEvent, false);

        // The manifest returns 404 or 410, the download failed,
        // or the manifest changed while the download was in progress.
        appCache.addEventListener('error', handleCacheError, false);

        // Fired after the first download of the manifest.
        appCache.addEventListener('noupdate', handleCacheEvent, false);

        // Fired if the manifest file returns a 404 or 410.
        // This results in the application cache being deleted.
        appCache.addEventListener('obsolete', handleCacheEvent, false);

        // Fired for each resource listed in the manifest as it is being fetched.
        appCache.addEventListener('progress', handleCacheEvent, false);

        // Fired when the manifest resources have been newly redownloaded.
        appCache.addEventListener('updateready', handleCacheEvent, false);
    }

    IdleTimer();

});

function ShowUpdateNotification() {
    $("#updateNotification").show();
}

function CancelUpdateNotification() {
    $("#updateNotification").hide();
}

function AcceptUpdateNotification() {
    window.location.reload();
}


// Whenever the user is idle, we will disconnect from the SignalR hub.
// We do this to improve the scability of the system to avoid too many
// multiple concurrent connections to the server.
function IdleTimer()
{
    // NOTE: SIGNALR DOES NOT SUPPORT MANUAL CONNECT/DISCONNECT PATTERN,
    // UNTIL THEN, THIS METHOD DOES NOTHING BUT LOG.

    // Consider reducing this value whenever the user base of the
    // system increases. This will ensure that users with the
    // app in the background won't keep a live connection for too long.

    // 2013-02-08: Set the idle time to 10 minutes (600 000)
    $(document).idleTimer(600000);

    $(document).on("idle.idleTimer", function () {
        console.log(Date() + "Disconnect from hubs...");
    });

    $(document).on("active.idleTimer", function () {
        console.log(Date() + "Connect to hubs...");
    });
}


function ConnectToHubs()
{

}

function DisconnectToHubs()
{

}


function VerifyFacebookAccess() {
    // TODO: Find the proper implementation to verify if Facebook has loaded or not.
    // The verification should handle two scenarios: www.facebook.com blocked and not connect.facebook.com,
    // and the scenario of both URLs unavailable.

    //if (FB.getLoginStatus() == null) {
    //    alert("It appears your network is blocking Facebook. To use InTheBoks, you need access to Facebook. Please contact your administrator.");
    //    ChangeState(State.Anonymous);
    //}

    if ($("#initializingBox").is(':visible')) {
        $("#initializingBox").html("... it appears things are not working properly. Please try to <a href='/'>reload</a> the page...");
    }
}

function HideConfirmationDialog() {
    $("#confirmationDialog").animate({ opacity: 0 }, 500, function () {
        $("#confirmationDialog").hide();
    });
}

function HideInformationDialog() {
    $('#mask , #infoDialog').fadeOut(300, function () {
        $('#mask').remove();
    });
}

function ShowInformationDialog(title, body, button1, button2, confirm) {
    $("#infoDialogTitle").html(title);
    $("#infoDialogBody").html(body);

    if (button1 == null || button1 == "") {
        $("#infoDialogButton1").hide();
    }
    else {
        $("#infoDialogButton1").show();
        $("#infoDialogButton1").text(button1);

        $("#infoDialogButton1").click(confirm);
    }

    if (button2 == null || button2 == "") {
        $("#infoDialogButton2").hide();
    }
    else {
        $("#infoDialogButton2").show();
        $("#infoDialogButton2").text(button2);
    }

    var dialog = $("#infoDialog");

    $(dialog).fadeIn(300);

    //Set the center alignment padding + border see css style
    var popMargTop = ($(dialog).height() + 24) / 2;
    //var popMargLeft = ($(dialog).width() + 24) / 2;

    $(dialog).css("margin-top", -popMargTop);

    // Add the mask to body
    $('body').append('<div id="mask"></div>');
    $('#mask').fadeIn(300);
}

function HookUpThumbnailEvents(wrapper) {
    $(wrapper).on('mouseenter', function (source) {
        //$(this).children(".description").fadeIn(50);
        $(this).children(".description").show();
    }).on('mouseleave', function (source) {
        $(this).children(".description").hide();
        //$(this).children(".description").fadeOut(150);
    }).on('click', function (source) {
        if ($(this).hasClass("selection")) {
            $(this).removeClass("selection");
        }
        else {
            $(this).addClass("selection");
        }
    });
}

var listViewType = 1; // 1 = Thumbnail, 2 = Medium, 3 = List
var thumbnailSize = 128;

function ChangeListType(type) {
    listViewType = type;

    if (listViewType == 1) {
        $("#slider").show();

        addCSSRule("div.thumbnail_container", "clear", "none");
        addCSSRule("div.thumbnail_container", "width", "inherit");

        $(".list_details").hide();
    }
    else {
        $("#slider").hide();

        addCSSRule("div.thumbnail_container", "clear", "both");
        addCSSRule("div.thumbnail_container", "width", "100%");

        $(".list_details").show();
    }

    ThumbnailRender();
}

function ThumbnailRender() {
    //var size = $("#thumbnailSize").val();

    var size = thumbnailSize;

    if (listViewType == 2) {
        size = 64;

        // Fix the height of the container to avoid content wrapping.
        addCSSRule("div.thumbnail_container", "height", size + 12 + "px");
    }
    else {
        addCSSRule("div.thumbnail_container", "height", "auto");
    }

    addCSSRule(".thumbnails div img", "height", size + "px");

    //addCSSRule(".thumbnails>span:before", "margin-left", size + "px");
}

function addCSSRule(sel, prop, val) {
    for (var i = 0; i < document.styleSheets.length; i++) {
        var ss = document.styleSheets[i];
        var rules = (ss.cssRules || ss.rules);
        var lsel = sel.toLowerCase();

        for (var i2 = 0, len = rules.length; i2 < len; i2++) {
            if (rules[i2].selectorText && (rules[i2].selectorText.toLowerCase() == lsel)) {
                if (val != null) {
                    rules[i2].style[prop] = val;
                    return;
                }
                else {
                    if (ss.deleteRule) {
                        ss.deleteRule(i2);
                    }
                    else if (ss.removeRule) {
                        ss.removeRule(i2);
                    }
                    else {
                        rules[i2].style.cssText = '';
                    }
                }
            }
        }
    }

    var ss = document.styleSheets[0] || {};
    if (ss.insertRule) {
        var rules = (ss.cssRules || ss.rules);
        ss.insertRule(sel + '{ ' + prop + ':' + val + '; }', rules.length);
    }
    else if (ss.addRule) {
        ss.addRule(sel, prop + ':' + val + ';', 0);
    }
}

function ToggleSettings() {
    if ($('#settings').is(':visible')) {
        $(".settings").hide();
        $(".content").fadeIn(300);
    } else {
        $(".content").hide();
        $(".settings").fadeIn(300);
    }
}

function ToggleSearch() {
    if ($('#search').is(':visible')) {
        $(".search").hide();
        $(".content").fadeIn(300);
    } else {
        $(".content").hide();
        $(".search").fadeIn(300);
    }

    ResizeContent();
}

function HideProperties() {
    $("#propertiesDialog").animate({ width: 0 }, 300, function () {
        $("#propertiesDialog").hide();
    });
}

var previousAction = "";

function ToggleProperties(title, action) {
    if (previousAction != action && !$("#propertiesDialog").is(":visible")) {
        $("#propertiesDialog").show();
        $("#propertiesDialog").animate({ width: 300 }, 300, function () {
        });
    }
    else if (previousAction == action && $("#propertiesDialog").is(":visible")) {
        HideProperties();
    } else {
        $("#propertiesDialog").show();
        $("#propertiesDialog").animate({ width: 300 }, 300, function () {
        });
    }

    previousAction = action;
}

function ToggleFilter() {
    if ($('#filterSearch').is(':visible')) {
        /* for some unknown reason, the UI "jumps" if we animate to 0. */
        $("#filterSearch").animate({ width: 50 }, 300, function () {
            $("#filterSearch").hide();
            $("#filterSearch").val("");
        });
    } else {
        $("#filterSearch").show();
        $("#filterSearch").animate({ width: 200 }, 300);
    }
}

function ToggleListOptions(button) {

    var left = $(button).position().left;
    var top = $(button).position().top + 36;

    left = left - $("#listOptions").width();

    $("#listOptions").css("left", left);
    $("#listOptions").css("top", top);

    if ($('#listOptions').is(':visible')) {
        $("#listOptions").fadeOut(300);
    } else {
        $("#listOptions").fadeIn(300);
    }
}

function ToggleFriends() {
    if ($('#friends').width() == 0) {
        // Before we can resize the friends list, we must shrink it so
        // it won't "jump" down below on the page.
        var sidebarwidth = $("#left-sidebar").width();
        $("#main-content").width($(window).width() - (sidebarwidth + 190));
        $("#friends").animate({ width: 190 }, 300, function () { ResizeContent(); });
    } else {
        $("#friends").animate({ width: 0 }, 300, function () { ResizeContent(); });
    }
}

function ResizeContent() {
    var logoHeight = $("#header").outerHeight();
    var headerheight = $("#header").outerHeight() + $("#toolbar").outerHeight();
    var friendswidth = $("#friends").width();
    var sidebarwidth = $("#left-sidebar").width();

    //console.log("logoHeight: " + logoHeight);

    var windowheight = $(window).outerHeight();
    var height = windowheight - headerheight;

    $(".stretch").height(windowheight - headerheight);

    $(".stretchFull").height(windowheight - headerheight);

    $("#main-content").width($(window).width() - (sidebarwidth + friendswidth));

    var friendlistheight = $("#friendlist").outerHeight();

    //console.log("friendlistheight: " + friendlistheight);

    $("#friendfeed").height(windowheight - headerheight - friendlistheight);

    var searchTasksHeight = $("#searchTasks").outerHeight();
    var searchheight = $("#search").outerHeight();

    $("#searchContent").height(windowheight - (logoHeight + searchTasksHeight));
}

function DisplayIntroduction() {
    $(".introduction_container").show();
    $("#gradient_transparent").show();

    // Do a smooth animated scroll to the introduction texts. This worked best when performed within a timer.
    setTimeout(function () { $("html").animate({ scrollTop: $("#introduction").offset().top }, 2000); }, 100);
}

function HideIntroduction() {
    $(".introduction_container").hide();
    $("#gradient_transparent").hide();
}

function ChangeState(state) {
    console.log("ChangeState: " + state);

    // Due to issues with Chrome on the initial loading, we'll
    // make an extra call to the resize method here.
    ResizeContent();

    switch (state) {
        case 0: // State.Initializing

            // Loading...
            $(".anonymous, .authenticated, .content").hide();
            $(".initializing").show();

            break;

        case 1: // State.Authenticated

            HideIntroduction();

            // Hide the loading indicator for auth status.
            $("#logo").animate({ padding: "0px" }, 200, function () {
                // Resize the content for the new state to ensure everything is up-to-speed.
                // It's important that we do this after the animation.
                ResizeContent();
            });

            $("body").addClass("bodybg");

            $(".anonymous, .initializing").hide();
            $(".authenticated").fadeIn(700);
            $("#logo").addClass("logo_authenticated");

            //$("#AppContainer").css("padding", "0px");

            break;

        case 2: // State.Anonymous

            // Hide the loading indicator for auth status.
            $("body").removeClass("bodybg");
            $("#logo").animate({ padding: "50px" }, 500);

            $(".authenticated, .initializing").hide();
            $(".anonymous").fadeIn(700);
            $("#logo").removeClass("logo_authenticated");

            break;
    }
}

var mainViewModel = function () {
    var self = this;

    self.Catalogs = ko.observableArray();
    self.Friends = ko.observableArray();
    self.Items = ko.observableArray();
    self.Results = ko.observableArray();
    self.SelectedResults = ko.observableArray();
    self.SelectedItems = ko.observableArray();
    self.Activities = ko.observableArray();

    self.User = ko.observable();

    self.LoadUser = function () {
        $.ajax({
            url: "/Api/User",
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }).done(function (data) {
            console.log(data);
            var item = ko.mapping.fromJS(data);
            self.User(item);
        });
    }

    self.Languages = ko.observableArray([
    { key: "auto", value: "Auto-detect" },
    { key: "en-US", value: "English" },
    { key: "nb-NO", value: "Norwegian" }
    ]);

    //self.SelectedItem = ko.observable();
    self.SelectedCatalog = ko.observable();
    self.SearchForItem = ko.observable(false);
    self.SearchTerm = ko.observable();
    self.SaveStatusText = ko.observable();

    self.SelectedObject = ko.observable();

    self.SelectedAction = ko.observable("Edit Catalog");

    self.LoadActivities = function () {
        //var now = new Date();
        //var utc = new Date(Date.UTC(
        //    now.getFullYear(),
        //    now.getMonth(),
        //    now.getDate(),
        //    now.getHours(),
        //    now.getMinutes()
        //));

        var now = new Date();
        //var utc = new Date(now.getTime() + now.getTimezoneOffset() * 60000);

        self.LoadActivitiesByTime(now);
    }

    self.LoadActivitiesByTime = function (date) {
        // TODO: Change this to use WebSockets: http://msdn.microsoft.com/en-us/library/ie/hh673567(v=vs.85).aspx

        var url = "/Api/Activities";

        if (date != null) {
            var queryDate = date.toISOString();
            var queryDateEncoded = encodeURIComponent(queryDate);
            url = url + "/?date=" + queryDateEncoded;
        }

        $.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }).done(function (data) {
            //self.Activities.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {
                var item = data[i];

                item.User.ProfileImageUrl = "https://graph.facebook.com/" + item.User.FacebookId + "/picture";

                self.Activities.push(item);
            }
        });
    }

    // Refresh the activities every minute.
    setInterval(self.LoadActivities, 60000);

    self.SelectCatalog = function (catalog) {
        self.SelectedCatalog(catalog);
        self.SelectedObject(catalog);
        self.LoadItems(catalog.Id());
    }

    self.SaveSettings = function () {
        //var catalog = self.SelectedObject();
        var user = ko.toJSON(self.User());
        //var json = JSON.stringify(user);

        $.ajax({
            url: "/api/User",
            data: user,
            type: "put",
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }
        ).done(function (data) {
            ToggleSettings();
            //$("#notificationDialog").fadeIn().delay(2000).fadeOut();
            //self.Catalogs.push(data);
        });
    }

    self.SaveCatalog = function () {
        HideProperties();

        var catalog = self.SelectedObject();
        var json = ko.mapping.toJSON(catalog);
        var jsonText = JSON.stringify(catalog);

        var id = catalog.Id();

        var url = "/api/Catalogs";
        var method = "post";

        if (id > 0)
        {
            url += "/" + id;
            method = "put";
        }

        $.ajax({
            url: url,
            data: json,
            type: method,
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }
        ).done(function (data) {

            $("#notificationDialog").fadeIn().delay(2000).fadeOut();

            if (id > 0)
            {
                ko.mapping.updateFromJSON(catalog, data);
            }
            else
            {
                self.Catalogs.push(data);
            }
        });
    }

    self.DeleteCatalog = function () {
        var catalog = self.SelectedCatalog();

        ShowInformationDialog("Confirm Catalog Delete",
            "Are you sure you want to delete the collection \"" + catalog.Name() + "\" and all it's items?",
            "Delete",
            "Cancel",
            function () {
                $.ajax({
                    url: "/Api/Catalogs/" + catalog.Id(),
                    type: "DELETE",
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("AccessToken", facebookAccessToken);
                        xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
                    }
                }).done(function () {

                    $("#notificationDialog").fadeIn().delay(2000).fadeOut();

                    self.Items.removeAll();
                    self.Catalogs.remove(catalog);
                    
                });
            })

        //var documentWidth = $(document).width();
        //var confirmationWidth = $("#confirmationDialog").width();
        //var leftPosition = (documentWidth / 2) - (confirmationWidth / 2);

        //$("#confirmationDialog").show();
        //$("#confirmationDialog").css("right", (leftPosition - 50) + "px");
        //$("#confirmationDialog").animate({ opacity: 1, right: leftPosition }, 500);
    }

    self.CreateCatalog = function () {
        self.SelectedAction("Add Collection");

        var newCatalog = { "Id": -1, "Name": "", "Count": 0, "Visibility": "Private" };
        self.SelectedObject(newCatalog);

        ToggleProperties("Add Collection", "Add");
    }

    self.EditCatalog = function () {
        self.SelectedAction("Edit Collection");

        self.SelectedObject(self.SelectedCatalog());

        ToggleProperties("Edit Collection", "Edit");
    }

    self.SelectResult = function (item) {
        if (item.Selected()) {
            self.SelectedResults.remove(item);
            item.Selected(false);
        }
        else {
            self.SelectedResults.push(item);
            item.Selected(true);
        }
    }

    self.SelectItem = function (item) {
        if (item.Selected()) {
            self.SelectedItems.remove(item);
            item.Selected(false);
        }
        else {
            self.SelectedItems.push(item);
            item.Selected(true);
        }
    }

    self.AddItem = function () {
        self.SearchForItem(true);
        ToggleSearch();

        self.SaveStatusText("");
    }

    self.DeleteItems = function () {
        var array = self.SelectedItems();

        for (i = 0; i < array.length; i++) {
            var item = array[i];
            //var json = JSON.stringify(item);

            $.ajax({
                url: "/Api/Items/" + item.Id(),
                type: "DELETE",
                dataType: "json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("AccessToken", facebookAccessToken);
                    xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
                }
            }).done(function () {
                self.SelectedItems.remove(item);
                self.Items.remove(item);
            });
        }
    }

    self.onRemove = function (item) {
        //console.log(item);

        //if ($(item).length > 0)
        //{
        //    $(item).fadeOut();
        //}

        //
        //console.log(item);
    }

    self.SaveSelectedItems = function () {
        var array = self.SelectedResults();

        for (i = 0; i < array.length; i++) {
            var item = array[i];
            item.Catalog_Id = self.SelectedCatalog().Id;
            var json = JSON.stringify(item);

            $.ajax({
                url: "/api/Items",
                data: json,
                type: "post",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("AccessToken", facebookAccessToken);
                    xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
                }
            }
                ).done(function (data) {
                    self.SaveStatusText("Saved " + i + " of " + array.length);
                });
        }
    }

    self.Search = function () {
        $.ajax({
            url: "/Api/Search/" + self.SearchTerm(),
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }).done(function (data) {
            self.Results.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {
                var item = data[i];
                item.Selected = ko.observable(false);
                self.Results.push(item);
            }
        });
    }

    self.LoadItems = function (catalogId) {
        $.ajax({
            url: "/Api/Items/" + catalogId,
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }).done(function (data) {
            self.Items.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {
                var item = ko.mapping.fromJS(data[i]);
                item.Selected = ko.observable(false);
                self.Items.push(item);
            }
        });
    }

    self.LoadFriends = function () {
        $.ajax({
            url: "/Api/Friends",
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }).done(function (data) {
            self.Friends.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {

                var item = ko.mapping.fromJS(data[i]);
                item.ImageUrl = ko.observable("https://graph.facebook.com/" + item.FacebookId() + "/picture");
                self.Friends.push(item);

                //if (i == 0) {
                //    self.SelectCatalog(data[i]);
                //    self.SelectedObject(data[i]);
                //}
            }
        });
    }

    self.LoadData = function () {
        self.LoadCatalogs();
        self.LoadFriends();

        // Load the current user, these values are used on the settings dialog.
        self.LoadUser();

        // Do the initial activity loading which will return the top 30 activities.
        self.LoadActivitiesByTime(null);

        var timeZonesJson = $.parseJSON($("#timezones").html());
        self.TimeZones(timeZonesJson.options);

        //for(timezone in timeZonesJson.options)
        //{
        //    self.TimeZones.push(timezone);
        //}

        //self.TimeZones(timeZonesJson);
    }

    self.TimeZones = ko.observableArray();

    self.LoadCatalogs = function () {
        $.ajax({
            url: "/Api/Catalogs",
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
                xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
            }
        }).done(function (data) {

            self.Catalogs.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {

                var item = ko.mapping.fromJS(data[i]);

                self.Catalogs.push(item);

                if (i == 0) {
                    self.SelectCatalog(item);
                    self.SelectedObject(item);
                }
            }
        });
    }
}

function ConfirmAccountRemoval() {
    ShowInformationDialog("Confirm Account Delete",
            "Are you sure you want to delete your account and all the data it contains? This operation cannot be undone.",
            "Delete",
            "Cancel",
            function () {
                $.ajax({
                    url: "/Api/Account/",
                    type: "DELETE",
                    dataType: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("AccessToken", facebookAccessToken);
                        xhr.setRequestHeader("AccessTokenExpiresIn", facebookAccessTokenExpiresIn);
                    }
                }).done(function () {

                    // TODO: Delete all the local cached files.
                    // This is very important, or else the user will be re-created on the next HTTP request.
                    facebookAccessToken = null;

                    FB.logout();
                });
            })
}