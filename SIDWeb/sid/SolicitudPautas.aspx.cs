using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BLLayer;
using BELayer;
using System.Data;

namespace sid
{
    public partial class SolicitudPautas : System.Web.UI.Page
    {
        BLPauta oBLPauta = new BLPauta();
        String operacion = String.Empty;

        #region "EVENTOS---------------------"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Util.SessionHelper.setOperacionPauta(Request.QueryString["op"]);
                setControles();
            }
        }

        protected void txtCodigoCanilla_TextChanged(object sender, EventArgs e)
        {
            buscarSolicitudes();
        }

        protected void clnFecha_SelectionChanged(object sender, EventArgs e)
        {
            buscarSolicitudes();
        }

        protected void btnSolicitarPauta_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Util.SessionHelper.getOperacionPauta()) && Util.SessionHelper.getOperacionPauta().Equals("d"))
            {
                devolverPautas();
            }
            else
            {
                solicitarPautas();
            }
        }

        protected void dgvPautaCanilla_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtSolicitada = (TextBox)e.Row.FindControl("txtSolicitada");
                Label lblSolicitada = (Label)e.Row.FindControl("lblSolicitada");
                TextBox txtDevuelta = (TextBox)e.Row.FindControl("txtDevuelta");
                Label lblDevuelta = (Label)e.Row.FindControl("lblDevuelta");
                if (!String.IsNullOrEmpty(Util.SessionHelper.getOperacionPauta()) && Util.SessionHelper.getOperacionPauta().Equals("d"))
                {
                    lblDevuelta.Visible = false;
                }
                else
                {
                    lblSolicitada.Visible = false;
                }
                
            }
        }
        #endregion

        #region "METODOS---------------------"
        protected void setControles()
        {
            clnFecha.SelectionMode = CalendarSelectionMode.Day;
            clnFecha.SelectedDate = DateTime.Today.AddDays(1);

            if (!String.IsNullOrEmpty(Util.SessionHelper.getOperacionPauta()) && Util.SessionHelper.getOperacionPauta().Equals("d"))
            {
                tituloPagina.InnerHtml = "Devolución de productos";
                btnSolicitarPauta.Text = "Registrar devolución";
                lblTxtCln.InnerText = "Fecha de devolución";
            }
            else
            {
                tituloPagina.InnerHtml = "Solicitud de pautas";
                btnSolicitarPauta.Text = "Solicitar pauta";
                lblTxtCln.InnerText = "Fecha de solicitud";
            }
        }

        protected void buscarSolicitudes()
        {
            spnMensaje.Attributes["class"] = string.Empty;
            spnMensaje.InnerText = string.Empty;
            spnMensaje.Visible = false;

            if (validarDatos())
            {
                BEPauta pauta = new BEPauta();
                pauta.codigoCanilla = txtCodigoCanilla.Text.Trim();
                pauta.fechaPauta = clnFecha.SelectedDate;
                pauta.estadoPauta = String.IsNullOrEmpty(Util.SessionHelper.getOperacionPauta()) ? "S" : "E";
                List<BEPauta> listaPautas = oBLPauta.selectPautasCanillas(pauta);

                var strMensaje = string.Empty;
                var strClass = string.Empty;

                if (listaPautas.Count > 0)
                {
                    dgvPautaCanilla.DataSource = listaPautas;
                    dgvPautaCanilla.DataBind();
                    divResultado.Visible = true;

                    if (!String.IsNullOrEmpty(Util.SessionHelper.getOperacionPauta()) && Util.SessionHelper.getOperacionPauta().Equals("d"))
                    {
                        dgvPautaCanilla.Columns[2].Visible = false;
                        dgvPautaCanilla.Columns[3].Visible = true;
                        dgvPautaCanilla.Columns[4].Visible = true;
                    }
                    else
                    {
                        dgvPautaCanilla.Columns[2].Visible = true;
                        dgvPautaCanilla.Columns[3].Visible = false;
                        dgvPautaCanilla.Columns[4].Visible = false;
                    }
                }
                else
                {
                    dgvPautaCanilla.DataSource = null;
                    dgvPautaCanilla.DataBind();
                    divResultado.Visible = false;
                    if (!String.IsNullOrEmpty(Util.SessionHelper.getOperacionPauta()) && Util.SessionHelper.getOperacionPauta().Equals("d"))
                    {
                        strMensaje = "No hay pautas entregadas para el canilla y/o fecha seleccionados";
                    }
                    else
                    {
                        strMensaje = "El canilla no tiene productos asignados";
                    }

                    strClass = "alert alert-warning";

                    spnMensaje.Attributes["class"] = strClass;
                    spnMensaje.InnerText = strMensaje;
                    spnMensaje.Visible = true;
                }

            }
        }

        protected Boolean validarDatos()
        {
            var resultado = false;

            if (String.IsNullOrEmpty(txtCodigoCanilla.Text) || String.IsNullOrEmpty(txtNombreCanilla.Text) || clnFecha.SelectedDate == DateTime.MinValue)
            {
                var strMensaje = string.Empty;
                var strClass = string.Empty;
                divResultado.Visible = false;
                strMensaje = "Se debe seleccionar un canilla y una fecha para la solicitud";
                strClass = "alert alert-warning";

                spnMensaje.Attributes["class"] = strClass;
                spnMensaje.InnerText = strMensaje;
                spnMensaje.Visible = true;
            }
            else
            {
                resultado = true;
            }

            return resultado;
        }

        protected void solicitarPautas()
        {
            List<BEPauta> listaPauta = new List<BEPauta>();
            BEPauta pauta;
            foreach (GridViewRow row in dgvPautaCanilla.Rows)
            {
                pauta = new BEPauta();
                pauta.codigoDistribuidor = ((Label)row.FindControl("lblCodigoDistribuidor")).Text.Trim();
                pauta.codigoAgencia = ((Label)row.FindControl("lblCodigoAgencia")).Text.Trim();
                pauta.codigoCanilla = ((Label)row.FindControl("lblCodigoCanilla")).Text.Trim();
                pauta.codigoEmpresa = ((Label)row.FindControl("lblCodigoEmpresa")).Text.Trim();
                pauta.codigoSector = ((Label)row.FindControl("lblCodigoSector")).Text.Trim();
                pauta.codigoProducto = row.Cells[0].Text.Trim();
                pauta.codigoCanal = ((Label)row.FindControl("lblCodigoCanal")).Text.Trim();
                pauta.codigoMotivoVenta = ((Label)row.FindControl("lblCodigoMotivoVenta")).Text.Trim();
                pauta.fechaPauta = clnFecha.SelectedDate;
                pauta.cantidadSolicitada = Convert.ToInt32(((TextBox)row.FindControl("txtSolicitada")).Text);
                listaPauta.Add(pauta);
            }

            var oDTOResultado = oBLPauta.grabarSolicitudPauta(listaPauta);

            listaPauta = (List<BEPauta>)oDTOResultado.Objeto;

            var strMensaje = string.Empty;
            var strClass = string.Empty;

            if (oDTOResultado.Codigo != (int)Constantes.CodigoSolicitarPauta.Ok)
            {
                strClass = "alert alert-warning";
                if (oDTOResultado.Codigo == (int)Constantes.CodigoSolicitarPauta.ErrorEnviadoASAP)
                {
                    strMensaje = "Ya se realizó la proyección de pautas para la fecha ingresada, no se puede actualizar la solicitud";
                }
                if (oDTOResultado.Codigo == (int)Constantes.CodigoSolicitarPauta.ErrorFechaSolicitud)
                {
                    strMensaje = "Sólo se pueden solicitar pautas para fechas posteriores a la fecha actual";
                }
            }
            else
            {
                strMensaje = "Registro de solicitudes de pauta exitoso";
                strClass = "alert alert-success";
            }

            spnMensaje.Attributes["class"] = strClass;
            spnMensaje.InnerText = strMensaje;
            spnMensaje.Visible = true;
        }

        protected void devolverPautas()
        {
            List<BEPauta> listaPauta = new List<BEPauta>();
            BEPauta pauta;
            foreach (GridViewRow row in dgvPautaCanilla.Rows)
            {
                pauta = new BEPauta();
                pauta.codigoDistribuidor = ((Label)row.FindControl("lblCodigoDistribuidor")).Text.Trim();
                pauta.codigoAgencia = ((Label)row.FindControl("lblCodigoAgencia")).Text.Trim();
                pauta.codigoCanilla = ((Label)row.FindControl("lblCodigoCanilla")).Text.Trim();
                pauta.codigoEmpresa = ((Label)row.FindControl("lblCodigoEmpresa")).Text.Trim();
                pauta.codigoSector = ((Label)row.FindControl("lblCodigoSector")).Text.Trim();
                pauta.codigoProducto = row.Cells[0].Text.Trim();
                pauta.codigoCanal = ((Label)row.FindControl("lblCodigoCanal")).Text.Trim();
                pauta.codigoMotivoVenta = ((Label)row.FindControl("lblCodigoMotivoVenta")).Text.Trim();
                pauta.fechaPauta = clnFecha.SelectedDate;
                pauta.cantidadDevuelta = Convert.ToInt32(((TextBox)row.FindControl("txtDevuelta")).Text);
                listaPauta.Add(pauta);
            }

            var oDTOResultado = oBLPauta.grabarDevolverProductos(listaPauta);

            listaPauta = (List<BEPauta>)oDTOResultado.Objeto;

            var strMensaje = string.Empty;
            var strClass = string.Empty;

            if (oDTOResultado.Codigo != (int)Constantes.CodigoDevolverProductos.Ok)
            {
                strClass = "alert alert-warning";
                if (oDTOResultado.Codigo == (int)Constantes.CodigoDevolverProductos.Error)
                {
                    strMensaje = "Ya se registró la devolución para el canilla y la fecha seleccionados";
                }
            }
            else
            {
                strMensaje = "Registro de devolución de productos exitoso";
                strClass = "alert alert-success";
            }

            spnMensaje.Attributes["class"] = strClass;
            spnMensaje.InnerText = strMensaje;
            spnMensaje.Visible = true;
        }
        #endregion
    }
}