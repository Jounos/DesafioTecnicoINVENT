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
            return await _dataContext.equipamentoEletronico.Select(ee => new EquipamentoEletronico
            {
                Id = ee.Id,
                Nome = ee.Nome,
                TipoEquipamento = ee.TipoEquipamento,
                QuantidadeEstoque = ee.QuantidadeEstoque,
                TemEstoque = ee.QuantidadeEstoque > 0,
                DataInclusao = ee.DataInclusao,
                DataExclusao = ee.DataExclusao
            }).Where(e => e.DataExclusao == null).ToListAsync();
        }

        public async Task<EquipamentoEletronico> buscarEquipamentoEletronicoPorId(int id)
        {
            return await _dataContext.equipamentoEletronico.SingleAsync(e => e.Id == id);
        }

        public async Task<EquipamentoEletronico> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            await _dataContext.equipamentoEletronico.AddAsync(equipamentoEletronico);
            await Salvar();
            return equipamentoEletronico;
        }

        public async Task<EquipamentoEletronico> atualizarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            _dataContext.equipamentoEletronico.Update(equipamentoEletronico);
            await Salvar();
            return equipamentoEletronico;
        }

        private async Task<bool> Salvar() 
        {
            return await _dataContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
