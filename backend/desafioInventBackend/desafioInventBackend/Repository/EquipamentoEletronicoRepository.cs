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

        public EquipamentoEletronico buscarEquipamentoEletronicoPorId(int id)
        {
            return _dataContext.equipamentoEletronico.Where(e => e.Id == id).Single();
        }

        public ICollection<EquipamentoEletronico> listarEquipamentosEletronicos()
        {
            return _dataContext.equipamentoEletronico.ToList();
        }

        public bool cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            _dataContext.equipamentoEletronico.Add(equipamentoEletronico);
            return Salvar();
        }

        public bool atualizarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            _dataContext.equipamentoEletronico.Update(equipamentoEletronico);
            return Salvar();
        }

        private bool Salvar() 
        {
            return _dataContext.SaveChanges() > 0 ? true : false;
        }
    }
}
