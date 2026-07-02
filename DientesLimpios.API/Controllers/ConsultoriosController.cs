using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/consultorios")]
    public class ConsultoriosController: ControllerBase
    {
        private readonly IMediator mediator;
        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConsultorioListadoDTO>>> Get()
        {
            //objeto consulta que contiene los parametros y filtros para la operacion
            //tambien se define lo que va retornar con el <List<ConsultorioListadoDTO>> del IRequest
            var consulta = new ConsultaObtenerListadoConsultorios(); // del tipo interface marcador IRequest<List<ConsultorioListadoDTO>>

            var resultado = await mediator.Send(consulta);//
            return resultado;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleConsultorio { Id  = id };
            var resultado = await mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            var comando = new ComandoCrearConsultorio { Nombre = crearConsultorioDTO.Nombre }; 
            await mediator.Send(comando);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarConsultorioDto actualizarConsultorioDto)
        {
            var comando = new ComandoActualizarConsultorio {Id = id,  Nombre = actualizarConsultorioDto.Nombre };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
