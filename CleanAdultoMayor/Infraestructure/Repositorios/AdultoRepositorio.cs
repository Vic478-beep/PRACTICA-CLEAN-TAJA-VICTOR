using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositorios
{
    public class AdultoRepositorio:IAdulto
    {
        private readonly AppDbContext _appDbContext;
        public AdultoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Adulto adulto)
        {
            // Update marca la entidad como modificada
            _appDbContext.adulto.Update(adulto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Adulto>> All()
        {
            return await _appDbContext.adulto.ToListAsync();
        }

        public async Task Crear(Adulto adulto)
        {
            _appDbContext.adulto.Add(adulto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Eliminar(Guid id)
        {
            var adulto = await _appDbContext.adulto.FindAsync(id);
            if (adulto != null)
            {
                _appDbContext.adulto.Remove(adulto);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Adulto> ObtenerId(Guid id)
        {
            // Busca por ID (retorna null si no encuentra)
            return await _appDbContext.adulto.FindAsync(id);
        }
    }
}
