/// <reference path="_references.ts" />

declare var Resource: any;

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
            self.Auth.LoggedIn.on(() => { console.log("FB: Logged in"); ChangeState(1); });
            self.Auth.LoggedOut.on(() => { console.log("FB: Logged out"); ChangeState(2); });

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

            // Start loading data
            InTheBoks.Application.App.Instance.ViewModel.Load();

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

function ResizeContent() {

    var logoHeight = $("#header").outerHeight();
    var headerheight = $("#header").outerHeight() + $("#toolbar").outerHeight();
    var friendswidth = $("#friends").width();
    var sidebarwidth = $("#left-sidebar").width();

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

$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready
    console.log("InTheBoks: document.ready");

    // Define the resources on a global level. We should do this before
    // we create any view models, etc.
    Resource = $.parseJSON($("#resources").html());

});

$(window).load(function () {
    // executes when complete page is fully loaded, including all frames, objects and images
    console.log("InTheBoks: window.loaded");

    // Start the static application instance.
    InTheBoks.Application.App.Instance.Start();

    ResizeContent();
});

$(window).resize(function () {

    ResizeContent();

});

