using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BELayer;

namespace DALayer
{
    public class DAPauta : DABase
    {
        public List<BEPauta> selectPautaProducto(BEPauta pauta)
        {
            List<BEPauta> listaPautas = new List<BEPauta>();
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_LISTAR_PAUTAS_AGRUPA_PRODUCTOS");
            db.AddInParameter(dbCommand, "@DT_FECHA_PAUTA", DbType.Date, pauta.fechaPauta);
            IDataReader rdr = null;
            rdr = db.ExecuteReader(dbCommand);
            while (rdr.Read())
            {
                pauta = new BEPauta();
                pauta.codigoEmpresa = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_EMPRESA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_EMPRESA"));
                pauta.razonSocialEmpresa = rdr.IsDBNull(rdr.GetOrdinal("VC_RAZON_SOCIAL_EMPRESA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_RAZON_SOCIAL_EMPRESA"));
                pauta.codigoCanal = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_CANAL")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_CANAL"));
                pauta.descripcionCanal = rdr.IsDBNull(rdr.GetOrdinal("VC_DESCRIPCION_CANAL")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_DESCRIPCION_CANAL"));
                pauta.codigoProducto = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_PRODUCTO")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_PRODUCTO"));
                pauta.descripcionProducto = rdr.IsDBNull(rdr.GetOrdinal("VC_DESCRIPCION_PRODUCTO")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_DESCRIPCION_PRODUCTO"));
                pauta.fechaPauta = rdr.IsDBNull(rdr.GetOrdinal("DT_FECHA_PAUTA")) ? new DateTime() : rdr.GetDateTime(rdr.GetOrdinal("DT_FECHA_PAUTA"));
                pauta.cantidadSolicitada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_SOLICITADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_SOLICITADA"));
                pauta.cantidadProyectada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_PROYECTADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_PROYECTADA"));
                pauta.cantidadSugerida = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_SUGERIDA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_SUGERIDA"));
                pauta.cantidadAprobada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_APROBADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_APROBADA"));
                pauta.cantidadEntregada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_ENTREGADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_ENTREGADA"));
                pauta.cantidadDevuelta = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_DEVUELTA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_DEVUELTA"));
                listaPautas.Add(pauta);
            }
            return listaPautas;
        }

        public Int32 validarProyectarPauta(ref BEPauta pauta)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_VALIDAR_PROYECTAR_PAUTA");
            Int32 intCodigoError = 0;

            IDbDataParameter myParam = dbCommand.CreateParameter();
            myParam.DbType = DbType.Int32;
            myParam.ParameterName = "@IN_CODIGO_ERROR";
            myParam.Direction = ParameterDirection.InputOutput;
            myParam.Value = intCodigoError;
            dbCommand.Parameters.Add(myParam);

            db.AddInParameter(dbCommand, "@DT_FECHA_PAUTA", DbType.Date, pauta.fechaPauta);
            db.AddOutParameter(dbCommand, "@DT_MIN_INICIO_R", DbType.DateTime, 8);
            db.AddOutParameter(dbCommand, "@DT_MAX_INICIO_R", DbType.DateTime, 8);

            db.ExecuteNonQuery(dbCommand);

            intCodigoError = Convert.ToInt32(myParam.Value);
            pauta.horaInicioMin = Convert.ToDateTime(dbCommand.Parameters["@DT_MIN_INICIO_R"].Value);
            pauta.horaInicioMax = Convert.ToDateTime(dbCommand.Parameters["@DT_MAX_INICIO_R"].Value);

            return intCodigoError;
        }

        public void proyectarPautas(BEPauta pauta, System.Data.Common.DbTransaction mTransaction)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommandWrapper = db.GetStoredProcCommand("SP_PROYECTAR_PAUTA");

            db.AddInParameter(dbCommandWrapper, "@DT_FECHA_PAUTA", DbType.Date, pauta.fechaPauta);

            db.ExecuteNonQuery(dbCommandWrapper, mTransaction);
        }

        public List<BEPauta> selectPautasCanillas(BEPauta pauta)
        {
            List<BEPauta> listaPautas = new List<BEPauta>();
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_LISTAR_PAUTAS_CANILLAS");
            db.AddInParameter(dbCommand, "@CH_CODIGO_CANILLA", DbType.String, pauta.codigoCanilla.Trim());
            db.AddInParameter(dbCommand, "@CH_ESTADO_PAUTA", DbType.String, pauta.estadoPauta);
            db.AddInParameter(dbCommand, "@DT_FECHA_PAUTA", DbType.Date, pauta.fechaPauta);
            IDataReader rdr = null;
            rdr = db.ExecuteReader(dbCommand);
            while (rdr.Read())
            {
                pauta = new BEPauta();
                pauta.codigoPauta = rdr.IsDBNull(rdr.GetOrdinal("IN_CODIGO_PAUTA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CODIGO_PAUTA"));
                pauta.codigoCanilla = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_CANILLA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_CANILLA"));
                pauta.codigoEmpresa = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_EMPRESA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_EMPRESA"));
                pauta.codigoSector = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_SECTOR")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_SECTOR"));
                pauta.codigoProducto = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_PRODUCTO")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_PRODUCTO"));
                pauta.codigoCanal = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_CANAL")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_CANAL"));
                pauta.codigoMotivoVenta = rdr.IsDBNull(rdr.GetOrdinal("CH_CODIGO_MOTIVO_VENTA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_CODIGO_MOTIVO_VENTA"));
                pauta.descripcionProducto = rdr.IsDBNull(rdr.GetOrdinal("VC_DESCRIPCION_PRODUCTO")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("VC_DESCRIPCION_PRODUCTO"));
                pauta.fechaPauta = rdr.IsDBNull(rdr.GetOrdinal("DT_FECHA_PAUTA")) ? new DateTime() : rdr.GetDateTime(rdr.GetOrdinal("DT_FECHA_PAUTA"));
                pauta.cantidadSolicitada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_SOLICITADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_SOLICITADA"));
                pauta.cantidadProyectada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_PROYECTADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_PROYECTADA"));
                pauta.cantidadAprobada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_APROBADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_APROBADA"));
                pauta.cantidadDevuelta = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_DEVUELTA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_DEVUELTA"));
                pauta.cantidadEntregada = rdr.IsDBNull(rdr.GetOrdinal("IN_CANTIDAD_ENTREGADA")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("IN_CANTIDAD_ENTREGADA"));
                pauta.estadoPauta = rdr.IsDBNull(rdr.GetOrdinal("CH_ESTADO_PAUTA")) ? String.Empty : rdr.GetString(rdr.GetOrdinal("CH_ESTADO_PAUTA"));
                listaPautas.Add(pauta);
            }
            return listaPautas;
        }

        public Int32 validarSolicitarPauta(BEPauta pauta)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_VALIDAR_SOLICITAR_PAUTA");

            Int32 intCodigoError = 0;

            IDbDataParameter myParam = dbCommand.CreateParameter();
            myParam.DbType = DbType.Int32;
            myParam.ParameterName = "@IN_CODIGO_ERROR";
            myParam.Direction = ParameterDirection.InputOutput;
            myParam.Value = intCodigoError;
            dbCommand.Parameters.Add(myParam);

            db.AddInParameter(dbCommand, "@IN_CODIGO_PAUTA", DbType.Int32, pauta.codigoPauta);
            

            db.ExecuteNonQuery(dbCommand);

            intCodigoError = Convert.ToInt32(myParam.Value);

            return intCodigoError;
        }

        public void grabarSolicitudPauta(BEPauta pauta, System.Data.Common.DbTransaction mTransaction)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommandWrapper = db.GetStoredProcCommand("SP_GRABAR_SOLICITUD_PAUTA");

            db.AddInParameter(dbCommandWrapper, "@IN_CODIGO_PAUTA", DbType.Int32, pauta.codigoPauta);
            db.AddInParameter(dbCommandWrapper, "@IN_CANTIDAD_SOLICITADA", DbType.Int32, pauta.cantidadSolicitada);

            db.ExecuteNonQuery(dbCommandWrapper, mTransaction);
        }

        public Int32 validarDevolverProductos(BEPauta pauta)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("SP_VALIDAR_DEVOLVER_PRODUCTOS");

            Int32 intCodigoError = 0;

            IDbDataParameter myParam = dbCommand.CreateParameter();
            myParam.DbType = DbType.Int32;
            myParam.ParameterName = "@IN_CODIGO_ERROR";
            myParam.Direction = ParameterDirection.InputOutput;
            myParam.Value = intCodigoError;
            dbCommand.Parameters.Add(myParam);

            db.AddInParameter(dbCommand, "@IN_CODIGO_PAUTA", DbType.String, pauta.codigoPauta);


            db.ExecuteNonQuery(dbCommand);

            intCodigoError = Convert.ToInt32(myParam.Value);

            return intCodigoError;
        }

        public void grabarDevolverProducto(BEPauta pauta, System.Data.Common.DbTransaction mTransaction)
        {
            Database db = DatabaseFactory.CreateDatabase();
            System.Data.Common.DbCommand dbCommandWrapper = db.GetStoredProcCommand("SP_GRABAR_DEVOLVER_PRODUCTOS");

            db.AddInParameter(dbCommandWrapper, "@IN_CODIGO_PAUTA", DbType.Int32, pauta.codigoPauta);
            db.AddInParameter(dbCommandWrapper, "@IN_CANTIDAD_DEVUELTA", DbType.Int32, pauta.cantidadDevuelta);

            db.ExecuteNonQuery(dbCommandWrapper, mTransaction);
        }
    }
}
