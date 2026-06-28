using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    //cuando se le mande al mediador este request "ConsultaObtenerDetalleConsultorio"  lo que se
    //va devolver es una instancia de ConsultorioDetalleDTO
    public class ConsultaObtenerDetalleConsultorio: IRequest<ConsultorioDetalleDTO>
    {
        //lo que se necesita para consultar un consultorio es:
        public Guid Id { get; set; }
    }
}
