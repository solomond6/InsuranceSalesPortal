using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StanbicIBTC.InsuranceSales.Utitlity;
using StanbicIBTC.InsuranceSales.Models;
using StanbicIBTC.InsuranceSales.Utility;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;

namespace StanbicIBTC.InsuranceSales.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LogWorker logworker = new LogWorker("HomeController", "Index", "Ok");
            return View();
        }

        public JsonResult GetCompany()
        {
            LogWorker logworker = new LogWorker("HomeController", "GetCompany", "Ok");
            var CompanyData = App.Company().ToList();
            return Json(new { result = CompanyData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInsuranceType()
        {
            LogWorker logworker = new LogWorker("HomeController", "GetInsuranceType", "Ok");
            var InsuranceTypeData = App.InsuranceType().ToList();
            return Json(new { result = InsuranceTypeData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInsuranceTypeComponent()
        {
            //LogWorker logworker = new LogWorker("HomeController", "GetInsuranceTypeComponent", "Ok");
            //var InsuranceTypeComponentData = App.InsuranceTypeComponent().ToList();
            //return Json(new { result = InsuranceTypeComponentData }, JsonRequestBehavior.AllowGet);

            IEnumerable<InsuranceTypeComponent> InsuranceTypeComponent = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"]);
                    var responseTask = client.GetAsync("Insurance-Sales");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<InsuranceTypeComponent>>();
                        readTask.Wait();
                        InsuranceTypeComponent = readTask.Result;

                        string json = JsonConvert.SerializeObject(InsuranceTypeComponent);

                        List<InsuranceTypeComponent> ResponseArray = JsonConvert.DeserializeObject<List<InsuranceTypeComponent>>(json);

                        var Response = ResponseArray
                                        .Cast<InsuranceTypeComponent>()
                                        .GroupBy(tm => tm.Company.Name)
                                        .Select(group => new { name = group.Key, Items = group.ToArray() }).ToArray();

                        LogWorker logworker = new LogWorker("App", "InsuranceTypeComponent", "Result Found");

                        return Json(new { result = Response }, JsonRequestBehavior.AllowGet);
                    }
                    else //web api sent error response 
                    {
                        LogWorker logworker = new LogWorker("App", "InsuranceTypeComponent", "No Result Found");
                        InsuranceTypeComponent = Enumerable.Empty<InsuranceTypeComponent>();

                        return Json(new { result = InsuranceTypeComponent }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                LogWorker logworker = new LogWorker("App", "InsuranceTypeComponent", ex.ToString());
                throw ex;
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}