using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public class DientesLimpiosDbContext : DbContext
    {
        //<especificarDbContext> porque vamos a tener varios DbContext en el proyecto
        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options ) : base(options)
        {
        }

        protected DientesLimpiosDbContext()
        {
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Para aplicar todas las configuraciones que coloquemos en este proyecto de Persistencia
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDbContext).Assembly);
        }

        //se especifican los DbSet para crear las tablas a partir de nuestra entidades
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
    }
}
