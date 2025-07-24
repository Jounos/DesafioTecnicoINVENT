using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico<T>
    {
        IEnumerable<T> ListarTodos();
        T BuscarPorId(string id);
        void Cadastrar(T entity);
        void Atualizar(string id, T entity);
        void Deletar(string id);
    }
}
