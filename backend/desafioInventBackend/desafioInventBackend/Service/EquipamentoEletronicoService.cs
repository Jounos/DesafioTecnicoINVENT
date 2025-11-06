using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Filters;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using FluentValidation;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService
    {

        private readonly IRepositoryEquipamentoEletronico _repository;
        private readonly EquipamentoEletronicoCadastrarValidator _equipamentoEletronicoCadastrarValidator;
        private readonly EquipamentoEletronicoAlterarValidator _equipamentoEletronicoAlterarValidator;
        private readonly EquipamentoEletronicoDeleteValidator _equipamentoEletronicoDeletarValidator;


        public EquipamentoEletronicoService(IRepositoryEquipamentoEletronico repository, EquipamentoEletronicoCadastrarValidator equipamentoEletronicoCadastrarValidator,
            EquipamentoEletronicoAlterarValidator equipamentoEletronicoAlterarValidator, EquipamentoEletronicoDeleteValidator equipamentoEletronicoDeletarValidator)
        {
            _repository = repository;
            _equipamentoEletronicoCadastrarValidator = equipamentoEletronicoCadastrarValidator;
            _equipamentoEletronicoAlterarValidator = equipamentoEletronicoAlterarValidator;
            _equipamentoEletronicoDeletarValidator = equipamentoEletronicoDeletarValidator;
        }

        public IEnumerable<EquipamentoEletronico> Buscar(BuscaFiltros filtros = null)
        {
            return _repository.Buscar(filtros);
        }

        public EquipamentoEletronico BuscarPorId(string id)
        {
            return _repository.BuscarPorId(id);
        }

        public void Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            _equipamentoEletronicoCadastrarValidator.ValidateAndThrow(equipamentoEletronico);
            _repository.Cadastrar(equipamentoEletronico);
        }

        public void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {
            _equipamentoEletronicoAlterarValidator.ValidateAndThrow(equipamentoEletronicoModificado);
            _repository.Atualizar(id, equipamentoEletronicoModificado);
        }

        public void Excluir(EquipamentoEletronico equipamentoEletronico)
        {
            _equipamentoEletronicoDeletarValidator.ValidateAndThrow(equipamentoEletronico);
            _repository.Deletar(equipamentoEletronico.Id);            
        }
    }
}
