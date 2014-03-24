<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaCanillas.aspx.cs" Inherits="sid.ConsultaCanillas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SID</title>
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="js/util.js"></script>
    <script type="text/javascript" src="js/moog.custom.alert.js"></script>
    <script type="text/javascript" src="js/sidd.js"></script>
    <link href="css/jquery-ui.css" rel="Stylesheet"/>
    <link href="css/custom.css" rel="Stylesheet"/>
    <link href="css/bootstrap.min.css" rel="Stylesheet"/>
    <link href="css/bootstrap-theme.min.css" rel="Stylesheet"/>
    <script type="text/javascript">
        function devolver(codigoCanilla, nombreCanilla) {
            window.opener.devolverCanilla(codigoCanilla, nombreCanilla);
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" role="form">
    <asp:ScriptManager ID="smFiltros" runat="server">
    </asp:ScriptManager>
    <div class="container bs-docs-container">
        <div class="row">
            <div class="col-md-12" role="main">
                <fieldset>
                    <legend>Conuslta de Canillas</legend>
                    <div class="form-group">
                        <label for='<%=txtCodigoCanilla.ClientID%>' class="col-sm-2 control-label">Código de Canilla</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtCodigoCanilla" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label for='<%=txtNombreCanilla.ClientID%>' class="col-sm-2 control-label">Nombre del Canilla</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtNombreCanilla" runat="server" Width="350px" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for='<%=ddlTipoDocumento.ClientID%>' class="col-sm-2 control-label">Tipo de documento</label>
                        <div class="col-sm-3">
							<asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="150px" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <label for='<%=txtNumeroDocumento.ClientID%>' class="col-sm-2 control-label">Número de documento</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtNumeroDocumento" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                        </div>
						<div class="col-sm-2">
							<asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-default" 
                                onclick="btnBuscar_Click"/>
						</div>
                    </div>
                </fieldset>
                <div class="form-group">
                    <div class="col-lg-9 col-lg-offset-2">
                        <div id="divResultado" runat="server">
                            <div style="overflow: auto; max-height: 850px;">
                                <asp:GridView id="dgvCanillas" runat="server" AutoGenerateColumns="False" Width="820px"
                                    BorderColor="#ECECEC" BorderStyle="Solid" ShowHeader="true" 
                                    onrowdatabound="dgvCanillas_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="codigoCanilla" HeaderText="Código del canilla">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" Font-Underline="true"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombreCompletoCanilla" HeaderText="Nombre del canilla">
                                            <ItemStyle HorizontalAlign="Left" Width="250px"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipoDocumento" HeaderText="Tipo documento">
                                            <ItemStyle HorizontalAlign="Center" Width="120px"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numeroDocumento" HeaderText="Número documento">
                                            <ItemStyle HorizontalAlign="Left" Width="150px"/>
                                        </asp:BoundField>
									    <asp:BoundField DataField="nombreAgencia" HeaderText="Agencia">
                                            <ItemStyle HorizontalAlign="Left" Width="170px"/>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div id="spnMensaje" runat="server"></div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-5 col-sm-7">
                        <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" class="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= btnCerrar.ClientID %>").click(function() {
                return cerrar();
            });
            $("#btnCerrar2").click(function() {
                return cerrar();
            });
            function cerrar() {
                window.close();
            }
        });
    </script>
    </form>
</body>
</html>
