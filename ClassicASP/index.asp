<%@ Language="VBScript" %>
<!DOCTYPE html>
<html>
<head>
    <title>Mi P&aacute;gina ASP Cl&aacute;sico</title>
    <!-- Enlace a la hoja de estilos de Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <h1 class="mt-5">Resultados de la Base de Datos</h1>
        <table class='table table-bordered table-striped table-hover mt-3'>
        <thead class='thead-dark'><tr><th>PersonaID</th><th>Nombre</th></tr></thead>
        <tbody>
        <% 
        ' Espacio para insertar código de conexión a la base de datos
        Dim conn, rs
        Set conn = Server.CreateObject("ADODB.Connection")
        conn.Open "Provider=SQLOLEDB;Data Source=(local);Initial Catalog=Ejemplo2;User ID=sa;Password=WRITEYOURPASSWORD!;"
        
        Set rs = conn.Execute("SELECT [PersonaID],[Nombre],[Tipo],[Gender] FROM [dbo].[Persona]")
        
        If Not rs.EOF Then
            Do While Not rs.EOF
                Response.Write "<tr>"
                Response.Write "<td>" & rs("PersonaID") & "</td>"
                Response.Write "<td>" & rs("Nombre") & "</td>"
                Response.Write "</tr>"
                rs.MoveNext
            Loop
            Response.Write ""
        Else
            Response.Write "<div class='alert alert-warning mt-3'>No se encontraron resultados.</div>"
        End If
        
        rs.Close
        Set rs = Nothing
        conn.Close
        Set conn = Nothing
        %>
        </tbody>
        </table>
    </div>
    <!-- Enlace a los scripts de Bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>