# EjemploMicroServicioPersona

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-Data%20Access-1F6FEB)
![Razor Pages](https://img.shields.io/badge/Razor%20Pages-Web%20UI-0C2D48)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Relational%20DB-CC2927?logo=microsoftsqlserver&logoColor=white)
![Swagger](https://img.shields.io/badge/OpenAPI-Swagger-85EA2D?logo=swagger&logoColor=black)
![Academico](https://img.shields.io/badge/Contexto-Ejemplo%20Academico-1F6FEB)

Ejemplo de una solucion .NET con un microservicio REST de personas y una aplicacion web consumidora.

## Tabla de contenido

- [Descripcion](#descripcion)
- [Objetivo academico](#objetivo-academico)
- [Arquitectura de la solucion](#arquitectura-de-la-solucion)
- [Tecnologias utilizadas](#tecnologias-utilizadas)
- [Endpoints y pantallas principales](#endpoints-y-pantallas-principales)
- [Como ejecutar el proyecto](#como-ejecutar-el-proyecto)
- [Configuracion](#configuracion)
- [Notas academicas](#notas-academicas)

## Descripcion

La solucion esta compuesta por dos aplicaciones:

- `EjemploMicroServicioPersona`: microservicio REST para CRUD de personas.
- `EjemploMicroServicioPersona.ConsumoWeb`: aplicacion web en Razor Pages que consume la API.

El ejemplo muestra el flujo completo entre backend, base de datos y cliente web.

## Objetivo academico

Este repositorio se usa como material de practica para:

- Diseñar una API REST simple pero consistente.
- Aplicar separacion por capas.
- Consumir un servicio HTTP desde una aplicacion web.
- Usar patrones basicos de acceso a datos y manejo de errores.
- Documentar una solucion tecnica con estilo GitHub.

## Arquitectura de la solucion

La solucion fue creada como dos proyectos Web en una misma solucion:

- `EjemploMicroServicioPersona`: expone endpoints REST y accede a SQL Server con Dapper.
- `EjemploMicroServicioPersona.ConsumoWeb`: consume la API mediante `HttpClient` y presenta pantallas Razor Pages.

## Tecnologias utilizadas

- .NET 8 / ASP.NET Core
- Minimal APIs
- Dapper
- Microsoft.Data.SqlClient
- Razor Pages
- HttpClient
- Swagger / OpenAPI
- SQL Server

## Endpoints y pantallas principales

### API REST

- `GET /api/Persona`
- `GET /api/Persona/page?pageNumber=1&pageSize=10`
- `GET /api/Persona/{id}`
- `POST /api/Persona`
- `PUT /api/Persona/{id}`
- `DELETE /api/Persona/{id}`

### Web consumidora

- Listado de personas
- Creacion de personas
- Edicion de personas
- Eliminacion de personas

## Como ejecutar el proyecto

1. Abrir la solucion [EjemploMicroServicioPersona.sln](EjemploMicroServicioPersona.sln).
2. Verificar la cadena de conexion en [appsettings.json](EjemploMicroServicioPersona/appsettings.json).
3. Verificar la URL base del servicio en [appsettings.json](EjemploMicroServicioPersona.ConsumoWeb/appsettings.json).
4. Ejecutar primero el microservicio y luego la aplicacion web consumidora.

## Configuracion

La API usa la cadena de conexion definida en el archivo de configuracion del proyecto principal.

La aplicacion web consumidora usa `PersonaApi:BaseUrl` para apuntar al microservicio.

Si cambias el host o el puerto del servicio, actualiza esa configuracion antes de probar la interfaz web.

### Paginacion y concurrencia de lecturas

El endpoint `GET /api/Persona/page` pagina desde SQL Server con `ORDER BY`, `OFFSET` y `FETCH`, y devuelve los campos `items`, `pageNumber`, `pageSize`, `totalCount` y `totalPages`.

Para evitar que las lecturas de mantenimiento bloqueen escrituras, ejecuta una vez el script [enable-read-committed-snapshot.sql](Database/enable-read-committed-snapshot.sql) con permisos de administrador en SQL Server. Esta configuracion usa versionado de filas y evita el uso de `NOLOCK`, que puede devolver datos no confirmados o duplicados.

## Notas academicas

- Este ejemplo esta orientado a practica y demostracion de conceptos.
- Incluye Swagger para validar rapidamente la API.
- La aplicacion web sirve como capa de consumo, no como sustituto de la API.
- Para ambientes reales, mover secretos y credenciales a variables de entorno o Secret Manager.