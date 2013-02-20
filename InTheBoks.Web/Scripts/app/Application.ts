/// <reference path="_references.ts" />

module InTheBoks.Application
{
    export class App
    {
        Auth: InTheBoks.Facebook;
        ViewModel: InTheBoks.ViewModels.MainViewModel;

        constructor() {
            var self = this;
        }

        static Instance = new App();

        Start()
        {
            var self = this;

            self.Auth = new InTheBoks.Facebook();
            self.ViewModel = new InTheBoks.ViewModels.MainViewModel();

            this.InitFacebook();
            this.InitUI();

            // Apply the bindings of the main view model with the DOM.
            ko.applyBindings(self.ViewModel);
        }

        private InitUI()
        {
            $("img").on("dragstart", function (event) { event.preventDefault(); });

            $(document).on("dragstart", function () {
                return false;
            });

            if (jQuery.ui) {
                // UI loaded
                $("#slider").slider({
                    value: 128,
                    min: 32,
                    max: 256,
                    step: 32,
                    slide: function (event, ui) {

                        //thumbnailSize = ui.value;
                        //ThumbnailRender();

                    }
                });
            }
        }

        private InitFacebook()
        {
            var self = this;

            // Hook up the auth events.
            self.Auth.LoggedIn.on(() => { alert("Logged in"); });
            self.Auth.LoggedOut.on(() => { alert("Logged out"); });

            // Initialize the auth provider.
            self.Auth.Initialize();

            setTimeout(function () {
                // Some corporations block Facebook URLs,
                // try to inform the user about potential issues if not authenticated within a certain time.

                //VerifyFacebookAccess();
            }, 5000);
        }
    }
}

$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready
    console.log("InTheBoks: document.ready");

});

$(window).load(function () {
    // executes when complete page is fully loaded, including all frames, objects and images
    console.log("InTheBoks: window.loaded");

    // Start the static application instance.
    InTheBoks.Application.App.Instance.Start();
});