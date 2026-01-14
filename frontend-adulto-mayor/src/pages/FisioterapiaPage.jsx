import React, { useEffect, useState } from 'react';
import Tabla from '../components/Tabla';
import FormularioFicha from '../components/FormularioFicha';
import Buscador from '../components/Buscador';
import ApiService from '../services/api';
import { fichaAdapter } from '../adapters'; // IMPORTAMOS ADAPTER

const FisioterapiaPage = ({ listAdultos }) => {
  const [fichas, setFichas] = useState([]);
  const [busqueda, setBusqueda] = useState('');
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState(null);

  const camposFisioterapia = [
    { name: 'fechaProgramacion', label: 'Fecha Programación', type: 'date' },
    { name: 'numeroSesiones', label: 'N° Sesiones', type: 'number' },
    { name: 'motivoConsulta', label: 'Motivo Consulta', type: 'text' },
    { name: 'equiposEmpleados', label: 'Equipos Empleados', type: 'text' }
  ];

  const fetchFichas = () => {
    const api = ApiService.getInstance();
    api.get('/FichaFisioterapia/listar_fichas_fisioterapia')
      .then(data => {
        // APLICAMOS ADAPTER
        const adaptados = data.map(f => fichaAdapter(f, listAdultos));
        setFichas(adaptados);
      })
      .catch(error => console.error(error));
  };

  useEffect(() => { fetchFichas(); }, [listAdultos]);

  const validarFicha = (datos) => {
    if (!datos.idAdulto) return "ID Adulto inválido.";
    if (!datos.fechaProgramacion) return "Fecha obligatoria.";
    const sesiones = parseInt(datos.numeroSesiones);
    if (isNaN(sesiones) || sesiones <= 0) return "Sesiones debe ser mayor a 0.";
    if (!datos.motivoConsulta?.trim()) return "Motivo obligatorio.";
    if (!datos.equiposEmpleados?.trim()) return "Equipos obligatorios.";
    return null;
  };

  const handleSubmit = (datos) => {
    const error = validarFicha(datos);
    if (error) { alert(error); return; }

    const api = ApiService.getInstance();
    const request = editingId 
      ? api.put(`/FichaFisioterapia/editar_ficha_fisioterapia/${editingId}`, { ...datos, codFis: editingId })
      : api.post('/FichaFisioterapia/crear_ficha_fisioterapia', datos);

    request.then(() => { fetchFichas(); handleCancel(); }).catch(err => console.error(err));
  };

  const resultadosFiltrados = fichas.filter(item => {
    const texto = busqueda.toLowerCase();
    return (
      item.nombreCompleto.toLowerCase().includes(texto) ||
      (item.motivoConsulta || '').toLowerCase().includes(texto) ||
      (item.equiposEmpleados || '').toLowerCase().includes(texto)
    );
  });

  const handleEdit = (item) => { setEditingId(item.codFis); setFormData(item); window.scrollTo(0, 0); };
  const handleCancel = () => { setEditingId(null); setFormData(null); };
  const handleDelete = (id) => {
    if(window.confirm("¿Eliminar?")) ApiService.getInstance().delete(`/FichaFisioterapia/eliminar_ficha_fisioterapia/${id}`).then(fetchFichas);
  };

  const columnas = [
    { header: 'Fecha', field: 'fechaProgramacion' },
    { header: 'Paciente', field: 'nombreCompleto' },
    { header: 'Motivo', field: 'motivoConsulta' },
    { header: 'Sesiones', field: 'numeroSesiones' },
    { header: 'Equipos', field: 'equiposEmpleados' }
  ];

  return (
    <div className="p-2">
      <FormularioFicha 
        title="Ficha Fisioterapia" 
        listAdultos={listAdultos} 
        fieldsConfig={camposFisioterapia} 
        onSubmit={handleSubmit} 
        initialData={formData}
        isEditing={!!editingId}
        onCancel={handleCancel}
      />
      <Buscador value={busqueda} onChange={setBusqueda} placeholder="Buscar..." />
      <Tabla data={resultadosFiltrados} cols={columnas} onEdit={handleEdit} onDelete={handleDelete} idField="codFis" />
    </div>
  );
};

export default FisioterapiaPage;