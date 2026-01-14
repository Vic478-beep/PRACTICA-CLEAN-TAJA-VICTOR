import React, { useState, useEffect } from 'react';
import ApiService from './services/api';
import './App.css'; // Importante: Importar los estilos

// Importar Páginas
import AdultosPage from './pages/AdultosPage';
import EnfermeriaPage from './pages/EnfermeriaPage';
import FisioterapiaPage from './pages/FisioterapiaPage';
import OrientacionPage from './pages/OrientacionPage';
import ProteccionPage from './pages/ProteccionPage';

function App() {
  const [activeTab, setActiveTab] = useState('adultos');
  const [adultosList, setAdultosList] = useState([]);

  // Cargar lista globalmente
  useEffect(() => {
    const fetchAdultos = async () => {
      try { 
        const api = ApiService.getInstance();
        const res = await api.get('/Adulto/listar_adultos'); 
        setAdultosList(res); 
      } catch(e) { console.error(e); }
    };
    fetchAdultos();
  }, [activeTab]);

  return (
    <div className="dashboard-container">
      {/* --- Sidebar Izquierda --- */}
      <aside className="sidebar">
        <div className="sidebar-header">
          <img src="../public/logo-alcaldia.png" alt="Logo Alcaldía de Tarija" />
          <small>Gestión Integral Adulto Mayor</small>
        </div>
        
        <nav className="sidebar-menu">
          <button 
            className={`nav-button ${activeTab === 'adultos' ? 'active' : ''}`}
            onClick={() => setActiveTab('adultos')}
          >
            <span>👵</span> Adultos Mayores
          </button>
          
          <button 
            className={`nav-button ${activeTab === 'enfermeria' ? 'active' : ''}`}
            onClick={() => setActiveTab('enfermeria')}
          >
            <span>💉</span> Enfermería
          </button>
          
          <button 
            className={`nav-button ${activeTab === 'fisioterapia' ? 'active' : ''}`}
            onClick={() => setActiveTab('fisioterapia')}
          >
            <span>💪</span> Fisioterapia
          </button>
          
          <button 
            className={`nav-button ${activeTab === 'orientacion' ? 'active' : ''}`}
            onClick={() => setActiveTab('orientacion')}
          >
            <span>🧠</span> Orientación
          </button>
          
          <button 
            className={`nav-button ${activeTab === 'proteccion' ? 'active' : ''}`}
            onClick={() => setActiveTab('proteccion')}
          >
            <span>🛡️</span> Protección Social
          </button>
        </nav>
        
        <div className="p-3 text-center text-white-50">
          <small>© 2026 Sistema V1.0</small>
        </div>
      </aside>

      {/* --- Contenido Principal Derecha --- */}
      <main className="main-content">
        {/* Cabecera dinámica */}
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h2 className="text-dark fw-bold text-capitalize">
            Gestión de {activeTab.replace('adultos', 'Adultos Mayores')}
          </h2>
          <div className="badge bg-warning text-dark p-2">
            Admin Conectado
          </div>
        </div>

        {/* Renderizado de Páginas */}
        <div className="fade-in">
          {activeTab === 'adultos' && <AdultosPage />}
          {activeTab === 'enfermeria' && <EnfermeriaPage listAdultos={adultosList} />}
          {activeTab === 'fisioterapia' && <FisioterapiaPage listAdultos={adultosList} />}
          {activeTab === 'orientacion' && <OrientacionPage listAdultos={adultosList} />}
          {activeTab === 'proteccion' && <ProteccionPage listAdultos={adultosList} />}
        </div>
      </main>
    </div>
  );
}

export default App;