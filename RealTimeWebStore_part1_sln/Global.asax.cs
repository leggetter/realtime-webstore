using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RealTimeWebStore.Code;
using RealTimeWebStore.Models;

namespace RealTimeWebStore
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static ProductRepository _productRepo = new ProductRepository();

        public const string BLUE_TSHIRT_ID = "PusherTShirt_Blue";

        public static ProductRepository ProductRepository
        {
            get
            {
                return _productRepo;
            }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Store", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var blueTShirt = new ProductModel()
            {
                ProductId = BLUE_TSHIRT_ID,
                Title = "The awesomely fantastic Pusher t-shirt. In Blue!",
                Description = "Super-cool Pusher t-shirt. Increases speed, durability, well-being and developer skills.",
                StockLevel = 10,
                Images = new ProductImage[] {
                    new ProductImage()
                    {
                        ImageId = "Front",
                        ImageUrl = "../../Content/pusher_tee.jpg",
                        ImageAltText = "Front of Pusher blue t-shirt"
                    },
                    new ProductImage()
                    {
                        ImageId = "Back",
                        ImageUrl = "../../Content/pusher_tee_back_small.jpg",
                        ImageAltText = "Back of Pusher blue t-shirt"
                    }
                }
            };
            ProductRepository.AddProduct(blueTShirt);
        }
    }
}