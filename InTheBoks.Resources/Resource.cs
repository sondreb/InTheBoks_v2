using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace InTheBoks.Resources
{
    public static class Resource
    {
        public static string FacebookCulture()
        {
            

            if (CultureInfo.CurrentUICulture.ThreeLetterWindowsLanguageName == "NOR")
            {
                return "nb_NO";   
            }
            else
            {
                return "en_US";
            }
        }

        public static string UserCulture()
        {
            return CultureInfo.CurrentUICulture.TextInfo.CultureName; // (eg: "nb-NO")
        }

        public static string UserDatePattern()
        {
            return CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
        }

        public static dynamic RenderJson()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("{");

            var resources = Text.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            foreach (DictionaryEntry entry in resources)
            {
                object resourceKey = entry.Key;
                string resource = entry.Value.ToString().Replace("\"", "\\\"");

                str.AppendFormat("\"{0}\": \"{1}\",\r\n", resourceKey, resource);
            }

            // Remove the last ","
            str.Remove(str.Length - 3, 3);

            str.AppendLine("");
            str.AppendLine("}");

            return str.ToString();
        }
    }
}
