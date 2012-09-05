var facebookAccessToken = null;
var facebookAccessTokenExpiresIn = null;

window.fbAsyncInit = function () {

    var fbAppId = "407544339296631";

    if (document.URL.indexOf("localhost") > -1)
    {
        fbAppId = "122645277750670";
    }

    FB.init({
        appId: fbAppId, /* (122645277750670:localhost:7474)  (407544339296631:preview.intheboks.com) */
        status: true, // check login status
        cookie: false, // enable cookies to allow the server to access the session
        xfbml: false,  // parse XFBML
        oauth: true
    });

    // listen for and handle auth.statusChange events
    FB.Event.subscribe('auth.statusChange', function (response) {

        console.log('FB: auth.statusChange');

        if (response.authResponse) { // user has auth'd your app and is logged into Facebook

            // Don't trust this user ID on the server-side. Can be used for lookup profile photo, etc.
            var uid = response.authResponse.userID;

            facebookAccessTokenExpiresIn = response.authResponse.expiresIn; // 5184000
            
            // The token which we should send to our server on AJAX requests.
            var accessToken = response.authResponse.accessToken;
            facebookAccessToken = accessToken;

            // API query to get the user's metadata.
            FB.api('/me', function (me) {
                if (me.name) {
                    document.getElementById('auth-displayname').innerHTML = me.name;
                    $("#profileImage").attr("src", "https://graph.facebook.com/" + me.id + "/picture");

                    ChangeState(State.Authenticated);

                }
            })

            InitializeUserData();

        } else {
            // user has not auth'd your app, or is not logged into Facebook
            ChangeState(State.Anonymous);
        }
    });

    FB.getLoginStatus(function (response) {

        console.log('FB: getLoginStatus');

        if (response.status === 'connected') {
            // We don't need to do anything here, the event
            // subscribed above will handle whenever the user
            // is authenticated.
        } else if (response.status === 'not_authorized') {
                // the user is logged in to Facebook, 
                // but has not authenticated your app
            console.log("You have not autorized this app.");
        } else {
            // the user isn't logged in to Facebook.
            ChangeState(State.Anonymous);
        }
    });

};


