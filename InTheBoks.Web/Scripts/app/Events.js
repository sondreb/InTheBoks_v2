var InTheBoks;
(function (InTheBoks) {
    var Event = (function () {
        function Event() {
            this.handlers = [];
        }
        Event.prototype.on = function (handler) {
            this.handlers.push(handler);
        };
        Event.prototype.off = function (handler) {
            this.handlers = this.handlers.filter(function (h) {
                return h !== handler;
            });
        };
        Event.prototype.trigger = function (data) {
            if(this.handlers) {
                this.handlers.forEach(function (h) {
                    return h(data);
                });
            }
        };
        return Event;
    })();
    InTheBoks.Event = Event;    
})(InTheBoks || (InTheBoks = {}));
