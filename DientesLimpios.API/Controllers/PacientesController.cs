using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoDePacientes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PacientesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO crearPacienteDTO)
        {
            var comando = new ComandoCrearPaciente { Nombre = crearPacienteDTO.Nombre, Email = crearPacienteDTO.Email };
            await mediator.Send(comando);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get()
        {
            var consulta = new ConsultaObtenerListadoDePacientes();
            var resultado = await mediator.Send(consulta);
            return resultado;
        }
    }
}
