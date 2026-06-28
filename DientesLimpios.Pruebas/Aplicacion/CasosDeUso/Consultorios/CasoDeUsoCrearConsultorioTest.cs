using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoCrearConsultorioTest
    {
        //traemos los campos de trabajo que utiliza el caso de uso
        private  IUnidadDeTrabajo unidadDeTrabajo;
       // private  IValidator<ComandoCrearConsultorio> validador;
        private  IRepositorioConsultorios repositorio;

        private CasoDeUsoCrearConsultorio casoDeUso;

        //Esta clase se va ejecutar en cada prueba automática para 
        //poder reutilizar las instancias de los tipos de arriba
        //como son interfaces no se pueden solo instanciar
        //pero para ello tenemos a Nsofttitute ( crea instancias de clase que implementan la interface)
        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            //validador = Substitute.For<IValidator<ComandoCrearConsultorio>>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();

            //Ya tenemos el caso de uso con sus dependencias sin inconvenientes
            casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_comandoValido_ObtenemosIdConsultorio()
        {
            //valores a la mano con objeto comando
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };

            //prueba de validacion del comando, Reurns viene de NSubstitute
            //cuando se ejecute el validateAsync del comando, debe retornar un validationResult que no tenga ningun error
            //validador.ValidateAsync(comando).Returns(new ValidationResult());

            //sigue validar Agregar y persistir del caso de uso
            //cuando agreguemos un consultorio simplemente retornaremos ese consultorio para atras
            var consultorioCreado = new Consultorio("Consultorio A");
            repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            //ahora estamos listos para realizar la prueba
            var resultado = await casoDeUso.Handle(comando);

            //ahora lo que sigue es validar si se llamaron varias partes del metodo handle 
            //del caso de uso como...
            //await validador.Received(1).ValidateAsync(comando); //si recibio una llamada al metodo ValidateAsync con este comando
            await repositorio.Received(1).Agregar(Arg.Any<Consultorio>());//luego si repositorio recibio una llamada al metodo Agregar y se le paso cualquier consultorio
            await unidadDeTrabajo.Received(1).Persistir();//finalmente si unidad de T. recibio una llamada la metodo Persistir
            Assert.AreNotEqual(Guid.Empty, resultado);//Y estamos diciendo que el Id devuelto por el metodo casoDeUso.Handle no sea un Guid vacío
            }

      

        //Tambien deseamos probar  que Reversar se ha llamado
        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            //creamos comando valido porque no se va verificar un error de validacion
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };

            //cuando mandemos a agregar el consultorio cualquiera que sea vamos a lanzar una Excepcion
            repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();

            //para validar que el validador retorna un validation result sin Errores
            //validador.ValidateAsync(comando).Returns(new ValidationResult());

            //luego ejecutamos codigo donde esperamos obtener una Excepcion
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                var resultado = await casoDeUso.Handle(comando);
            });

            //luego la prueba de la verdad, unidad de trabajo recibió un llamado al metodo reversar
            await unidadDeTrabajo.Received(1).Reversar();



        }
    }
}
