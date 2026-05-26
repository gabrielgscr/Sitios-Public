<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPNetSample.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Listado de usuarios</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlMessage" CssClass="alert alert-danger" runat="server">
        <asp:Label ID="lblMessage" Text="text" runat="server" />
    </asp:Panel>
    <asp:GridView ID="gvMain" runat="server" CssClass="table table-striped mt-3"></asp:GridView>
</asp:Content>
