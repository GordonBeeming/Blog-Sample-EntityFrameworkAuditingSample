using System.Linq;
using System.Net.Http;
using System.Web;
using EntityFrameworkAuditingSample.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace EntityFrameworkAuditingSample
{
    public static class OwinContextHelper
    {
        private static HttpRequestMessage HttpRequestMessage
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
                }
                return null;
            }
        }
        private static HttpRequestBase HttpRequestBase
        {
            get
            {
                try
                {
                    if (HttpContext.Current != null)
                    {
                        return new HttpRequestWrapper(HttpContext.Current.Request);
                    }
                }
                catch { }
                return null;
            }
        }

        public static IOwinContext OwinContext
        {
            get
            {
                // un comment this if Web API is supported
                //if (HttpRequestMessage != null)
                //{
                //    return OwinHttpRequestMessageExtensions.GetOwinContext(HttpRequestMessage);
                //}
                if (HttpRequestBase != null)
                {
                    return HttpContextBaseExtensions.GetOwinContext(HttpRequestBase);
                }
                //throw new NotSupportedException("Getting an Owin Context from the current context is not supported");
                return null;
            }
        }

        public static ApplicationSignInManager ApplicationSignInManager
        {
            get
            {
                return OwinContext?.Get<ApplicationSignInManager>();
            }
        }

        public static ApplicationUserManager ApplicationUserManager
        {
            get
            {
                return OwinContext?.Get<ApplicationUserManager>();
            }
        }

        public static ApplicationUser CurrentApplicationUser
        {
            get
            {
                if (OwinContext != null)
                {
                    var currentUsername = OwinContext.Authentication.User.Identity.Name;
                    using (var context = new ApplicationDbContext())
                    {
                        return context.Users.FirstOrDefault(o => o.UserName == currentUsername);
                    }
                }
                return null;
            }
        }

    }
}