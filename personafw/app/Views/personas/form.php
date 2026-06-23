<?php
$isEdit = $id !== null;
$formUrl = url($action, $isEdit ? ['id' => $id] : []);
?>
<div class="form-shell mx-auto">
    <a href="<?= e(url()) ?>" class="back-link d-inline-flex align-items-center gap-2 mb-3">
        <i class="bi bi-arrow-left"></i>Volver al listado
    </a>
    <div class="mb-4">
        <p class="section-kicker mb-1"><?= $isEdit ? 'Edición' : 'Nuevo registro' ?></p>
        <h1 class="h2 fw-bold mb-1"><?= e($title) ?></h1>
        <p class="text-secondary mb-0">Los campos marcados con asterisco son obligatorios.</p>
    </div>

    <section class="card border-0 shadow-sm">
        <div class="card-body p-4 p-lg-5">
            <?php if ($errors !== []): ?>
                <div class="alert alert-danger" role="alert">
                    <i class="bi bi-exclamation-circle-fill me-2"></i>
                    Revisa los campos señalados.
                </div>
            <?php endif; ?>

            <form method="post" action="<?= e($formUrl) ?>" novalidate>
                <input type="hidden" name="_token" value="<?= e(\App\Core\Session::csrfToken()) ?>">

                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="persona_id" class="form-label">Identificación <span class="text-danger">*</span></label>
                        <input type="text" id="persona_id" name="persona_id" maxlength="50" required autofocus
                               value="<?= e($persona['persona_id'] ?? $persona['PersonaID'] ?? '') ?>"
                               class="form-control form-control-lg <?= isset($errors['persona_id']) ? 'is-invalid' : '' ?>">
                        <?php if (isset($errors['persona_id'])): ?>
                            <div class="invalid-feedback"><?= e($errors['persona_id']) ?></div>
                        <?php endif; ?>
                    </div>

                    <div class="col-md-6">
                        <label for="nombre" class="form-label">Nombre completo <span class="text-danger">*</span></label>
                        <input type="text" id="nombre" name="nombre" maxlength="50" required
                               value="<?= e($persona['nombre'] ?? $persona['Nombre'] ?? '') ?>"
                               class="form-control form-control-lg <?= isset($errors['nombre']) ? 'is-invalid' : '' ?>">
                        <?php if (isset($errors['nombre'])): ?>
                            <div class="invalid-feedback"><?= e($errors['nombre']) ?></div>
                        <?php endif; ?>
                    </div>

                    <div class="col-md-4">
                        <label for="tipo" class="form-label">Tipo <span class="text-danger">*</span></label>
                        <input type="number" id="tipo" name="tipo" min="0" max="255" required
                               value="<?= e((string) ($persona['tipo'] ?? $persona['Tipo'] ?? '')) ?>"
                               class="form-control form-control-lg <?= isset($errors['tipo']) ? 'is-invalid' : '' ?>">
                        <?php if (isset($errors['tipo'])): ?>
                            <div class="invalid-feedback"><?= e($errors['tipo']) ?></div>
                        <?php endif; ?>
                    </div>

                    <div class="col-md-4">
                        <label for="gender" class="form-label">Género <span class="text-danger">*</span></label>
                        <input type="text" id="gender" name="gender" maxlength="10" required
                               placeholder="Ej.: female, male, F"
                               value="<?= e($persona['gender'] ?? $persona['Gender'] ?? '') ?>"
                               class="form-control form-control-lg <?= isset($errors['gender']) ? 'is-invalid' : '' ?>">
                        <?php if (isset($errors['gender'])): ?>
                            <div class="invalid-feedback"><?= e($errors['gender']) ?></div>
                        <?php endif; ?>
                    </div>

                    <div class="col-md-4">
                        <label for="password" class="form-label">
                            Contraseña <?= $isEdit ? '' : '<span class="text-danger">*</span>' ?>
                        </label>
                        <input type="password" id="password" name="password" maxlength="72"
                               <?= $isEdit ? '' : 'required' ?>
                               class="form-control form-control-lg <?= isset($errors['password']) ? 'is-invalid' : '' ?>">
                        <?php if ($isEdit): ?>
                            <div class="form-text">Déjala vacía para conservar la actual.</div>
                        <?php endif; ?>
                        <?php if (isset($errors['password'])): ?>
                            <div class="invalid-feedback"><?= e($errors['password']) ?></div>
                        <?php endif; ?>
                    </div>
                </div>

                <div class="d-flex flex-column-reverse flex-sm-row justify-content-end gap-2 mt-4 pt-3 border-top">
                    <a href="<?= e(url()) ?>" class="btn btn-light btn-lg px-4">Cancelar</a>
                    <button type="submit" class="btn btn-primary btn-lg px-4">
                        <i class="bi bi-check-lg me-2"></i><?= e($submitLabel) ?>
                    </button>
                </div>
            </form>
        </div>
    </section>
</div>
