using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nazca_Restaurant.Models.Internal.Entidades;

namespace Nazca_Restaurant.Models.Internal.Modelos
{
    public class PedidosModel
    {

        private NazResEntities2 databaseManager = new NazResEntities2();

        public List<Pedido> Agregar_Pedido(List<Pedido> arregloPedido, Pedido pedido)
        {
            List<Pedido> arrPedido = null;
            Boolean flag = true;
            try
            {
                arrPedido = new List<Pedido>();
                arrPedido = arregloPedido;
                foreach (var item in arrPedido)
                {
                    if (item.chrCodPro == pedido.chrCodPro)
                    {
                        if (item.tipoControl == pedido.tipoControl)
                        {
                            item.intCanPro = pedido.intCanPro;
                            item.chrComPro = pedido.chrComPro;
                            item.bytAteCoc = 0;
                            item.paraEliminar = 0;
                            flag = false;
                        }
                    }
                }

                if (flag)
                {
                    arrPedido.Add(pedido);
                }

                return arrPedido;
            }
            catch (Exception)
            {
                return arrPedido;
                throw;
            }
        }

        public List<Pedido> Eliminar_Pedido(List<Pedido> arregloPedido, Pedido pedido)
        {
            List<Pedido> arrPedido = null;
            try
            {
                arrPedido = new List<Pedido>();
                arrPedido = arregloPedido;

                foreach (var item in arrPedido)
                {
                    if (item.chrCodPro == pedido.chrCodPro)
                    {
                        if (item.tipoControl == pedido.tipoControl)
                        {
                            item.paraEliminar = 1;
                        }
                    }
                }

                return arrPedido;

            }
            catch (Exception ex)
            {
                return arrPedido;
                throw ex;
            }
        }

        public string Descripcion_Producto(string codProd)
        {
            string descripcion = "";
            sp_datosProducto_Result producto = null;
            try
            {
                producto = new sp_datosProducto_Result();
                producto = databaseManager.sp_datosProducto(codProd).ToList().First();
                descripcion = producto.chrDesPro;
                return descripcion;
            }
            catch (Exception)
            {
                return descripcion;
                throw;
            }
        }

        public double Precio_Producto(string codProd)
        {
            double descripcion = 0.00;
            sp_datosProducto_Result producto = null;
            try
            {
                producto = new sp_datosProducto_Result();
                producto = databaseManager.sp_datosProducto(codProd).ToList().First();
                descripcion = Convert.ToDouble(producto.numPrecio);
                return descripcion;
            }
            catch (Exception)
            {
                return descripcion;
                throw;
            }
        }
    }
}