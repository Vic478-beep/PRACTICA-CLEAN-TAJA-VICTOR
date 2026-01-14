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
    public class FichaEnfermeriaRepositorio:IFichaEnfermeria
    {
        private readonly AppDbContext _appDbContext;
        public FichaEnfermeriaRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(FichaEnfermeria ficha)
        {
            _appDbContext.fichaEnf.Update(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FichaEnfermeria>> All()
        {
            return await _appDbContext.fichaEnf.ToListAsync();
        }

        public async Task Crear(FichaEnfermeria ficha)
        {
            _appDbContext.fichaEnf.Add(ficha);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<FichaEnfermeria> ObtenerId(Guid CodEnf)
        {
            return await _appDbContext.fichaEnf.FindAsync(CodEnf);
        }
        public async Task Eliminar(Guid CodEnf)
        {
            var ficha = await _appDbContext.fichaEnf.FindAsync(CodEnf);
            if (ficha != null)
            {
                _appDbContext.fichaEnf.Remove(ficha);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
