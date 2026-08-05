# Node.js y su uso en APIs REST

![Node.js](https://img.shields.io/badge/Node.js-Backend-339933?logo=node.js&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-Runtime-F7DF1E?logo=javascript&logoColor=black)
![Express](https://img.shields.io/badge/Express-APIs-000000)
![Academico](https://img.shields.io/badge/Contexto-Ejemplo%20Academico-1F6FEB)

Node.js es un entorno de ejecucion para JavaScript que permite crear aplicaciones del lado del servidor. Su principal ventaja es que usa un modelo asincrono y orientado a eventos, lo que lo hace muy util para aplicaciones que manejan muchas peticiones al mismo tiempo.

## ¿Por que se usa para crear APIs?

- Permite construir servidores HTTP de forma rapida.
- Tiene un ecosistema amplio de librerias con npm.
- Se integra bien con Express, Sequelize y otras herramientas comunes en backend.
- Facilita crear APIs REST ligeras, escalables y faciles de mantener.

## Uso tipico en una API

En un proyecto como el de esta carpeta, Node.js se usa para:

- Recibir peticiones HTTP.
- Validar datos de entrada.
- Conectar con una base de datos.
- Procesar la logica del negocio.
- Responder con JSON a clientes web o moviles.

## Flujo basico

1. El cliente envia una peticion a un endpoint.
2. Express recibe la solicitud y la redirige a una ruta.
3. El controlador procesa la logica requerida.
4. Si hace falta, se consulta la base de datos mediante un modelo.
5. La API responde con datos en formato JSON.

## Buenas practicas

- Separar rutas, controladores y modelos.
- Guardar configuraciones sensibles en archivos `.env`.
- Responder siempre con mensajes claros y consistentes.
- Validar la informacion antes de enviarla a la base de datos.
- Mantener la API documentada para facilitar su uso.

## Resumen

Node.js es una buena opcion para construir APIs REST porque combina simplicidad, rendimiento y un ecosistema maduro. En proyectos academicos y profesionales, permite organizar mejor el backend y conectar facilmente con bases de datos y clientes frontend o mobile.