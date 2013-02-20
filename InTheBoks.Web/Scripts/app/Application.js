var InTheBoks;
(function (InTheBoks) {
    (function (Application) {
        var App = (function () {
            function App() {
                var self = this;
            }
            App.Instance = new App();
            App.prototype.Start = function () {
                var self = this;
                self.Auth = new InTheBoks.Facebook();
                self.ViewModel = new InTheBoks.ViewModels.MainViewModel();
                this.InitFacebook();
                this.InitUI();
                ko.applyBindings(self.ViewModel);
            };
            App.prototype.InitUI = function () {
                $("img").on("dragstart", function (event) {
                    event.preventDefault();
                });
                $(document).on("dragstart", function () {
                    return false;
                });
                if(jQuery.ui) {
                    $("#slider").slider({
                        value: 128,
                        min: 32,
                        max: 256,
                        step: 32,
                        slide: function (event, ui) {
                        }
                    });
                }
            };
            App.prototype.InitFacebook = function () {
                var self = this;
                self.Auth.LoggedIn.on(function () {
                    alert("Logged in");
                });
                self.Auth.LoggedOut.on(function () {
                    alert("Logged out");
                });
                self.Auth.Initialize();
                setTimeout(function () {
                }, 5000);
            };
            return App;
        })();
        Application.App = App;        
    })(InTheBoks.Application || (InTheBoks.Application = {}));
    var Application = InTheBoks.Application;
})(InTheBoks || (InTheBoks = {}));
$(document).ready(function () {
    console.log("InTheBoks: document.ready");
});
$(window).load(function () {
    console.log("InTheBoks: window.loaded");
    InTheBoks.Application.App.Instance.Start();
});
