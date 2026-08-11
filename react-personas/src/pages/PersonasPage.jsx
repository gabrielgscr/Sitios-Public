import { useNavigate } from 'react-router-dom';
import PersonaList from '../components/PersonaList';

export default function PersonasPage() {
  const navigate = useNavigate();

  return <PersonaList onEdit={personaId => navigate(`/personas/${encodeURIComponent(personaId)}/editar`)} />;
}