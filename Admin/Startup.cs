using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Admin
{
    public class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });
            app.UseExternalSignInCookie("ExternalCookie");
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}