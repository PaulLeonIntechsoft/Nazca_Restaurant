using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Nazca_Restaurant.Models;

namespace Nazca_Restaurant.Controllers
{

    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        
    }
}