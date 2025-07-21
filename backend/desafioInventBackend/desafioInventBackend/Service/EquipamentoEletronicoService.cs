using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using FluentValidation;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService
    {

        private readonly IRepositoryEquipamentoEletronico<EquipamentoEletronico> _repository;
        private readonly EquipamentoEletronicoValidator _equipamentoEletronicoValidator;
        private readonly EquipamentoEletronicoAlterarValidator _equipamentoEletronicoAlterarValidator;
        private readonly EquipamentoEletronicoDeleteValidator _equipamentoEletronicoDeletarValidator;


        public EquipamentoEletronicoService(IRepositoryEquipamentoEletronico<EquipamentoEletronico> repository, EquipamentoEletronicoValidator equipamentoEletronicoValidator,
            EquipamentoEletronicoAlterarValidator equipamentoEletronicoAlterarValidator, EquipamentoEletronicoDeleteValidator equipamentoEletronicoDeletarValidator)
        {
            _repository = repository;
            _equipamentoEletronicoValidator = equipamentoEletronicoValidator;
            _equipamentoEletronicoAlterarValidator = equipamentoEletronicoAlterarValidator;
            _equipamentoEletronicoDeletarValidator = equipamentoEletronicoDeletarValidator;
        }
        
        public IEnumerable<EquipamentoEletronico> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        public EquipamentoEletronico BuscarPorId(string id)
        {
            return _repository.BuscarPorId(id);
        }

        public EquipamentoEletronico Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            _equipamentoEletronicoValidator.ValidateAndThrow(equipamentoEletronico);
            return _repository.Cadastrar(equipamentoEletronico);
        }

        public EquipamentoEletronico Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {
            EquipamentoEletronico equipamentoEletronico = this.BuscarPorId(id);
            _equipamentoEletronicoAlterarValidator.ValidateAndThrow(equipamentoEletronico);
            return _repository.Atualizar(id, equipamentoEletronico);
        }

        public void Excluir(string id)
        {
            EquipamentoEletronico equipamentoEletronico = this.BuscarPorId(id);
            _equipamentoEletronicoDeletarValidator.ValidateAndThrow(equipamentoEletronico);
            _repository.Deletar(id);            
        }
    }
}
