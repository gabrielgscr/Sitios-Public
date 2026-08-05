# Microservicios y consumo de APIs REST en .NET

![Microservicios](https://img.shields.io/badge/Microservicios-API%20y%20Web-0A66C2)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![HttpClient](https://img.shields.io/badge/HttpClient-Consumption-1F6FEB)
![Swagger](https://img.shields.io/badge/OpenAPI-Swagger-85EA2D?logo=swagger&logoColor=black)
![Academico](https://img.shields.io/badge/Contexto-Ejemplo%20Academico-1F6FEB)

Referencia breve sobre el uso de microservicios y consumo de APIs REST en .NET.

## Que muestra este ejemplo

La solucion combina un microservicio REST con una aplicacion web que lo consume.

## Por que es util

- Separa responsabilidades entre proveedor y consumidor.
- Permite reutilizar la API desde distintas interfaces.
- Facilita practicar integracion HTTP real entre aplicaciones.
- Sirve para introducir conceptos de desacoplamiento y distribucion.

## Piezas principales

- API REST con Minimal APIs.
- Acceso a datos con Dapper y SQL Server.
- Swagger para documentacion y pruebas.
- Web consumidora con Razor Pages.
- `HttpClient` como cliente HTTP tipado.

## Flujo general

1. La web solicita datos al microservicio.
2. La API consulta o modifica la base de datos.
3. La API responde con JSON y codigos HTTP.
4. La web muestra el resultado al usuario.

## Idea clave

En un esquema de este tipo, la API es el contrato y la web es solo un consumidor.

## Comandos utiles

```bash
dotnet restore EjemploMicroServicioPersona.sln
dotnet run --project EjemploMicroServicioPersona/EjemploMicroServicioPersona.csproj
dotnet run --project EjemploMicroServicioPersona.ConsumoWeb/EjemploMicroServicioPersona.ConsumoWeb.csproj
```