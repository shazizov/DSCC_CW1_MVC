using EsavdoDAL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Esavdo.Models;
using EsavdoDAL.Repository;

namespace Esavdo.Controllers
{
    public class HomeController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetLogger("WebSite");

        private UserRepository _userRepo = new UserRepository();    

        public ActionResult Index(string LanguageAbbrevation)
        {
            if (!String.IsNullOrEmpty(LanguageAbbrevation))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = LanguageAbbrevation;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            ViewBag.LoggedOut = "Yes";
            return View(new RegistrationModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegistrationModel model)
        {
            ViewBag.LoggedOut = "Yes";
            if (model.Password != model.ConfirmPassword)
                ModelState.AddModelError("ConfirmPassword", "Does not match with the password");

            if (ModelState.IsValid && ValidateCaptcha())
            {
                if (_userRepo.GetAll().Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "User with such Username already registered");
                }
                else
                {
                    var user = new User
                    {
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        Email = model.Email,
                        Username = model.Username,
                        Password = model.Password
                    };

                    _userRepo.Create(user);
                    logger.Info(model.Username + " is Registered");

                    return RedirectToAction("Login");
                }

            }

            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.LoggedOut = "Yes";
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            ViewBag.LoggedOut = "Yes";
            if (ModelState.IsValid)
            {
                if (!_userRepo.GetAll().Any(u => u.Username == model.Username && u.Password == model.Password))
                {
                    ModelState.AddModelError("Email", "Wrong credentials");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    logger.Info(model.Username + " logged in");
                    return RedirectToAction("Index");
                }

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            logger.Info( "User logged Out");
            return RedirectToAction("Login");
        }



        private class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }

        private bool ValidateCaptcha()
        {
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret=6LcP7tgUAAAAAIJoFNSwhGlCsxSazCPUXUtFqKzw&response={0}", Request["g-recaptcha-response"]));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return false;
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ModelState.AddModelError("", "The secret parameter is missing.");
                        break;
                    case ("invalid-input-secret"):
                        ModelState.AddModelError("", "The secret parameter is invalid or malformed.");
                        break;
                    case ("missing-input-response"):
                        ModelState.AddModelError("", "The response parameter is missing. Please, preceed with reCAPTCHA.");
                        break;
                    case ("invalid-input-response"):
                        ModelState.AddModelError("", "The response parameter is invalid or malformed.");
                        break;
                    default:
                        ModelState.AddModelError("", "Error occured. Please try again");
                        break;
                }
                return false;
            }

            return true;
        }
    }
}