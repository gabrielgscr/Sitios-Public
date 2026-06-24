<?php
include 'data.php';
$personas = getPersonas();
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Listado de Personas</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <h1 class="mt-5">Listado de Personas</h1>
        <table class="table table-striped mt-3">
            <thead>
                <tr>
                    <th>PersonaID</th>
                    <th>Nombre</th>
                    <th>Tipo</th>
                    <th>Gender</th>
                    <th>Password</th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($personas as $persona): ?>
                    <tr>
                        <td><?php echo htmlspecialchars($persona['PersonaID']); ?></td>
                        <td><?php echo htmlspecialchars($persona['Nombre']); ?></td>
                        <td><?php echo htmlspecialchars($persona['Tipo']); ?></td>
                        <td><?php echo htmlspecialchars($persona['Gender']); ?></td>
                        <td><?php echo htmlspecialchars($persona['Password']); ?></td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    </div>
    <!-- <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.2/dist/umd/popper.min.js"></script> -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>