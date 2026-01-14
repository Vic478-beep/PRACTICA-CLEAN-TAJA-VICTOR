using System;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class EliminarAdulto
    {
        private readonly IAdulto _adultoRepo;

        public EliminarAdulto(IAdulto adultoRepo)
        {
            _adultoRepo = adultoRepo;
        }

        // Cambiamos Task por Task<string> para poder retornar texto
        public async Task<string> EjecutarAsync(Guid id)
        {
            await _adultoRepo.Eliminar(id);
            return "Adulto eliminado correctamente";
        }
    }
}