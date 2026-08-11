import { NavLink, Navigate, Route, Routes } from 'react-router-dom';
import { CircleUserRound, List, UserPlus } from 'lucide-react';
import PersonasPage from './pages/PersonasPage';
import PersonaCreatePage from './pages/PersonaCreatePage';
import PersonaEditPage from './pages/PersonaEditPage';
import './App.css';

function App() {
  return (
    <div className="full-width-app">
      <header className="app-header border-bottom">
        <nav className="app-shell container d-flex align-items-center justify-content-between gap-3 py-3" aria-label="Navegación principal">
          <NavLink className="brand d-flex align-items-center gap-2" to="/personas">
            <span className="brand-mark"><CircleUserRound size={23} strokeWidth={2.2} /></span>
            <span>Personas</span>
          </NavLink>

          <div className="d-flex align-items-center gap-2">
            <NavLink className="btn btn-light nav-action" to="/personas">
              <List size={18} aria-hidden="true" />
              <span>Listado</span>
            </NavLink>
            <NavLink className="btn btn-primary nav-action" to="/personas/nueva">
              <UserPlus size={18} aria-hidden="true" />
              <span>Nueva persona</span>
            </NavLink>
          </div>
        </nav>
      </header>

      <main className="app-shell container py-4 py-lg-5">
        <Routes>
          <Route path="/" element={<Navigate to="/personas" replace />} />
          <Route path="/personas" element={<PersonasPage />} />
          <Route path="/personas/nueva" element={<PersonaCreatePage />} />
          <Route path="/personas/:personaId/editar" element={<PersonaEditPage />} />
          <Route path="*" element={<Navigate to="/personas" replace />} />
        </Routes>
      </main>
    </div>
  );
}

export default App;
