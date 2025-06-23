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
            }).ToListAsync();
        }

        public async Task<EquipamentoEletronico> buscarEquipamentoEletronicoPorId(int id)
        {
            EquipamentoEletronico equipamentoEletronico = await _dataContext.equipamentoEletronico.Select(ee => new EquipamentoEletronico
            {
                Id = ee.Id,
                Nome = ee.Nome,
                TipoEquipamento = ee.TipoEquipamento,
                QuantidadeEstoque = ee.QuantidadeEstoque,
                TemEstoque = ee.QuantidadeEstoque > 0,
                DataInclusao = ee.DataInclusao,
                DataExclusao = ee.DataExclusao
            }).FirstAsync(e => e.Id == id);
            return equipamentoEletronico;
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
