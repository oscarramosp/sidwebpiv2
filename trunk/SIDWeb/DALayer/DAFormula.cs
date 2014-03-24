using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BELayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.SqlClient;

namespace DALayer
{
    public class DAFormula : DABase
    {
        public string validarFormula(BEFormula formula)
        {
            string resultado = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_VALIDAR_FORMULA");
            db.AddInParameter(dbCommand, "@VC_FORMULA1", DbType.String, formula.formula);
            db.AddOutParameter(dbCommand, "@VC_LISTA_ERROR", DbType.String, 32);

            try
            {
                db.ExecuteNonQuery(dbCommand);
                return dbCommand.Parameters["@VC_LISTA_ERROR"].Value.ToString();
            }
            catch (SqlException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Policy"))
                {
                    switch (ex.Number)
                    {
                        case 8134:
                            return "3";
                        default:
                            return "2";
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                return "2";
            }
            return "2";
        }

        public BEFormula obtenerFormula(BEFormula formula)
        {
            Database db = DatabaseFactory.CreateDatabase();
            IDataReader rdr = null;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_OBTENER_FORMULA");
            db.AddInParameter(dbCommand, "@CH_CODIGO_FORMULA", DbType.String, formula.codigoFormula);
            rdr = db.ExecuteReader(dbCommand);
            while (rdr.Read())
            {
                formula = new BEFormula();
                formula.codigoFormula = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_FORMULA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_FORMULA"));
                formula.formula = rdr.IsDBNull(rdr.GetOrdinal("VC_FORMULA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_FORMULA"));
                break;
            }
            return formula;
        }

        public string grabarFormula(BEFormula formula, System.Data.Common.DbTransaction mTransaction)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommandWrapper = db.GetStoredProcCommand("SP_GRABAR_FORMULA");

            System.Data.Common.DbParameter myParam = dbCommandWrapper.CreateParameter();
            myParam.DbType = DbType.String;
            myParam.ParameterName = "@CH_CODIGO_FORMULA";
            myParam.Direction = ParameterDirection.InputOutput;
            myParam.Value = formula.codigoFormula;
            myParam.Size = 4;
            dbCommandWrapper.Parameters.Add(myParam);

            db.AddInParameter(dbCommandWrapper, "@VC_FORMULA", DbType.String, formula.formula);

            db.ExecuteNonQuery(dbCommandWrapper, mTransaction);

            return Convert.ToString(myParam.Value);
        }
    }
}
