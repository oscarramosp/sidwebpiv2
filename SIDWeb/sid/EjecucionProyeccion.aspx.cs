using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BELayer;
using BLLayer;

namespace sid
{
    public partial class EjecucionProyeccion : System.Web.UI.Page
    {
        BLPauta oBLPauta = new BLPauta();

        #region "EVENTOS---------------------"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                clnFecha.SelectionMode = CalendarSelectionMode.Day;
                clnFecha.SelectedDate = DateTime.Today;
                cargarPautasFecha();
            }
        }

        protected void clnFecha_SelectionChanged(object sender, EventArgs e)
        {
            cargarPautasFecha();
        }

        protected void btnProyectarPauta_Click(object sender, EventArgs e)
        {
            var pauta = new BEPauta();
            pauta.fechaPauta = clnFecha.SelectedDate;

            pauta.fechaPauta = pauta.fechaPauta.Value.AddHours(DateTime.Now.Hour);
            pauta.fechaPauta = pauta.fechaPauta.Value.AddMinutes(DateTime.Now.Minute);

            var oDTOResultado = oBLPauta.proyectarPautas(pauta);

            pauta = (BEPauta)oDTOResultado.Objeto;
            var strMensaje = string.Empty;
            var strClass = string.Empty;

            if (oDTOResultado.Codigo != (int)Constantes.CodigoProyectarPauta.Ok)
            {
                strClass = "alert alert-warning";
                if (oDTOResultado.Codigo == (int)Constantes.CodigoProyectarPauta.FechaProyeccionIncorrecta)
                {
                    strMensaje = "La fecha a proyectar debe ser mayor a la fecha actual";
                }
                else if (oDTOResultado.Codigo == (int)Constantes.CodigoProyectarPauta.FueraDeHorario)
                {
                    strMensaje = "La proyección sólo se puede ejecutar entre la(s) " + pauta.horaInicioMin.Value.ToShortTimeString() + " y la(s) " + pauta.horaInicioMax.Value.ToShortTimeString();
                }
                else if (oDTOResultado.Codigo == (int)Constantes.CodigoProyectarPauta.FormulaNoDefinida)
	            {
                    strMensaje = "No se ha definido la fórmula de proyección";
	            }
                else if (oDTOResultado.Codigo == (int)Constantes.CodigoProyectarPauta.EstadoFueraFlujo)
                {
                    strMensaje = "Las pautas para la fecha ingresada ya fueron procesadas, no se puede ejecutar la proyección";
                }
                else
	            {
                    strMensaje = "Ocurrió un error al proyectar las pautas";
	            }
            }
            else
            {
                cargarPautasFecha();
                strMensaje = "Pautas proyectadas exitosamente";
                strClass = "alert alert-success";
            }
            spnMensaje.Attributes["class"] = strClass;
            spnMensaje.InnerText = strMensaje;
            spnMensaje.Visible = true;
            //upCalendario.Update();
        }
        #endregion

        #region "METODOS---------------------"
        protected void cargarPautasFecha()
        {
            spnMensaje.Attributes["class"] = string.Empty;
            spnMensaje.InnerText = string.Empty;
            spnMensaje.Visible = false;

            BEPauta pauta = new BEPauta();
            pauta.fechaPauta = clnFecha.SelectedDate;
            List<BEPauta> listaPautas = oBLPauta.selectPautaProducto(pauta);

            var strMensaje = string.Empty;
            var strClass = string.Empty;

            if (listaPautas.Count > 0)
            {
                dgvPauta.DataSource = listaPautas;
                dgvPauta.DataBind();
                divResultado.Visible = true;
            }
            else
            {
                dgvPauta.DataSource = null;
                dgvPauta.DataBind();
                divResultado.Visible = false;
                strMensaje = "No se han solicitado pautas para la fecha " + clnFecha.SelectedDate.ToString("dd/MM/yyyy");
                strClass = "alert alert-warning";

                spnMensaje.Attributes["class"] = strClass;
                spnMensaje.InnerText = strMensaje;
                spnMensaje.Visible = true;
            }
        }
        #endregion
    }
}
