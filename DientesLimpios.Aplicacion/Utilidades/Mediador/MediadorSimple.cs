using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {

        private readonly IServiceProvider serviceProvider;
        //Para usar Servicios en el contexto de .NET
        public MediadorSimple(IServiceProvider serviceProvider) {
            //para poder tomar un servicio del contenedor de inversion de controles, del sistema Inyeccion de dependencias
            this.serviceProvider = serviceProvider;
        }

        //el primreo <TReponse>: Define el tipo que devuelve la operacion (dentro del Task)
        //el <TResponse> despues del Send: Declara que el método Send es un método genérico
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            await RealizarValidaciones(request);

            //Identifica e ltipo del caso de uso es  a partir de su :Interface en este caso es
            // ejemplo si la consulta es: ConsultaObtenerListadoConsultorios
            // entonces el request.GetType es IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>
            //Para poder invocar el caso de uso correspondiente a partir del Request se utilizará Reflection
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse) );

            // de la especificacion de la inyección de dependencias identifica  la clase concreta del caso de uso
            //con el service provider se obtiene una instancia del caso de uso
            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);

            //si esto es nulo quiere decir que no hemos registrado el caso de uso en el sistema de inyección de dependencias
            if(casoDeUso is null)
            {
                //No se encontro un caso de uso para el comando o consulta
                throw new ExcepcionDeMediador($"No se encontró un handler para {request.GetType().Name}");
            }

            //se invoca el metodo handler del caso de uso
            //Se hace utilizando Reflection
            var metodo = tipoCasoDeUso.GetMethod("Handle");
            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;
        }

        public async Task Send(IRequest request)
        {
            await RealizarValidaciones(request);
            var tipoCasoDeUso = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);

            if(casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un handler para {request.GetType().Name}");
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle")!;
            await (Task)metodo.Invoke(casoDeUso, new object[] { request })!;

        }

        private async Task RealizarValidaciones(object request)
        {
            //Para aplicar las validaciones en el mismo Mediador en lugar del Caso de Uso
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());
            //Instanciar el validador a partor del service provider
            var validador = serviceProvider.GetService(tipoValidador);
            if (validador is not null)
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(validador,
                    new object[] { request, CancellationToken.None })!;

                await tareaValidar.ConfigureAwait(false); //no hace mucha diferencia en webbapp, en desktop si mas rapido

                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;

                if (!validationResult.IsValid)
                {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }
        }
    }
}
