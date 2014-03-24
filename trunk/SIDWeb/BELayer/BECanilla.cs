using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BELayer
{
    public class BECanilla : BEBase
    {
        public string codigoDistribuidor { get; set; }
        public string codigoAgencia { get; set; }
        public string codigoCanilla { get; set; }
        public string nombreCompletoCanilla { get; set; }
        public string codigoDireccion { get; set; }
        public string direccion { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string tipoCanilla { get; set; }

        public string nombreAgencia { get; set; }
    }
}
