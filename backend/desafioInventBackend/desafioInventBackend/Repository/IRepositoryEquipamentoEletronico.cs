namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico<T>
    {
        IEnumerable<T> ListarTodos();
        T BuscarPorId(string id);
        T Cadastrar(T entity);
        T Atualizar(string id, T entity);
        void Deletar(string id);
    }
}
