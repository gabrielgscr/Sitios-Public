# API Node.js

Guia de creacion, configuracion y ejecucion para una API REST de Personas construida con Node.js, Express, Sequelize y SQL Server.

## Requisitos previos

- Node.js 18+
- npm
- SQL Server disponible (local o remoto)

## 1. Crear el proyecto desde cero

1. Crear carpeta y entrar al directorio:

```powershell
mkdir ApiNodeJs
cd ApiNodeJs
```

2. Inicializar npm:

```powershell
npm init -y
```

3. Instalar dependencias:

```powershell
npm install express sequelize tedious dotenv cors helmet
```

4. Crear estructura base:

```text
server.js
src/app.js
src/config/database.js
src/models/Persona.js
src/controllers/personaController.js
src/routes/index.js
src/routes/personaRoutes.js
```

## 2. Configurar variables de entorno

Crear archivo `.env` en la raiz:

```env
PORT=3000
DB_HOST=localhost
DB_NAME=TuBaseDeDatos
DB_USER=tu_usuario
DB_PASS=tu_password
```

Notas:

- `PORT` define el puerto HTTP de la API.
- `DB_HOST`, `DB_NAME`, `DB_USER`, `DB_PASS` son usados en `src/config/database.js`.

## 3. Scripts de ejecucion

Scripts disponibles en `package.json`:

- `npm start`: ejecuta `server.js`
- `npm run dev`: ejecuta `server.js`

## 4. Ejecutar la API

1. Instalar dependencias (si aun no lo hiciste):

```powershell
npm install
```

2. Iniciar servidor:

```powershell
npm start
```

Si todo esta correcto, deberias ver mensajes como:

- Conectado a SQL Server
- Base de datos sincronizada
- Servidor corriendo en `http://localhost:3000`

## 5. Probar endpoints

Base URL:

```text
http://localhost:3000/api
```

Endpoints principales:

- `GET /personas`
- `GET /personas/:id`
- `POST /personas`
- `PUT /personas/:id`
- `PATCH /personas/:id`
- `DELETE /personas/:id`

Ejemplo rapido con cURL en PowerShell:

```powershell
curl -Method Get http://localhost:3000/api/personas
```

## 6. Coleccion de Postman

Importar la coleccion ubicada en:

- `postman/ApiNodeJs-Personas.postman_collection.json`

## 7. Problemas comunes

- Error de conexion a SQL Server:
  - Revisar `DB_HOST`, `DB_NAME`, `DB_USER` y `DB_PASS` en `.env`.
- Puerto ocupado:
  - Cambiar `PORT` en `.env`.
- Dependencias faltantes:
  - Ejecutar `npm install` nuevamente.
