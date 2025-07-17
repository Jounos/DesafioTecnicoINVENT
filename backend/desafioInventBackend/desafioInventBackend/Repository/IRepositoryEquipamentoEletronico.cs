namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        T Insert(T entity);
        T Update(string id, T entity);
        void Delete(string id);
    }
}
