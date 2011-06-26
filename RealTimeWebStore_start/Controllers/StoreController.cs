using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace RealTimeWebStore.Controllers
{
    public class StoreController : Controller
    {
        public StoreController()
        {
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
            }
            else
            {
                ViewBag.Error = "There was a problem buying " + model.Title;
            }
            
            return View("Index", model);
        }
    }
}