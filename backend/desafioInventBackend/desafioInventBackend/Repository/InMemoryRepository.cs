using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository
{
    public class InMemoryRepository<T>: IRepositoryEquipamentoEletronico<T> where T : class, IEntity
    {
        
        private readonly List<T> _items = new List<T>();
           
        public IEnumerable<T> GetAll()
        {
            return _items;
        }
        
        public T GetById(string id)
        {
            if (_items.Count == 0)
            {
                return null;
            }

            return _items.Find(i => i.Id == id);
        }

        public T Insert(T entity)
        {
            entity.Id = $"{_items.Count + 1}";
            _items.Add(entity);
            return entity;
        }

        public T Update(string id, T entityModified)
        {

            T entity = this.GetById(id);
            _items[_items.IndexOf(entity)] = entityModified;

            return entityModified;
        }

        public void Delete(string id)
        {   

            T entity = GetById(id);

            this._items.Remove(entity);
        }
    }
}
