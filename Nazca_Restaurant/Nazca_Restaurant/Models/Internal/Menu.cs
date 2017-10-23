using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nazca_Restaurant.Models.Internal
{
    public class Menu
    {

        public sp_listarDatosVentas_Result datosVenta { get; set; }
        public List<sp_lisarDetalleVenta_Result> detallesVenta { get; set; }

    }
}