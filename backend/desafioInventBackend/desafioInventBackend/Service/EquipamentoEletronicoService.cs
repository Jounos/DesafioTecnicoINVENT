using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository.Contract;
using DesafioInventBackend.Service.Contract;
using FluentValidation.Results;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService : IEquipamentoEletronicoService
    {

        private readonly IEquipamentoEletronicoRepository _repository;

        public EquipamentoEletronicoService(IEquipamentoEletronicoRepository equipamentoEletronicoRepositry)
        {
            this._repository = equipamentoEletronicoRepositry;
        }

        public async Task<ICollection<EquipamentoEletronico>> listarEquipamentosEletronicos()
        {
            return await _repository.listarEquipamentosEletronicos();
        }

        public async Task<EquipamentoEletronico> buscarEquipamentoEletronicoPorId(int id)
        {
            EquipamentoEletronico equipamentoEletronico = await _repository.buscarEquipamentoEletronicoPorId(id);
            EquipamentoEletronicoValidator validator = new EquipamentoEletronicoValidator();

            ValidationResult result = validator.Validate(equipamentoEletronico);
            if (!result.IsValid)
            {
                return null;
            }

            return equipamentoEletronico;
        }

        public async Task<EquipamentoEletronico> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {

            EquipamentoEletronicoValidator validator = new EquipamentoEletronicoValidator();
            ValidationResult result = validator.Validate(equipamentoEletronico);

            if (!result.IsValid) 
            {
                return null;
            }

            EquipamentoEletronico equipamentoEletronicoCadastrado = await _repository.cadastrarEquipamentoEletronico(equipamentoEletronico);

            return equipamentoEletronicoCadastrado;
        }

        public async Task<EquipamentoEletronico> atualizarEquipamentoEletronico(int id, EquipamentoEletronico equipamentoEletronico)
        {

            EquipamentoEletronicoValidator validator = new EquipamentoEletronicoValidator();
            ValidationResult result = validator.Validate(equipamentoEletronico);
            
            if (!result.IsValid)
            {
                return null;
            }

            return await _repository.atualizarEquipamentoEletronico(equipamentoEletronico);
        }

        public async Task<EquipamentoEletronico> excluirEquipamentoEletronico(int id)
        {
            
            EquipamentoEletronico equipamentoEletronico = await this.buscarEquipamentoEletronicoPorId(id);
            EquipamentoEletronicoDeleteValidator validator = new EquipamentoEletronicoDeleteValidator();
            ValidationResult result = validator.Validate(equipamentoEletronico);

            if (!result.IsValid)
            {
                return null;
            }

            equipamentoEletronico.DataExclusao = DateTime.Now;
            await _repository.atualizarEquipamentoEletronico(equipamentoEletronico);

            return await this.buscarEquipamentoEletronicoPorId(id);
        }

    }
}
