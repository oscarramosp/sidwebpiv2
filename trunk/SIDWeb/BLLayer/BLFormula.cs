using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BELayer;
using DALayer;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace BLLayer
{
    public class BLFormula
    {
        public BEFormula obtenerFormula(BEFormula formula)
        {
            var oDAFormula = new DAFormula();
            try
            {
                return oDAFormula.obtenerFormula(formula);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }
        }

        public DTOResultado grabarFormula(BEFormula formula)
        {
            var oDAFormula = new DAFormula();
            var oDTOResultado = new DTOResultado();

            try
            {
                string strValidacion = validarSintaxisFormula(formula);
                int intValidacion = Convert.ToInt32(strValidacion);

                if (intValidacion != (int)Constantes.CodigoGrabarFormula.Ok)
                {
                    if (intValidacion == (int)Constantes.CodigoGrabarFormula.ErrorReferenciaCircular)
                    {
                        oDTOResultado.Codigo = (int)Constantes.CodigoGrabarFormula.ErrorReferenciaCircular;
                    }
                    else if (intValidacion == (int)Constantes.CodigoGrabarFormula.ErrorDivisionporCero)
                    {
                        oDTOResultado.Codigo = (int)Constantes.CodigoGrabarFormula.ErrorDivisionporCero;
                    }
                    else if (intValidacion == (int)Constantes.CodigoGrabarFormula.ErrorSintaxis)
                    {
                        oDTOResultado.Codigo = (int)Constantes.CodigoGrabarFormula.ErrorSintaxis;
                    }
                    else
                    {
                        oDTOResultado.Codigo = (int)Constantes.CodigoGrabarFormula.Error;
                    }
                    oDTOResultado.Objeto = formula;
                    return oDTOResultado;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }

            oDAFormula.mIniciarTransaccion();

            try
            {
                string codigoFormula = oDAFormula.grabarFormula(formula, oDAFormula.mtransaction);
                oDAFormula.mCommitTransaccion();
                formula.codigoFormula = codigoFormula;
                oDTOResultado.Codigo = (int)Constantes.CodigoGrabarFormula.Ok;
                oDTOResultado.Objeto = formula;
                return oDTOResultado;
            }
            catch (Exception ex)
            {
                oDAFormula.mRollbackTransaccion();
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }

            return oDTOResultado;
        }

        private string validarSintaxisFormula(BEFormula formula)
        {
            var oDAFormula = new DAFormula();
            try
            {
                var strRutina = generarFormula(formula);
                if (strRutina.IndexOf("ER1") >= 0)
                {
                    return "1";
                }
                if (strRutina.IndexOf("ER2") >= 0)
                {
                    return "2";
                }
                return oDAFormula.validarFormula(formula);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return "-1";
            }
        }

        private String generarFormula(BEFormula formula) 
        {
            String strTextoCompleto = mCompressTexto(formula.formula);
            Int32 inLongitud = strTextoCompleto.Length;
            Int32 inContador = 0;
            Int32 inContadorAux = 0;
            String strTexto, strTextoAux;
            Char chrCaracter, chrCaracterAux;

            while (inContador < inLongitud)
            {
                if (formula.formula.Contains("IN_CANTIDAD_PROYECTADA"))
                {
                    return "ER1";
                }
                chrCaracter = Convert.ToChar(strTextoCompleto.Substring(inContador, 1));
                switch (chrCaracter)
                {
                    case '/':
                        if (strTextoCompleto.Substring(inContador, 2).Equals("/*"))
                        {
                            inContadorAux = inContador;
                            chrCaracter = Convert.ToChar(strTextoCompleto.Substring(inContador + 1, 1));
                            chrCaracterAux = Convert.ToChar(strTextoCompleto.Substring(inContador + 2, 1));
                            while (!(chrCaracter.ToString() + chrCaracterAux.ToString()).Equals("*/") && inContador < inLongitud - 2)
                            {
                                inContador++;
                                chrCaracter = Convert.ToChar(strTextoCompleto.Substring(inContador, 1));
                                chrCaracterAux = Convert.ToChar(strTextoCompleto.Substring(inContador + 1, 1));
                            }
                            strTextoAux = strTextoCompleto.Substring(inContadorAux, inContador - inContadorAux + 2);
                            strTextoCompleto = strTextoCompleto.Replace(strTextoAux, "");
                            inContador = inContadorAux - 1;
                        }
                        break;
                }
                inContador++;
                inLongitud = strTextoCompleto.Length;
            }
            inLongitud = strTextoCompleto.Length;
            inContador = 0;
            inContadorAux = 0;
            while (inContador < inLongitud)
            {
                chrCaracter = Convert.ToChar(strTextoCompleto.Substring(inContador, 1));
                switch (chrCaracter)
                {
                    case 'U':
                        if (strTextoCompleto.Substring(inContador, 3).Equals("UP_") && !strTextoCompleto.Substring(inContador - 1 <= 0 ? 0 : inContador - 1, 1).Equals("_") && !strTextoCompleto.Substring(inContador - 1 <= 0 ? 0 : inContador - 1, 1).Equals("."))
                        {
                            inContadorAux = inContador;
                            chrCaracter = Convert.ToChar(strTextoCompleto.Substring(inContador + 1, 1));
                            while (!chrCaracter.ToString().Equals("}") && inContador < inLongitud - 1)
                            {
                                inContador++;
                                chrCaracter = Convert.ToChar(strTextoCompleto.Substring(inContador, 1));
                            }
                            strTextoAux = strTextoCompleto.Substring(inContadorAux, inContador - inContadorAux + 1);
                            if (strTextoAux.IndexOf("{") > 0 && strTextoAux.IndexOf("}") > 0)
                            {
                                strTexto = ",[CH_CODIGO_DISTRIBUIDOR],[CH_CODIGO_AGENCIA],[CH_CODIGO_CANILLA],[CH_CODIGO_EMPRESA],[CH_CODIGO_SECTOR],[CH_CODIGO_PRODUCTO],[CH_CODIGO_CANAL],[CH_CODIGO_MOTIVO_VENTA],[DT_FECHA_PAUTA]) ";
                                strTexto = strTextoAux.Replace("}", strTexto);
                                strTexto = strTexto.Replace("{", "(");
                                strTexto = "dbo." + strTexto;
                                strTextoCompleto = strTextoCompleto.Replace(strTextoAux, strTexto);
                                inContador = inContadorAux + strTexto.Length - 1;
                            }
                        }
                        break;
                }
                inContador++;
                inLongitud = strTextoCompleto.Length;
            }
            return strTextoCompleto;
        }

        private String mCompressTexto(String pstrTexto)
        {
            String[] strTexto = pstrTexto.Split(' ');
            String strTexto1 = String.Empty;
            for (int i = 0; i < strTexto.Length; i++)
            {
                if (!strTexto.GetValue(i).ToString().Trim().Equals(""))
                {
                    strTexto1 = strTexto1 + " " + strTexto.GetValue(i).ToString().Trim();
                }
            }
            return strTexto1.Trim();
        }
    }
}
