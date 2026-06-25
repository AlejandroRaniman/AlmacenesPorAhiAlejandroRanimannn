using AlmacenesPorAhi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlmacenesPorAhi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Tablas mapeadas en la base de datos
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Cliente> Clientes => Set<Cliente>(); // CORRECCIÓN: Faltaba registrar la tabla de clientes

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Semilla de Productos (Importación inicial)
        modelBuilder.Entity<Producto>().HasData(
            new Producto
            {
                Id = 1,
                Nombre = "Polera basica algodon",
                Descripcion = "Polera de algodon peinado, corte regular.",
                Categoria = "Poleras",
                Talla = "M",
                Color = "Blanco",
                Precio = 7990m,
                Stock = 25,
                Estado = "Activo",
                FechaRegistro = new DateTime(2024, 1, 15)
            },
            new Producto
            {
                Id = 2,
                Nombre = "Jeans recto clasico",
                Descripcion = "Jeans de mezclilla rigida, tiro medio.",
                Categoria = "Pantalones",
                Talla = "L",
                Color = "Azul",
                Precio = 19990m,
                Stock = 12,
                Estado = "Activo",
                FechaRegistro = new DateTime(2024, 2, 3)
            },
            new Producto
            {
                Id = 3,
                Nombre = "Chaqueta de cuero",
                Descripcion = "Chaqueta biker de cuero sintetico.",
                Categoria = "Abrigos",
                Talla = "L",
                Color = "Negro",
                Precio = 49990m,
                Stock = 5,
                Estado = "Activo",
                FechaRegistro = new DateTime(2024, 3, 20)
            }
        );

        // IMPORTACIÓN MASIVA DE CLIENTES (Data Seeding para cumplir la rúbrica)
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente
            {
                Id = 1,
                Rut = "11.111.111-1",
                Nombre = "Juan",
                ApellidoPaterno = "Pérez",
                ApellidoMaterno = "González",
                Email = "juan.perez@example.com",
                Telefono = "+56912345678",
                Direccion = "Calle Principal 123, Santiago",
                Estado = "Activo",
                FechaRegistro = new DateTime(2026, 4, 15)
            },
            new Cliente
            {
                Id = 2,
                Rut = "22.222.222-2",
                Nombre = "María José",
                ApellidoPaterno = "Contreras",
                ApellidoMaterno = "Silva",
                Email = "maria.contreras@example.com",
                Telefono = "+56987654321",
                Direccion = "Avenida Alemania 0450, Temuco",
                Estado = "Activo",
                FechaRegistro = new DateTime(2026, 5, 20)
            },
            new Cliente
            {
                Id = 3,
                Rut = "33.333.333-3",
                Nombre = "Carlos",
                ApellidoPaterno = "Fuentes",
                ApellidoMaterno = "Muñoz",
                Email = "carlos.fuentes@example.com",
                Telefono = "+56955554444",
                Direccion = "Balmaceda 789, Nueva Imperial",
                Estado = "Inactivo",
                FechaRegistro = new DateTime(2026, 6, 01)
            }
        );
    }
}