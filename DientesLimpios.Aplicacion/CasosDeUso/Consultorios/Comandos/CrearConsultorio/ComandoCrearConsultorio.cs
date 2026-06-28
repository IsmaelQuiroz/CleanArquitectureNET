using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    //CQRS
    //Son los datos que se requiere tener a la mano para realizar una acción
    //por ello esta clase funciona como contenedor de datos
    //con el :IRequest indico que este comando se lo voy a enviar al Mediador
    //y el <Guid> indico el tipo de dato que espero de salida del caso de uso
    public class ComandoCrearConsultorio: IRequest<Guid> 
    {
        public required string Nombre { get; set; }
    }
}
