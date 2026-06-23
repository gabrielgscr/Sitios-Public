<?php if ($success): ?>
    <div class="alert alert-success alert-dismissible fade show shadow-sm" role="alert">
        <i class="bi bi-check-circle-fill me-2"></i><?= e($success) ?>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Cerrar"></button>
    </div>
<?php endif; ?>

<div class="d-flex flex-column flex-sm-row justify-content-between align-items-sm-center gap-3 mb-4">
    <div>
        <p class="section-kicker mb-1">Administración</p>
        <h1 class="h2 fw-bold mb-0">Personas</h1>
    </div>
    <a href="<?= e(url('create')) ?>" class="btn btn-primary btn-lg shadow-sm">
        <i class="bi bi-person-plus-fill me-2"></i>Nueva persona
    </a>
</div>

<section class="card border-0 shadow-sm overflow-hidden">
    <?php if ($personas === []): ?>
        <div class="empty-state text-center">
            <div class="empty-icon mx-auto mb-3"><i class="bi bi-people"></i></div>
            <h2 class="h5">Todavía no hay personas</h2>
            <p class="text-secondary">Crea el primer registro para comenzar.</p>
            <a href="<?= e(url('create')) ?>" class="btn btn-primary">Crear persona</a>
        </div>
    <?php else: ?>
        <div class="table-responsive">
            <table class="table table-hover align-middle mb-0">
                <thead>
                <tr>
                    <th>Persona</th>
                    <th>Tipo</th>
                    <th>Género</th>
                    <th class="text-end">Acciones</th>
                </tr>
                </thead>
                <tbody>
                <?php foreach ($personas as $persona): ?>
                    <?php
                    $fullName = $persona['Nombre'];
                    $initials = mb_strtoupper(mb_substr($fullName, 0, 2));
                    ?>
                    <tr>
                        <td>
                            <div class="d-flex align-items-center gap-3">
                                <span class="avatar"><?= e($initials) ?></span>
                                <div>
                                    <div class="fw-semibold"><?= e($fullName) ?></div>
                                    <div class="text-secondary small">ID <?= e($persona['PersonaID']) ?></div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <span class="badge text-bg-light border"><?= (int) $persona['Tipo'] ?></span>
                        </td>
                        <td><?= e($persona['Gender']) ?></td>
                        <td>
                            <div class="d-flex justify-content-end gap-2">
                                <a href="<?= e(url('edit', ['id' => $persona['PersonaID']])) ?>"
                                   class="btn btn-sm btn-outline-primary">
                                    <i class="bi bi-pencil-square"></i>
                                    <span class="d-none d-lg-inline ms-1">Editar</span>
                                </a>
                                <button type="button" class="btn btn-sm btn-outline-danger"
                                        data-bs-toggle="modal" data-bs-target="#deleteModal"
                                        data-delete-url="<?= e(url('delete', ['id' => $persona['PersonaID']])) ?>"
                                        data-person-name="<?= e($fullName) ?>">
                                    <i class="bi bi-trash3"></i>
                                    <span class="d-none d-lg-inline ms-1">Eliminar</span>
                                </button>
                            </div>
                        </td>
                    </tr>
                <?php endforeach; ?>
                </tbody>
            </table>
        </div>

        <?php if ($lastPage > 1): ?>
            <div class="card-footer bg-white px-3 px-md-4 py-3">
                <nav aria-label="Paginación">
                    <ul class="pagination justify-content-center mb-0">
                        <li class="page-item <?= $page === 1 ? 'disabled' : '' ?>">
                            <a class="page-link" href="<?= e(url('index', ['page' => $page - 1])) ?>">Anterior</a>
                        </li>
                        <?php for ($number = 1; $number <= $lastPage; $number++): ?>
                            <li class="page-item <?= $number === $page ? 'active' : '' ?>">
                                <a class="page-link" href="<?= e(url('index', ['page' => $number])) ?>">
                                    <?= $number ?>
                                </a>
                            </li>
                        <?php endfor; ?>
                        <li class="page-item <?= $page === $lastPage ? 'disabled' : '' ?>">
                            <a class="page-link" href="<?= e(url('index', ['page' => $page + 1])) ?>">Siguiente</a>
                        </li>
                    </ul>
                </nav>
            </div>
        <?php endif; ?>
    <?php endif; ?>
</section>

<div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content border-0 shadow">
            <div class="modal-body p-4 text-center">
                <div class="delete-icon mx-auto mb-3"><i class="bi bi-exclamation-triangle-fill"></i></div>
                <h2 class="h4" id="deleteModalLabel">¿Eliminar persona?</h2>
                <p class="text-secondary mt-2 mb-4">
                    Vas a eliminar a <strong id="deletePersonName"></strong>. Esta acción no se puede deshacer.
                </p>
                <form id="deleteForm" method="post">
                    <input type="hidden" name="_token" value="<?= e(\App\Core\Session::csrfToken()) ?>">
                    <div class="d-flex gap-2 justify-content-center">
                        <button type="button" class="btn btn-light px-4" data-bs-dismiss="modal">Cancelar</button>
                        <button type="submit" class="btn btn-danger px-4">Sí, eliminar</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
