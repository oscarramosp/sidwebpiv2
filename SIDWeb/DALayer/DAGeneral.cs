using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BELayer;

namespace DALayer
{
    public class DAGeneral : DABase
    {
        public DataTable listarColumnas(BEGeneral general)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_LISTAR_COLUMNAS_OBJETO");
            db.AddInParameter(dbCommand, "@VC_NOMBRE_OBJETO", DbType.String, general.nombreObjeto);
            db.AddInParameter(dbCommand, "@VC_TIPO_DATO", DbType.String, general.tipoDato);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
    }
}
