using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BELayer;

namespace sid.Util
{
    public class SessionHelper
    {
        public static void setFormulaEditar(BEFormula objFormula)
        {
            HttpContext.Current.Session["formulaEditarObjeto"] = objFormula;
        }

        public static BEFormula getFormulaEditar()
        {
            return HttpContext.Current.Session["formulaEditarObjeto"] != null ?
                (BEFormula)HttpContext.Current.Session["formulaEditarObjeto"] : null;
        }

        public static void setOperacionPauta(string operacion)
        {
            HttpContext.Current.Session["operacionPauta"] = operacion;
        }

        public static string getOperacionPauta()
        {
            return HttpContext.Current.Session["operacionPauta"] != null ?
                (string)HttpContext.Current.Session["operacionPauta"] : string.Empty;
        }
    }
}
