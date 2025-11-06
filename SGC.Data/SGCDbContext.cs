using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SGC.Data.Entities;

namespace SGC.Data
{
    public class SGCDbContext : DbContext
    {
        public SGCDbContext(DbContextOptions<SGCDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índice único para evitar duplicados
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Datos iniciales (Seed Data)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Username = "admin",
                    Password = "1234",
                    Rol = "Gestor"
                },
                new Usuario
                {
                    Id = 2,
                    Username = "maria",
                    Password = "abcd",
                    Rol = "Analista"
                },
                new Usuario
                {
                    Id = 3,
                    Username = "jose",
                    Password = "clave",
                    Rol = "Supervisor"
                }
            );
        }
    }
}


