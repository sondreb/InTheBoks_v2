namespace InTheBoks.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class Configuration
    {
        private static readonly string[] _configuration;

        static Configuration()
        {
            _configuration = File.ReadAllLines("C:\\InTheBoks\\InTheBoks.ini");
        }

        public static string AWSAccessKeyId { get { return AppConfig.Get("AWSAccessKeyId"); } }
        public static string AWSSecretAccessKey { get { return AppConfig.Get("AWSSecretAccessKey"); } }
        public static string AWSAID { get { return AppConfig.Get("AWSAID"); } }
    }

    public static class Constants
    {

 
        public static string AWSUrl = "webservices.amazon.com";
    }

    public enum ItemType
    {
        DVD,
        Books,
        Music,
        VideoGames
    }

    public enum SearchType
    {
        Title,
        Keywords,
        ASIN,
        ISBN
    }
}
