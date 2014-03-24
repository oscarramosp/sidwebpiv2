using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using DALayer;

namespace BLLayer
{
    public class BLEmpresa
    {

        public string grabarEmpresa(string codigo, string nombre)
        {
            DAEmpresa oDAEmpresa = new DAEmpresa();

            oDAEmpresa.mIniciarTransaccion();

            try
            {
                codigo = oDAEmpresa.grabarEmpresa(codigo, nombre, oDAEmpresa.mtransaction);

                oDAEmpresa.mCommitTransaccion();

                return codigo;
            }
            catch (Exception ex)
            {
                oDAEmpresa.mRollbackTransaccion();
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }
        }

    }
}
