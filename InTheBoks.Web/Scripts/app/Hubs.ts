/// <reference path="_references.ts" />

module InTheBoks.WebSocket {

    export class Hubs {
        Activities: any;
        Catalogs: any;

        static Instance = new Hubs();

        constructor() {
            var self = this;

        }

        Start() {

            var self = this;

            self.Activities = $.connection.activities;
            self.Catalogs = $.connection.catalogs;

            self.Activities.client.notify = function (message) {
                console.log(message);
                alert(message);
                //$.each(data, function () {
                //    $('#messages').append('<li>' + this + '</li>');
                //});
            }

            self.Catalogs.client.catalog = function (catalog) {
                alert("CATALOG!");
                console.log(catalog);
                alert(catalog);
            }

            $.connection.hub.start().done(function () {

                //activities.server.notifyClients();
                //$.connection.activities.notifyClients();

                $("#notifyButton").click(function () {
                    self.Activities.server.notifyClients();
                });
            });

        }
    }
}

$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready
    console.log("InTheBoks.WebSockets: document.ready");

});

$(window).load(function () {
    // executes when complete page is fully loaded, including all frames, objects and images
    console.log("InTheBoks.WebSockets: window.loaded");
    InTheBoks.WebSocket.Hubs.Instance.Start();

});