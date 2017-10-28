using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nazca_Restaurant.Models;
using Nazca_Restaurant.Models.Internal.Entidades;
using Nazca_Restaurant.Models.Internal.Modelos;

namespace Nazca_Restaurant.Controllers
{
    public class AtencionController : Controller
    {

        private NazResEntities2 databaseManager = new NazResEntities2();
        private VentasModel ventasModel = new VentasModel();
        private PedidosModel pedidosModel = new PedidosModel();

        public ActionResult Index()
        {
            List<sp_listarMesas_Result> listaMesasBase = new List<Models.sp_listarMesas_Result>();
            listaMesasBase = this.databaseManager.sp_listarMesas().ToList();
            listarEmpleados();
            listarTiposDeProductos();

            return View(listaMesasBase);
        }

        public void listarEmpleados()
        {
            List<sp_listarMozos_Result> listaBase = new List<sp_listarMozos_Result>();
            listaBase = this.databaseManager.sp_listarMozos().ToList();

            List<SelectListItem> listaSalida = new List<SelectListItem>();

            foreach (var item in listaBase)
            {
                listaSalida.Add(new SelectListItem
                {
                    Text = item.chrApeMoz + " " + item.chrNomMoz,
                    Value = item.chrCodMoz.Trim()
                });
            }

            ViewBag.Mozos = listaSalida;

        }

        public void listarTiposDeProductos()
        {

            List<sp_listarTiposDeProductos_Result> listaBase = new List<sp_listarTiposDeProductos_Result>();
            listaBase = this.databaseManager.sp_listarTiposDeProductos().ToList();

            List<SelectListItem> listaSalida = new List<SelectListItem>();
            foreach (var item in listaBase)
            {
                listaSalida.Add(new SelectListItem { Text = item.chrDesTipo, Value = item.chrCodTipo.Trim() });
            }

            ViewBag.TiposDeProductos = listaSalida;

        }

