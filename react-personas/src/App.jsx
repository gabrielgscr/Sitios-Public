import { useState } from 'react';
import PersonaList from './components/PersonaList';
import PersonaForm from './components/PersonaForm';
import './App.css';

function App() {
  const [refresh, setRefresh] = useState(false);

  const handleAdd = () => {
    setRefresh(!refresh);
  };

  return (
    <div className="full-width-app">
      <div className='container'>
        <h1 className="text-center mb-4">Gestión de Personas</h1>
        <PersonaForm onAdd={handleAdd} />
        <hr className="my-4" />
        <PersonaList key={refresh} />
      </div>
    </div>
  );
}

export default App;
