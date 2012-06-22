namespace InTheBoks.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Xml.Linq;

    public class ItemService : IItemService
    {
        public ItemService()
        {
            ItemType = Services.ItemType.DVD;
            SearchType = Services.SearchType.Keywords;
        }

        public string Keywords { get; set; }

        private string CreateSignedUrl()
        {
            SignedRequestHelper helper = new SignedRequestHelper(Configuration.AWSAccessKeyId, Configuration.AWSSecretAccessKey, Constants.AWSUrl);

            QueryString query = new QueryString();
            query.Add("Service", "AWSECommerceService");
            query.Add("AssociateTag", Configuration.AWSAID);
            query.Add("Version", "2011-08-01");
            query.Add("ResponseGroup", "Images,Small");
            query.Add("Operation", "ItemSearch");
            query.Add("SearchIndex", ItemType.ToString());
            query.Add(SearchType.ToString(), Uri.EscapeDataString(Keywords));

            var signedUrl = helper.Sign(query);
            return signedUrl;
        }

        public XDocument ReturnAsXml()
        {
            return XDocument.Parse(ReturnAsString());
        }

        //public int QueryTotalPages { get; set; }
        //public int TotalPages { get; set; }
        //public int CurrentPage { get; set; }
        //public int MaxTotalPages { get; set; }

        public string ReturnAsString()
        {
            WebClient http = new WebClient();
            var xml = http.DownloadString(CreateSignedUrl());
            return xml;
        }

        public ItemType ItemType { get; set; }

        public SearchType SearchType { get; set; }
    }
}
