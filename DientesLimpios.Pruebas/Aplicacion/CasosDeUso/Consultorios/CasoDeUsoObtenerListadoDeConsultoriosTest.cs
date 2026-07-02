using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerListadoDeConsultoriosTest
    {
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerListadoConsultorios casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerListadoConsultorios(repositorio);
        }

        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultorioListadoDTO()
        {
            //este será el Listado Mok resultado cuando se ejecute en el Handle del caso de uso la funcion del repositorio.ObtenerTodos()
            var consultorios = new List<Consultorio>
            {
                new Consultorio("Consultorio A"),
                new Consultorio("Consultorio B")
            };
            repositorio.ObtenerTodos().Returns(consultorios);

            //arma el listado de ConsultorioListadoDTO
            var esperado = consultorios.Select(c => new ConsultorioListadoDTO { Id = c.Id, Nombre = c.Nombre }).ToList();

            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());
            Assert.AreEqual(esperado.Count, resultado.Count);

            for(int i =0; i < esperado.Count; i++)
            {
                Assert.AreEqual(esperado[i].Id, resultado[i].Id);
                Assert.AreEqual(esperado[i].Nombre, resultado[i].Nombre);
            }
        }

        
        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia()
        {
            //lo que se va desolver cuando dentro del  Handle se ejecute repositorio.ObtenerTodos();
            repositorio.ObtenerTodos().Returns(new List<Consultorio>()); //aqui se le indica que retorne un listado vacio de consultorios

            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
