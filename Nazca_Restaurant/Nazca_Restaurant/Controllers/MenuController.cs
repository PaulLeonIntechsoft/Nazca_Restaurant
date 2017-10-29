using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;
using Nazca_Restaurant.Models.Internal;

namespace Nazca_Restaurant.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {

        NazResEntities2 _databaseManager = new NazResEntities2();

        // GET: Menu
        public ActionResult VerMenu()
        {
            return View();
        }

        public JsonResult llenarTabla()
        {
            try
            {
                List<sp_listarVerMenu_Result> listaBase = new List<sp_listarVerMenu_Result>();
                listaBase = this._databaseManager.sp_listarVerMenu().ToList();

                return Json(listaBase, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult detallesPlato(string codProd)
        {
            try
            {
                List<sp_detallesPlato_Result> listaBase = new List<sp_detallesPlato_Result>();
                listaBase = this._databaseManager.sp_detallesPlato(codProd).ToList();

                sp_detallesPlato_Result platoUnico = new sp_detallesPlato_Result();
                platoUnico = listaBase.First();

                return Json(platoUnico, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}