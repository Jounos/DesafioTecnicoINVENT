using DesafioInventBackend.Model.Entity;
using FluentValidation;

namespace DesafioInventBackend.Model.Validator
{
    public class EquipamentoEletronicoDeleteValidator: AbstractValidator<EquipamentoEletronico>
    {
        public EquipamentoEletronicoDeleteValidator()
        {
            RuleFor(ee => ee.QuantidadeEstoque).Equal(0);
            RuleFor(ee => ee.DataExclusao).Null();
        }
    }
}
