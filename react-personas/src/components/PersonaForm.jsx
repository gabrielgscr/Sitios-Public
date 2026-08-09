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

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = e => {
    e.preventDefault();
    addPersona({ ...form, telefono: [], rol: [] }).then(() => {
      onAdd();
      setForm({ personaId: '', nombre: '', tipo: 1, gender: 'male', password: '' });
    });
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2 className="mb-4">Agregar Persona</h2>
      <div className="mb-3">
        <input name="personaId" className="form-control w-100" placeholder="ID" value={form.personaId} onChange={handleChange} />
      </div>
      <div className="mb-3">
        <input name="nombre" className="form-control w-100" placeholder="Nombre" value={form.nombre} onChange={handleChange} />
      </div>
      <div className="mb-3">
        <select name="gender" className="form-select w-100" value={form.gender} onChange={handleChange}>
          <option value="female">Femenino</option>
          <option value="male">Masculino</option>
        </select>
      </div>
      <div className="mb-3">
        <input name="tipo" className="form-control w-100" placeholder="Tipo" type="number" value={form.tipo} onChange={handleChange} />
      </div>
      <div className="mb-3">
        <input name="password" className="form-control w-100" placeholder="Contraseña" value={form.password} onChange={handleChange} />
      </div>
      <button type="submit" className="btn btn-primary w-100">Guardar</button>
    </form>
  );
}
