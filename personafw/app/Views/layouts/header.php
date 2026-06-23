<!doctype html>
<html lang="es">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title><?= e($title ?? 'Personas') ?> | PHP + PDO</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet"
          integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB"
          crossorigin="anonymous">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css" rel="stylesheet">
    <link href="public/assets/css/app.css" rel="stylesheet">
</head>
<body>
<nav class="navbar navbar-dark app-navbar shadow-sm">
    <div class="container">
        <a class="navbar-brand fw-semibold" href="<?= e(url()) ?>">
            <i class="bi bi-people-fill me-2"></i>Personas
        </a>
    </div>
</nav>
<main class="container py-4 py-lg-5">

