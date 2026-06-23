<?php

declare(strict_types=1);

namespace App\Core;

use PDO;

final class Database
{
    private function __construct()
    {
    }

    public static function connect(array $config): PDO
    {
        $dsn = sprintf(
            'sqlsrv:Server=%s;Database=%s',
            $config['server'],
            $config['database'],
        );

        return new PDO(
            $dsn,
            $config['username'],
            $config['password'],
            [
                PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
                PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
                PDO::ATTR_EMULATE_PREPARES => false,
            ],
        );
    }
}

