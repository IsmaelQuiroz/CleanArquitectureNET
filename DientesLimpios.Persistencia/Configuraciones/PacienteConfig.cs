using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Configuraciones
{
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(p => p.Nombre)
                .HasMaxLength(250)
                .IsRequired();


            //hay un objeto de valor, es la propiedad Email de la entidad Paciente
            //complexProperty para mapeos especiales para tipos de datos especiales como los objetos de valor
            //es decir mapeo del objeto de valor hacia una columna con las características deseadas
            builder.ComplexProperty(prop => prop.Email, accion =>
            {
                accion.Property(e => e.Valor).HasColumnName("Email").HasMaxLength(254); //mapeo hacia una columna que se llama Email y tenga una longitud de 254
            });
        }
    }
}
