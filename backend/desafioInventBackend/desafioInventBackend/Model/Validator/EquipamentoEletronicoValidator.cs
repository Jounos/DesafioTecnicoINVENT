using DesafioInventBackend.Model.Entity;
using FluentValidation;

namespace DesafioInventBackend.Model.Validator
{
    public class EquipamentoEletronicoValidator: AbstractValidator<EquipamentoEletronico>
    {

        public EquipamentoEletronicoValidator() {

            RuleFor(ee => ee.Nome).NotNull().NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(ee => (int)ee.TipoEquipamento).NotNull().InclusiveBetween(1, 5);
        }
    }
}
