# Tarea Corta 2

**Colegio Universitario de Cartago**  
**Administración y programación de sitios Web**

## Objetivo

Elaborar software web para el control de tareas de una persona.
Reforzar el conocimiento sobre tecnologías Web, específicamente PHP.

## Historias de usuario

A continuación, se presentan las historias de usuario que deben ser elaboradas.

| ID | Historia | Criterios de aceptación |
|---|---|---|
| 1 | Como usuario del sitio web, requiero poder crear, editar y eliminar responsables para asignarles tareas. | - Debe existir una opción que permita crear responsables para asignarles tareas.<br>- Para crear responsables se solicita el nombre, apellidos e identificación.<br>- Los datos deben poder editarse.<br>- Los responsables pueden eliminarse.<br>- Nada debe impedir que un responsable se pueda eliminar.<br>- Si una tarea se queda sin responsable, entonces en el lugar donde va el responsable se indica: **“Sin responsable asignado”**. |
| 2 | Como usuario del sitio web, requiero poder crear, editar y eliminar tareas para llevar el control de estas. | - Debe existir una opción que permita crear tareas y asignarles un responsable.<br>- Para crear una tarea se solicita el detalle, en formato texto, y opcionalmente el responsable de llevarla a cabo. El responsable se debe seleccionar de una lista de selección.<br>- Además, para crear una tarea se debe indicar: **prioridad** y **fecha límite**, siendo la fecha límite opcional.<br>- Debe poder editarse el detalle de la tarea y seleccionar un diferente responsable si se requiere.<br>- Es posible seleccionar que la tarea no tenga responsable. En ese caso, debe desplegar **“Sin responsable asignado”** en el lugar donde se mostraría el nombre del responsable.<br>- Las tareas pueden eliminarse.<br>- Inicialmente, al crear una tarea debe tener como estado **“Pendiente”**.<br>- Debe existir una opción para marcar la tarea como finalizada, lo que hará que su estado pase a **“Finalizada”**. Esto le asocia a la tarea una fecha de finalización.<br>- Las tareas que se han finalizado deben estar al final del listado de tareas y el texto del detalle se debe mostrar tachado.<br>- Si una tarea se marca como finalizada por error, debe existir la posibilidad de reactivar la tarea. Debe anularse la asignación de la fecha de finalización.<br>- Debe manejarse los siguientes estados de tareas:<br>&nbsp;&nbsp;- **Pendiente**.<br>&nbsp;&nbsp;- **Finalizada**.<br>&nbsp;&nbsp;- **En progreso**.<br>&nbsp;&nbsp;- **Bloqueada**.<br>- Los siguientes son cambios de estado válidos:<br>&nbsp;&nbsp;- **Pendiente ↔ En progreso**.<br>&nbsp;&nbsp;- **En progreso ↔ Bloqueada**.<br>&nbsp;&nbsp;- **En progreso → Finalizada**.<br>&nbsp;&nbsp;- **Bloqueada → En progreso**. |
| 3 | Como usuario del sistema, quiero agrupar tareas para asociarlas a grupos de tareas. | - Debe ser posible asociar las tareas a un grupo.<br>- Para esto se debe permitir crear grupos.<br>- Para crear un grupo se solicita únicamente su nombre.<br>- Adicionalmente, cuando se crea un grupo se debe mostrar un listado de las tareas en estado pendiente y, mediante un **check**, se debe indicar las que se van a asociar al grupo que se creará. Esto será opcional.<br>- También al momento de crear una tarea, debe ser posible indicar dentro de sus datos si se desea asociar a un grupo.<br>- Debe ser posible ingresar a una tarea y cambiar o eliminar el grupo al que está asignada.<br>- Deben poder eliminarse los grupos.<br>- Debe existir un listado de grupos donde, al seleccionar uno de ellos, se puedan ver las tareas que lo conforman. |
| 4 | Como usuario del sistema, quiero un tablero para visualizar las tareas por diferentes criterios. | - Debe crear una vista tablero, tipo **kanban**, o una lista con filtros por estado, prioridad, responsable y fecha límite.<br>- Pueden moverse las tareas entre los diferentes grupos que componen el tablero y esto actualizará la información. Por ejemplo, pasar una tarea de estado. |

## Análisis y diseño

Debe generar un documento de análisis y diseño de software que contenga las siguientes secciones:

- Portada.
- Análisis del sistema de software: incluir las funcionalidades solicitadas en las historias de usuario.
- Si se realiza algún supuesto sobre alguna situación no descrita en las historias de usuario, indicarlo en esta sección.
- Diseño de software:
  - Incluir el diagrama de la base de datos generado para darle solución al problema.
  - Incluir la descripción de cada una de las tablas.
  - Hacer un diagrama de clases con los objetos involucrados en la solución.
  - Hacer diagrama de casos de uso.

## Base de datos de la aplicación

Con base en lo comprendido en las historias de usuario y en el diseño de solución propuesto para la base de datos, elabore la base de datos de la aplicación creando sus tablas, vistas, procedimientos, funciones y triggers necesarios para la correcta operación del sistema.

Una vez tenga la versión final de la base de datos, genere el script e inclúyalo dentro de los entregables del proyecto.

Puede utilizar el motor que mejor se adapte a sus necesidades.

## Pruebas

Debe ejecutar pruebas sobre la aplicación de tal forma que se cumpla con las funcionalidades y criterios de aceptación establecidos en la sección de historias de usuario.

Durante la ejecución de las pruebas, genere un documento con la evidencia de que las diferentes funcionalidades están implementadas y cumplen con los criterios de aceptación.

Cada criterio de aceptación debe generar una prueba y debe documentarse de la siguiente manera:

| # HU | Criterio de aceptación | Evidencia |
|---|---|---|
| Número de la historia de usuario respectiva | Descripción del criterio que se probó | Pantallazo con la ejecución exitosa |

## Entrega

Una vez finalizadas las actividades anteriores, debe realizar lo siguiente:

1. Preparar un archivo comprimido con los siguientes entregables y entregarlo por el CUC Virtual antes del inicio de la clase, en el enlace que se habilitará para tales efectos:
   - Código fuente de su solución.
   - Script de base de datos.
   - Documento de análisis y diseño.
   - Documentación de pruebas.
2. Debe entregarla el equipo de trabajo a más tardar el **30 de junio de 2026 antes de clases**.
