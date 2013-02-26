/// <reference path="_references.ts" />

declare var FB: any;

module InTheBoks
{
    export class Facebook
    {
        constructor() { };

        private AccessToken: string;
        private AccessTokenExpiresIn: number;
        private AppId: string;

        private onLogin = new Event();
        private onLogout = new Event();

        public get LoggedIn(): IEvent { return this.onLogin; }
        public get LoggedOut(): IEvent { return this.onLogout; }

        Login()
        {
            FB.login(function (response) { console.log(response); }, { scope: 'email,user_about_me,friends_about_me' });
        }

        Logout()
        {
            FB.logout();
        }

        // Run this function to get a fresh token.
        CheckStatus()
        {
            FB.getLoginStatus(function (response) {
                console.log(response);
                alert("Check Status...");
            });
        }
        
        Initialize()
        {
            var self = this;

            self.AppId = "407544339296631";

            if (document.URL.indexOf("localhost") > -1) {
                self.AppId = "122645277750670";
            }


            FB.init({
                appId: self.AppId, /* (122645277750670:localhost:7474)  (407544339296631:preview.intheboks.com) */
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

                    self.AccessTokenExpiresIn = response.authResponse.expiresIn; // 5184000

                    // The token which we should send to our server on AJAX requests.
                    var accessToken = response.authResponse.accessToken;
                    self.AccessToken = accessToken;

                    $.cookie("AccessToken", accessToken);
                    $.cookie("AccessTokenExpiresIn", self.AccessTokenExpiresIn);

                    // API query to get the user's metadata.
                    FB.api('/me', function (me) {
                        if (me.name) {
                            document.getElementById('auth-displayname').innerHTML = me.name;
                            $("#profileImage").attr("src", "https://graph.facebook.com/" + me.id + "/picture");

                            self.onLogin.trigger();
                            //ChangeState(State.Authenticated);
                        }
                    })

                    //InitializeUserData();
                } else {
                    // user has not auth'd your app, or is not logged into Facebook
                    //ChangeState(State.Anonymous);
                    self.onLogout.trigger();
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
                    //ChangeState(State.Anonymous);
                    self.onLogout.trigger();
                }
            });

        }
    }
}