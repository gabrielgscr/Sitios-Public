# CRUD de personas con PHP y SQL Server

Ejemplo de una aplicación web para administrar personas mediante PHP, PDO,
SQL Server y Bootstrap.

<div align="center">

![PHP](https://img.shields.io/badge/PHP-8.2-777BB4?style=for-the-badge&logo=php&logoColor=white)
![Servidor](https://img.shields.io/badge/Servidor-Apache-D22128?style=for-the-badge&logo=apache&logoColor=white)
![Base de datos](https://img.shields.io/badge/Base%20de%20datos-SQL%20Server-CC2927?style=for-the-badge)

</div>

## Navegación rápida

- [Guía de tecnologías y arquitectura](guia_php_pdo.md)
- [Requisitos](#requisitos)
- [Crear la base de datos](#2-crear-la-base-de-datos-y-la-tabla)
- [Configurar la conexión](#3-configurar-la-conexión)
- [Ejecutar la aplicación](#4-ejecutar-la-aplicación)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Seguridad aplicada](#seguridad-aplicada)

> Para estudiar el recorrido completo de una solicitud, PDO, la arquitectura y las medidas de seguridad, consulta la [guía paso a paso de PHP y PDO](guia_php_pdo.md).

## Requisitos

- PHP 8.2 o posterior.
- Servidor web Apache.
- SQL Server.
- Extensión de PHP `pdo_sqlsrv` habilitada.
- Extensión de PHP `mbstring` habilitada.
- Acceso a una base de datos de SQL Server.

En Windows, XAMPP puede utilizarse para ejecutar PHP y Apache.

## Funciones

- Listado de personas.
- Paginación de 10 registros.
- Creación de personas.
- Edición de personas.
- Eliminación con confirmación previa.
- Validación de datos en el servidor.
- Consultas preparadas con PDO.
- Protección CSRF en los formularios.
- Diseño responsive con Bootstrap.

## 1. Crear la carpeta del proyecto

Dentro del directorio público del servidor web, crea una carpeta para el
proyecto.

Ejemplo con XAMPP:

```text
C:\xampp\htdocs\personafw
```

Copia dentro de esa carpeta todos los archivos del proyecto.

## 2. Crear la base de datos y la tabla

Crea una base de datos en SQL Server o utiliza una existente.

Abre SQL Server Management Studio y ejecuta el archivo:

```text
database\01_create_personas.sql
```

Antes de ejecutarlo, modifica esta línea si tu base de datos tiene otro nombre:

```sql
USE [Ejemplo2];
```

La aplicación utiliza la tabla `dbo.Persona` con la siguiente estructura:

| Columna | Tipo | Descripción |
|---|---|---|
| `PersonaID` | `VARCHAR(50)` | Identificador y llave primaria |
| `Nombre` | `VARCHAR(50)` | Nombre completo |
| `Tipo` | `TINYINT` | Clasificación numérica |
| `Gender` | `VARCHAR(10)` | Género |
| `Password` | `VARCHAR(100)` | Contraseña protegida |

El script solo crea la tabla si todavía no existe.

## 3. Configurar la conexión

Copia el archivo:

```text
app\Config\database.example.php
```

y cambia el nombre de la copia a:

```text
app\Config\database.local.php
```

Edita los datos de conexión:

```php
<?php

return [
    'server' => 'localhost',
    'database' => 'Ejemplo2',
    'username' => 'sa',
    'password' => 'TU_CONTRASENA',
];
```

Descripción de las opciones:

- `server`: nombre o dirección del servidor de SQL Server.
- `database`: nombre de la base de datos.
- `username`: usuario de SQL Server.
- `password`: contraseña del usuario.

El archivo `database.local.php` está excluido de Git para evitar publicar las
credenciales.

## 4. Ejecutar la aplicación

Inicia Apache y abre en el navegador la dirección correspondiente a la carpeta
del proyecto.

Cuando Apache utiliza el puerto estándar:

```text
http://localhost/personafw/
```

Cuando Apache utiliza otro puerto:

```text
http://localhost:PUERTO/personafw/
```

Por ejemplo:

```text
http://localhost:8081/personafw/
```

## Estructura del proyecto

```text
personafw/
|-- app/
|   |-- Config/
|   |-- Controllers/
|   |-- Core/
|   |-- Repositories/
|   `-- Views/
|-- database/
|-- public/
|   `-- assets/
|-- index.php
|-- guia_php_pdo.md
`-- README.md
```

### Responsabilidad de cada directorio

- `app/Config`: configuración de la conexión.
- `app/Controllers`: flujo de las operaciones del CRUD.
- `app/Core`: conexión PDO, sesiones y validación.
- `app/Repositories`: consultas y operaciones con SQL Server.
- `app/Views`: plantillas HTML creadas con PHP.
- `database`: script de creación de la tabla.
- `public/assets`: estilos CSS y código JavaScript.

## Flujo de una solicitud

1. El navegador envía una solicitud a `index.php`.
2. `index.php` identifica la acción solicitada.
3. `PersonaController` valida la solicitud.
4. `PersonaRepository` ejecuta la consulta preparada.
5. El controlador carga la vista correspondiente.
6. La vista genera la respuesta HTML.

## Archivos principales

### `index.php`

Es el punto de entrada de la aplicación. Recibe acciones como listar, crear,
editar y eliminar.

### `app/Core/Database.php`

Crea la conexión PDO con SQL Server:

```php
$pdo = new PDO(
    'sqlsrv:Server=localhost;Database=Ejemplo2',
    $usuario,
    $contrasena
);
```

### `app/Repositories/PersonaRepository.php`

Contiene las consultas SQL. Utiliza sentencias preparadas para evitar la
inyección de código SQL.

### `app/Controllers/PersonaController.php`

Coordina la validación, las operaciones de la base de datos y las vistas.

### `app/Core/Validator.php`

Define las reglas de validación utilizadas al crear y editar personas.

### `app/Views/personas`

Contiene el listado y el formulario de personas.

## Seguridad aplicada

- Las consultas reciben valores mediante parámetros preparados.
- La salida HTML se escapa con `htmlspecialchars`.
- Los formularios incluyen un token CSRF.
- La eliminación requiere una solicitud `POST`.
- Las contraseñas nuevas se procesan con `password_hash`.
- Las credenciales se guardan en un archivo local excluido de Git.

## Agregar una nueva columna

Para agregar un campo nuevo:

1. Agrega la columna en la tabla de SQL Server.
2. Incluye el campo en el formulario.
3. Agrega su regla en `Validator.php`.
4. Actualiza las consultas en `PersonaRepository.php`.
5. Muestra el valor en la vista si es necesario.
