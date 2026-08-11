import { ArrowLeft, Pencil } from 'lucide-react';
import { useNavigate, useParams } from 'react-router-dom';
import PersonaEditForm from '../components/PersonaEditForm';

export default function PersonaEditPage() {
  const navigate = useNavigate();
  const { personaId } = useParams();

  return (
    <section className="form-page mx-auto">
      <button type="button" className="btn btn-link back-link mb-4" onClick={() => navigate('/personas')}>
        <ArrowLeft size={17} aria-hidden="true" />
        Volver al listado
      </button>
      <div className="form-card">
        <div className="form-page-heading">
          <span className="page-icon"><Pencil size={21} aria-hidden="true" /></span>
          <div>
            <p className="eyebrow mb-1">Mantenimiento</p>
            <h1 className="h2 mb-1">Editar persona</h1>
            <p className="text-muted mb-0">Actualiza los datos del registro seleccionado.</p>
          </div>
        </div>
        <hr className="my-4" />
        <PersonaEditForm
          personaId={personaId}
          onSaved={() => navigate('/personas')}
          onCancelled={() => navigate('/personas')}
        />
      </div>
    </section>
  );
}