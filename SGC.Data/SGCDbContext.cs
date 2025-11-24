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
        public DbSet<Cliente> Clientes { get; set; }
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

            // Seed Data - Clientes
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Identificacion = "101110111",
                    Nombre = "Carlos",
                    Apellido = "Mora",
                    Email = "carlos.mora@email.com",
                    Telefono = "88887777",
                    Direccion = "San José, Costa Rica",
                    FechaRegistro = new DateTime(2024, 11, 1),
                    Activo = true
                },
                new Cliente
                {
                    Id = 2,
                    Identificacion = "202220222",
                    Nombre = "Ana",
                    Apellido = "González",
                    Email = "ana.gonzalez@email.com",
                    Telefono = "77776666",
                    Direccion = "Heredia, Costa Rica",
                    FechaRegistro = new DateTime(2024, 11, 5),
                    Activo = true
                }
                );
        }
    }
}


