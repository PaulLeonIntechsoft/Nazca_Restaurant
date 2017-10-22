using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;
using Nazca_Restaurant.Controllers.Internal;

namespace Nazca_Restaurant.Controllers
{
    public class AtencionController : Controller
    {

        NazResEntities2 _databaseManager = new NazResEntities2();

        public ActionResult Index()
        {

            List<sp_listarMesas_Result> listaMesasBase = new List<Models.sp_listarMesas_Result>();
            listaMesasBase = this._databaseManager.sp_listarMesas().ToList();
            listarEmpleados();

            return View(listaMesasBase);
        }

        public JsonResult MesasBind()
        {
            try
            {
                List<Mesas> listaCompletaMesas = new List<Mesas>();

                List<sp_listarMesas_Result> listaMesasBase = new List<Models.sp_listarMesas_Result>();
                listaMesasBase = this._databaseManager.sp_listarMesas().ToList();

                foreach (var mesaUnica in listaMesasBase)
                {
                    Mesas nuevaMesa = new Mesas();
                    nuevaMesa.chrCodMesa = mesaUnica.chrCodMesa;
                    nuevaMesa.chrDesMesa = mesaUnica.chrDesMesa;
                    List<sp_listarEstadoMesa_Result> listaEstadoMesas = new List<sp_listarEstadoMesa_Result>();
                    listaEstadoMesas = this._databaseManager.sp_listarEstadoMesa(mesaUnica.chrCodMesa).ToList();
                    if (listaEstadoMesas.Count != 0)
                    {
                        sp_listarEstadoMesa_Result estadoUnico = listaEstadoMesas.First();
                        nuevaMesa.chrCodEstado = estadoUnico.chrCodEstado;
                        nuevaMesa.chrCodMoz = estadoUnico.chrCodMoz;
                        nuevaMesa.chrHorVen = estadoUnico.chrHorVen;
                    }
                    else
                    {
                        nuevaMesa.chrCodEstado = String.Empty;
                        nuevaMesa.chrCodMoz = String.Empty;
                        nuevaMesa.chrHorVen = String.Empty;
                    }
                    listaCompletaMesas.Add(nuevaMesa);
                }

                foreach (var mesaIncompleta in listaCompletaMesas)
                {
                    List<sp_datosMozo_Result> listaMozos = new List<Models.sp_datosMozo_Result>();
                    listaMozos = this._databaseManager.sp_datosMozo(mesaIncompleta.chrCodMoz).ToList();

                    if (listaMozos.Count != 0)
                    {
                        sp_datosMozo_Result mozoUnico = listaMozos.First();
                        mesaIncompleta.chrNomMoz = mozoUnico.chrNomMoz;
                        mesaIncompleta.chrApeMoz = mozoUnico.chrApeMoz;
                    }
                    else
                    {
                        mesaIncompleta.chrNomMoz = String.Empty;
                        mesaIncompleta.chrApeMoz = String.Empty;
                    }

                }

                return Json(listaCompletaMesas,JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public void listarEmpleados()
        {
            List<sp_listarMozos_Result> listaBase = new List<sp_listarMozos_Result>();
            listaBase = this._databaseManager.sp_listarMozos().ToList();

            List<SelectListItem> listaSalida = new List<SelectListItem>();

            foreach (var item in listaBase)
            {
                listaSalida.Add(new SelectListItem {
                                        Text = item.chrApeMoz + " " + item.chrNomMoz,
                                        Value = item.chrCodMoz.Trim()
                                });
            }

            ViewBag.Mozos = listaSalida;

        }

    }
}