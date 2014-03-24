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
    public class BLCanilla
    {
        public DataSet reporteCanillas()
        {
            DACanilla oDACanilla = new DACanilla();
            try
            {
                return oDACanilla.reporteCanillas();
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }
        }

        public List<BECanilla> selectCanillas(BECanilla canilla)
        {
            DACanilla oDACanilla = new DACanilla();
            try
            {
                return oDACanilla.selectCanillas(canilla);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return null;
            }
        }
    }
}
