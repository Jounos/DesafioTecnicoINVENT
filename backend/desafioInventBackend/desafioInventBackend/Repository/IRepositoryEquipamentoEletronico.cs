namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Insert(T entity);
        void Update(string id, T entity);
        void Delete(string id);
    }
}
