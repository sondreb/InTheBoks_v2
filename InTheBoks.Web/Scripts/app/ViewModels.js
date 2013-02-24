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
                var self = this;
                self.Catalogs = new CatalogsViewModel();
                self.Items = new ItemsViewModel();
                self.Friends = new FriendsViewModel();
                self.Activities = new ActivitiesViewModel();
                self.SelectedItems = ko.observableArray();
                self.User = ko.observable();
                self.States = {
                    "Initializing": 0,
                    "Authenticated": 1,
                    "Anonymous": 2
                };
                self.Languages = ko.observableArray([
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
                self.SelectedCatalog = ko.observable();
                self.Auth = new InTheBoks.Facebook();
                self.Login = function () {
                    self.Auth.Login();
                };
                self.Logout = function () {
                    self.Auth.Logout();
                };
            }
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
                var self = this;
                self.Items = ko.observableArray();
                self.Selected = ko.observable();
            }
            CollectionModelBase.prototype.Create = function (item) {
            };
            CollectionModelBase.prototype.Delete = function (item) {
            };
            CollectionModelBase.prototype.Edit = function (item) {
            };
            CollectionModelBase.prototype.Select = function (item) {
                console.log(item);
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
                var self = this;
            }
            CatalogsViewModel.prototype.Load = function () {
                var self = this;
                self.service.Execute(function (data) {
                    self.Items.removeAll();
                    for(var i = 0; i < data.length; i++) {
                        var item = ko.mapping.fromJS(data[i]);
                        self.Items.push(item);
                        if(i == 0) {
                            self.Selected(item);
                        }
                    }
                    ;
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
