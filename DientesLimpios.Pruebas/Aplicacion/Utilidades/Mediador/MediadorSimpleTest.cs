using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using FluentValidation;
using Microsoft.VisualStudio.TestPlatform.Common;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.Utilidades.Mediador
{
    [TestClass]
    public class MediadorSimpleTest
    {
        //se va probar que al enviar el IReuest<TResponse> al Mediador se va invocar el método Handler
        public class RequestFalso : IRequest<string> { 
            public required string Nombre { get; set; }
        }

        public class HandlerFalso : IRequestHandler<RequestFalso, string>
        {
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("respuesta correcta");
            }
        }

        public class ValidadorRequestFalso : AbstractValidator<RequestFalso>
        {
            public ValidadorRequestFalso()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }


        [TestMethod]
        public async Task Send_LlamaMetodoHandler()
        {
            //en el contexto del casoDeUso crear consultorio
            //este request seria el comandoCrearConsultorio
            //pero aqui es un simple request generico cualquier cosa
            var request = new RequestFalso(){ Nombre = "Nombre A"};
            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
            var serviceProvider = Substitute.For<IServiceProvider>();

            serviceProvider.GetService(typeof(IRequestHandler<RequestFalso, string>))
                .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProvider);
            var resultado = await mediador.Send(request);

            //utlizar el casoDeUsoMock para verificar que se halla ejecutado 1 vez el metodo Handle
            await casoDeUsoMock.Received(1).Handle(request);

        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeMediador))]
        public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
        {
            //en el contexto del casoDeUso crear consultorio
            //este request seria el comandoCrearConsultorio
            //pero aqui es un simple request generico cualquier cosa
            var request = new RequestFalso() { Nombre = "Nombre A" };
            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
            var serviceProvider = Substitute.For<IServiceProvider>();

            //serviceProvider.GetService(typeof(IRequestHandler<RequestFalso, string>))
            //    .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProvider);
            var resultado = await mediador.Send(request);

            //utilizar el casoDeUsoMock para verificar que se halla ejecutado 1 vez el metodo Handle
           // await casoDeUsoMock.Received(1).Handle(request);

        }

        [TestMethod]
        public async Task Send_ComandoNoValido_LanzaExcepcion()
        {
            var request = new RequestFalso() { Nombre = "" };

            //creamos un Mock para el service provider
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validador = new ValidadorRequestFalso();

            serviceProvider
                .GetService(typeof(IValidator<RequestFalso>))
                .Returns(validador);

            var mediador = new MediadorSimple(serviceProvider);

            await Assert.ThrowsExceptionAsync<ExcepcionDeValidacion>(async () =>
            {
                await mediador.Send(request);
            });
        }
    }
}
