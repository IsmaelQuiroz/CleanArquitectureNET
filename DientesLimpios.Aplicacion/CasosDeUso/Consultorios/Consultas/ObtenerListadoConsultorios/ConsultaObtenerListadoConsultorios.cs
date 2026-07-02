using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    //cuando se le envíe la Mediador el "ConsultaObtenerListadoConsultorios" va a retornar un listado de ConsultorioListadoDTO

    //ConsultaObtenerListadoConsultorios:  Es la clase que contiene los parámetros o filtros necesarios para ejecutar la consulta.
    //IRequest<> : Es una interfaz de marcador que le indica a MediatR que esta clase es un mensaje que debe ser procesado por un manejador (Handler).
    //<List<ConsultorioListadoDTO>>:  Es el tipo de dato que se espera como respuesta. En este caso, el sistema devolverá una lista tipada (List) usando un DTO (Data Transfer Object) llamado ConsultorioListadoDTO, lo que asegura que solo se transfiera la información necesaria para mostrar en la interfaz de usuario
    public class ConsultaObtenerListadoConsultorios : IRequest<List<ConsultorioListadoDTO>>
    {

    }
}
