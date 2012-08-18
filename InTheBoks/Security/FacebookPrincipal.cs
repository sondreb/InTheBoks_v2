using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace InTheBoks.Security
{
    public class FacebookPrincipal : IPrincipal
    {
        public FacebookPrincipal(long id, long facebookId, string name, string email, string link, string token)
        {
            Identity = new FacebookIdentity(id, facebookId, name, email, link, token);
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException("Roles are not implemented");
        }
    }
}
