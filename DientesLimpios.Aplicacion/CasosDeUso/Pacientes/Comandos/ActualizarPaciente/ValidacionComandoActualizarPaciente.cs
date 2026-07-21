using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    public class ValidacionComandoActualizarPaciente : AbstractValidator<ComandoActualizarPaciente>
    {
        public ValidacionComandoActualizarPaciente()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(254).WithMessage("La longitud del {PropertyName} debe ser menor o igual a {MaxLength}")
                .EmailAddress().WithMessage("El formato de Email no es válido");
        }
        
    }
}
