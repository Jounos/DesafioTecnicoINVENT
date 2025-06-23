using DesafioInventBackend.Model.DTO;

namespace DesafioInventBackend.Service.Contract
{
    public interface IEquipamentoEletronicoService
    {
        public Task<ICollection<RetornoEquipamentoEletronicoDto>> listarEquipamentosEletronicos();

        public Task<RetornoEquipamentoEletronicoDto> buscarEquipamentoEletronicoPorId(int id);

        public Task<RetornoEquipamentoEletronicoDto> cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronico);
        
        public Task<RetornoEquipamentoEletronicoDto> atualizarEquipamentoEletronico(int id, AtualizarEquipamentoEletronicoDto equipamentoEletronico);

        public Task<RetornoEquipamentoEletronicoDto> excluirEquipamentoEletronico(int id);
    }
}
