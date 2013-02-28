/// <reference path="_references.ts" />

declare var Modernizr: any;

module InTheBoks.ViewModels
{
    export class MainViewModel
    {
        Catalogs: CatalogsViewModel;
        Items: ItemsViewModel;
        Friends: FriendsViewModel;

        //Friends: KnockoutObservableArray;
        //SelectedItems: KnockoutObservableArray;
        //SelectedCatalog: KnockoutObservableAny;
        Activities: ActivitiesViewModel;
        User: KnockoutObservableAny;
        States: any;
        State: KnockoutObservableAny;
        Auth: Facebook;
        Languages: KnockoutObservableArray;

        Login: () => void;
        Logout: () => void;

        constructor()
        {
            Rebind(this);

            this.Catalogs = new CatalogsViewModel();
            this.Items = new ItemsViewModel();
            this.Friends = new FriendsViewModel();
            this.Activities = new ActivitiesViewModel();

            //self.SelectedItems = ko.observableArray();
            this.User = ko.observable();

            this.States = { "Initializing": 0, "Authenticated": 1, "Anonymous": 2 };
            //self.State(self.States()[0]);

            this.Languages = ko.observableArray([
                 { key: "auto", value: "Auto-detect" },
                 { key: "en-US", value: "English" },
                 { key: "nb-NO", value: "Norwegian" }
            ]);

            //self.SelectedCatalog = ko.observable();
            //self.SearchForItem = ko.observable(false);
            
            //self.SaveStatusText = ko.observable();

            //self.SelectedObject = ko.observable();

            //self.SelectedAction = ko.observable("Edit Catalog");


            this.Auth = new InTheBoks.Facebook();

            this.Login = () => { this.Auth.Login(); };
            this.Logout = () => { this.Auth.Logout(); };
        }

        Load()
        {
            var self = this;

            // Load the catalogs.
            self.Catalogs.Load();

        }

        LoadUser() {

            var service = new InTheBoks.ServiceClient("User");

            service.Execute(function (data) {
                console.log(data);
                var item = ko.mapping.fromJS(data);
                console.log(item);
                //self.User(item);
            });

        }

        ToggleFriends() {
            if ($('#friends').width() == 0) {
                // Before we can resize the friends list, we must shrink it so
                // it won't "jump" down below on the page.
                var sidebarwidth = $("#left-sidebar").width();
                $("#main-content").width($(window).width() - (sidebarwidth + 190));
                //$("#friends").animate({ width: 190 }, 300, function () { ResizeContent(); });
            } else {
                //$("#friends").animate({ width: 0 }, 300, function () { ResizeContent(); });
            }
        }
    }

    export class SearchViewModel
    {
        SearchTerm: KnockoutObservableString;

        constructor()
        {
            var self = this;

            self.SearchTerm = ko.observable();
        }
    }

    export class SettingsViewModel
    {
        TimeZones: KnockoutObservableArray;

        constructor()
        {
            var self = this;

            self.TimeZones = ko.observableArray();

            //var timeZonesJson = $.parseJSON($("#timezones").html());
            //self.TimeZones(timeZonesJson.options);
        }
    }

    export class CollectionModelBase
    {
        Items: KnockoutObservableArray;
        Selected: KnockoutObservableArray;
        SingleSelect: KnockoutObservableBool;
        SelectedFirst: KnockoutComputed;

        constructor()
        {
            Rebind(this);

            var self = this;

            self.Items = ko.observableArray();
            self.Selected = ko.observableArray();
            self.SingleSelect = ko.observable();
            self.SelectedFirst = ko.computed(function ()
            {
                if (self.Selected().length > 0) {
                    return self.Selected()[0];
                }
                else { return null; }
            });
        }

        Add(item)
        {
            this.Items.push(item);
        }

        Remove(item)
        {
            this.Items.remove(item);
        }

        Select(item)
        {
            if (this.SingleSelect()) {
                this.Selected.removeAll();
            }

            // Set the selected item.
            this.Selected.push(item);
        }
    }

