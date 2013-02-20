/// <reference path="_references.ts" />

module InTheBoks
{
    export class ServiceClient {

        Action: string;

        constructor(action:string = "")
        {
            this.Action = action;
        }

        Execute(callback:any, type:string = "GET", action:string = "") {

            var a = (action != "") ? action: this.Action;

            $.ajax({
                url: "/Api/" + a,
                type: type,
                dataType: "json"
            }).done(callback);
        }
    }
}

var listViewType = 1; // 1 = Thumbnail, 2 = Medium, 3 = List
var thumbnailSize = 128;
