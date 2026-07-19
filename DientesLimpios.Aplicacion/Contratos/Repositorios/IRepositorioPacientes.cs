using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoDePacientes;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    public interface IRepositorioPacientes: IRepositorio<Paciente>
    {
        Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro);
    }
}
