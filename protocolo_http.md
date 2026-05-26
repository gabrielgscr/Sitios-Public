# Resumen introductorio: Protocolo HTTP

## 1. ¿Qué es HTTP?

**HTTP** significa **Hypertext Transfer Protocol**, o Protocolo de Transferencia de Hipertexto.

Es el protocolo principal que permite la comunicación en la Web. Gracias a HTTP, un cliente, como un navegador, puede solicitar recursos a un servidor, como páginas HTML, imágenes, archivos CSS, JavaScript, documentos JSON o datos de una API.

HTTP funciona mediante un modelo de **cliente-servidor**:

```text
Cliente  --->  Solicitud HTTP  --->  Servidor
Cliente  <---  Respuesta HTTP   <---  Servidor
```

Ejemplo cotidiano:

1. El usuario escribe una dirección web en el navegador.
2. El navegador envía una solicitud HTTP al servidor.
3. El servidor procesa la solicitud.
4. El servidor responde con el recurso solicitado o con un código de error.
5. El navegador interpreta la respuesta y muestra el resultado.

## 2. Cliente y servidor

### Cliente

El **cliente** es quien inicia la comunicación. Normalmente es un navegador web, aunque también puede ser una aplicación móvil, una herramienta como Postman o un programa que consume una API.

Ejemplos de clientes:

- Google Chrome.
- Microsoft Edge.
- Firefox.
- Postman.
- Una aplicación en JavaScript usando `fetch`.
- Una app móvil consumiendo una API.

### Servidor

El **servidor** recibe la solicitud, la procesa y devuelve una respuesta.

Ejemplos de servidores o tecnologías relacionadas:

- IIS.
- Apache.
- Nginx.
- Node.js.
- ASP.NET Core.
- PHP.
- Servicios REST API.

## 3. HTTP es un protocolo sin estado

HTTP se considera un protocolo **sin estado**. Esto significa que cada solicitud se procesa de forma independiente.

Por ejemplo, si un cliente hace estas solicitudes:

```text
GET /productos
GET /carrito
GET /perfil
```

El servidor no necesariamente recuerda por sí solo lo que ocurrió en solicitudes anteriores.

Para mantener información entre solicitudes se usan mecanismos como:

- Cookies.
- Sesiones.
- Tokens.
- JWT.
- Parámetros en la URL.
- Almacenamiento del lado del cliente.

## 4. Estructura básica de una solicitud HTTP

Una solicitud HTTP normalmente contiene:

- Método HTTP.
- Ruta o recurso solicitado.
- Versión del protocolo.
- Headers o cabeceras.
- Body o cuerpo, cuando aplica.

Ejemplo simplificado:

```http
GET /productos HTTP/1.1
Host: tienda.com
Accept: application/json
User-Agent: Mozilla/5.0
```

En este caso:

- `GET` es el método.
- `/productos` es el recurso solicitado.
- `HTTP/1.1` es la versión del protocolo.
- `Host`, `Accept` y `User-Agent` son headers.

## 5. Estructura básica de una respuesta HTTP

Una respuesta HTTP normalmente contiene:

- Versión del protocolo.
- Código de estado.
- Mensaje asociado al estado.
- Headers.
- Body o cuerpo de respuesta.

Ejemplo simplificado:

```http
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 58

[
  { "id": 1, "nombre": "Teclado" }
]
```

En este caso:

- `200` indica que la solicitud fue exitosa.
- `Content-Type` indica el tipo de contenido devuelto.
- El cuerpo contiene datos en formato JSON.

## 6. Métodos HTTP principales

Los métodos HTTP indican la acción que el cliente desea realizar sobre un recurso.

| Método | Uso general | Ejemplo |
|---|---|---|
| GET | Solicitar información | Obtener lista de productos |
| POST | Enviar datos para crear o procesar algo | Registrar un usuario |
| PUT | Reemplazar completamente un recurso | Actualizar todos los datos de un producto |
| PATCH | Actualizar parcialmente un recurso | Cambiar solo el precio de un producto |
| DELETE | Eliminar un recurso | Borrar un producto |
| HEAD | Solicitar solo headers, sin body | Verificar si un recurso existe |
| OPTIONS | Consultar métodos permitidos | Revisar capacidades de una API |

Ejemplos:

```http
GET /api/productos
POST /api/productos
PUT /api/productos/15
PATCH /api/productos/15
DELETE /api/productos/15
```

## 7. Headers o cabeceras HTTP

Los **headers** permiten enviar información adicional en una solicitud o respuesta.

Ejemplos de headers comunes en solicitudes:

