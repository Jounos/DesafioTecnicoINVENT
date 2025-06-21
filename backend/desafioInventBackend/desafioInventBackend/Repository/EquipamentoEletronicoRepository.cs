using DesafioInventBackend.Context;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository.Contract;

namespace DesafioInventBackend.Repository
{
    public class EquipamentoEletronicoRepository: IEquipamentoEletronicoRepository
    {

        private readonly DataContext _dataContext;

        public EquipamentoEletronicoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        Task<ICollection<EquipamentoEletronico>> IEquipamentoEletronicoRepository.buscarEquipamentoEletronicoPorId(int id)
        {            
            throw new NotImplementedException();
        }

        Task<ICollection<EquipamentoEletronico>> IEquipamentoEletronicoRepository.listarEquipamentosEletronicos()
        {
            throw new NotImplementedException();
        }

        Task<EquipamentoEletronico> IEquipamentoEletronicoRepository.salvarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            throw new NotImplementedException();
        }
    }
}
