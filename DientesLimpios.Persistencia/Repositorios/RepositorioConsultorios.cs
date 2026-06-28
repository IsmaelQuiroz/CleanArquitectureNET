using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioConsultorios : Repositorio<Consultorio>, IRepositorioConsultorios
    {
        private readonly DientesLimpiosDbContext context;

        public RepositorioConsultorios(DientesLimpiosDbContext context)
            :base(context)
        {
            this.context = context;
        }



    }
}
