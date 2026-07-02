using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoActualizarConsultorioTest
    {
        private IUnidadDeTrabajo unidadDeTrabajo;
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoActualizarConsultorio casoDeUso;

        //public CasoDeUsoActualizarConsultorioTest(IUnidadDeTrabajo unidadDeTrabajo, IRepositorioConsultorios repositorioConsultorios)
        //{
        //    this.unidadDeTrabajo = unidadDeTrabajo;
        //    this.repositorioConsultorios = repositorioConsultorios;
        //}

        [TestInitialize]
        public void Setup()
        {
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
        }


        [TestMethod]
        public async Task Handle_CuandoCosultorioExiste_ActualizaNombreYPersiste()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;

            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Nuevo Nombre" };

            //Configuración del Mock
            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUso.Handle(comando);

            //verificar que del repositorio hallamos llammado el metodo Actualizar pasandole el consultorio
            await repositorio.Received(1).Actualizar(consultorio);

            // y que se halla invocado el metodo Persistir de unidad de trabajo
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var comando = new ComandoActualizarConsultorio { Id = Guid.NewGuid(), Nombre = "Nombre" };
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            await casoDeUso.Handle(comando);
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionAlActualizar_LlamaAReversarYLanzaExcepcion()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = "Consultorio B" };

            repositorio.ObtenerPorId(id).Returns(consultorio);
            repositorio.Actualizar(consultorio).Throws(new InvalidOperationException("Error al Actualizar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));
            await unidadDeTrabajo.Received(1).Reversar();
        }



    }
}
