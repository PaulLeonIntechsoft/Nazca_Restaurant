using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;
using Nazca_Restaurant.Models.Internal;
using Nazca_Restaurant.Models.Internal.Entidades;

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
            StockPlato plato = new StockPlato();
            try
            {
                var base64 = "";
                List<sp_detallesPlato_Result> listaBase = new List<sp_detallesPlato_Result>();
                listaBase = this._databaseManager.sp_detallesPlato(codProd).ToList();

                sp_detallesPlato_Result platoUnico = new sp_detallesPlato_Result();
                platoUnico = listaBase.First();

                if (platoUnico.imgProducto != null && platoUnico.imgProducto.Length > 0)
                {
                    base64 = Convert.ToBase64String(platoUnico.imgProducto);
                }else
                {
                    base64 = "";
                }

                plato.chrCodPro = platoUnico.chrCodPro;
                plato.chrDesPro = platoUnico.chrDesPro;
                plato.chrComentario = platoUnico.chrComentario;
                plato.cantVendido = Convert.ToInt32(platoUnico.cantVendido);
                plato.imgProducto = base64;

                return Json(plato, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw ex;
            }
        }
    }
}