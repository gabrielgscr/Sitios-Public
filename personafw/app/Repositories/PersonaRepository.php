<?php

declare(strict_types=1);

namespace App\Repositories;

use PDO;

final class PersonaRepository
{
    public function __construct(private readonly PDO $connection)
    {
    }

    public function paginate(int $page, int $perPage): array
    {
        $offset = ($page - 1) * $perPage;

        $statement = $this->connection->prepare(
            'SELECT PersonaID, Nombre, Tipo, Gender
             FROM dbo.Persona
             ORDER BY Nombre, PersonaID
             OFFSET :offset ROWS FETCH NEXT :limit ROWS ONLY'
        );
        $statement->bindValue(':offset', $offset, PDO::PARAM_INT);
        $statement->bindValue(':limit', $perPage, PDO::PARAM_INT);
        $statement->execute();

        return [
            'items' => $statement->fetchAll(),
            'total' => (int) $this->connection
                ->query('SELECT COUNT(*) FROM dbo.Persona')
                ->fetchColumn(),
        ];
    }

    public function find(string $id): ?array
    {
        $statement = $this->connection->prepare(
            'SELECT PersonaID, Nombre, Tipo, Gender
             FROM dbo.Persona
             WHERE PersonaID = :id'
        );
        $statement->execute(['id' => $id]);
        $persona = $statement->fetch();

        return $persona ?: null;
    }

    public function idExists(string $id, ?string $exceptId = null): bool
    {
        $sql = 'SELECT COUNT(*) FROM dbo.Persona WHERE PersonaID = :id';
        $parameters = ['id' => $id];

        if ($exceptId !== null) {
            $sql .= ' AND PersonaID <> :except_id';
            $parameters['except_id'] = $exceptId;
        }

        $statement = $this->connection->prepare($sql);
        $statement->execute($parameters);

        return (int) $statement->fetchColumn() > 0;
    }

    public function create(array $data): void
    {
        $statement = $this->connection->prepare(
            'INSERT INTO dbo.Persona
                (PersonaID, Nombre, Tipo, Gender, Password)
             VALUES
                (:persona_id, :nombre, :tipo, :gender, :password)'
        );
        $statement->execute([
            'persona_id' => $data['persona_id'],
            'nombre' => $data['nombre'],
            'tipo' => (int) $data['tipo'],
            'gender' => $data['gender'],
            'password' => password_hash($data['password'], PASSWORD_DEFAULT),
        ]);
    }

    public function update(string $id, array $data): void
    {
        $fields = [
            'PersonaID = :persona_id',
            'Nombre = :nombre',
            'Tipo = :tipo',
            'Gender = :gender',
        ];
        $parameters = [
            'persona_id' => $data['persona_id'],
            'nombre' => $data['nombre'],
            'tipo' => (int) $data['tipo'],
            'gender' => $data['gender'],
            'id' => $id,
        ];

        if ($data['password'] !== '') {
            $fields[] = 'Password = :password';
            $parameters['password'] = password_hash($data['password'], PASSWORD_DEFAULT);
        }

        $statement = $this->connection->prepare(sprintf(
            'UPDATE dbo.Persona SET %s WHERE PersonaID = :id',
            implode(', ', $fields),
        ));
        $statement->execute($parameters);
    }

    public function delete(string $id): void
    {
        $statement = $this->connection->prepare(
            'DELETE FROM dbo.Persona WHERE PersonaID = :id'
        );
        $statement->execute(['id' => $id]);
    }
}
