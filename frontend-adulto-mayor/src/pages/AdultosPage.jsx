import React, { useEffect, useState } from 'react';
import Tabla from '../components/Tabla';
import FormularioAdulto from '../components/FormularioAdulto';
import Buscador from '../components/Buscador';
import ApiService from '../services/api';
import { adultoAdapter } from '../adapters'; // IMPORTAMOS ADAPTER

const AdultosPage = () => {
  const [adultos, setAdultos] = useState([]);
  const [busqueda, setBusqueda] = useState('');
  const [error, setError] = useState(null);
  
  const [editingId, setEditingId] = useState(null);
  const [formData, setFormData] = useState(null);

  const fetchAdultos = () => {
    const api = ApiService.getInstance();
    api.get('/Adulto/listar_adultos')
      .then(data => {
        // APLICAMOS ADAPTER: Convertimos la data cruda a data segura
        const adaptados = data.map(item => adultoAdapter(item));
        setAdultos(adaptados);
        setError(null);
      })
      .catch(err => setError("Error de conexión."));
  };

  useEffect(() => { fetchAdultos(); }, []);

  const validarAdulto = (datos) => {
    if (!datos.nombres?.trim()) return "El nombre no puede estar vacío.";
    if (!datos.apellidos?.trim()) return "El apellido no puede estar vacío.";
    if (!datos.fechaNac) return "La fecha de nacimiento es obligatoria.";
    const edad = parseInt(datos.edad);
    if (isNaN(edad) || edad < 60 || edad > 110) return "La edad debe estar entre 60 y 110 años.";
    if (!datos.estadoCivil?.trim()) return "El estado civil es obligatorio.";
    if (!datos.domicilio?.trim()) return "El domicilio es obligatorio.";
    const tel = parseInt(datos.telefono);
    if (isNaN(tel) || tel <= 0) return "El número de teléfono no es válido.";
    return null;
  };

  const handleSubmit = (datos) => {
    const errorValidacion = validarAdulto(datos);
    if (errorValidacion) { alert(errorValidacion); return; }

    const api = ApiService.getInstance();
    const request = editingId 
      ? api.put(`/Adulto/editar_adulto/${editingId}`, { ...datos, id: editingId })
      : api.post('/Adulto/crear_adulto', datos);

    request
      .then(() => { fetchAdultos(); handleCancel(); })
      .catch(err => alert("Error: " + err));
  };

  // FILTRO LIMPIO: Gracias al Adapter, ya no necesitamos validar nulos aquí
  const resultadosFiltrados = adultos.filter(item => {
    const texto = busqueda.toLowerCase();
    return (
      item.nombres.toLowerCase().includes(texto) ||
      item.apellidos.toLowerCase().includes(texto) ||
      item.telefono.toString().includes(texto) ||
      item.domicilio.toLowerCase().includes(texto)
    );
  });

  const handleEdit = (item) => { setEditingId(item.id); setFormData(item); window.scrollTo(0, 0); };
  const handleCancel = () => { setEditingId(null); setFormData(null); };
  const handleDelete = (id) => {
    if(window.confirm("¿Eliminar?")) ApiService.getInstance().delete(`/Adulto/eliminar_adulto/${id}`).then(fetchAdultos);
  };

  const columnas = [
    { header: 'Nombres', field: 'nombres' },
    { header: 'Apellidos', field: 'apellidos' },
    { header: 'Edad', field: 'edad' },
    { header: 'Teléfono', field: 'telefono' },
    { header: 'Domicilio', field: 'domicilio' }
  ];

  return (
    <div className="p-2">
      {error && <div className="alert alert-danger">{error}</div>}
      <div className="mb-4">
        <FormularioAdulto 
          title="Adulto" 
          onSubmit={handleSubmit}
          initialData={formData}
          isEditing={!!editingId}
          onCancel={handleCancel}
        />
      </div>
      <Buscador value={busqueda} onChange={setBusqueda} placeholder="Buscar adulto..." />
      <Tabla data={resultadosFiltrados} cols={columnas} onEdit={handleEdit} onDelete={handleDelete} idField="id" />
    </div>
  );
};

export default AdultosPage;