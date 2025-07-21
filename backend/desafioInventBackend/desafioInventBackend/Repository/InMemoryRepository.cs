using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository
{
    public class InMemoryRepository: IRepositoryEquipamentoEletronico<EquipamentoEletronico>
    {
        
        private readonly List<EquipamentoEletronico> _items = new List<EquipamentoEletronico>();
           
        public IEnumerable<EquipamentoEletronico> ListarTodos()
        {
            return _items;
        }
        
        public EquipamentoEletronico BuscarPorId(string id)
        {
            if (_items.Count == 0)
            {
                return null;
            }

            return _items.Find(i => i.Id == id);
        }

        public EquipamentoEletronico Cadastrar(EquipamentoEletronico entity)
        {
            entity.Id = $"{_items.Count + 1}";
            entity.DataInclusao = DateTimeOffset.Now;

            _items.Add(entity);
            return entity;
        }

        public EquipamentoEletronico Atualizar(string id, EquipamentoEletronico entityModified)
        {

            EquipamentoEletronico entity = BuscarPorId(id);
            var index = _items.IndexOf(entity);

            entity.Nome = entityModified.Nome;
            entity.TipoEquipamento = entityModified.TipoEquipamento;
            entity.QuantidadeEstoque = entityModified.QuantidadeEstoque;

            _items[index] = entity;

            return entityModified;
        }

        public void Deletar(string id)
        {

            EquipamentoEletronico entity = BuscarPorId(id);

            _items.Remove(entity);
        }
    }
}
