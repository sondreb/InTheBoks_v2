using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using InTheBoks.Models;
using InTheBoks.Services;

namespace InTheBoks.Web.Api
{
    public class SearchController : ApiController
    {
        //[Authorize]
        public IEnumerable<Item> Get(string id)
        {
            List<Item> items = new List<Item>();

            ItemService service = new ItemService();
            service.ItemType = ItemType.DVD;
            service.SearchType = SearchType.Keywords;
            service.Keywords = id;

            XDocument xml = null;

            try
            {
                xml = service.ReturnAsXml();
            }
            catch (System.Net.WebException wex)
            {
                if (wex.Message.Contains("403"))
                {
                    throw new ServiceUnavailableExceptions("Unable to connect with Amazon Service. Please verify your configuration settings.", wex);
                }

                throw;
            }

            var ns = "{http://webservices.amazon.com/AWSECommerceService/2011-08-01}";

            foreach (XElement item in xml.Descendants(ns + "Item"))
            {
                var asin = item.Element(ns + "ASIN").Value;
                var url = item.Element(ns + "DetailPageURL").Value;

                var attributesNode = item.Element(ns + "ItemAttributes");
                var title = attributesNode.Element(ns + "Title").Value;

                var largeImageNode = item.Element(ns + "LargeImage");
                string largeImageUrl;

                if (largeImageNode != null)
                {
                    largeImageUrl = largeImageNode.Element(ns + "URL").Value;
                }
                else
                {
                    largeImageUrl = "/Content/albums/missing.jpg";
                }
                

                items.Add(new Item{ ASIN = asin, Url = url, ImageUrl = largeImageUrl, Title = title });
            }

            return items;
        }
    }
}
