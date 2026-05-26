# Resumen introductorio: Administración básica de IIS

## 1. ¿Qué es IIS?

**IIS** significa **Internet Information Services**. Es el servidor web de Microsoft usado para publicar sitios web, aplicaciones web y servicios en sistemas Windows o Windows Server.

Desde una perspectiva inicial, IIS permite:

- Publicar sitios web en una red local o en Internet.
- Administrar aplicaciones web desarrolladas en tecnologías como ASP.NET, ASP.NET Core, PHP u otras.
- Configurar puertos, dominios, certificados HTTPS y permisos de acceso.
- Supervisar errores, registros y comportamiento del servidor.

## 2. Conceptos principales de IIS

### Sitio web

Un **sitio web** en IIS representa una aplicación o conjunto de archivos publicados bajo una dirección específica.

Un sitio normalmente se asocia con:

- Una carpeta física en el servidor.
- Un puerto, por ejemplo `80` para HTTP o `443` para HTTPS.
- Un nombre de host, por ejemplo `www.ejemplo.com`.
- Una dirección IP o todas las direcciones disponibles.

Ejemplo conceptual:

```text
Sitio: SistemaAcademico
Ruta física: C:\Sites\SistemaAcademico
Puerto: 80
Host: sistema.institucion.local
```

### Aplicación

Una **aplicación** es una unidad lógica dentro de un sitio web. Puede tener configuración propia y ejecutarse dentro de un grupo de aplicaciones.

Por ejemplo:

```text
Sitio principal: www.institucion.com
Aplicación: /matricula
Aplicación: /biblioteca
Aplicación: /pagos
```

### Directorio virtual

Un **directorio virtual** permite exponer una carpeta que no necesariamente está dentro de la carpeta física principal del sitio.

Ejemplo:

```text
URL: /documentos
Ruta real: D:\ArchivosCompartidos\Documentos
```

Esto permite organizar contenido sin mover físicamente todos los archivos al directorio raíz del sitio.

## 3. Grupos de aplicaciones o Application Pools

Un **Application Pool** es un contenedor de ejecución para una o varias aplicaciones web.

Su utilidad principal es aislar aplicaciones entre sí. Si una aplicación falla, consume demasiados recursos o necesita reiniciarse, no necesariamente afecta a las demás.

Aspectos básicos que se pueden configurar:

- Versión de .NET CLR, cuando aplica.
- Identidad con la que se ejecuta el proceso.
- Reciclado automático del proceso.
- Tiempo de inactividad.
- Límite de memoria o CPU.
- Modo de ejecución integrado o clásico, en escenarios antiguos.

Ejemplo:

```text
Application Pool: SistemaAcademicoPool
Aplicaciones asociadas:
- /SistemaAcademico
- /SistemaAcademicoAPI
```

## 4. Bindings o enlaces del sitio

Los **bindings** indican cómo se accede a un sitio web.

Un binding combina normalmente:

- Protocolo: `http` o `https`.
- Dirección IP.
- Puerto.
- Nombre de host.
- Certificado, cuando se usa HTTPS.

Ejemplos:

```text
http  | * | 80  | sistema.local
https | * | 443 | sistema.local
```

Si varios sitios usan el mismo servidor y el mismo puerto, el nombre de host ayuda a IIS a saber qué sitio debe responder.

## 5. Ruta física y permisos

Cada sitio en IIS apunta a una **ruta física** donde se encuentran los archivos de la aplicación.

Ejemplo:

```text
C:\Sites\CarnetDigital\UsersWeb
```

Además de configurar IIS, es necesario revisar permisos del sistema de archivos. La identidad del Application Pool debe poder leer, y en algunos casos escribir, sobre la carpeta de la aplicación.

Permisos comunes:

- Lectura: para servir archivos estáticos.
- Ejecución: para ejecutar aplicaciones.
- Escritura: solo cuando la aplicación necesita guardar archivos, logs o documentos.

Una mala configuración de permisos puede provocar errores como acceso denegado o errores internos del servidor.

## 6. Archivos de configuración

IIS utiliza archivos de configuración para definir el comportamiento del servidor y de las aplicaciones.

### applicationHost.config

Es un archivo de configuración global de IIS. Contiene información sobre sitios, application pools, bindings y configuraciones generales del servidor.

### web.config

Es un archivo de configuración ubicado dentro de la aplicación o sitio. Se usa para definir reglas específicas de esa aplicación.

