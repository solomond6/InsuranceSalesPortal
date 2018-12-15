using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StanbicIBTC.InsuranceSales.Models;
using StanbicIBTC.InsuranceSales.Utility;
using StanbicIBTC.InsuranceSales.Utitlity;

namespace StanbicIBTC.InsuranceSales.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            LogWorker logworker = new LogWorker("UsersController", "Index", "Ok");
            return View();
        }


        public ActionResult Login()
        {
            LogWorker logworker = new LogWorker("UsersController", "Logins", "Ok");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Users users, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //EncryptionSuite encryptor = new EncryptionSuite();
                //model.Password = encryptor.Encrypt(model.Password);
                users.EmailAddress = "temidayo@yahoo.com";
                //var userDetails = db.Users.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();
                if (users != null)
                {
                    FormsAuthentication.SetAuthCookie(users.EmailAddress, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        LogWorker logworker = new LogWorker("UsersController", "Login", "LoginOk");
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        LogWorker logworker = new LogWorker("UsersController", "Login", "LoginOk");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    LogWorker logworker = new LogWorker("UsersController", "Index", "LoginOk");
                    ModelState.AddModelError("", "Invalid Username/Password");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Signup()
        {
            LogWorker logworker = new LogWorker("UsersController", "Signup", "Ok");
            return View();
        }

        public JsonResult PostSignUpDetails(Users users, string returnUrl)
        {
            LogWorker logworker = new LogWorker("UsersController", "PostSignUpDetails", "Ok");
            var SignUpData = App.SignupUsers(users).ToString();
            return Json(new { result = SignUpData }, JsonRequestBehavior.AllowGet);
            
            
        }

        public JsonResult PostUserLogin(Users users)
        {
            LogWorker logworker = new LogWorker("HomeController", "PostUserLogin", "Ok");
            var LoginData = App.LoginUsers(users).ToString();

            FormsAuthentication.SetAuthCookie(users.EmailAddress, false);
            
            return Json(new { result = LoginData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyProfile()
        {
            LogWorker logworker = new LogWorker("UsersController", "MyProfile", "Ok");
            return View();
        }

        public ActionResult Claims()
        {
            LogWorker logworker = new LogWorker("UsersController", "Claims", "Ok");
            return View();
        }

        public ActionResult CreateClaim()
        {
            LogWorker logworker = new LogWorker("UsersController", "CreateClaim", "Ok");
            return View();
        }

        public ActionResult Signout()
        {
            LogWorker logworker = new LogWorker("UsersController", "Signout", "Ok");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        } 
    }
}