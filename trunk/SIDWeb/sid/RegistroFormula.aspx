<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RegistroFormula.aspx.cs" Inherits="sid.RegistroFormula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Fórmula de proyección de pauta</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="smFiltros" runat="server">
    </asp:ScriptManager>
    <div class="form-group">
        <label for='<%=txtCodigoFormula.ClientID%>' class="col-sm-3 control-label">Código de fórmula</label>
        <div class="col-sm-9">
            <asp:TextBox ID="txtCodigoFormula" runat="server" Width="402px" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label for='<%=txtEditor.ClientID%>' class="col-sm-3 control-label">Definición de fórmula</label>
        <div class="col-sm-9">
            <asp:textbox id="txtEditor" runat="server" Width="600px" BorderColor="#999999" CssClass="form-control"
				BorderWidth="1px" TextMode="MultiLine" Height="167"></asp:textbox>
        </div>
    </div>
    <div class="form-group">
        <label for='<%=ddlOperadores.ClientID%>' class="col-sm-2 control-label">Operadores</label>
        <div class="col-sm-4">
            <asp:DropDownList ID="ddlOperadores" runat="server" AutoPostBack="True" Width="220px"
                onselectedindexchanged="ddlOperadores_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            <asp:listbox id="lstOperadores" runat="server" Width="220px" CssClass="form-control" Height="130px"></asp:listbox>
        </div>        
        <label for='<%=lstVariables.ClientID%>' class="col-sm-2 control-label">Variables</label>
        <div class="col-sm-4">
            <asp:listbox id="lstVariables" runat="server" Width="220px" CssClass="form-control" Height="130px"></asp:listbox>
        </div>
    </div>
    <div class="form-group">
        <span ></span>
        <div id="spnMensaje" runat="server"></div>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-6 col-sm-6">
            <asp:Button ID="btnGrabarFx" runat="server" Text="Grabar" onclick="btnGrabarFx_Click" class="btn btn-default"/>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= btnGrabarFx.ClientID %>").click(function() {
                return validar();
            });
        });

        function validar() {
            var formula = $.trim($("#<%= txtEditor.ClientID %>").val());
            var mensaje = '';

            if (formula == '')
                mensaje += "<li>La fórmula no puede estar en blanco.</li>";

            if (mensaje != '') {
                mensaje = '<ul>' + mensaje + '</ul>';
                jcAlert('Datos Incompletos', mensaje, function(choice) { return choice });
                return false;
            }

            jcConfirm('Actualizar Formula', '¿Desea actualizar la fórmula?', function(choice) {

                if (choice) {
                    var event = "<%= btnGrabarFx.ClientID %>";
                    event = event.replace(/_/g, "$");
                    __doPostBack(event, '');
                }
            });

            return false;
        }   
    </script>
</asp:Content>