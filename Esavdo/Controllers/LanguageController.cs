using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Esavdo.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index(string LanguageAbbrevation)
        {
            if (!String.IsNullOrEmpty(LanguageAbbrevation))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);
            return View();
        }
    }
}