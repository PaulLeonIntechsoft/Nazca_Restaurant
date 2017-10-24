using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;
using Nazca_Restaurant.Models.Internal;
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
            listarTiposDeProductos();

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

        public JsonResult MenuBind(String idMesa)
        {
            try
            {
                Menu menuCompleto = new Menu();
                int nroV = 0;
                List<sp_lisarDetalleVenta_Result> listaBase = new List<sp_lisarDetalleVenta_Result>();
                List<sp_listarEstadoMesa_Result> listaEstadoMesas = new List<sp_listarEstadoMesa_Result>();
                listaEstadoMesas = this._databaseManager.sp_listarEstadoMesa(idMesa).ToList();

                if (listaEstadoMesas.Count > 0)
                {
                    sp_listarEstadoMesa_Result estadoUnico = listaEstadoMesas.First();
                    nroV = Convert.ToInt32(estadoUnico.intNroVen);
                    listaBase = this._databaseManager.sp_lisarDetalleVenta(nroV).ToList();
                } else
                {
                    listaBase = null;
                }

                List<sp_listarDatosVentas_Result> listaDatoBase = new List<sp_listarDatosVentas_Result>();
                listaDatoBase = this._databaseManager.sp_listarDatosVentas(nroV).ToList();

                if (listaDatoBase.Count > 0)
                {
                    sp_listarDatosVentas_Result datosVentaSolo = listaDatoBase.First();
                    menuCompleto.datosVenta = datosVentaSolo;
                }
                else
                {
                    menuCompleto.datosVenta = null;
                }

                if (listaBase != null)
                {
                    menuCompleto.detallesVenta = listaBase;
                }
                else
                {
                    menuCompleto.detallesVenta = null;
                }
                
                return Json(menuCompleto, JsonRequestBehavior.AllowGet);
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

        public void listarTiposDeProductos()
        {

            List<sp_listarTiposDeProductos_Result> listaBase = new List<sp_listarTiposDeProductos_Result>();
            listaBase = this._databaseManager.sp_listarTiposDeProductos().ToList();

            List<SelectListItem> listaSalida = new List<SelectListItem>();
            foreach (var item in listaBase)
            {
                listaSalida.Add(new SelectListItem { Text = item.chrDesTipo, Value = item.chrCodTipo.Trim()});
            }

            ViewBag.TiposDeProductos = listaSalida;

        }

        public JsonResult ArmarMenu(string chrCodPro, string chrComPro, string intCanPro)
        {
            try
            {
                List<sp_lisarDetalleVenta_Result> listaSalida = new List<sp_lisarDetalleVenta_Result>();
                DatosMenu pedido = new DatosMenu();
                pedido.chrCodPro = chrCodPro;
                pedido.chrComPro = chrComPro;
                pedido.intCanPro = Convert.ToInt32(intCanPro);

                List<DatosMenu> listaBase = new List<DatosMenu>();

                if (Session["arrayDatosMenu"] != null)
                {
                    listaBase = (List<DatosMenu>)Session["arrayDatosMenu"];
                }

                listaBase.Add(pedido);

                Session["arrayDatosMenu"] = listaBase;

                foreach (var item in listaBase)
                {
                    sp_datosProducto_Result productoUnico = new sp_datosProducto_Result();
                    productoUnico = this._databaseManager.sp_datosProducto(item.chrCodPro).ToList().First();

                    sp_lisarDetalleVenta_Result detalleUnico = new sp_lisarDetalleVenta_Result();
                    detalleUnico.chrCodPro = item.chrCodPro;
                    detalleUnico.chrDesPro = productoUnico.chrDesPro;
                    detalleUnico.chrComPro = item.chrComPro;
                    detalleUnico.intCanPro = Convert.ToInt16(item.intCanPro);
                    detalleUnico.numPreVen = productoUnico.numPrecio;
                    listaSalida.Add(detalleUnico);
                }

                return Json(listaSalida, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult Productos_Bind(String idTipo)
        {
            List<sp_productosPorTipo_Result> listaBase = new List<sp_productosPorTipo_Result>();
            listaBase = this._databaseManager.sp_productosPorTipo(idTipo).ToList();
            return Json(listaBase, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Guardar_Cambios(string codMesa, string codMozo, string codTarjeta, string codProp, string montoPropina, string tipoPago, string montoTotal)
        {
            try
            {
                int n = this._databaseManager.sp_insertarVenta(codMesa, codMozo, codTarjeta, codProp, Convert.ToDecimal(montoPropina), Convert.ToDecimal(montoTotal), tipoPago, "MOZOS");
                List<sp_listarEstadoMesa_Result> listaEstadoMesas = new List<sp_listarEstadoMesa_Result>();
                listaEstadoMesas = this._databaseManager.sp_listarEstadoMesa(codMesa).ToList();
                int nroV = 0;
                int contador = 1;
                if (listaEstadoMesas.Count != 0)
                {
                    sp_listarEstadoMesa_Result estadoUnico = listaEstadoMesas.First();
                    nroV = estadoUnico.intNroVen;
                }


                List<DatosMenu> listaBase = new List<DatosMenu>();

                if (Session["arrayDatosMenu"] != null)
                {
                    listaBase = (List<DatosMenu>)Session["arrayDatosMenu"];
                }


                foreach (var item in listaBase)
                {
                    sp_datosProducto_Result productoUnico = new sp_datosProducto_Result();
                    productoUnico = this._databaseManager.sp_datosProducto(item.chrCodPro).ToList().First();

                    int n2 = this._databaseManager.sp_insertarDetalleVenta(nroV, productoUnico.chrCodPro, item.chrComPro, Convert.ToInt16(item.intCanPro), Convert.ToInt16(contador));

                    contador++;
                }

                return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}