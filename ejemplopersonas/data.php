<?php
include 'config.php';

function getPersonas() {
    global $serverName, $connectionOptions;

    // Conectar a la base de datos
    $conn = sqlsrv_connect($serverName, $connectionOptions);
    if ($conn === false) {
        die(print_r(sqlsrv_errors(), true));
    }

    // Consulta para obtener los datos de la tabla Persona
    $sql = "SELECT PersonaID, Nombre, Tipo, Gender, Password FROM dbo.Persona";
    $stmt = sqlsrv_query($conn, $sql);
    if ($stmt === false) {
        die(print_r(sqlsrv_errors(), true));
    }

    $personas = [];
    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        $personas[] = $row;
    }

    // Cerrar la conexión
    sqlsrv_free_stmt($stmt);
    sqlsrv_close($conn);

    return $personas;
}
?>