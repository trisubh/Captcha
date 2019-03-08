using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Captacha
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        void Session_Start(object sender, EventArgs e)
        {
            var config = ConfigurationManager.GetSection("CaptchaSection");
            if(config != null)
            {
                var _captchas = (config as CaptchaSection).Captchas;          
                for (int i = 0; i < _captchas.Count; i++)
                {
                    CaptcahaMode.mode = _captchas[i].mode;
                    CaptcahaMode.isstrict = _captchas[i].strict;
                }
            };
            
            
        }
    }
}