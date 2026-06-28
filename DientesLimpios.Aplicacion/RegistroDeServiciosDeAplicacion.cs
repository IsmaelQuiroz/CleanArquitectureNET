using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion
{
    /// <Registro_de_Servicios_Capa_Aplicacion>
    /// para despues en la capa de presentación del WebAPI, se puedean registrar 
    //  dichos servicios en una sola línea de código
    /// </summary>

    public static class RegistroDeServiciosDeAplicacion
    {
        //Al ejecutar esta funcion en el WebAPI se van a registrar todos los servicios
        //en el sistema de inyección de dependencias
        public static IServiceCollection AgregarServiciosDeAplicacion(
            this IServiceCollection services)
        {
            // <Servicio , la clase que se va servir>
            services.AddTransient<IMediator, MediadorSimple>();
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>, CasoDeUsoCrearConsultorio>();
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio,ConsultorioDetalleDTO>, 
                                    CasoDeUsoObtenerDetalleConsultorio>();

            return services;
        }
    }
}
