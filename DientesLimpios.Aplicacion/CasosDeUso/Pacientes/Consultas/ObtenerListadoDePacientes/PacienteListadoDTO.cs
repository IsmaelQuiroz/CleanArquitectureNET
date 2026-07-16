using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoDePacientes
{
    public class PacienteListadoDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
            
    }
}
