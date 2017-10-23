﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nazca_Restaurant.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NazResEntities2 : DbContext
    {
        public NazResEntities2()
            : base("name=NazResEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int sp_agregarTipoDeCambio(string fecha, Nullable<decimal> valorCambio)
        {
            var fechaParameter = fecha != null ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(string));
    
            var valorCambioParameter = valorCambio.HasValue ?
                new ObjectParameter("valorCambio", valorCambio) :
                new ObjectParameter("valorCambio", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_agregarTipoDeCambio", fechaParameter, valorCambioParameter);
        }
    
        public virtual int sp_aperturarCaja(string fecha, Nullable<decimal> soles, Nullable<decimal> dolares, string usuario)
        {
            var fechaParameter = fecha != null ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(string));
    
            var solesParameter = soles.HasValue ?
                new ObjectParameter("soles", soles) :
                new ObjectParameter("soles", typeof(decimal));
    
            var dolaresParameter = dolares.HasValue ?
                new ObjectParameter("dolares", dolares) :
                new ObjectParameter("dolares", typeof(decimal));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_aperturarCaja", fechaParameter, solesParameter, dolaresParameter, usuarioParameter);
        }
    
        public virtual ObjectResult<sp_datosMozo_Result> sp_datosMozo(string codigoMozo)
        {
            var codigoMozoParameter = codigoMozo != null ?
                new ObjectParameter("codigoMozo", codigoMozo) :
                new ObjectParameter("codigoMozo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_datosMozo_Result>("sp_datosMozo", codigoMozoParameter);
        }
    
        public virtual ObjectResult<sp_lisarDetalleVenta_Result> sp_lisarDetalleVenta(Nullable<int> codigoVenta)
        {
            var codigoVentaParameter = codigoVenta.HasValue ?
                new ObjectParameter("codigoVenta", codigoVenta) :
                new ObjectParameter("codigoVenta", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_lisarDetalleVenta_Result>("sp_lisarDetalleVenta", codigoVentaParameter);
        }
    
        public virtual ObjectResult<sp_listarAperturaCaja_Result> sp_listarAperturaCaja()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarAperturaCaja_Result>("sp_listarAperturaCaja");
        }
    
        public virtual ObjectResult<sp_listarDatosVentas_Result> sp_listarDatosVentas(Nullable<int> codigoVenta)
        {
            var codigoVentaParameter = codigoVenta.HasValue ?
                new ObjectParameter("codigoVenta", codigoVenta) :
                new ObjectParameter("codigoVenta", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarDatosVentas_Result>("sp_listarDatosVentas", codigoVentaParameter);
        }
    
        public virtual ObjectResult<sp_listarEstadoMesa_Result> sp_listarEstadoMesa(string codigoMesa)
        {
            var codigoMesaParameter = codigoMesa != null ?
                new ObjectParameter("codigoMesa", codigoMesa) :
                new ObjectParameter("codigoMesa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarEstadoMesa_Result>("sp_listarEstadoMesa", codigoMesaParameter);
        }
    
        public virtual ObjectResult<sp_listarMesas_Result> sp_listarMesas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarMesas_Result>("sp_listarMesas");
        }
    
        public virtual ObjectResult<sp_listarMozos_Result> sp_listarMozos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarMozos_Result>("sp_listarMozos");
        }
    
        public virtual ObjectResult<sp_listarTiposDeCambio_Result> sp_listarTiposDeCambio()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarTiposDeCambio_Result>("sp_listarTiposDeCambio");
        }
    
        public virtual ObjectResult<sp_listarTiposDeProductos_Result> sp_listarTiposDeProductos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarTiposDeProductos_Result>("sp_listarTiposDeProductos");
        }
    
        public virtual ObjectResult<sp_productosPorTipo_Result> sp_productosPorTipo(string codigoTipo)
        {
            var codigoTipoParameter = codigoTipo != null ?
                new ObjectParameter("codigoTipo", codigoTipo) :
                new ObjectParameter("codigoTipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_productosPorTipo_Result>("sp_productosPorTipo", codigoTipoParameter);
        }
    }
}
