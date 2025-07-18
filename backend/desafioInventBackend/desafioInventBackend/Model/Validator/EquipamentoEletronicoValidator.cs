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
            RuleFor(ee => ee.DataInclusao).NotNull().Must(DeveSerDataRecente);
            RuleFor(ee => ee.DataExclusao).Null();
        }

        protected bool DeveSerDataRecente(DateTime date)
        {
            if (date.Equals(DateTime.MinValue))
            {
                return false;
            }

            if (date.Equals(DateTime.MaxValue))
            {
                return false;
            }

            if (DateTime.Compare(date, new DateTime(2009, 12, 31)) < 0)
            {
                return false;
            }

            if (DateTime.Compare(date, new DateTime(2030, 1, 1)) > 0)
            {
                return false;
            }

            return true;
        }
    }
}
