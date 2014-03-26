using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BELayer
{
    public class BEPauta : BEBase
    {
        public Int32 codigoPauta { get; set; }
        public string codigoDistribuidor { get; set; }
        public string codigoAgencia { get; set; }
        public string codigoCanilla { get; set; }
        public string codigoEmpresa { get; set; }
        public string codigoSector { get; set; }
        public string codigoMotivoVenta { get; set; }
        public string razonSocialEmpresa { get; set; }
        public string codigoCanal { get; set; }
        public string descripcionCanal { get; set; }
        public string codigoProducto { get; set; }
        public string descripcionProducto { get; set; }
        public string estadoPauta { get; set; }
        public DateTime? fechaPauta { get; set; }
        public DateTime? horaInicioMin { get; set; }
        public DateTime? horaInicioMax { get; set; }
        public Int32 cantidadSolicitada { get; set; }
        public Int32 cantidadProyectada { get; set; }
        public Int32 cantidadSugerida { get; set; }
        public Int32 cantidadAprobada { get; set; }
        public Int32 cantidadEntregada { get; set; }
        public Int32 cantidadDevuelta { get; set; }
    }
}
