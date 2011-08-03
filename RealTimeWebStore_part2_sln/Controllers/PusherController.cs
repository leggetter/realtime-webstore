using System;
using System.Configuration;
using System.Web.Mvc;
using PusherRESTDotNet;
using PusherRESTDotNet.Authentication;

namespace RealTimeWebStore.Controllers
{
    public class PusherController : Controller
    {
        string applicationId;
        string applicationKey;
        string applicationSecret;

        public PusherController()
        {
            applicationId = ConfigurationManager.AppSettings["application_id"];
            applicationKey = ConfigurationManager.AppSettings["application_key"];
            applicationSecret = ConfigurationManager.AppSettings["application_secret"];
        }

        public ActionResult Auth(string channel_name, string socket_id)
        {
            var channelData = new PresenceChannelData();
            if (User.Identity.IsAuthenticated)
            {
                channelData.user_id = User.Identity.Name;
            }
            else
            {
                channelData.user_id = GetUniqueUserId();
            }
            channelData.user_info = GetUserInfo();

            var provider = new PusherProvider(applicationId, applicationKey, applicationSecret);
            string authJson = provider.Authenticate(channel_name, socket_id, channelData);

            return new ContentResult { Content = authJson, ContentType = "application/json" };
        }

        private string GetUniqueUserId()
        {
            string sessionUserId = null;
            if (Session["SessionUserId"] != null)
            {
                sessionUserId = (string)Session["SessionUserId"];
            }
            else
            {
                if (HttpContext.Application["AppUserCount"] == null)
                {
                    HttpContext.Application["AppUserCount"] = 0;
                }
                int newUserCount = ((int)HttpContext.Application["AppUserCount"] + 1);
                HttpContext.Application["AppUserCount"] = newUserCount;
                sessionUserId = "Guest " + newUserCount;
                Session["SessionUserId"] = sessionUserId;
            }
            return sessionUserId;
        }

        private object GetUserInfo()
        {
            if (Session["SessionUserInfo"] == null)
            {
                DateTime objUTC = DateTime.Now.ToUniversalTime();
                long epoch = (objUTC.Ticks - 621355968000000000) / 10000;
                Session["SessionUserInfo"] = new { timestamp = epoch };
            }
            return Session["SessionUserInfo"];
        }
    }
}