```http
Host: ejemplo.com
Accept: application/json
Authorization: Bearer token123
Content-Type: application/json
User-Agent: Mozilla/5.0
```

Ejemplos de headers comunes en respuestas:

```http
Content-Type: text/html
Content-Length: 1024
Set-Cookie: sesion=abc123
Cache-Control: no-cache
Location: /login
```

Algunos usos frecuentes de los headers:

- Indicar el formato esperado de respuesta.
- Indicar el formato del contenido enviado.
- Enviar credenciales o tokens.
- Controlar caché.
- Gestionar cookies.
- Indicar redirecciones.

## 8. Body o cuerpo del mensaje

El **body** contiene datos enviados por el cliente o devueltos por el servidor.

No todas las solicitudes tienen body. Por ejemplo, `GET` normalmente no envía cuerpo. En cambio, `POST`, `PUT` y `PATCH` suelen incluir datos.

Ejemplo de body en JSON:

```http
POST /api/usuarios HTTP/1.1
Host: sistema.com
Content-Type: application/json

{
  "nombre": "Ana",
  "correo": "ana@ejemplo.com"
}
```

## 9. Códigos de estado HTTP

Los códigos de estado indican el resultado de la solicitud.

### 2xx: éxito

| Código | Significado |
|---|---|
| 200 | OK: solicitud exitosa |
| 201 | Created: recurso creado |
| 204 | No Content: éxito sin contenido de respuesta |

### 3xx: redirección

| Código | Significado |
|---|---|
| 301 | Moved Permanently: recurso movido permanentemente |
| 302 | Found: redirección temporal |
| 304 | Not Modified: recurso no modificado, útil para caché |

### 4xx: error del cliente

| Código | Significado |
|---|---|
| 400 | Bad Request: solicitud mal formada |
| 401 | Unauthorized: requiere autenticación |
| 403 | Forbidden: acceso prohibido |
| 404 | Not Found: recurso no encontrado |
| 405 | Method Not Allowed: método no permitido |

### 5xx: error del servidor

| Código | Significado |
|---|---|
| 500 | Internal Server Error: error interno del servidor |
| 502 | Bad Gateway: error entre servidores |
| 503 | Service Unavailable: servicio no disponible |
| 504 | Gateway Timeout: tiempo de espera agotado |

## 10. URL, recurso y parámetros

Una URL puede contener distintas partes:

```text
https://www.ejemplo.com:443/productos?id=10&categoria=libros
```

Partes principales:

| Parte | Ejemplo | Descripción |
|---|---|---|
| Protocolo | `https` | Indica el protocolo usado |
| Dominio | `www.ejemplo.com` | Nombre del servidor |
| Puerto | `443` | Puerto de comunicación |
| Ruta | `/productos` | Recurso solicitado |
| Query string | `?id=10&categoria=libros` | Parámetros enviados en la URL |

## 11. HTTP y HTTPS

**HTTP** transmite información sin cifrado. **HTTPS** utiliza cifrado mediante TLS para proteger la comunicación.

HTTPS ayuda a proteger:

- Credenciales de usuario.
- Formularios.
- Cookies.
- Tokens.
- Datos personales.
- Información enviada entre cliente y servidor.

Para sitios reales o sistemas con datos sensibles, HTTPS debe considerarse obligatorio.

## 12. Ejemplo de interacción completa

Solicitud:

```http
GET /api/cursos HTTP/1.1
Host: universidad.local
Accept: application/json
```

Respuesta:

```http
HTTP/1.1 200 OK
Content-Type: application/json

[
  { "codigo": "IF-101", "nombre": "Programación I" },
  { "codigo": "IF-202", "nombre": "Bases de Datos" }
]
```

Interpretación:

- El cliente solicita `/api/cursos`.
- El servidor responde exitosamente.
- El contenido devuelto está en formato JSON.

## 13. Relación con APIs REST

Muchas APIs web usan HTTP como base de comunicación.

Ejemplo de diseño común:

```text
GET    /api/estudiantes       -> listar estudiantes
GET    /api/estudiantes/5     -> obtener estudiante específico
POST   /api/estudiantes       -> crear estudiante
PUT    /api/estudiantes/5     -> reemplazar estudiante
PATCH  /api/estudiantes/5     -> modificar parcialmente estudiante
DELETE /api/estudiantes/5     -> eliminar estudiante
```

En este tipo de APIs, los métodos HTTP ayudan a expresar la intención de la operación.

## Fuentes consultadas

- MDN Web Docs. *Generalidades del protocolo HTTP*.
- MDN Web Docs. *HTTP*.
- MDN Web Docs. *HTTP headers*.
- MDN Web Docs. *Métodos de petición HTTP*.
