const DEFAULT_API_BASE_URL = 'https://localhost:7231/api/Persona';

export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL?.trim() || DEFAULT_API_BASE_URL;

async function requestJson(url, options = {}) {
  try {
    const response = await fetch(url, options);
    const contentType = response.headers.get('content-type') ?? '';
    const isJson = contentType.includes('application/json');
    const payload = isJson
      ? await response.json().catch(() => null)
      : await response.text().catch(() => null);

    if (!response.ok) {
      const message = payload && typeof payload === 'object' && 'message' in payload
        ? payload.message
        : 'Error en la petición al servidor';

      throw new Error(message);
    }

    return payload;
  } catch (error) {
    if (error instanceof TypeError) {
      throw new Error('No se pudo conectar con el servidor');
    }

    throw error;
  }
}

export const getPersonas = ({ pageNumber = 1, pageSize = 10, signal } = {}) => {
  const query = new URLSearchParams({
    pageNumber: String(pageNumber),
    pageSize: String(pageSize),
  });

  return requestJson(`${API_BASE_URL}/page?${query}`, { signal });
};

export const getPersona = (personaId, { signal } = {}) => {
  return requestJson(`${API_BASE_URL}/${encodeURIComponent(personaId)}`, { signal });
};

export const addPersona = (persona) => {
  return requestJson(API_BASE_URL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(persona),
  });
};

export const updatePersona = (persona) => {
  return requestJson(`${API_BASE_URL}/${encodeURIComponent(persona.personaId)}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(persona),
  });
};

export const deletePersona = (personaId) => {
  return requestJson(`${API_BASE_URL}/${encodeURIComponent(personaId)}`, {
    method: 'DELETE',
  });
};
