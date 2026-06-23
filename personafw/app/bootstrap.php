<?php

declare(strict_types=1);

spl_autoload_register(static function (string $class): void {
    $prefix = 'App\\';

    if (!str_starts_with($class, $prefix)) {
        return;
    }

    $relativeClass = substr($class, strlen($prefix));
    $file = __DIR__.'/'.str_replace('\\', '/', $relativeClass).'.php';

    if (is_file($file)) {
        require $file;
    }
});

function render(string $view, array $data = []): void
{
    extract($data, EXTR_SKIP);

    $viewFile = __DIR__.'/Views/'.$view.'.php';

    if (!is_file($viewFile)) {
        throw new RuntimeException("La vista {$view} no existe.");
    }

    require __DIR__.'/Views/layouts/header.php';
    require $viewFile;
    require __DIR__.'/Views/layouts/footer.php';
}

function redirect(string $url): never
{
    header("Location: {$url}");
    exit;
}

function e(?string $value): string
{
    return htmlspecialchars($value ?? '', ENT_QUOTES | ENT_SUBSTITUTE, 'UTF-8');
}

function url(string $action = 'index', array $parameters = []): string
{
    return 'index.php?'.http_build_query(['action' => $action] + $parameters);
}

