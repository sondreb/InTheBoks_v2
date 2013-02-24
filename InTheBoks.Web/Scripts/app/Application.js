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
                    console.log("Logged in");
                    ChangeState(1);
                });
                self.Auth.LoggedOut.on(function () {
                    console.log("Logged out");
                    ChangeState(2);
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
function ChangeState(state) {
    console.log("ChangeState: " + state);
    ResizeContent();
    switch(state) {
        case 0:
            $(".anonymous, .authenticated, .content").hide();
            $(".initializing").show();
            break;
        case 1:
            HideIntroduction();
            $("#logo").animate({
                padding: "0px"
            }, 200, function () {
                ResizeContent();
            });
            $("body").addClass("bodybg");
            $(".anonymous, .initializing").hide();
            $(".authenticated").fadeIn(700);
            $("#logo").addClass("logo_authenticated");
            break;
        case 2:
            $("body").removeClass("bodybg");
            $("#logo").animate({
                padding: "50px"
            }, 500);
            $(".authenticated, .initializing").hide();
            $(".anonymous").fadeIn(700);
            $("#logo").removeClass("logo_authenticated");
            break;
    }
}
function DisplayIntroduction() {
    $(".introduction_container").show();
    $("#gradient_transparent").show();
    setTimeout(function () {
        $("html").animate({
            scrollTop: $("#introduction").offset().top
        }, 2000);
    }, 100);
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
    $("#friendfeed").height(windowheight - headerheight - friendlistheight);
    var searchTasksHeight = $("#searchTasks").outerHeight();
    var searchheight = $("#search").outerHeight();
    $("#searchContent").height(windowheight - (logoHeight + searchTasksHeight));
}
$(document).ready(function () {
    console.log("InTheBoks: document.ready");
    Resource = $.parseJSON($("#resources").html());
});
$(window).load(function () {
    console.log("InTheBoks: window.loaded");
    InTheBoks.Application.App.Instance.Start();
    ResizeContent();
});
$(window).resize(function () {
    ResizeContent();
});
