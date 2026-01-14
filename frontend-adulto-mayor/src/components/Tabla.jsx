import React from 'react';

const Tabla = ({ cols, columns, data, onEdit, onDelete, idField = 'id' }) => {
  const headers = cols || columns || [];

  return (
    <div className="card-custom">
      <div className="table-responsive">
        <table className="table table-custom table-hover align-middle">
          <thead>
            <tr>
              {headers.map((c, i) => <th key={i}>{c.header}</th>)}
              <th className="text-center" style={{width: '120px'}}>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {data && data.length > 0 ? (
              data.map((item) => (
                <tr key={item[idField]}>
                  {headers.map((c, i) => <td key={i}>{item[c.field]}</td>)}
                  <td className="text-center">
                    <button 
                      className="btn btn-sm btn-outline-orange me-2" 
                      onClick={() => onEdit(item)}
                      title="Editar"
                    >
                      <i className="bi bi-pencil"></i> ✏️
                    </button>
                    <button 
                      className="btn btn-sm btn-outline-danger" 
                      onClick={() => onDelete(item[idField])}
                      title="Eliminar"
                    >
                      <i className="bi bi-trash"></i> 🗑️
                    </button>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={headers.length + 1} className="text-center p-5 text-muted">
                  <div className="fs-1 mb-2">📂</div>
                  <p>No hay registros disponibles</p>
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Tabla;