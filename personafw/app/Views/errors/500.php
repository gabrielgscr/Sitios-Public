<div class="error-page text-center mx-auto">
    <div class="display-4 text-danger"><i class="bi bi-database-exclamation"></i></div>
    <h1 class="h3"><?= e($title) ?></h1>
    <p class="text-secondary"><?= e($message) ?></p>
    <a href="<?= e(url()) ?>" class="btn btn-primary">Intentar de nuevo</a>
</div>

