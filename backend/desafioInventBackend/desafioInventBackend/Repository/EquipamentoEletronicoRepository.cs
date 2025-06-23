using DesafioInventBackend.Context;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace DesafioInventBackend.Repository
{
    public class EquipamentoEletronicoRepository: IEquipamentoEletronicoRepository
    {

        private readonly DataContext _dataContext;

        public EquipamentoEletronicoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ICollection<EquipamentoEletronico>> listarEquipamentosEletronicos()
        {
            return await _dataContext.equipamentoEletronico.ToListAsync();
        }

        public async Task<EquipamentoEletronico> buscarEquipamentoEletronicoPorId(int id)
        {
            return await _dataContext.equipamentoEletronico.FirstAsync(e => e.Id == id);
        }

        public async Task<bool> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            await _dataContext.equipamentoEletronico.AddAsync(equipamentoEletronico);
            return await Salvar();
        }

        public async Task<bool> atualizarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            _dataContext.equipamentoEletronico.Update(equipamentoEletronico);
            return await Salvar();
        }

        private async Task<bool> Salvar() 
        {
            return await _dataContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
