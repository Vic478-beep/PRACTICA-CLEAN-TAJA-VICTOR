using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositorios
{
    public class FichaFisioterapiaRepositorio : IFichaFisioterapia
    {
        private readonly AppDbContext _appDbContext;
        public FichaFisioterapiaRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Actualizar(FichaFisioterapia ficha)
        {
            _appDbContext.fichaFis.Update(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FichaFisioterapia>> All()
        {
            return await _appDbContext.fichaFis.ToListAsync();
        }

        public async Task Crear(FichaFisioterapia ficha)
        {
            _appDbContext.fichaFis.Add(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Eliminar(Guid id)
        {
            var ficha = await _appDbContext.fichaFis.FindAsync(id);
            if (ficha != null)
            {
                _appDbContext.fichaFis.Remove(ficha);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<FichaFisioterapia> ObtenerId(Guid id)
        {
            return await _appDbContext.fichaFis.FindAsync(id);
        }
    }
}
