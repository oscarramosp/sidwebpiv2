<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SolicitudPautas.aspx.cs" Inherits="sid.SolicitudPautas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function devolverCanilla(codigoCanilla, nombreCanilla) {
            document.getElementById("ctl00_ContentPlaceHolder2_txtCodigoCanilla").value = codigoCanilla;
            document.getElementById("ctl00_ContentPlaceHolder2_txtNombreCanilla").value = nombreCanilla;
            document.getElementById('ctl00_ContentPlaceHolder2_btnGenerarEvento').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 runat="server" id="tituloPagina" >Solicitud de pautas</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="smFiltros" runat="server">
    </asp:ScriptManager>
    <div class="form-group">
        <label for='<%=txtCodigoCanilla.ClientID%>' class="col-sm-2 control-label">Canilla</label>
        <div class="col-sm-2">
            <asp:TextBox ID="txtCodigoCanilla" runat="server" Width="100px" 
                CssClass="form-control" Enabled="false" ontextchanged="txtCodigoCanilla_TextChanged"></asp:TextBox>
            <asp:Button CssClass="no-display" ID="btnGenerarEvento" runat="server" useSubmitBehavior="false"/>
        </div>
        <div class="col-sm-6">
            <asp:TextBox ID="txtNombreCanilla" runat="server" Width="350px" CssClass="form-control" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <button id="btnBuscar" type="button" class="btn btn-default">
                <span class="glyphicon glyphicon-search"></span>
            </button>
        </div>
    </div>
    <asp:UpdatePanel ID="upSolicitudPauta" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-group">
                <label id="lblTxtCln" runat="server" for='<%=clnFecha.ClientID%>' class="col-sm-3 control-label">Fecha de solicitud</label>
                <div class="col-sm-9">
                    <asp:Calendar ID="clnFecha" runat="server" 
                        onselectionchanged="clnFecha_SelectionChanged" ></asp:Calendar>
                    <!-- <asp:TextBox ID="txtFechaProyeccion" runat="server" Width="100" CssClass="classInput" MaxLength="10"></asp:TextBox> -->
                </div>
            </div>
            <div class="form-group">
                <div class=" col-sm-offset-1 col-sm-11">
                    <div id="divResultado" runat="server">
                        <asp:GridView id="dgvPautaCanilla" runat="server" AutoGenerateColumns="False" Width="670px"
                            BorderColor="#ECECEC" BorderStyle="Solid" ShowHeader="true" 
                            onrowdatabound="dgvPautaCanilla_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="codigoProducto" HeaderText="Código del producto">
                                    <ItemStyle HorizontalAlign="Center" Width="150px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcionProducto" HeaderText="Nombre del producto">
                                    <ItemStyle HorizontalAlign="Left" Width="220px"/>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Cantidad a solicitar">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSolicitada" runat="server" CssClass="form-control" Text='<%# Bind("cantidadSolicitada") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <asp:Label ID="lblSolicitada" runat="server" Text='<%# Bind("cantidadSolicitada") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cantidadEntregada" HeaderText="Cantidad entregada">
                                    <ItemStyle HorizontalAlign="Center" Width="150px"/>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Cantidad a devolver" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDevuelta" runat="server" CssClass="form-control" Text='<%# Bind("cantidadDevuelta") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <asp:Label ID="lblDevuelta" runat="server" Text='<%# Bind("cantidadDevuelta") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código del Distribuidor" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoDistribuidor" runat="server" Text='<%# Bind("codigoDistribuidor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código de la Agencia" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoAgencia" runat="server" Text='<%# Bind("codigoAgencia") %>'></asp:Label>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código del Canilla" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoCanilla" runat="server" Text='<%# Bind("codigoCanilla") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código de la Empresa" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoEmpresa" runat="server" Text='<%# Bind("codigoEmpresa") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código del Sector" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoSector" runat="server" Text='<%# Bind("codigoSector") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código del Canal" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoCanal" runat="server" Text='<%# Bind("codigoCanal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código del Motivo de Venta" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoMotivoVenta" runat="server" Text='<%# Bind("codigoMotivoVenta") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div id="spnMensaje" runat="server"></div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="clnFecha" EventName="SelectionChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnGenerarEvento" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnSolicitarPauta" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="form-group">
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button ID="btnSolicitarPauta" runat="server" Text="Solicitar pauta" 
                class="btn btn-default" onclick="btnSolicitarPauta_Click"/>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#btnBuscar").click(function() {
                return abrirBusqueda();
            });
            function abrirBusqueda() {
                window.open("../ConsultaCanillas.aspx", "_blank", "toolbar=no, scrollbars=yes, resizable=0, top=225, left=300, width=870, height=400");
            }
            $("#<%= btnSolicitarPauta.ClientID %>").click(function() {
                return validar();
            });
            function validar() {
                var codigoCanilla = $.trim($("#<%= txtCodigoCanilla.ClientID %>").val());
                var nombreCanilla = $.trim($("#<%= txtNombreCanilla.ClientID %>").val());

                var mensaje = '';

                if (nombreCanilla == '')
                    mensaje += "<li>Ingrese el canilla.</li>";

                var cantProductos = 0;
                var errorEnGrilla = false;

                $("#<%= dgvPautaCanilla.ClientID %> tr").each(function() {

                    if (cantProductos >= 1) {

                        if (window.location.href.substring(window.location.href.length - 1) == 'd') {
                            var cantidadDevuelta = $(this).children("td").eq(3).find('input').eq(0).val();
                            var cantidadEntregada = $(this).children("td").eq(2).html();

                            if (cantidadDevuelta == '' || !esEnteroValido(cantidadDevuelta)) {
                                errorEnGrilla = true;
                                mensaje += "<li>Las cantidades a devolver deben tener un valor mayor o igual a cero (0).</li>";
                            }

                            if (!errorEnGrilla && parseInt(cantidadEntregada) < parseInt(cantidadDevuelta)) {
                                errorEnGrilla = true;
                                mensaje += "<li>Las cantidades a devolver no pueden ser superiores a las cantidades etregadas (" + cantidadDevuelta + ">" + cantidadEntregada + ").</li>";
                            }
                        }
                        else {
                            var cantidadSolicitada = $(this).children("td").eq(2).find('input').eq(0).val();

                            if (cantidadSolicitada == '' || !esEnteroValido(cantidadSolicitada)) {
                                errorEnGrilla = true;
                                mensaje += "<li>Las cantidades solicitadas deben tener un valor mayor o igual a cero (0).</li>";
                            }
                        }
                        if (errorEnGrilla)
                            return;
                    }

                    cantProductos++;
                });
                if (cantProductos == 0)
                    mensaje += "<li>No hay pautas a procesar.</li> <li>Verifique que se hayan seleccionado un canilla y la fecha de la pauta para realizar la búsqueda.</li>";

                if (mensaje != '') {
                    mensaje = '<ul>' + mensaje + '</ul>';
                    jcAlert('Datos Incompletos', mensaje, function(choice) { return choice });
                    return false;
                }

                var titulo = '';
                var pregunta = '';

                if (window.location.href.substring(window.location.href.length - 1) == 'd') {
                    titulo = 'Devolver productos';
                    pregunta = '¿Desea registrar las devoluciones de los productos?';
                } else {
                    titulo = 'Solicitar pauta';
                    pregunta = '¿Desea registrar la solicitudd de pauta?';
                }

                jcConfirm(titulo, pregunta, function(choice) {

                    if (choice) {
                        var event = "<%= btnSolicitarPauta.ClientID %>";
                        event = event.replace(/_/g, "$");
                        __doPostBack(event, '');
                    }
                });

                return false;
            }
        });
    </script>
</asp:Content>
