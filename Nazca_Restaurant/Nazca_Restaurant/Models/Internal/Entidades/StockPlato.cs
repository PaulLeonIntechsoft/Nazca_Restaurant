using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nazca_Restaurant.Models.Internal.Entidades
{
    public class StockPlato
    {
        public string chrCodPro { get; set; }
        public string chrDesPro { get; set; }
        public string chrComentario { get; set; }
        public string imgProducto { get; set; }
        public int cantVendido { get; set; }
    }
}