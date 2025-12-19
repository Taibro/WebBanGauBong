using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin.Security.Providers.GitHub;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

[assembly: OwinStartup(typeof(WebBanGauBong.Startup))]
namespace WebBanGauBong
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Cấu hình Cookie chính cho ứng dụng 
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/User/LoginPage"),
                CookieSameSite = Microsoft.Owin.SameSiteMode.None, 
                CookieSecure = CookieSecureOption.Always
            });

            // Cấu hình Cookie tạm thời 
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Cấu hình Google Login
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "585058597994-ag4qgumm1ms17uljsb5u90k2eh8uveku.apps.googleusercontent.com",
                ClientSecret = "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",
            });

            // Cấu hình GitHub Login
            var githubOptions = new Owin.Security.Providers.GitHub.GitHubAuthenticationOptions
            {
                ClientId = "Ov23liLGLrsib2XXQXfq",
                ClientSecret = "44ced703d1cfe0015e1e85ccfec9a864cc608c2d",
                Provider = new GitHubAuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("GitHubAccessToken", context.AccessToken));
                    }
                }
            };
            //githubOptions.Scope.Add("user:email");
            app.UseGitHubAuthentication(githubOptions);
        }

    }
}