using DesafioInventBackend.Model.Entity;
using FluentValidation;

namespace DesafioInventBackend.Model.Validator
{
    public class EquipamentoEletronicoValidator: AbstractValidator<EquipamentoEletronico>
    {

        public EquipamentoEletronicoValidator() {

            RuleFor(ee => ee.Nome).NotNull().NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(ee => (int)ee.TipoEquipamento).NotNull().InclusiveBetween(1, 4);
            RuleFor(ee => ee.QuantidadeEstoque).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(ee => ee.DataInclusao).NotNull().Must(_deveSerDataRecente);
            RuleFor(ee => ee.DataExclusao).Null();
        }

        private bool _deveSerDataRecente(DateTimeOffset date)
        {
            if (date.Equals(DateTimeOffset.MinValue) || date.Equals(DateTimeOffset.MaxValue))
            {
                return false;
            }

            if ((DateTimeOffset.Compare(date, new DateTime(2009, 12, 31)) < 0) && (DateTimeOffset.Compare(date, new DateTime(2030, 1, 1)) > 0))
            {
                return false;
            }

            return true;
        }
    }
}
