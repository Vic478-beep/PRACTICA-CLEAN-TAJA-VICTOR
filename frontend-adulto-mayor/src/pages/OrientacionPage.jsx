import React, { useEffect, useState } from 'react';
import Tabla from '../components/Tabla';
import FormularioFicha from '../components/FormularioFicha';
import Buscador from '../components/Buscador';
import ApiService from '../services/api';
import { fichaAdapter } from '../adapters'; // IMPORTAMOS ADAPTER

const OrientacionPage = ({ listAdultos }) => {
  const [fichas, setFichas] = useState([]);
  const [busqueda, setBusqueda] = useState('');
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState(null);

  const camposOrientacion = [
    { name: 'fechaOrientacion', label: 'Fecha Orientación', type: 'date' },
    { name: 'tipoOrientacion', label: 'Tipo Orientación', type: 'text' },
    { name: 'descripcion', label: 'Descripción / Observaciones', type: 'text' }
  ];

  const fetchFichas = () => {
    const api = ApiService.getInstance();
    api.get('/FichaOrientacion/listar_fichas_orientacion')
      .then(data => {
        // APLICAMOS ADAPTER
        const adaptados = data.map(f => fichaAdapter(f, listAdultos));
        setFichas(adaptados);
      })
      .catch(error => console.error(error));
  };

  useEffect(() => { fetchFichas(); }, [listAdultos]);

  const validarFicha = (datos) => {
    if (!datos.idAdulto) return "Error en ID adulto.";
    if (!datos.fechaOrientacion) return "Fecha obligatoria.";
    if (!datos.tipoOrientacion?.trim()) return "Tipo obligatorio.";
    if (!datos.descripcion?.trim()) return "Descripción obligatoria.";
    return null;
  };

  const handleSubmit = (datos) => {
    const error = validarFicha(datos);
    if (error) { alert(error); return; }

    const api = ApiService.getInstance();
    const request = editingId 
      ? api.put(`/FichaOrientacion/editar_ficha_orientacion/${editingId}`, { ...datos, codOri: editingId })
      : api.post('/FichaOrientacion/crear_ficha_orientacion', datos);

    request.then(() => { fetchFichas(); handleCancel(); });
  };

  const resultadosFiltrados = fichas.filter(item => {
    const texto = busqueda.toLowerCase();
    return (
      item.nombreCompleto.toLowerCase().includes(texto) ||
      (item.tipoOrientacion || '').toLowerCase().includes(texto) ||
      (item.descripcion || '').toLowerCase().includes(texto)
    );
  });

  const handleEdit = (item) => { setEditingId(item.codOri); setFormData(item); window.scrollTo(0, 0); };
  const handleCancel = () => { setEditingId(null); setFormData(null); };
  const handleDelete = (id) => {
    if(window.confirm("¿Eliminar?")) ApiService.getInstance().delete(`/FichaOrientacion/eliminar_ficha_orientacion/${id}`).then(fetchFichas);
  };

  const columnas = [
    { header: 'Fecha', field: 'fechaOrientacion' },
    { header: 'Paciente', field: 'nombreCompleto' },
    { header: 'Tipo', field: 'tipoOrientacion' },
    { header: 'Descripción', field: 'descripcion' }
  ];

  return (
    <div className="p-2">
      <FormularioFicha 
        title="Ficha Orientación" 
        listAdultos={listAdultos} 
        fieldsConfig={camposOrientacion} 
        onSubmit={handleSubmit}
        initialData={formData}
        isEditing={!!editingId}
        onCancel={handleCancel}
      />
      <Buscador value={busqueda} onChange={setBusqueda} placeholder="Buscar..." />
      <Tabla data={resultadosFiltrados} cols={columnas} onEdit={handleEdit} onDelete={handleDelete} idField="codOri" />
    </div>
  );
};

export default OrientacionPage;