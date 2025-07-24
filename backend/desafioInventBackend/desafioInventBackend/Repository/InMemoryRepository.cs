using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository
{
    public class InMemoryRepository: IRepositoryEquipamentoEletronico
    {
        
        private readonly List<EquipamentoEletronico> _itens = new List<EquipamentoEletronico>();
           
        public IEnumerable<EquipamentoEletronico> ListarTodos()
        {
            return _itens;
        }
        
        public EquipamentoEletronico BuscarPorId(string id)
        {
            if (_itens.Count == 0)
            {
                return null;
            }

            return _itens.Find(i => i.Id == id);
        }

        public void Cadastrar(EquipamentoEletronico entity)
        {
            entity.Id = $"{_itens.Count + 1}";
            entity.DataInclusao = DateTimeOffset.Now;

            _itens.Add(entity);
        }

        public void Atualizar(string id, EquipamentoEletronico entityModified)
        {

            EquipamentoEletronico entity = BuscarPorId(id);
            var index = _itens.IndexOf(entity);

            entity.Nome = entityModified.Nome;
            entity.TipoEquipamento = entityModified.TipoEquipamento;
            entity.QuantidadeEstoque = entityModified.QuantidadeEstoque;

            _itens[index] = entity;
        }

        public void Deletar(string id)
        {

            EquipamentoEletronico entity = BuscarPorId(id);

            _itens.Remove(entity);
        }
    }
}
