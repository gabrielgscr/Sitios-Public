<?php

declare(strict_types=1);

use App\Controllers\PersonaController;
use App\Core\Database;
use App\Core\Session;
use App\Repositories\PersonaRepository;

require __DIR__.'/app/bootstrap.php';

Session::start();

$config = require __DIR__.'/app/Config/database.php';
$repository = new PersonaRepository(Database::connect($config));
$controller = new PersonaController($repository);

$action = filter_input(INPUT_GET, 'action') ?: 'index';

try {
    match ($action) {
        'index' => $controller->index(),
        'create' => $controller->create(),
        'store' => $controller->store(),
        'edit' => $controller->edit(),
        'update' => $controller->update(),
        'delete' => $controller->delete(),
        default => $controller->notFound(),
    };
} catch (Throwable $exception) {
    http_response_code(500);

    error_log($exception->__toString());

    render('errors/500', [
        'title' => 'Error de base de datos',
        'message' => 'No fue posible completar la operación. Revisa la configuración, la tabla dbo.Persona y el registro de errores de PHP.',
    ]);
}
