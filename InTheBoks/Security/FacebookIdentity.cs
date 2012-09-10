using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Facebook;

namespace InTheBoks.Security
{
    public class FacebookIdentity : IIdentity
    {
        public FacebookIdentity(long id, long facebookId, string name, string email, string link, string token, string language, string timeZone)
        {
            Id = id;
            FacebookId = facebookId;
            Name = name;
            Email = email;
            Link = link;
            Token = token;
            Language = language;
        }

        public long Id { get; private set; }

        public long FacebookId { get; private set; }

        public string Email { get; private set; }

        public string Token { get; private set; }

        public string Link { get; private set; }

        public FacebookClient CreateClient()
        {
            var fb = new FacebookClient(Token);
            return fb;
        }

        public string AuthenticationType
        {
            get { return "Facebook"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name { get; private set; }

        public string TimeZone { get; private set; }

        public TimeZoneInfo TimeZoneInfo
        {
            get
            {
                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
                }
                catch
                {
                    return TimeZoneInfo.FindSystemTimeZoneById("UTC");
                }
            }
        }

        public string Language { get; private set; }
    }
}
