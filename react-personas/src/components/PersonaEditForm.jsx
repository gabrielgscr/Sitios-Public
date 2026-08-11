import { useEffect, useState } from 'react';
import { getPersona, updatePersona } from '../services/personaService';

const EMPTY_FORM = {
  personaId: '',
  nombre: '',
  tipo: 1,
  gender: 'male'
};

export default function PersonaEditForm({ personaId, onSaved, onCancelled }) {
  const [form, setForm] = useState(EMPTY_FORM);
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const controller = new AbortController();

    const loadPersona = async () => {
      setIsLoading(true);
      setError('');

      try {
        const persona = await getPersona(personaId, { signal: controller.signal });
        setForm({
          personaId: persona.personaId ?? '',
          nombre: persona.nombre ?? '',
          tipo: persona.tipo ?? 1,
          gender: persona.gender === 'female' ? 'female' : 'male'
        });
      } catch (requestError) {
        if (requestError.name !== 'AbortError') {
          setError(requestError.message || 'No se pudo cargar la persona.');
        }
      } finally {
        if (!controller.signal.aborted) {
          setIsLoading(false);
        }
      }
    };

    loadPersona();

    return () => controller.abort();
  }, [personaId]);

  const handleChange = e => {
    const { name, value, type } = e.target;

    setForm(current => ({
      ...current,
      [name]: type === 'number' ? (value === '' ? '' : Number(value)) : value
    }));
  };

  const handleSubmit = async e => {
    e.preventDefault();

    if (!form.nombre.trim()) {
      setError('El nombre es requerido.');
      return;
    }

    if (form.nombre.trim().length < 5) {
      setError('El nombre debe tener al menos 5 caracteres.');
      return;
    }

    if (form.tipo === '' || Number.isNaN(form.tipo) || form.tipo < 1 || form.tipo > 3) {
      setError('El tipo debe estar entre 1 y 3.');
      return;
    }

    setIsSubmitting(true);
    setError('');

    try {
      await updatePersona({
        personaId: form.personaId,
        nombre: form.nombre,
        tipo: form.tipo,
        gender: form.gender
      });
      onSaved();
    } catch (requestError) {
      setError(requestError.message || 'No se pudo actualizar la persona.');
    } finally {
      setIsSubmitting(false);
    }
  };

  if (isLoading) {
    return (
      <div className="text-center py-5 text-muted">Cargando persona...</div>
    );
  }

  if (error && !form.personaId) {
    return <div className="alert alert-danger mb-0">{error}</div>;
  }

  return (
    <form onSubmit={handleSubmit} noValidate>
      <div className="d-flex align-items-start justify-content-between gap-3 mb-3">
        <p className="text-muted mb-0">ID: {form.personaId}</p>

        <button type="button" className="btn btn-outline-secondary btn-sm" onClick={onCancelled}>
          Cancelar
        </button>
      </div>

      {error ? <div className="alert alert-danger py-2">{error}</div> : null}

      <div className="mb-3">
        <label className="form-label" htmlFor="edit-nombre">Nombre</label>
        <input
          id="edit-nombre"
          name="nombre"
          className="form-control"
          value={form.nombre}
          onChange={handleChange}
          minLength="5"
          required
        />
      </div>

      <div className="mb-3">
        <label className="form-label" htmlFor="edit-gender">Género</label>
        <select
          id="edit-gender"
          name="gender"
          className="form-select"
          value={form.gender}
          onChange={handleChange}
        >
          <option value="female">Femenino</option>
          <option value="male">Masculino</option>
        </select>
      </div>

      <div className="mb-3">
        <label className="form-label" htmlFor="edit-tipo">Tipo</label>
        <input
          id="edit-tipo"
          name="tipo"
          className="form-control"
          type="number"
          min="1"
          max="3"
          step="1"
          value={form.tipo}
          onChange={handleChange}
          required
        />
      </div>

      <button type="submit" className="btn btn-primary w-100" disabled={isSubmitting}>
        {isSubmitting ? 'Guardando...' : 'Actualizar'}
      </button>
    </form>
  );
}