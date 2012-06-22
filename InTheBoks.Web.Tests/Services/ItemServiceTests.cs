using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using InTheBoks.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InTheBoks.Web.Tests.Services
{
    [TestClass]
    public class ItemServiceTests
    {
        [TestMethod]
        public void CreateAndCallAmazon()
        {
            SignedRequestHelper helper = new SignedRequestHelper(Configuration.AWSAccessKeyId, Configuration.AWSSecretAccessKey, Constants.AWSUrl);

            QueryString query = new QueryString();
            query.Add("Service", "AWSECommerceService");
            query.Add("AssociateTag", Configuration.AWSAID);
            query.Add("Version", "2011-08-01");
            query.Add("ResponseGroup", "Images,Small");
            query.Add("Operation", "ItemSearch");
            query.Add("SearchIndex", InTheBoks.Services.ItemType.DVD.ToString());
            query.Add(InTheBoks.Services.SearchType.Keywords.ToString(), Uri.EscapeDataString("The Matrix"));

            var signedUrl = helper.Sign(query);

            WebClient client = new WebClient();
            var xmlText = client.DownloadString(signedUrl);

            XDocument xml = XDocument.Parse(xmlText);

            var ns = "{http://webservices.amazon.com/AWSECommerceService/2011-08-01}";

            foreach (XElement item in xml.Descendants(ns + "Item"))
            {
                var asin = item.Element(ns + "ASIN").Value;
                var url = item.Element(ns + "DetailPageURL").Value;

                var attributesNode = item.Element(ns + "ItemAttributes");
                var title = attributesNode.Element(ns + "Title").Value;

                var largeImageNode = item.Element(ns + "LargeImage");
                var largeImageUrl = largeImageNode.Element(ns + "URL").Value;
            }

            Assert.IsNotNull(xmlText);
        }
    }
}
