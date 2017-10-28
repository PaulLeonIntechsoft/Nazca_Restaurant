using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nazca_Restaurant.Models.Internal.Entidades;

namespace Nazca_Restaurant.Models.Internal.Modelos
{
    public class VentasModel
    {

        private NazResEntities2 databaseManager = new NazResEntities2();

        /*HACIA BASE DE DATOS*/
        public int RegistrarVenta(Ventas nuevaVenta)
        {
            Ventas venta = null;
            int col = -1;
            try
            {
                venta = new Ventas();

                venta = nuevaVenta;
                int n = databaseManager.sp_insertarVenta(   Convert.ToString(venta.chrCodMesa), venta.chrCodMozo, venta.bytPagTar, venta.bytPropina, Convert.ToDecimal(venta.numPropina),
                                                            Convert.ToDecimal(venta.numSTotal), venta.chrForPag, venta.chrCodUsr);

                return n;

            }
            catch (Exception ex)
            {
                return col;
                throw ex;
            }
        }

        public int EditarVenta(int nroVenta, Ventas nuevaVenta)
        {
            Ventas venta = null;
            int col = -1;
            try
            {
                venta = new Ventas();

                venta = nuevaVenta;
                int n = databaseManager.sp_editarVentas(nroVenta, venta.bytPagTar, Convert.ToDecimal(venta.numSTotal), venta.chrForPag);

                return n;

            }
            catch (Exception ex)
            {
                return col;
                throw;
            }
        }

        public int PreRegistroDetalleVenta(int numVenta, int peticion, List<Pedido> arrayPedidos)
        {
            int col = -1;
            List<Pedido> arrPedido = null;
            sp_ultimoIndice_Result ultimoPedido = null;
            try
            {
                arrPedido = new List<Pedido>();
                arrPedido = arrayPedidos;

                col = 0;
                int contador = 1;

                if (peticion == 2)
                {
                    ultimoPedido = new sp_ultimoIndice_Result();
                    ultimoPedido = databaseManager.sp_ultimoIndice(numVenta).ToList().First();
                    contador = Convert.ToInt32(ultimoPedido.bytOrden);
                }

                foreach (var item in arrPedido)
                {
                    if (item.paraEliminar == 0)
                    {
                        if (item.tipoControl == 2)
                        {
                            col += registrarDetalle(numVenta, item, contador);
                            contador++;
                        }
                    }
                    else if (item.paraEliminar == 1)
                    {
                        if (item.tipoControl == 1)
                        {
                            col += databaseManager.sp_eliminarPedido(numVenta,item.chrCodPro);
                        }
                    }
                }

                return col;

            }
            catch (Exception)
            {
                return col;
                throw;
            }
        }

        public int registrarDetalle(int numVenta, Pedido pedido, int orden)
        {
            int n = 0;
            try
            {
                n = databaseManager.sp_insertarDetalleVenta(numVenta, pedido.chrCodPro, pedido.chrComPro, Convert.ToInt16(pedido.intCanPro), Convert.ToInt16(orden));
                return n;
            }
            catch (Exception ex)
            {
                return n;
                throw ex;
            }
        }
        

        /*DESDE BASE DE DATOS*/

        public int Get_NumeroVenta(string codMesa)
        {
            sp_listarEstadoMesa_Result mesaElegida = null;
            int numeroVenta = 0;
            try
            {
                mesaElegida = new sp_listarEstadoMesa_Result();
                mesaElegida = databaseManager.sp_listarEstadoMesa(codMesa).ToList().First();
                numeroVenta = Convert.ToInt32(mesaElegida.intNroVen);
                return numeroVenta;
            }
            catch (NullReferenceException ex)
            {
                return -2;
                throw ex;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }

    }
}