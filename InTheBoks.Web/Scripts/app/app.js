
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


function ChangeState(state) {
    console.log("ChangeState: " + state);

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