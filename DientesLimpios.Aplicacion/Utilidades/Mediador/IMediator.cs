using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    //esta interface va representar al Mediador
    //viene siendo la que le vamos a inyectar al controlador
    public interface IMediator //En inglés para que sea compatible con la libreria MediaTR
    {
        //un metodo Send que va recibir un IRequest "que representa un comando o una consulta"
        //vamos a devolver un TResponse que puede ser un Guid, un consultorio, puede ser lo que sea 
        //gracias al <TResponse> que tenemos en el argumento vamos a poder
        //determinar el tipo de dato de salida Task<TResponse> que vamos a sacar del Mediador cuando mandemos el comando o la consulta
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
