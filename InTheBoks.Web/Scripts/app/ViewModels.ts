/// <reference path="_references.ts" />

module InTheBoks.ViewModels
{
    export class MainViewModel
    {
        Catalogs: CatalogsViewModel;
        Items: ItemsViewModel;
        Friends: FriendsViewModel;

        //Friends: KnockoutObservableArray;
        SelectedItems: KnockoutObservableArray;
        SelectedCatalog: KnockoutObservableAny;
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
            var self = this;

            self.Catalogs = new CatalogsViewModel();
            self.Items = new ItemsViewModel();
            self.Friends = new FriendsViewModel();
            self.Activities = new ActivitiesViewModel();

            self.SelectedItems = ko.observableArray();
            self.User = ko.observable();

            self.States = { "Initializing": 0, "Authenticated": 1, "Anonymous": 2 };
            //self.State(self.States()[0]);

            self.Languages = ko.observableArray([
                 { key: "auto", value: "Auto-detect" },
                 { key: "en-US", value: "English" },
                 { key: "nb-NO", value: "Norwegian" }
            ]);

            self.SelectedCatalog = ko.observable();
            //self.SearchForItem = ko.observable(false);
            
            //self.SaveStatusText = ko.observable();

            //self.SelectedObject = ko.observable();

            //self.SelectedAction = ko.observable("Edit Catalog");


            self.Auth = new InTheBoks.Facebook();

            self.Login = () => { self.Auth.Login(); };
            self.Logout = () => { self.Auth.Logout(); };
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
        Selected: KnockoutObservableAny;

        constructor()
        {
            var self = this;

            self.Items = ko.observableArray();
            self.Selected = ko.observable();
        }

        Create(item)
        { }

        Delete(item)
        { }

        Edit(item)
        { }

        Select(item)
        {
            console.log(item);
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
            var self = this;
        }

        private service = new InTheBoks.ServiceClient("Catalogs");

        // Loads the users catalogs from disk or api.
        Load() {
            var self = this;

            self.service.Execute(function (data) {

                //self.Items.removeAll();
                ko.mapping.fromJS(data, {}, self.Items);

                self.Selected(self.Items()[0]);

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