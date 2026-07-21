using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class CasoDeUsoActualizarPaciente : IRequestHandler<ComandoActualizarPaciente>
    {
        private readonly IUnidadDeTrabajo unidadDeTrabajo;
        private readonly IRepositorioPacientes repositorio;

        public CasoDeUsoActualizarPaciente(IUnidadDeTrabajo unidadDeTrabajo, IRepositorioPacientes repositorio)
        {
            this.unidadDeTrabajo = unidadDeTrabajo;
            this.repositorio= repositorio;
        }

        public async Task Handle(ComandoActualizarPaciente request)
        {
            var paciente = await repositorio.ObtenerPorId(request.Id);
            if (paciente is null) 
            {
                throw new ExcepcionNoEncontrado();
            }

            paciente.ActualizarNombre(request.Nombre);
            var email = new Email(request.Email);
            paciente.ActualizarEmail(email);

            try
            {
                await repositorio.Actualizar(paciente);
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception )
            {
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
