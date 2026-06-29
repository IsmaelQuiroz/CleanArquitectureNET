using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    //cuando se le envíe la Mediador el "ConsultaObtenerListadoConsultorios" va a retornar un listado de ConsultorioListadoDTO
    public class ConsultaObtenerListadoConsultorios : IRequest<List<ConsultorioListadoDTO>>
    {

    }
}
