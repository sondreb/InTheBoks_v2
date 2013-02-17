using System;
using System.Security.Principal;

namespace InTheBoks.Security
{
    public class FacebookPrincipal : IPrincipal
    {
        public FacebookPrincipal(long id, long facebookId, string name, string email, string link, string token, string language, string timeZone)
        {
            Identity = new FacebookIdentity(id, facebookId, name, email, link, token, language, timeZone);
        }

        public long Id { get { return FacebookIdentity.FacebookId; } }

        public FacebookIdentity FacebookIdentity
        {
            get { return (FacebookIdentity)Identity; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException("Roles are not implemented");
        }

        public IIdentity Identity { get; private set; }
    }
}