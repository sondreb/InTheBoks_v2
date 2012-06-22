using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace InTheBoks
{
    internal class ParamComparer : IComparer<string>
    {
        public int Compare(string p1, string p2)
        {
            return string.CompareOrdinal(p1, p2);
        }
    }

    public class QueryString : Dictionary<string, string>
    {
        public QueryString()
        {
     
        }

        public override string ToString()
        {
            List<string> returnParams = new List<string>();

            foreach (var param in this)
            {
                returnParams.Add(String.Format("{0}={1}", param.Key, param.Value));
            }

            return "?" + String.Join("&", returnParams.ToArray());
        }

        //public void Add(string key, string value)
        //{
        //    this.Add(

        //    _params.Add(key, HttpUtility.UrlEncode(value));
        //}

        public void Signature()
        {
            //_params = _params.OrderByKey();

            StringBuilder str = new StringBuilder();
            str.Append("GET\n");
            str.Append("webservices.amazon.com\n");
            str.Append("/onca/xml\n");
            str.Append(this.ToString());
        }

        private string PercentEncodeRfc3986(string str)
        {
            str = HttpUtility.UrlEncode(str, System.Text.Encoding.UTF8);
            str = str.Replace("'", "%27").Replace("(", "%28").Replace(")", "%29").Replace("*", "%2A").Replace("!", "%21").Replace("%7e", "~").Replace("+", "%20");

            StringBuilder sbuilder = new StringBuilder(str);
            for (int i = 0; i < sbuilder.Length; i++)
            {
                if (sbuilder[i] == '%')
                {
                    if (Char.IsLetter(sbuilder[i + 1]) || Char.IsLetter(sbuilder[i + 2]))
                    {
                        sbuilder[i + 1] = Char.ToUpper(sbuilder[i + 1]);
                        sbuilder[i + 2] = Char.ToUpper(sbuilder[i + 2]);
                    }
                }
            }
            return sbuilder.ToString();
        }
    }
}
