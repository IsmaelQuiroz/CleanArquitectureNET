using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoBorrarConsultorioTest
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IUnidadDeTrabajo unidadDeTrabajo;
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoBorrarConsultorio casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoBorrarConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorraConsultorioYPersiste()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUso.Handle(comando);

            await repositorio.Received(1).Borrar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();

        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var comando = new ComandoBorrarConsultorio { Id = Guid.NewGuid() };
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            await casoDeUso.Handle(comando);
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionAlBorrar_LlamaAReversarYLanzaExcepcion()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            repositorio.ObtenerPorId(id).Returns(consultorio);

            repositorio.Borrar(consultorio).Throws(new InvalidOperationException("Error al Borrar"));

            //Aquio vamos a ver que se lanza una InvalidOperationException si invocamos el caso de uso
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));
            await unidadDeTrabajo.Received(1).Reversar();


            //crea un consultorio para intentar borrarlo despues
            //var consultorio = new Consultorio("Consultorio A");
            //var id = consultorio.Id;
            //var comando = new ComandoBorrarConsultorio{ Id = id }; //el id del consultorio creado de prueba

            //repositorio.ObtenerPorId(id).Returns(consultorio);//cuando se busque se retornará el creado 
            //repositorio.Borrar(consultorio).Throws(new InvalidOperationException("Error al Borrar"));

            //await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => casoDeUso.Handle(comando));
            //await unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
