using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace Proyecto_test.Models
{
    class GestionPersonas
    {
        static string database = "dbSqlite.db";

        static async Task Main(String[] args)
        {
            using (var db = new DatabaseDbContext())
            {
                await db.Database.EnsureCreatedAsync();

                var Persona1 = new Persona()
                {
                    RutPersona = "19326168-K",
                    PrimerNombre = "Esteban",
                    PrimerApellido = "Pizarro",
                    SegundoApellido = "Arancibia",
                    DireccionPersona = "Ricardo de Ferrari 105",
                    TelefonoPersona = "56956255712",
                    CorreoPersona = "esteban.pizarro.arancibia@gmail.com"
                };
                var Persona2 = new Persona()
                {
                    RutPersona = "19973751-1",
                    PrimerNombre = "Fernanda",
                    PrimerApellido = "Rivera",
                    SegundoApellido = "Acevedo",
                    DireccionPersona = "Ricardo de Ferrari 105",
                    TelefonoPersona = "56956255712",
                    CorreoPersona = "fer.rivera.acevedo@gmail.com"
                };
                db.Personas.Add(Persona1);
                db.Personas.Add(Persona2);

                await db.SaveChangesAsync();
            }
        }

        public class Persona //Base de datos 
        {
            public int ID { get; set; }
            public string RutPersona { get; set; }
            public string PrimerNombre { get; set; }
            public string PrimerApellido { get; set; }
            public string SegundoApellido { get; set; }
            public string? DireccionPersona { get; set; }
            public string? TelefonoPersona { get; set; }
            public string? CorreoPersona { get; set; }
        }

        public class DatabaseDbContext : DbContext //Contexto de la base de datos
        {
            public DbSet<Persona> Personas { get; set; } //representación de la colección de la base de datos

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //sobre carga de configuración, para sobreescribir 
            {
                optionsBuilder.UseSqlite(connectionString: "Filename=" + database, sqliteOptionsAction: op =>
                {
                    op.MigrationsAssembly
                        (
                            Assembly.GetExecutingAssembly().FullName
                        );
                });

                base.OnConfiguring(optionsBuilder);
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder) //Función para modelar la base de datos
            {
                modelBuilder.Entity<Persona>().ToTable("Personas");
                modelBuilder.Entity<Persona>(entity => //identificar como quiero que se llame / expresiones lambda
                {
                    entity.HasKey(e => e.ID);
                });

                base.OnModelCreating(modelBuilder);
            }

        }
    }
}
