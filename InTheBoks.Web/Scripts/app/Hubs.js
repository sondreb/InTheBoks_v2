var InTheBoks;
(function (InTheBoks) {
    (function (WebSocket) {
        var Hubs = (function () {
            function Hubs() {
                var self = this;
            }
            Hubs.Instance = new Hubs();
            Hubs.prototype.Start = function () {
                var self = this;
                self.Activities = $.connection.activities;
                self.Catalogs = $.connection.catalogs;
                self.Activities.client.notify = function (message) {
                    console.log(message);
                    alert(message);
                };
                self.Catalogs.client.catalog = function (catalog) {
                    alert("CATALOG!");
                    console.log(catalog);
                    alert(catalog);
                };
                $.connection.hub.start().done(function () {
                    $("#notifyButton").click(function () {
                        self.Activities.server.notifyClients();
                    });
                });
            };
            return Hubs;
        })();
        WebSocket.Hubs = Hubs;        
    })(InTheBoks.WebSocket || (InTheBoks.WebSocket = {}));
    var WebSocket = InTheBoks.WebSocket;
})(InTheBoks || (InTheBoks = {}));
$(document).ready(function () {
    console.log("InTheBoks.WebSockets: document.ready");
});
$(window).load(function () {
    console.log("InTheBoks.WebSockets: window.loaded");
});
