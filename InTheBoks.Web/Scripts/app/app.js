
var State = { "Initializing": 0, "Authenticated": 1, "Anonymous": 2 };

// Onload event handler
$(function () {

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
    });

    $('img').bind('dragstart', function (event) { event.preventDefault(); });

    $(document).bind("dragstart", function () {
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


        if (ctrlDown && (e.keyCode == aKey))
        {
            selectAll = !selectAll;

            if (selectAll)
            {
                $(".wrapper").addClass("selection");
            }
            else
            {
                $(".wrapper").removeClass("selection");
            }


            return false;
        }
        else if (ctrlDown && (e.keyCode == cKey))
        {
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



    //$('.wrapper').mouseover(function (source) {
    //    $(this).children(".description").fadeIn(300);
    //}).mouseout(function () {
    //    $(this).children(".description").fadeOut(100);
    //});

    $('.wrapper').bind('mouseenter', function (source) {
        //$(this).children(".description").fadeIn(50);
        $(this).children(".description").show();
    }).bind('mouseleave', function (source) {

        $(this).children(".description").hide();
        //$(this).children(".description").fadeOut(150);
    }).bind('click', function (source) {
        if ($(this).hasClass("selection"))
        {
            $(this).removeClass("selection");
        }
    else
    {
            $(this).addClass("selection");
        }
    });

});




function HookUpThumbnailEvents(wrapper)
{
    $(wrapper).bind('mouseenter', function (source) {
        //$(this).children(".description").fadeIn(50);
        $(this).children(".description").show();
    }).bind('mouseleave', function (source) {

        $(this).children(".description").hide();
        //$(this).children(".description").fadeOut(150);
    }).bind('click', function (source) {
        if ($(this).hasClass("selection")) {
            $(this).removeClass("selection");
        }
        else {
            $(this).addClass("selection");
        }
    });
}

function ThumbnailRender()
{
    var size = $("#thumbnailSize").val();
    addCSSRule(".thumbnails div img", "height", size + "px");
    //addCSSRule(".thumbnails>span:before", "margin-left", size + "px");
    
    console.log(size);
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


function ToggleSettings()
{
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
}

function ToggleFilter()
{
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

    var left = $(button).position().left - 110;
    var top = $(button).position().top + 36;

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
        $("#main-content").width($(window).width() - (sidebarwidth + 150));
        $("#friends").animate({ width: 150 }, 300, function () { ResizeContent(); });

    } else {

        $("#friends").animate({ width: 0 }, 300, function () { ResizeContent(); });

    }
}

function ResizeContent() {

    var logoHeight = $("#header").height();

    var headerheight = $("#header").height() + $("#toolbar").height();
    var footerheight = $("#footer").height();
    var friendswidth = $("#friends").width();
    var sidebarwidth = $("#left-sidebar").width();


    var windowheight = $(window).height();
    var height = windowheight - (headerheight + footerheight);

    $(".stretch").height(windowheight - (headerheight + footerheight));
    $(".stretchFull").height(windowheight - (headerheight + footerheight) + $("#toolbar").height());

    $("#main-content").width($(window).width() - (sidebarwidth + friendswidth));

    var friendlistheight = $("#friendlist").height();

    $("#friendfeed").height(windowheight - friendlistheight - (headerheight + footerheight));

    var searchTasksHeight = $("#searchTasks").height();
    var searchheight = $("#search").height();

    console.log("searchheight: " + searchheight);
    console.log("searchTasksHeight: " + searchTasksHeight);

    $("#searchContent").height(windowheight - (logoHeight + footerheight + searchTasksHeight));

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

            // Hide the loading indicator for auth status.
            $(".anonymous, .initializing").hide();
            $(".authenticated").fadeIn(700);

            break;

        case 2: // State.Anonymous

            // Hide the loading indicator for auth status.
            $(".authenticated, .initializing").hide();
            $(".anonymous").fadeIn(700);

            break;
    }
}


var mainViewModel = function ()
{
    var self = this;

    self.Catalogs = ko.observableArray();
    self.Items = ko.observableArray();
    self.Results = ko.observableArray();
    self.SelectedResults = ko.observableArray();
    self.SelectedItem = ko.observable();
    self.SelectedCatalog = ko.observable();
    self.SearchForItem = ko.observable(false);
    self.SearchTerm = ko.observable();

    self.SelectCatalog = function (catalog)
    {
        self.SelectedCatalog(catalog);
    }

    self.SelectResult = function (item) {

        if (item.Selected())
        {
            self.SelectedResults.remove(item);
            item.Selected(false);
        }
        else
        {
            self.SelectedResults.push(item);
            item.Selected(true);
        }
    }

    self.SelectItem = function (item) {

        item.Selected = true;
        self.SelectedItem(item);
    }

    self.AddItem = function ()
    {
        self.SearchForItem(true);
        ToggleSearch();
        //self.Search();
    }

    self.Search = function () {
        $.ajax({
            url: "/Api/Search/" + self.SearchTerm(),
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
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

    self.LoadItems = function () {
        $.ajax({
            url: "/Api/Items",
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
            }
        }).done(function (data) {

            self.Items.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {
                self.Items.push(data[i]);
            }

        });
    }

    self.LoadCatalogs = function ()
    {
        $.ajax({
            url: "/Api/Catalogs",
            type: "GET",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("AccessToken", facebookAccessToken);
            }
        }).done(function (data) {

            self.Catalogs.removeAll();
            console.log(data);

            for (i = 0; i < data.length; i++) {
                self.Catalogs.push(data[i]);

                if (i == 0)
                {
                    self.SelectCatalog(data[i]);
                }
            }

        });
    }
}