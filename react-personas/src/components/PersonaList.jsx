import { useEffect, useState } from 'react';
import { getPersonas } from '../services/personaService';

export default function PersonaList() {
  const [personas, setPersonas] = useState([]);

  useEffect(() => {
    getPersonas().then(setPersonas);
  }, []);

  return (
    <div>
      <h2 className="mb-4">Lista de Personas</h2>
      <div className="table">
        <table className="table table-striped table-bordered">
          <thead className="table-dark">
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Género</th>
              <th>Tipo</th>
            </tr>
          </thead>
          <tbody>
            {personas.map(p => (
              <tr key={p.personaId}>
                <td>{p.personaId}</td>
                <td>{p.nombre}</td>
                <td>{p.gender === 'male' ? 'Masculino' : 'Femenino'}</td>
                <td>{p.tipo}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
