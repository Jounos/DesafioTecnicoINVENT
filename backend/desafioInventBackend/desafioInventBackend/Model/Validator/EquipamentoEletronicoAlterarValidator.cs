using DesafioInventBackend.Model.Entity;
using FluentValidation;

namespace DesafioInventBackend.Model.Validator
{
    public class EquipamentoEletronicoAlterarValidator: EquipamentoEletronicoValidator
    {
        public EquipamentoEletronicoAlterarValidator() {
            RuleFor(ee => ee.Id).NotNull().NotEmpty();
        }
    }
}
