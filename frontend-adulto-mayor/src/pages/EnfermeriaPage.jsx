import React, { useEffect, useState } from 'react';
import Tabla from '../components/Tabla';
import FormularioFicha from '../components/FormularioFicha';
import Buscador from '../components/Buscador';
import ApiService from '../services/api';
import { fichaAdapter } from '../adapters'; // IMPORTAMOS ADAPTER

const EnfermeriaPage = ({ listAdultos }) => {
  const [fichas, setFichas] = useState([]);
  const [busqueda, setBusqueda] = useState('');
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState(null);

  const camposEnfermeria = [
    { name: 'temperatura', label: 'Temperatura', type: 'text' },
    { name: 'pesoTalla', label: 'Peso / Talla', type: 'text' },
    { name: 'tratamiento', label: 'Tratamiento', type: 'text' }
  ];

  const fetchFichas = () => {
    const api = ApiService.getInstance();
    api.get('/FichaEnfermeria/listar_fichas_enfermeria')
      .then(data => {
        // APLICAMOS ADAPTER: Mapeamos la data cruda con la lista de adultos
        const adaptados = data.map(f => fichaAdapter(f, listAdultos));
        setFichas(adaptados);
      })
      .catch(error => console.error(error));
  };

  useEffect(() => { fetchFichas(); }, [listAdultos]);

  const validarFicha = (datos) => {
    if (!datos.idAdulto) return "Debe seleccionar un adulto.";
    if (!datos.temperatura?.trim()) return "Error en temperatura.";
    if (!datos.pesoTalla?.trim()) return "Error en peso y talla.";
    if (!datos.tratamiento?.trim()) return "Error en tratamiento.";
    return null;
  };

  const handleSubmit = (datos) => {
    const error = validarFicha(datos);
    if (error) { alert(error); return; }

    const api = ApiService.getInstance();
    const request = editingId 
      ? api.put(`/FichaEnfermeria/editar_ficha_enfermeria/${editingId}`, { ...datos, codEnf: editingId })
      : api.post('/FichaEnfermeria/crear_ficha_enfermeria', datos);

    request.then(() => { fetchFichas(); handleCancel(); }).catch(err => console.error(err));
  };

  // FILTRO LIMPIO: Usamos 'nombreCompleto' que viene del Adapter
  const resultadosFiltrados = fichas.filter(item => {
    const texto = busqueda.toLowerCase();
    return (
      item.nombreCompleto.toLowerCase().includes(texto) ||
      (item.tratamiento || '').toLowerCase().includes(texto)
    );
  });

  const handleEdit = (item) => { setEditingId(item.codEnf); setFormData(item); window.scrollTo(0, 0); };
  const handleCancel = () => { setEditingId(null); setFormData(null); };
  const handleDelete = (id) => {
    if(window.confirm("¿Eliminar?")) ApiService.getInstance().delete(`/FichaEnfermeria/eliminar_ficha_enfermeria/${id}`).then(fetchFichas);
  };

  const columnas = [
    { header: 'Paciente', field: 'nombreCompleto' },
    { header: 'Temp.', field: 'temperatura' },
    { header: 'Peso/Talla', field: 'pesoTalla' },
    { header: 'Tratamiento', field: 'tratamiento' }
  ];

  return (
    <div className="p-2">
      <FormularioFicha 
        title="Ficha de Enfermería" 
        listAdultos={listAdultos} 
        fieldsConfig={camposEnfermeria} 
        onSubmit={handleSubmit}
        initialData={formData}
        isEditing={!!editingId}
        onCancel={handleCancel}
      />
      <Buscador value={busqueda} onChange={setBusqueda} placeholder="Buscar..." />
      <Tabla data={resultadosFiltrados} cols={columnas} onEdit={handleEdit} onDelete={handleDelete} idField="codEnf" />
    </div>
  );
};

export default EnfermeriaPage;