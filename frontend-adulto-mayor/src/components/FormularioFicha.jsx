import React, { useState, useEffect } from 'react';

const FormularioFicha = ({ 
  title, 
  initialData, 
  onSubmit, 
  onCancel, 
  isEditing, 
  listAdultos = [], // Valor por defecto para evitar crash
  fieldsConfig = [] // Array de objetos { name: 'diagnostico', label: 'Diagnóstico', type: 'text' }
}) => {
  
  const [form, setForm] = useState({ idAdulto: '' });

  useEffect(() => {
    if (initialData) {
      setForm(initialData);
    } else {
      // Resetear form con campos vacíos según config
      const cleanForm = { idAdulto: '' };
      fieldsConfig.forEach(f => cleanForm[f.name] = '');
      setForm(cleanForm);
    }
  }, [initialData, fieldsConfig]);

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <div className="card p-4 mb-4 border-0 shadow-sm bg-light">
      <h5 className="mb-3 text-primary">{isEditing ? 'Editar' : 'Nueva'} {title}</h5>
      <form onSubmit={handleSubmit} className="row g-3">
        
        {/* Selector de Adulto */}
        <div className="col-12">
          <label className="form-label fw-bold text-secondary">Adulto (Adulto Mayor)</label>
          <select 
            className="form-select" 
            value={form.idAdulto || ''} 
            onChange={e => setForm({...form, idAdulto: e.target.value})} 
            required
          >
            <option value="">-- Seleccione --</option>
            {listAdultos.map(a => (
              <option key={a.id} value={a.id}>{a.nombres} {a.apellidos}</option>
            ))}
          </select>
        </div>

        {/* Campos Dinámicos basados en config */}
        {fieldsConfig.map((field) => (
          <div className="col-md-6" key={field.name}>
            <label className="form-label small text-muted">{field.label}</label>
            <input 
              type={field.type || 'text'}
              className="form-control" 
              value={form[field.name] || ''} 
              onChange={e => setForm({...form, [field.name]: e.target.value})} 
              required={!field.optional}
            />
          </div>
        ))}

        <div className="col-12 d-flex gap-2 mt-4">
          <button type="submit" className={`btn flex-grow-1 ${isEditing ? 'btn-warning' : 'btn-success'}`}>
            {isEditing ? 'Actualizar Ficha' : 'Crear Ficha'}
          </button>
          {isEditing && <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancelar</button>}
        </div>
      </form>
    </div>
  );
};

export default FormularioFicha;