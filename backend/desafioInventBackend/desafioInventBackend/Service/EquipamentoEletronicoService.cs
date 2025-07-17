using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using FluentValidation;

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

        public EquipamentoEletronico BuscarPorId(string id)
        {
            return _repository.GetById(id);
        }

        public EquipamentoEletronico Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            equipamentoEletronico.DataInclusao = DateTime.Now;
            
            EquipamentoEletronicoValidator validator = new EquipamentoEletronicoValidator();
            validator.ValidateAndThrow(equipamentoEletronico);

            return _repository.Insert(equipamentoEletronico);
        }

        public EquipamentoEletronico Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {

            EquipamentoEletronico equipamentoEletronico = this.BuscarPorId(id);
            equipamentoEletronico.Nome = equipamentoEletronicoModificado.Nome;
            equipamentoEletronico.TipoEquipamento = equipamentoEletronicoModificado.TipoEquipamento;
            equipamentoEletronico.QuantidadeEstoque = equipamentoEletronicoModificado.QuantidadeEstoque;

            EquipamentoEletronicoAlterarValidator validator = new EquipamentoEletronicoAlterarValidator();
            validator.ValidateAndThrow(equipamentoEletronico);

            return _repository.Update(id, equipamentoEletronico);
        }

        public EquipamentoEletronico Excluir(string id)
        {
            
            EquipamentoEletronico equipamentoEletronico = this.BuscarPorId(id);
            equipamentoEletronico.DataExclusao = DateTime.Now;
            
            EquipamentoEletronicoDeleteValidator validator = new EquipamentoEletronicoDeleteValidator();
            validator.ValidateAndThrow(equipamentoEletronico);

            return _repository.Update(id, equipamentoEletronico);
        }
    }
}
