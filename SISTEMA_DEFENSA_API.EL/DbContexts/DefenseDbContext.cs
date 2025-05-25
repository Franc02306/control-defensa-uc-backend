using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SISTEMA_DEFENSA_API.EL.DbContexts
{
    public class DefenseDbContext : DbContext
    {
        public DefenseDbContext(DbContextOptions<DefenseDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<StudentsGetResponse> StudentSearchResults { get; set; }
        public DbSet<ProfessorGetResponse> ProfessorSearchResults { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
