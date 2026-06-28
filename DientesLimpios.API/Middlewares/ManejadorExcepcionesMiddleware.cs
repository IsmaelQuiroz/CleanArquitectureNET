using DientesLimpios.Aplicacion.Excepciones;
using System.Net;
using System.Text.Json;

namespace DientesLimpios.API.Middlewares
{
    public class ManejadorExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;

        public ManejadorExcepcionesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //significa invocar al siguiente Middleware, es decir cuado no hubo ningun error
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcion(context, ex);
            }
        }

        //logica de manejo de errores
        private Task ManejarExcepcion(HttpContext context, Exception exception)
        {
            //Respuesta por defecto que se le enviará al cliente, es un error 500
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var resultado = string.Empty;

            switch (exception)
            {
                case ExcepcionNoEncontrado:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ExcepcionDeValidacion excepcionDeValidacion:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    resultado = JsonSerializer.Serialize(excepcionDeValidacion.ErroresValidacion);
                    break;

            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(resultado); //para escribir la respuesta hacia el cliente
        }

    }

    //Se crea una clase estatica para faciliar el uso de este middleware
    //solo es un metodo auxiliar
    public static class ManejadorExcepcionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseManejadorExcepciones(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManejadorExcepcionesMiddleware>();
        }
    }
}
