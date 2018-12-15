using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using StanbicIBTC.InsuranceSales.Models;
using System.Configuration;
using Newtonsoft.Json;
using StanbicIBTC.InsuranceSales.Utility;

namespace StanbicIBTC.InsuranceSales.Utitlity
{
    public class App
    {

        public static List<Company> Company()
        {
            IEnumerable<Company> Company = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"]);
                    var responseTask = client.GetAsync("Company");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Company>>();
                        readTask.Wait();
                        Company = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        Company = Enumerable.Empty<Company>();
                    }
                }
                LogWorker logworker = new LogWorker("App", "Company", "Ok");
                return Company.ToList();
            }
            catch (Exception ex)
            {
                LogWorker logworker = new LogWorker("App", "Company", ex.ToString());
                throw ex;
            }
        }

        public static List<InsuranceType> InsuranceType()
        {
            IEnumerable<InsuranceType> insuranceType = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"]);
                    var responseTask = client.GetAsync("Insurance-Type");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<InsuranceType>>();
                        readTask.Wait();
                        insuranceType = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        insuranceType = Enumerable.Empty<InsuranceType>();
                    }
                }
                LogWorker logworker = new LogWorker("App", "InsuranceType", "Ok");
                return insuranceType.ToList();
            }
            catch (Exception ex)
            {
                LogWorker logworker = new LogWorker("App", "InsuranceType", ex.ToString());
                throw ex;
            }

        }

        public static List<InsuranceTypeComponent> InsuranceTypeComponent()
        {
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
                    }
                    else //web api sent error response 
                    {
                        InsuranceTypeComponent = Enumerable.Empty<InsuranceTypeComponent>();
                    }
                }
                LogWorker logworker = new LogWorker("App", "InsuranceTypeComponent", "Ok");
                return InsuranceTypeComponent.ToList();
            }
            catch (Exception ex)
            {
                LogWorker logworker = new LogWorker("App", "InsuranceTypeComponent", ex.ToString());
                throw ex;
            }

        }

        public static string SignupUsers(Users users)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["SignUpUrl"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Users>("users", users);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return result.ToString();
                    }
                }

                return string.Empty + "Server Error. Error creating user.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static string LoginUsers(Users users)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["LoginUrl"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Users>("users", users);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return result.ToString();
                    }
                }

                return string.Empty + "Server Error. Email Address/Passowrd Incorrect.";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}