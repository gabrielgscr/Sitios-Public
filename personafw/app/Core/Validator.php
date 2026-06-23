<?php

declare(strict_types=1);

namespace App\Core;

final class Validator
{
    private function __construct()
    {
    }

    public static function persona(array $data, bool $isEdit = false): array
    {
        $errors = [];

        if ($data['persona_id'] === '') {
            $errors['persona_id'] = 'La identificación es obligatoria.';
        } elseif (mb_strlen($data['persona_id']) > 50) {
            $errors['persona_id'] = 'La identificación no puede superar 50 caracteres.';
        } elseif (!preg_match('/^[A-Za-z0-9._-]+$/', $data['persona_id'])) {
            $errors['persona_id'] = 'Usa únicamente letras, números, punto, guion o guion bajo.';
        }

        if ($data['nombre'] === '') {
            $errors['nombre'] = 'El nombre es obligatorio.';
        } elseif (mb_strlen($data['nombre']) > 50) {
            $errors['nombre'] = 'El nombre no puede superar 50 caracteres.';
        }

        if (
            filter_var($data['tipo'], FILTER_VALIDATE_INT) === false
            || (int) $data['tipo'] < 0
            || (int) $data['tipo'] > 255
        ) {
            $errors['tipo'] = 'El tipo debe ser un número entre 0 y 255.';
        }

        if ($data['gender'] === '') {
            $errors['gender'] = 'El género es obligatorio.';
        } elseif (mb_strlen($data['gender']) > 10) {
            $errors['gender'] = 'El género no puede superar 10 caracteres.';
        }

        if (!$isEdit && $data['password'] === '') {
            $errors['password'] = 'La contraseña es obligatoria.';
        } elseif ($data['password'] !== '' && mb_strlen($data['password']) < 8) {
            $errors['password'] = 'La contraseña debe tener al menos 8 caracteres.';
        } elseif (mb_strlen($data['password']) > 72) {
            $errors['password'] = 'La contraseña no puede superar 72 caracteres.';
        }

        return $errors;
    }
}
