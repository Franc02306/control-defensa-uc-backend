using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SISTEMA_DEFENSA_API.EL.DbContexts
{
    public class DefensaDbContext : DbContext
    {
        public DefensaDbContext(DbContextOptions<DefensaDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Provincia> Provincias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
