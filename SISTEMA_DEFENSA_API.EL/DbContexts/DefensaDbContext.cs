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

        public DbSet<Student> Estudiantes { get; set; }
        public DbSet<Address> Direcciones { get; set; }
        public DbSet<Professor> Profesores { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Municipality> Municipios { get; set; }
        public DbSet<Province> Provincias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
