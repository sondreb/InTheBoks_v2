/// <reference path="_references.ts" />

module InTheBoks.ViewModels
{
    export class MainViewModel
    {
        Catalogs: KnockoutObservableArray;
        Friends: KnockoutObservableArray;
        SelectedItems: KnockoutObservableArray;
        SelectedCatalog: KnockoutObservableAny;
        Activities: KnockoutObservableArray;
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

            self.Catalogs = ko.observableArray();
            self.Friends = ko.observableArray();
            self.SelectedItems = ko.observableArray();
            self.Activities = ko.observableArray();
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

        LoadUser() {

            var service = new InTheBoks.ServiceClient("User");

            service.Execute(function (data) {
                console.log(data);
                var item = ko.mapping.fromJS(data);
                console.log(item);
                //self.User(item);
            });

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

        Load() {
            var self = this;

            self.service.Execute(function (data) {

                self.Items.removeAll();

                for (var i = 0; i < data.length; i++) {

                    var item = ko.mapping.fromJS(data[i]);

                    self.Items.push(item);

                    if (i == 0) {
                        self.Selected(item);
                    }

                };
            
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