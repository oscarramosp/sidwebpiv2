using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using DALayer;
using BELayer;
using System.Data;


namespace BLLayer
{
    public class BLGeneral
    {
        public DataTable listarColumnas(BEGeneral general)
        {
            DAGeneral oDAGeneral = new DAGeneral();
            try
            {
                return oDAGeneral.listarColumnas(general);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }
        }
    }
}
