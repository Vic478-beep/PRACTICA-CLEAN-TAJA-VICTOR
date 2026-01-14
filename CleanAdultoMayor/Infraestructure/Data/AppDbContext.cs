using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infraestructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Adulto> adulto { get; set; }
        public DbSet<FichaEnfermeria> fichaEnf { get; set; }
        public DbSet<FichaOrientacion> fichaOri { get; set; }
        public DbSet<FichaProteccion> fichaPro { get; set; }
        public DbSet<FichaFisioterapia> fichaFis { get; set; }
    }
}
