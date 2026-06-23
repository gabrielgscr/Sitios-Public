<?php

declare(strict_types=1);

namespace App\Controllers;

use App\Core\Session;
use App\Core\Validator;
use App\Repositories\PersonaRepository;

final class PersonaController
{
    private const PER_PAGE = 10;

    public function __construct(private readonly PersonaRepository $repository)
    {
    }

    public function index(): void
    {
        $page = max(1, filter_input(INPUT_GET, 'page', FILTER_VALIDATE_INT) ?: 1);
        $result = $this->repository->paginate($page, self::PER_PAGE);
        $lastPage = max(1, (int) ceil($result['total'] / self::PER_PAGE));

        if ($page > $lastPage) {
            redirect(url('index', ['page' => $lastPage]));
        }

        render('personas/index', [
            'title' => 'Listado de personas',
            'personas' => $result['items'],
            'page' => $page,
            'lastPage' => $lastPage,
            'total' => $result['total'],
            'success' => Session::getFlash('success'),
        ]);
    }

    public function create(): void
    {
        $this->renderForm(
            title: 'Crear persona',
            action: 'store',
            submitLabel: 'Guardar persona',
        );
    }

    public function store(): void
    {
        $this->requirePost();
        $this->verifyCsrf();

        $data = $this->input();
        $errors = Validator::persona($data);

        if ($this->repository->idExists($data['persona_id'])) {
            $errors['persona_id'] = 'Ya existe una persona con esta identificación.';
        }

        if ($errors !== []) {
            $this->renderForm('Crear persona', 'store', 'Guardar persona', $data, $errors);

            return;
        }

        $this->repository->create($data);
        Session::flash('success', 'La persona se creó correctamente.');
        redirect(url());
    }

    public function edit(): void
    {
        $persona = $this->findPersona();
        $this->renderForm(
            title: 'Editar persona',
            action: 'update',
            submitLabel: 'Guardar cambios',
            data: $persona,
            id: $persona['PersonaID'],
        );
    }

    public function update(): void
    {
        $this->requirePost();
        $this->verifyCsrf();

        $persona = $this->findPersona();
        $id = $persona['PersonaID'];
        $data = $this->input();
        $errors = Validator::persona($data, true);

        if ($this->repository->idExists($data['persona_id'], $id)) {
            $errors['persona_id'] = 'Ya existe otra persona con esta identificación.';
        }

        if ($errors !== []) {
            $this->renderForm('Editar persona', 'update', 'Guardar cambios', $data, $errors, $id);

            return;
        }

        $this->repository->update($id, $data);
        Session::flash('success', 'La persona se actualizó correctamente.');
        redirect(url());
    }

    public function delete(): void
    {
        $this->requirePost();
        $this->verifyCsrf();

        $persona = $this->findPersona();
        $this->repository->delete($persona['PersonaID']);

        Session::flash('success', 'La persona se eliminó correctamente.');
        redirect(url());
    }

    public function notFound(): void
    {
        http_response_code(404);
        render('errors/404', ['title' => 'Página no encontrada']);
    }

    private function renderForm(
        string $title,
        string $action,
        string $submitLabel,
        array $data = [],
        array $errors = [],
        ?string $id = null,
    ): void {
        render('personas/form', [
            'title' => $title,
            'action' => $action,
            'submitLabel' => $submitLabel,
            'persona' => $data,
            'errors' => $errors,
            'id' => $id,
        ]);
    }

    private function input(): array
    {
        return [
            'persona_id' => trim((string) filter_input(INPUT_POST, 'persona_id')),
            'nombre' => trim((string) filter_input(INPUT_POST, 'nombre')),
            'tipo' => trim((string) filter_input(INPUT_POST, 'tipo')),
            'gender' => trim((string) filter_input(INPUT_POST, 'gender')),
            'password' => (string) filter_input(INPUT_POST, 'password'),
        ];
    }

    private function findPersona(): array
    {
        $id = trim((string) filter_input(INPUT_GET, 'id'));
        $persona = $id !== '' ? $this->repository->find($id) : null;

        if ($persona === null) {
            $this->notFound();
            exit;
        }

        return $persona;
    }

    private function requirePost(): void
    {
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            $this->notFound();
            exit;
        }
    }

    private function verifyCsrf(): void
    {
        if (!Session::verifyCsrf(filter_input(INPUT_POST, '_token'))) {
            http_response_code(419);
            render('errors/419', ['title' => 'Sesión vencida']);
            exit;
        }
    }
}