Puede contener configuración sobre:

- Redirecciones.
- Reglas de reescritura.
- Módulos.
- Handlers.
- Seguridad.
- Manejo de errores.
- Configuración específica de ASP.NET o ASP.NET Core.

Un `web.config` mal formado puede provocar errores como **HTTP 500.19**, relacionado con configuración inválida.

## 7. Administración desde IIS Manager

La herramienta gráfica más común para administrar IIS es **IIS Manager**.

Desde ella se puede:

- Crear sitios web.
- Iniciar, detener o reiniciar sitios.
- Crear y configurar Application Pools.
- Configurar bindings.
- Revisar autenticación y autorización.
- Configurar documentos predeterminados.
- Revisar certificados.
- Ver logs y opciones de diagnóstico.

Acciones básicas frecuentes:

```text
Sitios > Agregar sitio web
Sitios > Seleccionar sitio > Bindings
Application Pools > Seleccionar pool > Advanced Settings
Sitio > Manage Website > Start / Stop / Restart
```

## 8. Documentos predeterminados

Un **documento predeterminado** es el archivo que IIS intenta mostrar cuando el usuario entra a una carpeta sin indicar un archivo específico.

Ejemplo:

```text
https://sistema.local/
```

IIS puede buscar archivos como:

```text
index.html
index.htm
default.aspx
default.htm
```

Si no existe un documento predeterminado y no está habilitado el listado de directorios, el servidor puede devolver un error.

## 9. Archivos estáticos y contenido dinámico

IIS puede servir distintos tipos de contenido.

### Archivos estáticos

Son archivos que se envían directamente al cliente:

- HTML.
- CSS.
- JavaScript.
- Imágenes.
- Archivos PDF.
- Manifest, JSON, fuentes, etc.

### Contenido dinámico

Es generado por una aplicación en ejecución:

- Aplicaciones ASP.NET.
- APIs ASP.NET Core.
- Aplicaciones PHP.
- Servicios web.

Cuando una aplicación dinámica falla, se deben revisar logs, configuración del Application Pool, permisos y dependencias instaladas.

## 10. Logs de IIS

Los **logs** registran las solicitudes que llegan al servidor.

Pueden ayudar a identificar:

- Qué recurso fue solicitado.
- Código de estado HTTP devuelto.
- Dirección IP del cliente.
- Fecha y hora.
- Método HTTP usado.
- Agente de usuario.

Ubicación común:

```text
C:\inetpub\logs\LogFiles
```

Ejemplo de códigos útiles:

```text
200: solicitud exitosa
301/302: redirección
403: prohibido
404: no encontrado
500: error interno del servidor
500.19: error de configuración
```

## 11. HTTPS y certificados

Para publicar un sitio seguro se utiliza **HTTPS**.

Esto requiere:

- Instalar o importar un certificado digital.
- Crear un binding HTTPS en el puerto `443`.
- Asociar el certificado al sitio.
- Usar un nombre de host válido.

HTTPS permite cifrar la comunicación entre cliente y servidor, protegiendo datos como credenciales, formularios o tokens.

## 12. Seguridad básica en IIS

Aspectos mínimos que los estudiantes deberían explorar:

- Usar HTTPS cuando se transmiten datos sensibles.
- Evitar permisos de escritura innecesarios.
- Separar aplicaciones en distintos Application Pools.
- Revisar autenticación y autorización.
- No mostrar errores detallados en producción.
- Mantener Windows Server, IIS y frameworks actualizados.
- Revisar logs ante comportamientos sospechosos.

## 13. Errores comunes para analizar

| Error | Posible causa |
|---|---|
| 404 Not Found | Archivo o ruta inexistente |
| 403 Forbidden | Falta de permisos o acceso bloqueado |
| 500 Internal Server Error | Error general de la aplicación o servidor |
| 500.19 | Configuración inválida en `web.config` o IIS |
| 503 Service Unavailable | Application Pool detenido o con fallos |


## Fuentes consultadas

- Microsoft Learn. *Introduction to IIS Architecture*.
- Microsoft Learn. *Application Pools \<applicationPools\>*.
- Microsoft Learn. *Understanding Sites, Applications, and Virtual Directories on IIS*.
- Microsoft Learn. *Create a Web Site*.
- Microsoft Learn. *Binding \<binding\>*.
