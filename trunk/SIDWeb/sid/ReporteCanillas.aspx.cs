using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLLayer;
using BELayer;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

namespace sid
{
    public partial class ReporteCanillas : System.Web.UI.Page
    {
        BLCanilla oBLCanilla = new BLCanilla();

        #region "EVENTOS---------------------"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarCanillas();
            }
        }
        #endregion

        #region "METODOS---------------------"
        protected void cargarCanillas()
        {
            rptVwCanillas.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            rptVwCanillas.LocalReport.ReportPath = Server.MapPath("~/Reportes/rptCanillas.rdlc");
            rptVwCanillas.ShowExportControls = true;
            rptVwCanillas.ShowRefreshButton = false;
            var canillas = new DataSets.Canillas();
            canillas.Tables[0].Merge(oBLCanilla.reporteCanillas().Tables[0]);
            ReportDataSource reportSource = new ReportDataSource("Canillas_DataTable1", canillas.Tables[0]);
            rptVwCanillas.LocalReport.DataSources.Clear();
            rptVwCanillas.LocalReport.DataSources.Add(reportSource);
        }
        #endregion
    }
}
