import React, { useState, useEffect } from 'react';

const FormularioAdulto = ({ title, initialData, onSubmit, onCancel, isEditing }) => {
  // ... (mismo estado y lógica de antes) ...
  const [form, setForm] = useState({ 
    nombres: '', apellidos: '', fechaNac: '', edad: '', 
    estadoCivil: '', domicilio: '', telefono: '' 
  });

  useEffect(() => {
    if (initialData) setForm(initialData);
    else setForm({ nombres: '', apellidos: '', fechaNac: '', edad: '', estadoCivil: '', domicilio: '', telefono: '' });
  }, [initialData]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    /* CAMBIO: Usamos card-custom y eliminamos bg-light */
    <div className="card-custom p-4">
      <div className="d-flex justify-content-between align-items-center mb-4 border-bottom pb-2">
        <h5 className="m-0 text-dark fw-bold">
          {isEditing ? '✏️ Editar Registro' : '➕ Nuevo Registro'}
        </h5>
        {isEditing && <span className="badge bg-warning text-dark">Modo Edición</span>}
      </div>
      
      <form onSubmit={handleSubmit} className="row g-3">
        {/* ... (Mismos inputs de antes) ... */}
        
        <div className="col-md-6">
          <label className="form-label text-muted fw-bold small">Nombres</label>
          <input type="text" name="nombres" className="form-control" value={form.nombres} onChange={handleChange} required />
        </div>
        <div className="col-md-6">
          <label className="form-label text-muted fw-bold small">Apellidos</label>
          <input type="text" name="apellidos" className="form-control" value={form.apellidos} onChange={handleChange} required />
        </div>
        <div className="col-md-4">
          <label className="form-label text-muted fw-bold small">Fecha Nac.</label>
          <input type="date" name="fechaNac" className="form-control" value={form.fechaNac} onChange={handleChange} required />
        </div>
        <div className="col-md-2">
          <label className="form-label text-muted fw-bold small">Edad</label>
          <input type="number" name="edad" className="form-control" value={form.edad} onChange={handleChange} required />
        </div>
        <div className="col-md-6">
          <label className="form-label text-muted fw-bold small">Estado Civil</label>
          <select name="estadoCivil" className="form-select" value={form.estadoCivil} onChange={handleChange} required>
            <option value="">Seleccione...</option>
            <option value="Soltero(a)">Soltero(a)</option>
            <option value="Casado(a)">Casado(a)</option>
            <option value="Viudo(a)">Viudo(a)</option>
            <option value="Divorciado(a)">Divorciado(a)</option>
          </select>
        </div>
        <div className="col-md-8">
          <label className="form-label text-muted fw-bold small">Domicilio</label>
          <input type="text" name="domicilio" className="form-control" value={form.domicilio} onChange={handleChange} required />
        </div>
        <div className="col-md-4">
          <label className="form-label text-muted fw-bold small">Teléfono</label>
          <input type="number" name="telefono" className="form-control" value={form.telefono} onChange={handleChange} required />
        </div>

        {/* Botones Estilizados */}
        <div className="col-12 d-flex gap-2 mt-4 pt-2 border-top">
          <button type="submit" className="btn btn-orange flex-grow-1 shadow-sm">
            {isEditing ? 'Actualizar Datos' : 'Guardar Registro'}
          </button>
          
          {isEditing && (
            <button type="button" className="btn btn-secondary px-4" onClick={onCancel}>
              Cancelar
            </button>
          )}
        </div>
      </form>
    </div>
  );
};

export default FormularioAdulto;