using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    //interface para el Caso de uso para indicar que existe una conexion entre el caso de uso y el comando
    //Manejador de Request ó peticiones
    //<TRequest es la peticion por ejemplo un comando
    //TResponse> es el tipo de dato de salida que vamos a devolver del caso de uso
    //  en el caso de crearConsultorios seria un Guid
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse> //donde el TRequest va a ser del tipo IRequest<TResponse>
    {
        //Este Handle se corresponde con el Handle del caso de uso
        //se recibe el TRequest y el request define el tipo de dato de salida TResponse.. esto 
        //viene de la firma del metodo del caso de uso
        Task<TResponse> Handle(TRequest request);
    }
}
