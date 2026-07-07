<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EjemploWebService.PruebasWeb._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Prueba de servicio de personas</title>
    <style type="text/css">
        body { font-family: Arial, Helvetica, sans-serif; margin: 20px; }
        .panel { border: 1px solid #ccc; padding: 12px; margin-bottom: 16px; }
        .row { margin-bottom: 8px; }
        label { display: inline-block; width: 140px; font-weight: bold; vertical-align: top; }
        input[type=text], input[type=password] { width: 320px; }
        .actions button, .actions input { margin-right: 8px; }
        .mensaje { margin: 12px 0; font-weight: bold; }
        .ok { color: #1a7f37; }
        .error { color: #b42318; }
        table { border-collapse: collapse; width: 100%; }
        th, td { border: 1px solid #ddd; padding: 6px 8px; }
        th { background: #f4f4f4; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel">
            <h2>Conexión</h2>
            <div class="row">
                <label for="txtServicioUrl">URL del servicio</label>
                <asp:TextBox ID="txtServicioUrl" runat="server" />
            </div>
            <div class="actions">
                <asp:Button ID="btnCargarPersonas" runat="server" Text="Obtener personas" OnClick="btnCargarPersonas_Click" />
                <asp:Button ID="btnCargarRoles" runat="server" Text="Obtener roles" OnClick="btnCargarRoles_Click" />
            </div>
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />
        </div>

        <div class="panel">
            <h2>Persona</h2>
            <div class="row">
                <label for="txtPersonaId">PersonaID</label>
                <asp:TextBox ID="txtPersonaId" runat="server" />
            </div>
            <div class="row">
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" />
            </div>
            <div class="row">
                <label for="txtTipo">Tipo</label>
                <asp:TextBox ID="txtTipo" runat="server" />
            </div>
            <div class="row">
                <label for="txtGender">Gender</label>
                <asp:TextBox ID="txtGender" runat="server" />
            </div>
            <div class="row">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            </div>
            <div class="actions">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar persona" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar persona" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar persona" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <div class="panel">
            <h2>Personas</h2>
            <asp:GridView ID="gvPersonas" runat="server" AutoGenerateColumns="true" />
        </div>

        <div class="panel">
            <h2>Roles</h2>
            <div class="actions">
                <asp:Button ID="btnCargarTelefonos" runat="server" Text="Obtener teléfonos de la persona" OnClick="btnCargarTelefonos_Click" />
            </div>
            <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="true" />
        </div>

        <div class="panel">
            <h2>Teléfonos</h2>
            <asp:GridView ID="gvTelefonos" runat="server" AutoGenerateColumns="true" />
        </div>
    </form>
</body>
</html>
