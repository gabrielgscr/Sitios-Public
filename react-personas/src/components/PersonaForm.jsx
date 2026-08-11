import { useState } from 'react';
import { addPersona } from '../services/personaService';

export default function PersonaForm({ onAdd }) {
  const [form, setForm] = useState({
    personaId: '',
    nombre: '',
    tipo: 1,
    gender: 'male',
    password: ''
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState('');

  const handleChange = e => {
    const { name, value, type } = e.target;

    setForm(current => ({
      ...current,
      [name]: type === 'number' ? (value === '' ? '' : Number(value)) : value
    }));
  };

  const handleSubmit = async e => {
    e.preventDefault();

    if (!form.personaId.trim() || !form.nombre.trim() || !form.password.trim()) {
      setError('Completa ID, nombre y contraseña.');
      return;
    }

    if (form.nombre.trim().length < 5) {
      setError('El nombre debe tener al menos 5 caracteres.');
      return;
    }

    if (form.password.length < 8) {
      setError('La contraseña debe tener al menos 8 caracteres.');
      return;
    }

    if (form.tipo === '' || Number.isNaN(form.tipo) || form.tipo < 1 || form.tipo > 3) {
      setError('El tipo debe estar entre 1 y 3.');
      return;
    }

    setIsSubmitting(true);
    setError('');

    try {
      await addPersona({ ...form, telefono: [], rol: [] });
      onAdd();
      setForm({ personaId: '', nombre: '', tipo: 1, gender: 'male', password: '' });
    } catch (requestError) {
      setError(requestError.message || 'No se pudo guardar la persona.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} noValidate>
      {error ? <div className="alert alert-danger py-2">{error}</div> : null}
      <div className="mb-3">
        <label className="form-label" htmlFor="personaId">ID</label>
        <input id="personaId" name="personaId" className="form-control" placeholder="Ej. 123456789" value={form.personaId} onChange={handleChange} required autoFocus />
      </div>
      <div className="mb-3">
        <label className="form-label" htmlFor="nombre">Nombre</label>
        <input id="nombre" name="nombre" className="form-control" placeholder="Nombre completo" value={form.nombre} onChange={handleChange} minLength="5" required />
      </div>
      <div className="mb-3">
        <label className="form-label" htmlFor="gender">Género</label>
        <select id="gender" name="gender" className="form-select" value={form.gender} onChange={handleChange}>
          <option value="female">Femenino</option>
          <option value="male">Masculino</option>
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label" htmlFor="tipo">Tipo</label>
        <input id="tipo" name="tipo" className="form-control" placeholder="1" type="number" min="1" max="3" step="1" value={form.tipo} onChange={handleChange} required />
      </div>
      <div className="mb-3">
        <label className="form-label" htmlFor="password">Contraseña</label>
        <input id="password" name="password" className="form-control" placeholder="Mínimo 8 caracteres" type="password" value={form.password} onChange={handleChange} minLength="8" required />
      </div>
      <button type="submit" className="btn btn-primary w-100" disabled={isSubmitting}>
        {isSubmitting ? 'Guardando...' : 'Guardar'}
      </button>
    </form>
  );
}
