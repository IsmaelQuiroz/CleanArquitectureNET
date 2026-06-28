using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{

    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            new Email(null!);
            //Assert.ThrowsExactly<ExcepcionDeReglaDeNegocio>()
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeReglaDeNegocio))]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            new Email("ismael.co");
        }

        [TestMethod]
        public void Constructor_EmailVaido_NoLanzaExcepcion()
        {
            new Email("ismael@.co");
        }

    }


    //[TestMethod]
    //public void MetodoDePrueba()
    //{
    //    Assert.ThrowsExactly<MiExcepcion>(() => MiFuncion());
    //}


    //      [ExpectedException(typeof(MiExcepcion))]
    //      [TestMethod]
    //      public void MetodoDePrueba()
    //      {
    //           MiFuncion();
    //      }
}
