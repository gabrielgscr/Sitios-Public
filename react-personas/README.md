# React + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Babel](https://babeljs.io/) for Fast Refresh
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh

## Expanding the ESLint configuration

If you are developing a production application, we recommend using TypeScript with type-aware lint rules enabled. Check out the [TS template](https://github.com/vitejs/vite/tree/main/packages/create-vite/template-react-ts) for information on how to integrate TypeScript and [`typescript-eslint`](https://typescript-eslint.io) in your project.


# Instrucciones
Crear este proyecto:
npm create vite@latest react-personas -- --template react
cd react-personas
npm install

Para ejecutar:
# React Personas

Aplicación web de mantenimiento de personas. Permite listar, buscar, crear, editar y eliminar registros mediante el microservicio `EjemploMicroServicioPersona`.

## ¿Qué es React?

[React](https://react.dev/) es una biblioteca de JavaScript para construir interfaces de usuario a partir de componentes reutilizables. Actualiza la pantalla cuando cambia el estado de la aplicación y permite organizar una interfaz en partes pequeñas y mantenibles.

En este proyecto React construye las pantallas de listado, creación y edición de personas, mientras que la información se almacena y consulta desde una API REST independiente.

## Crear el proyecto

El proyecto fue creado con Vite y la plantilla de React:

```bash
npm create vite@latest react-personas -- --template react
cd react-personas
npm install
```

## Instalación de dependencias

Instala todas las dependencias declaradas en `package.json`:

```bash
npm install
```

Dependencias principales:

| Paquete | Objetivo |
| --- | --- |
| `react` | Biblioteca para construir la interfaz basada en componentes. |
| `react-dom` | Renderiza los componentes React en el navegador. |
| `react-router-dom` | Gestiona las rutas del cliente: listado, creación y edición. |
| `bootstrap` | Aporta estilos y componentes base para formularios, tablas, botones y diseño responsivo. |
| `lucide-react` | Proporciona iconos accesibles para las acciones y navegación. |

Herramientas de desarrollo:

| Paquete | Objetivo |
| --- | --- |
| `vite` | Servidor de desarrollo y generador de la compilación de producción. |
| `@vitejs/plugin-react` | Integra React con Vite y habilita Fast Refresh durante el desarrollo. |
| `eslint` y plugins de React | Revisan errores comunes y reglas de hooks antes de compilar. |

## Configuración necesaria

### 1. Ejecutar el microservicio

Esta interfaz consume el microservicio de personas en:

```text
https://localhost:7231/api/Persona
```

Inicia primero el proyecto `EjemploMicroServicioPersona` con el perfil `https`. El servicio debe responder antes de abrir la interfaz React.

La política CORS del microservicio permite el origen de desarrollo habitual de Vite:

```text
http://localhost:5173
```

### 2. Configurar la URL de la API

La URL se define mediante `VITE_API_BASE_URL`. El archivo `.env.local` ya contiene la configuración de desarrollo:

```env
VITE_API_BASE_URL=https://localhost:7231/api/Persona
```

Para apuntar a otro ambiente, actualiza ese valor o crea un archivo `.env.local` a partir de `.env.example`:

```env
VITE_API_BASE_URL=https://servidor.ejemplo.com/api/Persona
```

Después de cambiar una variable `VITE_`, reinicia el servidor de Vite para que la nueva configuración se aplique.

## Ejecución

Inicia la aplicación en modo desarrollo:

```bash
npm run dev
```

Vite mostrará la URL local. Normalmente será `http://localhost:5173`.

Rutas disponibles:

| Ruta | Función |
| --- | --- |
| `/personas` | Lista, busca y elimina personas. |
| `/personas/nueva` | Crea una persona. |
| `/personas/:personaId/editar` | Edita una persona existente. |

## Validación y compilación

Ejecuta el análisis estático:

```bash
npm run lint
```

Genera la compilación de producción en `dist/`:

```bash
npm run build
```
