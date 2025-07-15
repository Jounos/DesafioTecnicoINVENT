using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using FluentValidation.Results;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService
    {

        private readonly IRepositoryEquipamentoEletronico<EquipamentoEletronico> _repository;

        public EquipamentoEletronicoService(IRepositoryEquipamentoEletronico<EquipamentoEletronico> repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<EquipamentoEletronico> Listar()
        {
            return _repository.GetAll();
        }

        public EquipamentoEletronico BuscarPorId(int id)
        {
            return _repository.GetById(id);
        }

        public void Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {

            EquipamentoEletronicoValidator validator = new EquipamentoEletronicoValidator();
            ValidationResult result = validator.Validate(equipamentoEletronico);

            if (!result.IsValid) 
            {
                return;
            }

            _repository.Insert(equipamentoEletronico);
        }

        public void Atualizar(int id, EquipamentoEletronico equipamentoEletronico)
        {

            EquipamentoEletronicoValidator validator = new EquipamentoEletronicoValidator();
            ValidationResult result = validator.Validate(equipamentoEletronico);
            
            if (!result.IsValid)
            {
                return;
            }

            _repository.Update(id, equipamentoEletronico);
        }

        public void Excluir(int id)
        {
            
            EquipamentoEletronico equipamentoEletronico = this.BuscarPorId(id);
            EquipamentoEletronicoDeleteValidator validator = new EquipamentoEletronicoDeleteValidator();
            ValidationResult result = validator.Validate(equipamentoEletronico);

            if (!result.IsValid)
            {
                return;
            }

            _repository.Delete(id);
        }

    }
}
