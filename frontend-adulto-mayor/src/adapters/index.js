/**
 * PATRÓN ADAPTER (Estructural)
 * Convierte los datos crudos del backend en objetos seguros para la UI.
 */

// ADAPTADOR 1: Para la lista de Adultos
export const adultoAdapter = (adulto) => {
  return {
    // Mantenemos los datos originales
    id: adulto.id,
    edad: adulto.edad,
    
    // Transformamos nulos en strings vacíos para evitar errores en React
    nombres: adulto.nombres || '',
    apellidos: adulto.apellidos || '',
    telefono: adulto.telefono || '',
    domicilio: adulto.domicilio || '',
    fechaNac: adulto.fechaNac || '',
    estadoCivil: adulto.estadoCivil || '',

    // Propiedad calculada para facilitar búsquedas
    nombreCompleto: `${adulto.nombres || ''} ${adulto.apellidos || ''}`.trim()
  };
};

// ADAPTADOR 2: Genérico para todas las Fichas (Enfermería, Fisioterapia, etc.)
export const fichaAdapter = (ficha, listaAdultos) => {
  // Buscamos el adulto en la lista que ya tenemos en memoria
  const adultoEncontrado = ficha.adulto || listaAdultos.find(a => a.id === ficha.idAdulto);
  
  return {
    ...ficha, // Copiamos todas las propiedades originales (temp, fecha, etc.)

    // Agregamos la propiedad 'nombreCompleto' resuelta
    nombreCompleto: adultoEncontrado 
      ? `${adultoEncontrado.nombres} ${adultoEncontrado.apellidos}` 
      : 'Paciente No Encontrado'
  };
};