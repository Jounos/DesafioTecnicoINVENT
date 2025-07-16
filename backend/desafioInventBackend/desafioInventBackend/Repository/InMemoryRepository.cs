using DesafioInventBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioInventBackend.Repository
{
    public class InMemoryRepository<T>: IRepositoryEquipamentoEletronico<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public InMemoryRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
           
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        
        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(string id, T entityModified)
        {
            T entity = _dbSet.Find(id);
            entity = entityModified;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(string id)
        {   

            T entity = GetById(id);

            _dbSet.Remove(entity);
        }
    }
}
