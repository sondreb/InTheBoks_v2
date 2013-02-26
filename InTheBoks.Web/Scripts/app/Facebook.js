var InTheBoks;
(function (InTheBoks) {
    var Facebook = (function () {
        function Facebook() {
            this.onLogin = new InTheBoks.Event();
            this.onLogout = new InTheBoks.Event();
        }
        Object.defineProperty(Facebook.prototype, "LoggedIn", {
            get: function () {
                return this.onLogin;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Facebook.prototype, "LoggedOut", {
            get: function () {
                return this.onLogout;
            },
            enumerable: true,
            configurable: true
        });
        Facebook.prototype.Login = function () {
            FB.login(function (response) {
                console.log(response);
            }, {
                scope: 'email,user_about_me,friends_about_me'
            });
        };
        Facebook.prototype.Logout = function () {
            FB.logout();
        };
        Facebook.prototype.CheckStatus = function () {
            FB.getLoginStatus(function (response) {
                console.log(response);
                alert("Check Status...");
            });
        };
        Facebook.prototype.Initialize = function () {
            var self = this;
            self.AppId = "407544339296631";
            if(document.URL.indexOf("localhost") > -1) {
                self.AppId = "122645277750670";
            }
            FB.init({
                appId: self.AppId,
                status: true,
                cookie: false,
                xfbml: false,
                oauth: true
            });
            FB.Event.subscribe('auth.statusChange', function (response) {
                console.log('FB: auth.statusChange');
                if(response.authResponse) {
                    var uid = response.authResponse.userID;
                    self.AccessTokenExpiresIn = response.authResponse.expiresIn;
                    var accessToken = response.authResponse.accessToken;
                    self.AccessToken = accessToken;
                    $.cookie("AccessToken", accessToken);
                    $.cookie("AccessTokenExpiresIn", self.AccessTokenExpiresIn);
                    FB.api('/me', function (me) {
                        if(me.name) {
                            document.getElementById('auth-displayname').innerHTML = me.name;
                            $("#profileImage").attr("src", "https://graph.facebook.com/" + me.id + "/picture");
                            self.onLogin.trigger();
                        }
                    });
                } else {
                    self.onLogout.trigger();
                }
            });
            FB.getLoginStatus(function (response) {
                console.log('FB: getLoginStatus');
                if(response.status === 'connected') {
                } else if(response.status === 'not_authorized') {
                    console.log("You have not autorized this app.");
                } else {
                    self.onLogout.trigger();
                }
            });
        };
        return Facebook;
    })();
    InTheBoks.Facebook = Facebook;    
})(InTheBoks || (InTheBoks = {}));
