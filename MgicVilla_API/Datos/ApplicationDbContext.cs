using MgicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MgicVilla_API.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        //Declaramos la base de datos que sera creada con DbSet<T> 
        //T = modelo de base de datos creado como clase 
        //Se lo nombra generalmente en plural y se le asignan metodos get y set
        public DbSet<Villa> villas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Nombre = "Villa Real",
                    Detalle = "Detalle de villa",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 1000,
                    Tarifa = 200,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Nombre = "Villa Real 2",
                    Detalle = "Detalle de villa 2",
                    ImagenUrl = "",
                    Ocupantes = 4,
                    MetrosCuadrados = 2000,
                    Tarifa = 400,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
                );
        }
    }
}
