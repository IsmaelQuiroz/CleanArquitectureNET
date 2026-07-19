using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoDePacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    [TestClass]
    public class CasoDeUsoObtenerListadoPacientesTest
    {
        private IRepositorioPacientes repositorio;
        private CasoDeUsoObtenerListadoPacientes casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            casoDeUso = new CasoDeUsoObtenerListadoPacientes(repositorio);
        }

        [TestMethod]
        public async Task Handle_RetornaPacientesPaginadosCorrectamente()
        {
            var pagina = 1;
            var registrosPorPagina = 2;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina=pagina, RegistrosPorPagina=registrosPorPagina};
            var paciente1 = new Paciente("Felipe", new Email("felipe@ejemplo.com"));
            var paciente2 = new Paciente("claudia", new Email("claudia@ejemplo.com"));

            //They are test pacientes that will return Obtener filtrado repository method
            IEnumerable<Paciente> pacientes = new List<Paciente> { paciente1, paciente2 };

        }


    }
}
