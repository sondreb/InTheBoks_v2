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

        public static string AWSAccessKeyId { get { return GetValue("AWSAccessKeyId"); } }
        public static string AWSSecretAccessKey { get { return GetValue("AWSSecretAccessKey"); } }
        public static string AWSAID { get { return GetValue("AWSAID"); } }

        private static string GetValue(string key)
        {
            foreach (var line in _configuration)
            {
                var values = line.Split(':');

                var configKey = values[0];
                var configValue = values[1];

                if (configKey == key)
                {
                    return configValue;
                }

            }

            return string.Empty;
        }
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
