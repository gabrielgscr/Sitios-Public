import { ArrowLeft, UserPlus } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import PersonaForm from '../components/PersonaForm';

export default function PersonaCreatePage() {
  const navigate = useNavigate();

  return (
    <section className="form-page mx-auto">
      <button type="button" className="btn btn-link back-link mb-4" onClick={() => navigate('/personas')}>
        <ArrowLeft size={17} aria-hidden="true" />
        Volver al listado
      </button>
      <div className="form-card">
        <div className="form-page-heading">
          <span className="page-icon"><UserPlus size={22} aria-hidden="true" /></span>
          <div>
            <p className="eyebrow mb-1">Nuevo registro</p>
            <h1 className="h2 mb-1">Crear persona</h1>
            <p className="text-muted mb-0">Completa los datos para agregarla al padrón.</p>
          </div>
        </div>
        <hr className="my-4" />
        <PersonaForm onAdd={() => navigate('/personas')} />
      </div>
    </section>
  );
}