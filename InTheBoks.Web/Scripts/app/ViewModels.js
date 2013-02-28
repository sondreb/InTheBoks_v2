var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var InTheBoks;
(function (InTheBoks) {
    (function (ViewModels) {
        var MainViewModel = (function () {
            function MainViewModel() {
                var _this = this;
                Rebind(this);
                this.Catalogs = new CatalogsViewModel();
                this.Items = new ItemsViewModel();
                this.Friends = new FriendsViewModel();
                this.Activities = new ActivitiesViewModel();
                this.User = ko.observable();
                this.States = {
                    "Initializing": 0,
                    "Authenticated": 1,
                    "Anonymous": 2
                };
                this.Languages = ko.observableArray([
                    {
                        key: "auto",
                        value: "Auto-detect"
                    }, 
                    {
                        key: "en-US",
                        value: "English"
                    }, 
                    {
                        key: "nb-NO",
                        value: "Norwegian"
                    }
                ]);
                this.Auth = new InTheBoks.Facebook();
                this.Login = function () {
                    _this.Auth.Login();
                };
                this.Logout = function () {
                    _this.Auth.Logout();
                };
            }
            MainViewModel.prototype.Load = function () {
                var self = this;
                self.Catalogs.Load();
            };
            MainViewModel.prototype.LoadUser = function () {
                var service = new InTheBoks.ServiceClient("User");
                service.Execute(function (data) {
                    console.log(data);
                    var item = ko.mapping.fromJS(data);
                    console.log(item);
                });
            };
            MainViewModel.prototype.ToggleFriends = function () {
                if($('#friends').width() == 0) {
                    var sidebarwidth = $("#left-sidebar").width();
                    $("#main-content").width($(window).width() - (sidebarwidth + 190));
                } else {
                }
            };
            return MainViewModel;
        })();
        ViewModels.MainViewModel = MainViewModel;        
        var SearchViewModel = (function () {
            function SearchViewModel() {
                var self = this;
                self.SearchTerm = ko.observable();
            }
            return SearchViewModel;
        })();
        ViewModels.SearchViewModel = SearchViewModel;        
        var SettingsViewModel = (function () {
            function SettingsViewModel() {
                var self = this;
                self.TimeZones = ko.observableArray();
            }
            return SettingsViewModel;
        })();
        ViewModels.SettingsViewModel = SettingsViewModel;        
        var CollectionModelBase = (function () {
            function CollectionModelBase() {
                Rebind(this);
                var self = this;
                self.Items = ko.observableArray();
                self.Selected = ko.observableArray();
                self.SingleSelect = ko.observable();
                self.SelectedFirst = ko.computed(function () {
                    if(self.Selected().length > 0) {
                        return self.Selected()[0];
                    } else {
                        return null;
                    }
                });
            }
            CollectionModelBase.prototype.Add = function (item) {
                this.Items.push(item);
            };
            CollectionModelBase.prototype.Remove = function (item) {
                this.Items.remove(item);
            };
            CollectionModelBase.prototype.Select = function (item) {
                if(this.SingleSelect()) {
                    this.Selected.removeAll();
                }
                this.Selected.push(item);
            };
            return CollectionModelBase;
        })();
        ViewModels.CollectionModelBase = CollectionModelBase;        
        var ActivitiesViewModel = (function (_super) {
            __extends(ActivitiesViewModel, _super);
            function ActivitiesViewModel() {
                        _super.call(this);
            }
            return ActivitiesViewModel;
        })(CollectionModelBase);
        ViewModels.ActivitiesViewModel = ActivitiesViewModel;        
        var FriendsViewModel = (function (_super) {
            __extends(FriendsViewModel, _super);
            function FriendsViewModel() {
                        _super.call(this);
            }
            return FriendsViewModel;
        })(CollectionModelBase);
        ViewModels.FriendsViewModel = FriendsViewModel;        
        var ItemsViewModel = (function (_super) {
            __extends(ItemsViewModel, _super);
            function ItemsViewModel() {
                        _super.call(this);
            }
            return ItemsViewModel;
        })(CollectionModelBase);
        ViewModels.ItemsViewModel = ItemsViewModel;        
        var CatalogsViewModel = (function (_super) {
            __extends(CatalogsViewModel, _super);
            function CatalogsViewModel() {
                        _super.call(this);
                this.service = new InTheBoks.ServiceClient("Catalogs");
                this.storage = new InTheBoks.Storage("Catalogs");
                Rebind(this);
                var self = this;
                self.SingleSelect(true);
            }
            CatalogsViewModel.prototype.Load = function () {
                var self = this;
                if(self.storage.Supported) {
                    console.log("Reading from local storage...");
                }
                self.service.Execute(function (data) {
                    ko.mapping.fromJS(data, {
                    }, self.Items);
                    if(self.Items().length > 0) {
                        self.Select(self.Items()[0]);
                    }
                    var itemsObject = ko.toJS(self.Items());
                    var itemsJson = JSON.stringify(itemsObject);
                    var parsedObject = JSON.parse(itemsJson);
                    localStorage.setItem("Catalogs", itemsJson);
                    localStorage.setItem("1|3|timestamp", "ThisIsAnItem");
                    localStorage.setItem("1|4|timestamp", "ThisIsAnItem #2");
                    localStorage.setItem("2|3|timestamp", "ThisIsAnItem #2");
                    for(var i in window.localStorage) {
                        var val = localStorage.getItem(i);
                        console.log(i);
                        console.log(val);
                    }
                });
            };
            CatalogsViewModel.prototype.Save = function () {
            };
            return CatalogsViewModel;
        })(CollectionModelBase);
        ViewModels.CatalogsViewModel = CatalogsViewModel;        
        var ModelBase = (function () {
            function ModelBase() {
                var self = this;
                self.Id = ko.observable();
                self.Name = ko.observable();
                self.Created = ko.observable();
                self.Modified = ko.observable();
            }
            ModelBase.prototype.update = function (instance) {
            };
            return ModelBase;
        })();
        ViewModels.ModelBase = ModelBase;        
        var Catalog = (function (_super) {
            __extends(Catalog, _super);
            function Catalog() {
                _super.apply(this, arguments);

            }
            Catalog.prototype.defaults = function () {
                this.Name("New catalog");
            };
            return Catalog;
        })(ModelBase);
        ViewModels.Catalog = Catalog;        
        var Item = (function (_super) {
            __extends(Item, _super);
            function Item() {
                _super.apply(this, arguments);

            }
            return Item;
        })(ModelBase);
        ViewModels.Item = Item;        
        var User = (function (_super) {
            __extends(User, _super);
            function User() {
                _super.apply(this, arguments);

            }
            return User;
        })(ModelBase);
        ViewModels.User = User;        
        var Activity = (function () {
            function Activity(name, id) {
                var self = this;
                self.Name = name;
                self.Id = id;
            }
            return Activity;
        })();
        ViewModels.Activity = Activity;        
        var Friend = (function () {
            function Friend(id) {
                this.id = id;
            }
            return Friend;
        })();
        ViewModels.Friend = Friend;        
    })(InTheBoks.ViewModels || (InTheBoks.ViewModels = {}));
    var ViewModels = InTheBoks.ViewModels;
})(InTheBoks || (InTheBoks = {}));
function Rebind(obj) {
    var prototype = obj.constructor.prototype;
    for(var name in prototype) {
        if(!obj.hasOwnProperty(name) && typeof prototype[name] === "function") {
            var method = prototype[name];
            obj[name] = method.bind(obj);
        }
    }
}
