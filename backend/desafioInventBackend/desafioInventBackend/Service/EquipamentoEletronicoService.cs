using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using FluentValidation;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService
    {

        private readonly IRepositoryEquipamentoEletronico _repository;
        private readonly EquipamentoEletronicoValidator _equipamentoEletronicoValidator;
        private readonly EquipamentoEletronicoAlterarValidator _equipamentoEletronicoAlterarValidator;
        private readonly EquipamentoEletronicoDeleteValidator _equipamentoEletronicoDeletarValidator;


        public EquipamentoEletronicoService(IRepositoryEquipamentoEletronico repository, EquipamentoEletronicoValidator equipamentoEletronicoValidator,
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

        public void Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            _equipamentoEletronicoValidator.ValidateAndThrow(equipamentoEletronico);
            _repository.Cadastrar(equipamentoEletronico);
        }

        public void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {
            _equipamentoEletronicoAlterarValidator.ValidateAndThrow(equipamentoEletronicoModificado);
            _repository.Atualizar(id, equipamentoEletronicoModificado);
        }

        public void Excluir(string id)
        {
            EquipamentoEletronico equipamentoEletronico = this.BuscarPorId(id);
            _equipamentoEletronicoDeletarValidator.ValidateAndThrow(equipamentoEletronico);
            _repository.Deletar(id);            
        }
    }
}
