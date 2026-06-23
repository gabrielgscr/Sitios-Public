<?php

declare(strict_types=1);

$localConfig = __DIR__.'/database.local.php';

if (!is_file($localConfig)) {
    throw new RuntimeException(
        'Falta app/Config/database.local.php. Copia database.example.php y configura la conexión.'
    );
}

return require $localConfig;

