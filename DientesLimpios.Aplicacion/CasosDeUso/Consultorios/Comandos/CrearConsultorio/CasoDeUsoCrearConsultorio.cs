using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    //CQRS
    //Esta es la clase que realiza la acción
    public class CasoDeUsoCrearConsultorio : IRequestHandler<ComandoCrearConsultorio, Guid>
    {
        private readonly IUnidadDeTrabajo unidadDeTrabajo;
        //private readonly IValidator<ComandoCrearConsultorio> validador;
        private readonly IRepositorioConsultorios repositorio;

        public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo
            //,IValidator<ComandoCrearConsultorio> validador
            )
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
            //this.validador = validador;
        }
       

        public async Task<Guid> Handle(ComandoCrearConsultorio comando)
        {
            //orquestamos las accionesa realizar
            //var resultadoValidacion = await validador.ValidateAsync(comando);
            //if (!resultadoValidacion.IsValid)
            //{
            //    //Si hubiese errores de validacion, todos serian devueltos al cliente mediante este mecanismo
            //    throw new ExcepcionDeValidacion(resultadoValidacion);
            //}

            var consultorio = new Consultorio(comando.Nombre);
            try
            {
                var respuesta = await repositorio.Agregar(consultorio);
                await unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar();
                throw; //para relanzar la excepcion y que una capa superior pueda atraparla, loggearla, procesarla..etc
            }
            
        }
    }
}