    export class ActivitiesViewModel extends CollectionModelBase
    {
        constructor()
        {
            super();
        }
    }

    export class FriendsViewModel extends CollectionModelBase
    {
        constructor()
        {
            super();
        }
    }

    export class ItemsViewModel extends CollectionModelBase
    {
        constructor()
        {
            super();
        }
    }

    export class CatalogsViewModel extends CollectionModelBase
    {
        constructor() {
            super();

            Rebind(this);
            var self = this;

            // The user should only be able to select a single catalog
            // at any given time.
            self.SingleSelect(true);
        }

        private service = new InTheBoks.ServiceClient("Catalogs");
        private storage = new InTheBoks.Storage("Catalogs");

        // Loads the users catalogs from disk or api.
        Load() {
            var self = this;

            if (self.storage.Supported)
            {
                console.log("Reading from local storage...");


                //self.storage.Open();
            }

            self.service.Execute(function (data) {

                //self.Items.removeAll();
                ko.mapping.fromJS(data, {}, self.Items);

                // Select the first item.
                if (self.Items().length > 0)
                {
                    self.Select(self.Items()[0]);
                }

                var itemsObject = ko.toJS(self.Items());
                var itemsJson = JSON.stringify(itemsObject);
                var parsedObject = JSON.parse(itemsJson);

                //console.log(itemsObject);
                //console.log(itemsJson);
                //console.log(parsedObject);

                localStorage.setItem("Catalogs", itemsJson);

                // ITEMS: CATALOG ID|ITEM ID|TIMESTAMP
                // CATALOGS: Stored as array

                localStorage.setItem("1|3|timestamp", "ThisIsAnItem");
                localStorage.setItem("1|4|timestamp", "ThisIsAnItem #2");
                localStorage.setItem("2|3|timestamp", "ThisIsAnItem #2");

                for (var i in window.localStorage) {
                    var val = localStorage.getItem(i);
                    console.log(i);
                    console.log(val);
                    //var value = val.split(","); //splitting string inside array to get name
                    //name[i] = value[1]; // getting name from split string
                }

                //for (var i = 0; i < data.length; i++) {

                //    var item = ko.mapping.fromJS(data[i]);

                //    self.Items.push(item);

                //    if (i == 0) {
                //        self.Selected(item);
                //    }

                //};
            
            });
        }
            
        Save()
        { }


    }

    export class ModelBase
    {
        Id: KnockoutObservableNumber;
        Name: KnockoutObservableString;
        Created: KnockoutObservableDate;
        Modified: KnockoutObservableDate;

        constructor()
        {
            var self = this;

            self.Id = ko.observable();
            self.Name = ko.observable();
            self.Created = ko.observable();
            self.Modified = ko.observable();
        }

        update(instance: ModelBase){ }
    }

    export class Catalog extends ModelBase
    {
        defaults()
        {
            this.Name("New catalog");
        }
    }

    export class Item extends ModelBase
    {

    }

    export class User extends ModelBase
    {

    }

    export class Activity
    {
        constructor(name: KnockoutObservableString, id: KnockoutObservableNumber)
        {
            var self = this;

            self.Name = name;
            self.Id = id;
        }

        Name: KnockoutObservableString;
        Id: KnockoutObservableNumber;
    }

    export class Friend
    {
        constructor(public id: JQueryXHR)
        { }
    }

    /*
    export interface IItem
    {
        id: number;
        created: Date;
        modified: Date;

        load(): any;
        update(any): any;
    }*/

    /*
    export class Catalog implements ICatalog
    {
        constructor(public id: number)
        { }

        load() { return null; }

        static instance = new Catalog(123);
    }*/
}

function Rebind(obj: any) {
    var prototype = <Object>obj.constructor.prototype;
    for (var name in prototype) {
        if (!obj.hasOwnProperty(name)
                && typeof prototype[name] === "function") {
            var method = <Function>prototype[name];
            obj[name] = method.bind(obj);
        }
    }
}