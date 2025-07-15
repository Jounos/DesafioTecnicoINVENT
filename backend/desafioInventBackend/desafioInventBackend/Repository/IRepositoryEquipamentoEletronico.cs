namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
