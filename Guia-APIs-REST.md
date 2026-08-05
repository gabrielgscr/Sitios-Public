# Guia para la elaboracion de APIs REST y buenas practicas

![REST](https://img.shields.io/badge/REST-API%20Design-0A66C2)
![HTTP](https://img.shields.io/badge/HTTP-Best%20Practices-4B5563)
![JSON](https://img.shields.io/badge/JSON-Response%20Format-000000)
![Academico](https://img.shields.io/badge/Contexto-Ejemplo%20Academico-1F6FEB)

## Propósito

Esta guia resume criterios tecnicos para disenar, implementar y documentar APIs alineadas con el estilo REST. El objetivo no es solo exponer endpoints, sino construir interfaces consistentes, predecibles, seguras y faciles de mantener.

## Que significa cumplir con REST?

REST es un estilo de arquitectura para sistemas distribuidos basado en recursos, una interfaz uniforme y comunicacion sin estado. Una API orientada a REST debe procurar:

- Identificar cada recurso mediante una URL clara.
- Usar correctamente los metodos HTTP.
- Mantener cada solicitud independiente, sin depender de estado almacenado en el servidor entre peticiones.
- Representar recursos en formatos estandar, normalmente JSON.
- Aprovechar los codigos de estado HTTP para comunicar el resultado de cada operacion.
- Facilitar cache, escalabilidad y evolucion controlada de la interfaz.

Cumplir con REST no consiste unicamente en usar JSON y HTTP; tambien implica disenar la API como una coleccion coherente de recursos y comportamientos.

## Restricciones REST que conviene explicitar

Si se quiere hablar con mayor precision de una API RESTful, conviene contrastarla con las restricciones clasicas de REST:

- Cliente-servidor: separacion clara entre consumidor y proveedor del servicio.
- Sin estado: cada peticion debe ser autosuficiente.
- Almacenable en cache: las respuestas deben indicar cuando pueden almacenarse y reutilizarse.
- Interfaz uniforme: recursos identificables, mensajes autodescriptivos, representaciones consistentes y semantica HTTP bien aplicada.
- Sistema por capas: el cliente no necesita conocer si interactua con el servicio final, un proxy o una pasarela.
- Codigo bajo demanda: es opcional y poco comun en APIs modernas.

En terminos estrictos, una API plenamente RESTful tambien deberia considerar hipermedia como motor del estado de la aplicacion, es decir, incluir enlaces o acciones relacionadas dentro de las respuestas para guiar la navegacion del cliente. En la practica, muchas APIs academicas y empresariales siguen solo una parte de estas restricciones; en esos casos suele ser mas preciso decir que son APIs HTTP inspiradas en REST o APIs con estilo REST.

## Principios base de diseño

### 1. Modelar recursos, no acciones

La API debe girar alrededor de sustantivos que representen entidades del dominio.

Correcto:

- GET /usuarios
- GET /usuarios/15
- POST /usuarios

Evitar:

- GET /obtenerUsuarios
- POST /crearUsuario
- POST /eliminarUsuario

El verbo principal ya lo aporta el metodo HTTP. La URL debe describir el recurso.

### 2. Usar metodos HTTP de forma semantica

Los metodos mas comunes deben respetar su intencion:

- GET: consultar recursos sin modificar estado.
- POST: crear recursos o ejecutar operaciones no idempotentes.
- PUT: reemplazar completamente un recurso existente.
- PATCH: actualizar parcialmente un recurso.
- DELETE: eliminar un recurso.

Una mala practica frecuente es usar POST para todo. Eso rompe la semantica HTTP, dificulta el uso de cache y vuelve menos clara la API.

### 3. Mantener ausencia de estado

Cada solicitud debe contener toda la informacion necesaria para ser procesada. La autenticacion, autorizacion, filtros y contexto deben viajar en la peticion.

Evitar depender de sesiones implicitas del lado servidor cuando el objetivo es una API REST publica o integrable.

### 4. Disenar URLs consistentes

Buenas practicas para las rutas:

- Usar sustantivos en plural cuando represente colecciones: /productos, /clientes, /pedidos.
- Mantener nombres cortos y claros.
- Usar minusculas y guiones si hace falta legibilidad.
- Evitar mezclar estilos en la misma API.
- Reflejar jerarquias reales solo cuando exista relacion fuerte entre recursos.

Ejemplos:

- GET /pedidos
- GET /pedidos/120
- GET /pedidos/120/detalles
- GET /clientes/44/pedidos

### 5. Representar correctamente el resultado con HTTP

El resultado de una operacion debe comunicarse con el codigo HTTP correcto, no solamente con un campo interno como success = true.

## Codigos de estado recomendados

### Respuestas exitosas

- 200 OK: consulta o actualizacion exitosa.
- 201 Created: recurso creado correctamente.
- 202 Accepted: solicitud aceptada para procesamiento posterior.
- 204 No Content: operacion exitosa sin cuerpo de respuesta, comun en DELETE o algunos PUT/PATCH.

### Errores del cliente

- 400 Bad Request: solicitud mal construida.
- 401 Unauthorized: falta autenticacion valida.
- 403 Forbidden: autenticado, pero sin permisos suficientes.
- 404 Not Found: recurso inexistente.
- 409 Conflict: conflicto de estado, por ejemplo duplicidad.
- 422 Unprocessable Entity: datos validos en formato, pero invalidos en reglas de negocio o validacion.

### Errores del servidor

- 500 Internal Server Error: fallo inesperado.
- 502 Bad Gateway: respuesta invalida recibida desde un servicio externo o dependencia intermedia.
- 503 Service Unavailable: servicio temporalmente no disponible.

## Convención recomendada para respuestas JSON

Una API consistente debe responder con estructuras previsibles. Por ejemplo:

### Respuesta exitosa

```json
{
  "data": {
    "id": 15,
    "nombre": "Ana",
    "correo": "ana@correo.com"
  }
}
```

Si se busca un diseño mas estrictamente RESTful, la respuesta puede incluir enlaces relacionados para facilitar la navegacion del cliente:

```json
{
  "data": {
    "id": 15,
    "nombre": "Ana",
    "correo": "ana@correo.com"
  },
  "links": {
    "self": "/usuarios/15",
    "pedidos": "/usuarios/15/pedidos"
  }
}
```

### Respuesta de coleccion con paginacion

```json
{
  "data": [
    {
      "id": 1,
      "nombre": "Producto A"
    },
    {
      "id": 2,
      "nombre": "Producto B"
    }
  ],
  "meta": {
    "page": 1,
    "pageSize": 10,
    "total": 35,
    "totalPages": 4
  }
}
```

### Respuesta de error

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Los datos enviados no son validos.",
    "details": [
      {
        "field": "correo",
        "message": "El correo ya existe."
      }
    ]
  }
}
```

Lo importante no es una estructura unica universal, sino mantener una convencion estable en toda la API.

## Buenas practicas clave

### Nombres claros y consistentes

- Mantener el mismo criterio de nombres en rutas, atributos y parametros.
- Evitar abreviaturas ambiguas.
- No mezclar español e ingles sin una razon definida por el proyecto.

### Validacion de entrada

Toda API debe validar:

- Tipos de datos.
- Campos requeridos.
- Rangos permitidos.
- Formatos, por ejemplo correo, fecha o telefono.
- Reglas de negocio, por ejemplo unicidad o estados validos.

Nunca se debe confiar en que el cliente enviara informacion correcta.

### Manejo uniforme de errores

Los errores deben ser comprensibles para quien consume la API.

Evitar:

- Mensajes genericos sin contexto.
- Exponer trazas internas de la aplicacion.
- Responder siempre con 200 aunque exista error logico.

Preferir:

- Codigos HTTP adecuados.
- Mensajes claros.
- Identificadores de error reutilizables.
- Detalles de validacion por campo cuando sea necesario.

### Paginacion, filtros y ordenamiento

Cuando una coleccion puede crecer, no conviene devolver todos los elementos de una sola vez.

Conviene soportar parametros como:

- page
- pageSize
- sort
- order
- filtros o campos especificos como estado, fecha, categoria

Ejemplo:

- GET /productos?page=2&pageSize=20&sort=nombre&order=asc

### Versionado de la API

Una API evoluciona. Para evitar romper clientes existentes, se recomienda versionar.

Una estrategia comun es incluir la version en la ruta:

- /api/v1/usuarios
- /api/v2/usuarios

Tambien es valido versionar por encabezados, pero en contextos academicos y de integracion basica la version en la URL suele ser mas simple de entender y mantener.

### Idempotencia cuando aplica

Una operacion idempotente produce el mismo resultado observable aunque se ejecute varias veces con la misma entrada.

- GET debe ser idempotente.
- PUT y DELETE deberian comportarse como idempotentes.
- POST normalmente no es idempotente.