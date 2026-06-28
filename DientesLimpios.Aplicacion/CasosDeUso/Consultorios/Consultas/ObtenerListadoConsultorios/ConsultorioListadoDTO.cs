using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    //cada caso de uso tiene su propio DTO
    //parte del trate off
    public class ConsultorioListadoDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
    }
}
