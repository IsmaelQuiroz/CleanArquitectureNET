using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoDePacientes
{
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoDePacientes, PaginadoDTO<PacienteListadoDTO>>
    {

        private IRepositorioPacientes repositorio;

        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }


        public async Task<PaginadoDTO<PacienteListadoDTO>> Handle(ConsultaObtenerListadoDePacientes request)
        {
            var pacientes = await repositorio.ObtenerFiltrado(request);
            var totalPacientes = await repositorio.ObtenerCantidadTotalRegistros();
            var pacientesDTO = pacientes.Select(pacientes => pacientes.ADTo()).ToList();

            var paginadoDTO = new PaginadoDTO<PacienteListadoDTO>
            {
                Elementos = pacientesDTO,
                Total = totalPacientes
            };

            return paginadoDTO;
        }
    }
}
