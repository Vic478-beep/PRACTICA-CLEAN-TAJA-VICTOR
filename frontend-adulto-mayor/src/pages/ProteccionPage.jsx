import React, { useEffect, useState } from 'react';
import Tabla from '../components/Tabla';
import FormularioFicha from '../components/FormularioFicha';
import Buscador from '../components/Buscador';
import ApiService from '../services/api';
import { fichaAdapter } from '../adapters'; // IMPORTAMOS ADAPTER

const ProteccionPage = ({ listAdultos }) => {
  const [fichas, setFichas] = useState([]);
  const [busqueda, setBusqueda] = useState('');
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState(null);

  const camposProteccion = [
    { name: 'fechaProteccion', label: 'Fecha Protección', type: 'date' },
    { name: 'tipoProteccion', label: 'Tipo Protección', type: 'text' },
    { name: 'descripcion', label: 'Descripción del Caso', type: 'text' }
  ];

  const fetchFichas = () => {
    const api = ApiService.getInstance();
    api.get('/FichaProteccion/listar_fichas_proteccion')
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
    if (!datos.fechaProteccion) return "Fecha obligatoria.";
    if (!datos.tipoProteccion?.trim()) return "Tipo obligatorio.";
    if (!datos.descripcion?.trim()) return "Descripción obligatoria.";
    return null;
  };

  const handleSubmit = (datos) => {
    const error = validarFicha(datos);
    if (error) { alert(error); return; }

    const api = ApiService.getInstance();
    const request = editingId 
      ? api.put(`/FichaProteccion/editar_ficha_proteccion/${editingId}`, { ...datos, codPro: editingId })
      : api.post('/FichaProteccion/crear_ficha_proteccion', datos);

    request.then(() => { fetchFichas(); handleCancel(); });
  };

  const resultadosFiltrados = fichas.filter(item => {
    const texto = busqueda.toLowerCase();
    return (
      item.nombreCompleto.toLowerCase().includes(texto) ||
      (item.tipoProteccion || '').toLowerCase().includes(texto) ||
      (item.descripcion || '').toLowerCase().includes(texto)
    );
  });

  const handleEdit = (item) => { setEditingId(item.codPro); setFormData(item); window.scrollTo(0, 0); };
  const handleCancel = () => { setEditingId(null); setFormData(null); };
  const handleDelete = (id) => {
    if(window.confirm("¿Eliminar?")) ApiService.getInstance().delete(`/FichaProteccion/eliminar_ficha_proteccion/${id}`).then(fetchFichas);
  };

  const columnas = [
    { header: 'Fecha', field: 'fechaProteccion' },
    { header: 'Paciente', field: 'nombreCompleto' },
    { header: 'Tipo', field: 'tipoProteccion' },
    { header: 'Descripción', field: 'descripcion' }
  ];

  return (
    <div className="p-2">
      <FormularioFicha 
        title="Caso Protección" 
        listAdultos={listAdultos} 
        fieldsConfig={camposProteccion} 
        onSubmit={handleSubmit}
        initialData={formData}
        isEditing={!!editingId}
        onCancel={handleCancel}
      />
      <Buscador value={busqueda} onChange={setBusqueda} placeholder="Buscar..." />
      <Tabla data={resultadosFiltrados} cols={columnas} onEdit={handleEdit} onDelete={handleDelete} idField="codPro" />
    </div>
  );
};

export default ProteccionPage;