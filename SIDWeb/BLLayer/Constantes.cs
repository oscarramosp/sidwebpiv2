using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLLayer
{
    public class Constantes
    {
        public enum CodigoGrabarFormula
        {
            Ok = 0,
            ErrorSintaxis = 2,
            Error = -2,
            ErrorReferenciaCircular = 1,
            ErrorDivisionporCero = 3
        }

        public enum CodigoProyectarPauta
        {
            Ok = 0,
            Error = -1,
            FechaProyeccionIncorrecta = -2,
            FueraDeHorario = -3,
            FormulaNoDefinida = -4,
            EstadoFueraFlujo = -5
        }

        public enum CodigoSolicitarPauta
        {
            Ok = 0,
            ErrorEnviadoASAP = -1,
            ErrorFechaSolicitud = -2
        }

        public enum CodigoDevolverProductos
        {
            Ok = 0,
            Error = -1
        }

        public static string EstadoPautaNuevo = "N";
        public static string EstadoPautaSolicitado = "S";
        public static string EstadoPautaProyectado = "P";
        public static string EstadoPautaSugerido = "G";
        public static string EstadoPautaAprobadoo = "A";
        public static string EstadoPautaEntregado = "E";
        public static string EstadoPautaDevuelto = "D";
    }
}
