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
    public class FichaProteccionRepositorio: IFichaProteccion
    {
        private readonly AppDbContext _appDbContext;
        public FichaProteccionRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(FichaProteccion ficha)
        {
            _appDbContext.fichaPro.Update(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FichaProteccion>> All()
        {
            return await _appDbContext.fichaPro.ToListAsync();
        }

        public async Task Crear(FichaProteccion ficha)
        {
            _appDbContext.fichaPro.Add(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<FichaProteccion> ObtenerId(Guid id)
        {
            return await _appDbContext.fichaPro.FindAsync(id);
        }
        public async Task Eliminar(Guid id)
        {
            var fichaExistente = await _appDbContext.fichaPro.FindAsync(id);
            if (fichaExistente != null)
            {
                _appDbContext.fichaPro.Remove(fichaExistente);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
