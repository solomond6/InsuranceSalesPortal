using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StanbicIBTC.InsuranceSales.Utility;

namespace StanbicIBTC.InsuranceSales.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Component
        public ActionResult Index()
        {
            LogWorker logworker = new LogWorker("productsController", "Index", "Ok");
            return View();
        }
    }
}