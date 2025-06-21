using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service.Contract;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService : IEquipamentoEletronicoService
    {
        Task<ICollection<EquipamentoEletronico>> IEquipamentoEletronicoService.buscarEquipamentoEletronicoPorId(int id)
        {
            throw new NotImplementedException();
        }

        Task<EquipamentoEletronico> IEquipamentoEletronicoService.cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            throw new NotImplementedException();
        }

        Task<EquipamentoEletronico> IEquipamentoEletronicoService.editarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            throw new NotImplementedException();
        }

        Task<bool> IEquipamentoEletronicoService.excluirEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<EquipamentoEletronico>> IEquipamentoEletronicoService.listarEquipamentosEletronicos()
        {
            throw new NotImplementedException();
        }
    }
}
