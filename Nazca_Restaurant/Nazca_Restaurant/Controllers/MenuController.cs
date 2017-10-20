using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;

namespace Nazca_Restaurant.Controllers
{
    public class MenuController : Controller
    {

        NazResEntities2 _databaseManager = new NazResEntities2();

        // GET: Menu
        public ActionResult VerMenu()
        {
            return View();
        }

        public ActionResult BuscarPlato()
        {

            List<sp_listarTiposDeProductos_Result> listaBase = new List<sp_listarTiposDeProductos_Result>();
            listaBase = this._databaseManager.sp_listarTiposDeProductos().ToList();

            return View(listaBase);
        }

        public JsonResult Productos_Bind(String codTipo)
        {
            List<sp_productosPorTipo_Result> listaBase = new List<sp_productosPorTipo_Result>();
            listaBase = this._databaseManager.sp_productosPorTipo(codTipo).ToList();
            return Json(listaBase, JsonRequestBehavior.AllowGet);
        }
    }
}