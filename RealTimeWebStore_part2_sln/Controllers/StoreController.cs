using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherRESTDotNet;
using System.Configuration;
using RealTimeWebStore.Code;
using System.Net;

namespace RealTimeWebStore.Controllers
{
    public class StoreController : Controller
    {
        private IPusherProvider _provider;

        public StoreController()
        {
            string applicationKey = ConfigurationManager.AppSettings["application_key"];
            string applicaitonSecret = ConfigurationManager.AppSettings["application_secret"];
            string applicationId = ConfigurationManager.AppSettings["application_id"];
            _provider = new PusherProvider(applicationId, applicationKey, applicaitonSecret);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = MvcApplication.ProductRepository.GetProductById(MvcApplication.BLUE_TSHIRT_ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string productId, string socketId)
        {
            ActionResult result = null;

            bool bought = MvcApplication.ProductRepository.Buy(productId);
            var model = MvcApplication.ProductRepository.GetProductById(productId);

            if (bought)
            {
                ViewBag.Info = model.Title + " successfully bought";

                var stockEvent = new StockUpdatedEvent(model, socketId);
                ObjectPusherRequest request = new ObjectPusherRequest("presence-" + stockEvent.ProductId, "stockUpdated", stockEvent);
                _provider.Trigger(request);
            }
            else
            {
                ViewBag.Error = "There was a problem buying " + model.Title;
            }

            if (socketId != null)
            {
                result = GetBoughtStatusCode(bought);
            }
            else
            {
                result = View("Index", model);
            }
            return result;
        }

        private ActionResult GetBoughtStatusCode(bool bought)
        {
            ActionResult result = null;
            if (bought)
            {
                result = new HttpStatusCodeResult((int)HttpStatusCode.OK);
            }
            else
            {
                result = new HttpStatusCodeResult((int)HttpStatusCode.NotFound);
            }
            return result;
        }
    }
}
