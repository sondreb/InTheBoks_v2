var InTheBoks;
(function (InTheBoks) {
    var Storage = (function () {
        function Storage(name) {
            if (typeof name === "undefined") { name = ""; }
            this.Name = name;
            this.Supported = Modernizr.localstorage;
        }
        Storage.prototype.Open = function () {
        };
        return Storage;
    })();
    InTheBoks.Storage = Storage;    
    var ServiceClient = (function () {
        function ServiceClient(action) {
            if (typeof action === "undefined") { action = ""; }
            this.Action = action;
        }
        ServiceClient.prototype.Execute = function (callback, type, action) {
            if (typeof type === "undefined") { type = "GET"; }
            if (typeof action === "undefined") { action = ""; }
            var a = (action != "") ? action : this.Action;
            $.ajax({
                url: "/Api/" + a,
                type: type,
                dataType: "json"
            }).done(callback);
        };
        return ServiceClient;
    })();
    InTheBoks.ServiceClient = ServiceClient;    
})(InTheBoks || (InTheBoks = {}));
var listViewType = 1;
var thumbnailSize = 128;
