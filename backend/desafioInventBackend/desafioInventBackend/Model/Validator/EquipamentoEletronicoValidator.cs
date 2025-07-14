using DesafioInventBackend.Model.Entity;
using FluentValidation;

namespace DesafioInventBackend.Model.Validator
{
    public class EquipamentoEletronicoValidator: AbstractValidator<EquipamentoEletronico>
    {

        public EquipamentoEletronicoValidator() {

            RuleFor(ee => ee.Nome).NotNull().NotEmpty();
            RuleFor(ee => ee.TipoEquipamento).NotNull();
            RuleFor(ee => ee.QuantidadeEstoque).NotNull();
            RuleFor(ee => ee.TemEstoque).NotNull();
            RuleFor(ee => ee.DataInclusao).NotNull();
        }
    }
}
