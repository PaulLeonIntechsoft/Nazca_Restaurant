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
using System.Collections.Generic;

namespace Nazca_Restaurant.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {

        private NazResEntities2 _databaseManager = new NazResEntities2();

        public AccountController()
        {
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(sp_login_Result model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginInfo = this._databaseManager.sp_login(model.chrCodUsr, model.chrNomUsr).ToList();
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        var logindetails = loginInfo.First();
                        this.SignInUser(logindetails.chrNomUsr, logindetails.chrCodUsr, logindetails.chrAcceso, false);
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Contraseña o usuario invalid@.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.View(model);
        }
        
        public ActionResult LogOff()
        {
            try
            {
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.RedirectToAction("Login", "Account");
        }


        private void SignInUser(string username, string identificador, string permisos, bool isPersistent)
        {
            var claims = new List<Claim>();
            try
            {
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, identificador));
                claims.Add(new Claim(ClaimTypes.Surname, permisos));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.RedirectToAction("Index", "Home");
        }

    }
}