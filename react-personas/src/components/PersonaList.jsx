import { useEffect, useState } from 'react';
import { Pencil, RefreshCw, Search, Trash2 } from 'lucide-react';
import { deletePersona, getPersonas } from '../services/personaService';

export default function PersonaList({ onEdit }) {
  const [personas, setPersonas] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  const [deletingId, setDeletingId] = useState('');
  const [query, setQuery] = useState('');
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [reloadToken, setReloadToken] = useState(0);

  useEffect(() => {
    const controller = new AbortController();

    const loadPersonas = async () => {
      setIsLoading(true);
      setError('');

      try {
        const data = await getPersonas({ pageNumber, pageSize, signal: controller.signal });
        setPersonas(Array.isArray(data.items) ? data.items : []);
        setTotalCount(Number.isInteger(data.totalCount) ? data.totalCount : 0);
        setTotalPages(Number.isInteger(data.totalPages) ? data.totalPages : 0);
      } catch (requestError) {
        if (requestError.name !== 'AbortError') {
          setError(requestError.message || 'No se pudieron cargar las personas.');
        }
      } finally {
        if (!controller.signal.aborted) {
          setIsLoading(false);
        }
      }
    };

    loadPersonas();

    return () => {
      controller.abort();
    };
  }, [pageNumber, pageSize, reloadToken]);

  const handleDelete = async persona => {
    const confirmed = window.confirm(`¿Eliminar definitivamente a ${persona.nombre}?`);

    if (!confirmed) {
      return;
    }

    setDeletingId(persona.personaId);
    setError('');

    try {
      await deletePersona(persona.personaId);
      if (personas.length === 1 && pageNumber > 1) {
        setPageNumber(current => current - 1);
      } else {
        setReloadToken(current => current + 1);
      }
    } catch (requestError) {
      setError(requestError.message || 'No se pudo eliminar la persona.');
    } finally {
      setDeletingId('');
    }
  };

  const visiblePersonas = personas.filter(persona => {
    const normalizedQuery = query.trim().toLowerCase();
    return !normalizedQuery
      || persona.personaId.toLowerCase().includes(normalizedQuery)
      || persona.nombre.toLowerCase().includes(normalizedQuery);
  });

  return (
    <div>
      <div className="d-flex flex-column flex-sm-row justify-content-between align-items-sm-center gap-3 mb-4">
        <div>
          <h1 className="h3 mb-1">Personas registradas</h1>
          <p className="text-muted mb-0">{totalCount} registros disponibles</p>
        </div>
        <button
          type="button"
          className="btn btn-light icon-text-button align-self-start align-self-sm-auto"
          onClick={() => setReloadToken(current => current + 1)}
          disabled={isLoading}
        >
          <RefreshCw size={17} className={isLoading ? 'spin' : ''} aria-hidden="true" />
          Actualizar
        </button>
      </div>

      {error ? <div className="alert alert-danger py-2">{error}</div> : null}

      {isLoading ? (
        <div className="text-center py-4 text-muted">Cargando personas...</div>
      ) : (
        <>
          <div className="search-field mb-3">
            <Search size={18} aria-hidden="true" />
            <input
              type="search"
              className="form-control"
              placeholder="Buscar en esta página por ID o nombre"
              value={query}
              onChange={event => setQuery(event.target.value)}
            />
          </div>
          <div className="table-responsive">
            <table className="table table-hover align-middle mb-0">
            <thead className="table-dark">
              <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Género</th>
                <th>Tipo</th>
                <th className="text-end">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {visiblePersonas.length === 0 ? (
                <tr>
                  <td colSpan="5" className="text-center text-muted py-4">
                    {personas.length === 0 ? 'No hay personas registradas.' : 'No hay coincidencias.'}
                  </td>
                </tr>
              ) : visiblePersonas.map(persona => (
                <tr key={persona.personaId}>
                  <td>{persona.personaId}</td>
                  <td>{persona.nombre}</td>
                  <td>{persona.gender === 'male' ? 'Masculino' : 'Femenino'}</td>
                  <td>{persona.tipo}</td>
                  <td className="text-end">
                    <div className="d-inline-flex gap-1" aria-label={`Acciones para ${persona.nombre}`}>
                      <button
                        type="button"
                        className="btn btn-outline-primary action-icon"
                        onClick={() => onEdit(persona.personaId)}
                        title={`Editar a ${persona.nombre}`}
                        aria-label={`Editar a ${persona.nombre}`}
                      >
                        <Pencil size={16} aria-hidden="true" />
                      </button>
                      <button
                        type="button"
                        className="btn btn-outline-danger action-icon"
                        onClick={() => handleDelete(persona)}
                        disabled={deletingId === persona.personaId}
                        title={`Eliminar a ${persona.nombre}`}
                        aria-label={`Eliminar a ${persona.nombre}`}
                      >
                        {deletingId === persona.personaId ? <span className="spinner-border spinner-border-sm" /> : <Trash2 size={16} aria-hidden="true" />}
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          </div>
          <div className="d-flex flex-column flex-sm-row justify-content-between align-items-sm-center gap-3 mt-3">
            <div className="d-flex align-items-center gap-2 text-muted small">
              <label htmlFor="page-size">Filas por página</label>
              <select
                id="page-size"
                className="form-select form-select-sm page-size-select"
                value={pageSize}
                onChange={event => {
                  setPageSize(Number(event.target.value));
                  setPageNumber(1);
                }}
              >
                <option value="5">5</option>
                <option value="10">10</option>
                <option value="25">25</option>
                <option value="50">50</option>
              </select>
            </div>
            <nav className="d-flex align-items-center gap-2" aria-label="Paginación de personas">
              <button
                type="button"
                className="btn btn-outline-secondary btn-sm"
                onClick={() => setPageNumber(current => current - 1)}
                disabled={pageNumber === 1 || isLoading}
              >
                Anterior
              </button>
              <span className="small text-muted text-nowrap">
                Página {pageNumber} de {Math.max(totalPages, 1)}
              </span>
              <button
                type="button"
                className="btn btn-outline-secondary btn-sm"
                onClick={() => setPageNumber(current => current + 1)}
                disabled={pageNumber >= totalPages || isLoading || totalPages === 0}
              >
                Siguiente
              </button>
            </nav>
          </div>
        </>
      )}
    </div>
  );
}
