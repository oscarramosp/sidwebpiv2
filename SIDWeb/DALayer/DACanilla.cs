using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BELayer;


namespace DALayer
{
    public class DACanilla : DABase
    {
        public DataSet reporteCanillas()
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_LISTAR_CANILLAS");
            DataSet ds = db.ExecuteDataSet(dbCommand);
            ds.Tables[0].TableName = "DataTable1";
            return db.ExecuteDataSet(dbCommand);
        }

        public List<BECanilla> selectCanillas(BECanilla canilla)
        {
            List<BECanilla> listaCanillas = new List<BECanilla>();
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_LISTAR_CANILLAS_BUSQUEDA");
            db.AddInParameter(dbCommand, "@CH_CODIGO_DISTRIBUIDOR", DbType.String, canilla.codigoDistribuidor);
            db.AddInParameter(dbCommand, "@CH_CODIGO_AGENCIA", DbType.String, canilla.codigoAgencia);
            db.AddInParameter(dbCommand, "@CH_CODIGO_CANILLA", DbType.String, canilla.codigoCanilla);
            db.AddInParameter(dbCommand, "@VC_NOMBRE_COMPLETO_CANILLA", DbType.String, canilla.nombreCompletoCanilla);
            db.AddInParameter(dbCommand, "@CH_CODIGO_DIRECCION_CANILLA", DbType.String, canilla.codigoDireccion);
            db.AddInParameter(dbCommand, "@VC_DIRECCION_COMPLETA_CANILLA", DbType.String, canilla.direccion);
            db.AddInParameter(dbCommand, "@CH_TIPO_DOCUMENTO_CANILLA", DbType.String, canilla.tipoDocumento);
            db.AddInParameter(dbCommand, "@VC_NUMERO_DOCUMENTO_CANILLA", DbType.String, canilla.numeroDocumento);
            db.AddInParameter(dbCommand, "@DT_FECHA_NACIMIENTO_CANILLA", DbType.Date, canilla.fechaNacimiento);
            db.AddInParameter(dbCommand, "@CH_TIPO_CANILLA", DbType.String, canilla.tipoCanilla);
            db.AddInParameter(dbCommand, "@CH_ESTADO_REGISTRO", DbType.String, canilla.estadoRegistro);
            IDataReader rdr = null;
            rdr = db.ExecuteReader(dbCommand);
            while (rdr.Read())
            {
                canilla = new BECanilla();
                canilla.codigoDistribuidor = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_DISTRIBUIDOR")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_DISTRIBUIDOR"));
                canilla.codigoAgencia = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_AGENCIA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_AGENCIA"));
                canilla.codigoCanilla = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_CANILLA"));
                canilla.nombreCompletoCanilla = rdr.IsDBNull(rdr.GetOrdinal("VC_NOMBRE_COMPLETO_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_NOMBRE_COMPLETO_CANILLA"));
                canilla.codigoDireccion = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_DIRECCION_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_DIRECCION_CANILLA"));
                canilla.direccion = rdr.IsDBNull(rdr.GetOrdinal("VC_DIRECCION_COMPLETA_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_DIRECCION_COMPLETA_CANILLA"));
                canilla.tipoDocumento = rdr.IsDBNull(rdr.GetOrdinal("CH_TIPO_DOCUMENTO_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_TIPO_DOCUMENTO_CANILLA"));
                canilla.numeroDocumento = rdr.IsDBNull(rdr.GetOrdinal("VC_NUMERO_DOCUMENTO_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_NUMERO_DOCUMENTO_CANILLA"));
                canilla.fechaNacimiento = rdr.IsDBNull(rdr.GetOrdinal("DT_FECHA_NACIMIENTO_CANILLA")) ? new DateTime() : rdr.GetDateTime(rdr.GetOrdinal("DT_FECHA_NACIMIENTO_CANILLA"));
                canilla.tipoCanilla = rdr.IsDBNull(rdr.GetOrdinal("CH_TIPO_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_TIPO_CANILLA"));
                canilla.estadoRegistro = rdr.IsDBNull(rdr.GetOrdinal("CH_ESTADO_REGISTRO")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_ESTADO_REGISTRO"));
                canilla.nombreAgencia = rdr.IsDBNull(rdr.GetOrdinal("VC_DESCRIPCION_AGENCIA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_DESCRIPCION_AGENCIA"));
                listaCanillas.Add(canilla);
            }
            return listaCanillas;
        }
    }
}
