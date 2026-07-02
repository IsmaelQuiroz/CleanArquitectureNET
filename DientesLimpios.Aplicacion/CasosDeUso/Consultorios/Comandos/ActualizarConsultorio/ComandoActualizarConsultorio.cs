using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    //comando porque va modificar el estado la BD y : IRequest porque no va devolver ningun tipo de dato de retorno
    public class ComandoActualizarConsultorio : IRequest
    {
        //los parametros que ocupara el comando para realizar al acción
        public Guid Id { get; set; } //Id del consultorio a actualizar
        public required string Nombre { get; set; }//Nuevo nombre
    }
}
