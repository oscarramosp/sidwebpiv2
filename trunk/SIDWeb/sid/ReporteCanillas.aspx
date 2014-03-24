<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ReporteCanillas.aspx.cs" Inherits="sid.ReporteCanillas" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Reporte de Canillas</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="smReporte" runat="server">
    </asp:ScriptManager>
    
    <rsweb:ReportViewer ID="rptVwCanillas" runat="server" Width="860" Height="300">
    </rsweb:ReportViewer>
    
    <script type="text/javascript">
        $(document).ready(function() {
        });  
    </script>
</asp:Content>
