using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nazca_Restaurant.Models.Internal.Entidades
{
    public class Ventas
    {

        public int intNroVen { get; set; }
        public string chrCodMesa { get; set; }
        public string chrCodMozo { get; set; }
        public string bytPagTar { get; set; }
        public string bytPropina { get; set; }
        public double numPropina { get; set; }
        public double numSTotal { get; set; }
        public string chrForPag { get; set; }
        public string chrCodUsr { get; set; }

    }
}