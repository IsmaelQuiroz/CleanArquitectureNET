using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerDetalleConsultorioTest
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerDetalleConsultorio casoDeUso;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerDetalleConsultorio(repositorio);
        }

        //consultorio existe then retorna un DTO
        [TestMethod]
        public async Task Handle_ConsultorioExiste_RetornaDTO()
        {
            //Preparacion
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            //crea una instancia del objeto IRequest<> con los datos para la consulta, 
            var consulta = new ConsultaObtenerDetalleConsultorio { Id= id }; //seteando el id del consultorio creado para busqueda
            //esta parte simula que pasará cuando se llame a ObtenerPorId con el parametro id,
            repositorio.ObtenerPorId(id).Returns(consultorio);//indicandole a Nsubstitute que va a retornar al ejecutar el metodo

            //prueba
            var resultado = await casoDeUso.Handle(consulta);

            //verificacion
            Assert.IsNotNull(resultado);
            Assert.AreEqual(id, resultado.Id);
            Assert.AreEqual("Consultorio A", resultado.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_consultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            //Preparacion
            var id = Guid.NewGuid();
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).ReturnsNull();

            //prueba
            await casoDeUso.Handle(consulta);
        }
    }
}
