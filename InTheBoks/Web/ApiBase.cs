namespace InTheBoks.Web
{
    using InTheBoks.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Returns the current Facebook principal associated with this request.
        /// </summary>
        /// <returns>The current principal associated with this request.</returns>
        public new FacebookPrincipal User
        {
            get
            {
                return (FacebookPrincipal)base.User;
            }
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public string UserId
        {
            get { return User.Id.ToString(); }
        }
    }
}
