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
    public class FichaOrientacionRepositorio: IFichaOrientacion
    {
        private readonly AppDbContext _appDbContext;
        public FichaOrientacionRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(FichaOrientacion ficha)
        {
            _appDbContext.fichaOri.Update(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FichaOrientacion>> All()
        {
            return await _appDbContext.fichaOri.ToListAsync();
        }

        public async Task Crear(FichaOrientacion ficha)
        {
            _appDbContext.fichaOri.Add(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<FichaOrientacion> ObtenerId(Guid id)
        {
            return await _appDbContext.fichaOri.FindAsync(id);
        }
        public async Task Eliminar(Guid id)
        {
            var fichaExistente = await _appDbContext.fichaOri.FindAsync(id);
            if (fichaExistente != null)
            {
                _appDbContext.fichaOri.Remove(fichaExistente);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
