
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

});

function ThumbnailRender()
{
    var size = $("#thumbnailSize").val();
    addCSSRule("#thumbnails img", "height", size + "px");
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


var catalogsViewModel = function ()
{
    var self = this;

    self.Items = ko.observableArray();

    self.Select = function (catalog)
    {
        alert(Resource.YouSelected + ": " + catalog.Name);
    }

    self.Load = function ()
    {
        $.ajax({
            url: "/Api/Catalogs",
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
}