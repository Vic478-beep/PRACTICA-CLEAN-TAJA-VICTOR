import React from 'react';

const Buscador = ({ value, onChange, placeholder = "Buscar..." }) => {
  return (
    <div className="mb-3">
      <input
        type="text"
        className="form-control p-2 border rounded w-full md:w-1/3"
        placeholder={placeholder}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        style={{ maxWidth: '300px' }}
      />
    </div>
  );
};

export default Buscador;