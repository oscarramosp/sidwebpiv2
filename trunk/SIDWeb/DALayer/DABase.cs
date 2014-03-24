using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Text;

namespace DALayer
{
    public class DABase
    {
        #region Propiedades
        private System.Data.Common.DbConnection _mconnection;
        private System.Data.Common.DbTransaction _mtransaction;
        private Database _mdb;

        protected System.Data.Common.DbConnection mconnection
        {
            get
            {
                return _mconnection;
            }
            set
            {
                _mconnection = value;
            }
        }
        public System.Data.Common.DbTransaction mtransaction
        {
            get
            {
                return _mtransaction;
            }
            set
            {
                _mtransaction = value;
            }
        }
        protected Database mdb
        {
            get
            {
                return _mdb;
            }
            set
            {
                _mdb = value;
            }
        }
        #endregion

        #region Metodos
        public void mIniciarTransaccion()
        {
            mdb = DatabaseFactory.CreateDatabase();
            //idbconnection
            mconnection = mdb.CreateConnection();
            mconnection.Open();
            mtransaction = mconnection.BeginTransaction();
        }
        public void mCommitTransaccion()
        {
            mtransaction.Commit();
            mtransaction.Dispose();
            mtransaction = null;
            mconnection.Close();
            mconnection.Dispose();
            mconnection = null;
            mdb = null;
        }
        public void mRollbackTransaccion()
        {
            mtransaction.Rollback();
            mtransaction.Dispose();
            mtransaction = null;
            mconnection.Close();
            mconnection.Dispose();
            mconnection = null;
            mdb = null;
        }
        #endregion
    }
}
