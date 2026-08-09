const API_BASE_URL = 'https://tiusr13pl.cuc-carrera-ti.ac.cr/apipersonas/api/Persona';

// Obtener lista de personas
export const getPersonas = () => {
  return fetch(API_BASE_URL)
    .then(response => {
      if (!response.ok) throw new Error('Error al obtener personas');
      return response.json();
    });
};

// Agregar nueva persona
export const addPersona = (persona) => {
  return fetch(API_BASE_URL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(persona)
  })
  .then(response => {
    if (!response.ok) throw new Error('Error al agregar persona');
    return response.json();
  });
};
