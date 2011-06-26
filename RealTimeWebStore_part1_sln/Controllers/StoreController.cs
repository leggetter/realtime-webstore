using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherRESTDotNet;
using System.Configuration;

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
        public ActionResult Index(string id)
        {
            bool bought = MvcApplication.ProductRepository.Buy(MvcApplication.BLUE_TSHIRT_ID);
            var model = MvcApplication.ProductRepository.GetProductById(MvcApplication.BLUE_TSHIRT_ID);

            if (bought)
            {
                ViewBag.Info = model.Title + " successfully bought";

                ObjectPusherRequest request = new ObjectPusherRequest("product-" + model.ProductId, "stockUpdated", model);
                _provider.Trigger(request);
            }
            else
            {
                ViewBag.Error = "There was a problem buying " + model.Title;
            }
            
            return View("Index", model);
        }
    }
}
