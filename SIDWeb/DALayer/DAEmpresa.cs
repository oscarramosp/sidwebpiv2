using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DALayer
{
    public class DAEmpresa : DABase
    {

        public string grabarEmpresa(string codigoEmpresa, string razonSocialEmpresa, System.Data.Common.DbTransaction mTransaction)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommandWrapper = db.GetStoredProcCommand("SP_GRABAR_EMPRESA");

            System.Data.Common.DbParameter myParam = dbCommandWrapper.CreateParameter();
            myParam.DbType = DbType.String;
            myParam.ParameterName = "@CH_CODIGO_EMPRESA";
            myParam.Direction = ParameterDirection.InputOutput;
            myParam.Value = codigoEmpresa;
            myParam.Size = 4;
            dbCommandWrapper.Parameters.Add(myParam);

            db.AddInParameter(dbCommandWrapper, "@VC_RAZON_SOCIAL_EMPRESA", DbType.String, razonSocialEmpresa);

            db.ExecuteNonQuery(dbCommandWrapper, mTransaction);

            codigoEmpresa = Convert.ToString(myParam.Value);

            return codigoEmpresa;
        }

    }
}
