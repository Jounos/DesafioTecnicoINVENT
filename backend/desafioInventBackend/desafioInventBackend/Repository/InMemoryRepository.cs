using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository
{
    public class InMemoryRepository: IRepositoryEquipamentoEletronico<EquipamentoEletronico>
    {
        
        private readonly List<EquipamentoEletronico> _items = new List<EquipamentoEletronico>();
           
        public IEnumerable<EquipamentoEletronico> GetAll()
        {
            return _items;
        }
        
        public EquipamentoEletronico GetById(string id)
        {
            if (_items.Count == 0)
            {
                return null;
            }

            return _items.Find(i => i.Id == id);
        }

        public EquipamentoEletronico Insert(EquipamentoEletronico entity)
        {
            entity.Id = $"{_items.Count + 1}";
            _items.Add(entity);
            return entity;
        }

        public EquipamentoEletronico Update(string id, EquipamentoEletronico entityModified)
        {

            EquipamentoEletronico entity = GetById(id);
            var index = _items.IndexOf(entity);

            entity.Nome = entityModified.Nome;
            entity.TipoEquipamento = entityModified.TipoEquipamento;
            entity.QuantidadeEstoque = entityModified.QuantidadeEstoque;

            _items[index] = entity;

            return entityModified;
        }

        public void Delete(string id)
        {

            EquipamentoEletronico entity = GetById(id);

            _items.Remove(entity);
        }
    }
}