        public JsonResult MesasBind()
        {
            try
            {
                List<Mesas> listaCompletaMesas = new List<Mesas>();

                List<sp_listarMesas_Result> listaMesasBase = new List<Models.sp_listarMesas_Result>();
                listaMesasBase = this.databaseManager.sp_listarMesas().ToList();

                foreach (var mesaUnica in listaMesasBase)
                {
                    Mesas nuevaMesa = new Mesas();
                    nuevaMesa.chrCodMesa = mesaUnica.chrCodMesa;
                    nuevaMesa.chrDesMesa = mesaUnica.chrDesMesa;
                    List<sp_listarEstadoMesa_Result> listaEstadoMesas = new List<sp_listarEstadoMesa_Result>();
                    listaEstadoMesas = this.databaseManager.sp_listarEstadoMesa(mesaUnica.chrCodMesa).ToList();
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
                    listaMozos = this.databaseManager.sp_datosMozo(mesaIncompleta.chrCodMoz).ToList();

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

        public JsonResult ProductosBind(string idTipo)
        {
            List<sp_productosPorTipo_Result> listaBase = new List<sp_productosPorTipo_Result>();
            listaBase = this.databaseManager.sp_productosPorTipo(idTipo).ToList();
            return Json(listaBase, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MenuBind(string idMesa)
        {
            Menu menuCompleto = null;

            Pedido pedido = null;
            List<Pedido> arrPedidos = null;

            List<sp_lisarDetalleVenta_Result> detalleVenta = null;
            sp_listarDatosVentas_Result datosVenta = null;

            int numVenta = 0;

            try
            {
                #region Instancias
                menuCompleto = new Menu();
                arrPedidos = new List<Pedido>();
                detalleVenta = new List<sp_lisarDetalleVenta_Result>();
                datosVenta = new sp_listarDatosVentas_Result();
                #endregion

                numVenta = ventasModel.Get_NumeroVenta(idMesa);

                if (numVenta > 0)
                {
                    detalleVenta = this.databaseManager.sp_lisarDetalleVenta(numVenta).ToList();
                    datosVenta = this.databaseManager.sp_listarDatosVentas(numVenta).ToList().First();
                }

                foreach (var item in detalleVenta)
                {
                    pedido = new Pedido();
                    pedido.chrCodPro = item.chrCodPro;
                    pedido.chrDesPro = item.chrDesPro;
                    pedido.chrComPro = item.chrComPro;
                    pedido.numPreVen = Convert.ToDouble(item.numPreVen);
                    pedido.intCanPro = item.intCanPro;
                    pedido.bytAteCoc = item.bytAteCoc;
                    pedido.tipoControl = 1;
                    pedido.paraEliminar = 0;
                    arrPedidos.Add(pedido);
                }

                Session["arrayPedidos"] = arrPedidos;

                menuCompleto.datosVenta = datosVenta;
                menuCompleto.detallesVenta = arrPedidos;

                return Json(menuCompleto, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(menuCompleto, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult AgregarPedido(string chrCodPro, string chrComPro, string intCanPro)
        {
            Pedido pedido = null;
            List<Pedido> arrPedidos = null;
            try
            {
                #region Instancias
                pedido = new Pedido();
                arrPedidos = new List<Pedido>();
                #endregion

                #region Pedido
                pedido.chrCodPro = chrCodPro;
                pedido.chrDesPro = pedidosModel.Descripcion_Producto(chrCodPro);
                pedido.chrComPro = chrComPro;
                pedido.numPreVen = pedidosModel.Precio_Producto(chrCodPro);
                pedido.intCanPro = Convert.ToInt32(intCanPro);
                pedido.bytAteCoc = 0;
                pedido.tipoControl = 2;
                pedido.paraEliminar = 0;
                #endregion

                if (Session["arrayPedidos"] != null)
                {
                    arrPedidos = (List<Pedido>)Session["arrayPedidos"];
                }

                arrPedidos = pedidosModel.Agregar_Pedido(arrPedidos, pedido);

                Session["arrayPedidos"] = arrPedidos;

                return Json(arrPedidos, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(arrPedidos, JsonRequestBehavior.AllowGet);
                throw ex;
            }
        }

        public JsonResult EliminarPedido(string codProd, string tipoControl)
        {
            Pedido pedido = null;
            List<Pedido> arrPedido = null;
            try
            {
                arrPedido = new List<Pedido>();
                pedido = new Pedido();

                pedido.chrCodPro = codProd;
                pedido.tipoControl = Convert.ToInt32(tipoControl);

                arrPedido = pedidosModel.Eliminar_Pedido((List<Pedido>)Session["arrayPedidos"], pedido);

                return Json(arrPedido, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(arrPedido, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }

        public JsonResult GuardarVenta(string codMesa, string codMozo, string codTarjeta, string tipoPago, string montoTotal)
        {
            List<Pedido> arrPedidos = null;
            Ventas datosVenta = null;
            int n1 = 0;
            int n2 = 0;
            try
            {

                #region Instanciacion
                datosVenta = new Ventas();
                arrPedidos = new List<Pedido>();
                #endregion

                #region DatosVenta
                datosVenta.chrCodMesa = codMesa;
                datosVenta.chrCodMozo = codMozo;
                datosVenta.bytPagTar = codTarjeta;
                datosVenta.bytPropina = "0";
                datosVenta.numPropina = Convert.ToDouble("0.00");
                datosVenta.numSTotal = Convert.ToDouble(montoTotal);
                datosVenta.chrForPag = tipoPago;
                datosVenta.chrCodUsr = "MOZOS";
                #endregion

                n1 = ventasModel.RegistrarVenta(datosVenta);

                int numVenta = 0;
                numVenta = ventasModel.Get_NumeroVenta(codMesa);
                
                if (Session["arrayPedidos"] != null)
                {
                    arrPedidos = (List<Pedido>)Session["arrayPedidos"];
                }

                n2 = ventasModel.PreRegistroDetalleVenta(numVenta, 1, arrPedidos);
                
                return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult EditarVenta(string codMesa, string codTarjeta, string tipoPago, string montoTotal)
        {

            List<Pedido> arrPedidos = null;
            Ventas datosVenta = null;
            int n1 = 0;
            int n2 = 0;

            try
            {
                #region Instanciacion
                datosVenta = new Ventas();
                arrPedidos = new List<Pedido>();
                #endregion

                #region DatosVenta
                datosVenta.chrCodMesa = codMesa;
                datosVenta.bytPagTar = codTarjeta;
                datosVenta.numSTotal = Convert.ToDouble(montoTotal);
                datosVenta.chrForPag = tipoPago;
                #endregion

                int numVenta = 0;
                numVenta = ventasModel.Get_NumeroVenta(codMesa);

                n1 = ventasModel.EditarVenta(numVenta, datosVenta);

                if (Session["arrayPedidos"] != null)
                {
                    arrPedidos = (List<Pedido>)Session["arrayPedidos"];
                }

                n2 = ventasModel.PreRegistroDetalleVenta(numVenta, 2, arrPedidos);
                
                return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw ex;
            }
        }

        public JsonResult FinalizarVenta(string codMesa)
        {

            try
            {
                int numVenta = ventasModel.Get_NumeroVenta(codMesa);
                int n = databaseManager.sp_finalizarVenta(numVenta);
                return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                throw ex;
            }
        }

        public JsonResult Vaciar_Array_Session()
        {
            Session["arrayPedidos"] = null;
            return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }

    }
}