namespace InTheBoks.Services
{
    using System.IO;

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

    public static class Configuration
    {
        private static readonly string[] _configuration;

        static Configuration()
        {
            _configuration = File.ReadAllLines("C:\\InTheBoks\\InTheBoks.ini");
        }

        public static string AWSAccessKeyId { get { return AppConfig.Get("AWSAccessKeyId"); } }

        public static string AWSAID { get { return AppConfig.Get("AWSAID"); } }

        public static string AWSSecretAccessKey { get { return AppConfig.Get("AWSSecretAccessKey"); } }
    }

    public static class Constants
    {
        public static string AWSUrl = "webservices.amazon.com";
    }
}