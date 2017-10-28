using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nazca_Restaurant.Models.Internal.Entidades
{
    public class Pedido
    {
        public string chrCodPro { get; set; }
        public string chrDesPro { get; set; }
        public string chrComPro { get; set; }
        public double numPreVen { get; set; }
        public int intCanPro { get; set; }
        public int bytAteCoc { get; set; }
        public int tipoControl { get; set; }
        public int paraEliminar { get; set; }
    }
}