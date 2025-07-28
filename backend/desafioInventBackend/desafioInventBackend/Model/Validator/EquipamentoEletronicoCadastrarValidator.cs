using FluentValidation;

namespace DesafioInventBackend.Model.Validator
{
    public class EquipamentoEletronicoCadastrarValidator: EquipamentoEletronicoValidator
    {

        public EquipamentoEletronicoCadastrarValidator(): base()
        {
            RuleFor(ee => ee.QuantidadeEstoque).NotNull().GreaterThanOrEqualTo(1);
        }
    }
}
