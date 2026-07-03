using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
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

            //Registrar todos los IRequestHandler<> con y sin TResponse con sus respectivos Casos de Uso
            services.Scan(scan => scan.FromAssembliesOf(typeof(IMediator))
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>))) //los que no retornan valor
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))//los que si retornan valor
            .AsImplementedInterfaces()
            .WithScopedLifetime());



        
            //services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>, CasoDeUsoCrearConsultorio>();
            //services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio,ConsultorioDetalleDTO>, 
            //                        CasoDeUsoObtenerDetalleConsultorio>();
            //services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>, 
            //                                CasoDeUsoObtenerListadoConsultorios>();
            //services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>();
            //services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>();




            return services;
        }
    }
}
