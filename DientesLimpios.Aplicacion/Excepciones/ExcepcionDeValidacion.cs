using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Excepciones
{
    public class ExcepcionDeValidacion : Exception
    {
        public List<string> ErroresValidacion { get; set; } = [];

        public ExcepcionDeValidacion(ValidationResult validationResult)
        {
           foreach(var errorValidacion in validationResult.Errors)
            {
                ErroresValidacion.Add(errorValidacion.ErrorMessage);
            }
        }
    }
}
