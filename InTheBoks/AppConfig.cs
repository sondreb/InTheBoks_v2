namespace InTheBoks
{
    using System.Configuration;
    using System.IO;

    public static class AppConfig
    {
        private static string[] _configuration;

        private static void InitializeConfigurationFile()
        {
            if (_configuration == null)
            {
                _configuration = File.ReadAllLines("C:\\InTheBoks\\InTheBoks.ini");
            }
        }

        private static string GetValue(string name)
        {
            foreach (var line in _configuration)
            {
                var values = line.Split(':');

                var configKey = values[0];
                var configValue = values[1];

                if (configKey == name)
                {
                    return configValue;
                }
            }

            return string.Empty;
        }

        public static string Get(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (string.IsNullOrWhiteSpace(value))
            {
                InitializeConfigurationFile();
                value = GetValue(name);
            }

            return value;
        }
    }
}
