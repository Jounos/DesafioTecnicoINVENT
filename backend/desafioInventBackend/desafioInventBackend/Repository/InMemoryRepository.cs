using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Enum;
using DesafioInventBackend.Model.Filters;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Repository
{
    public class InMemoryRepository: IRepositoryEquipamentoEletronico
    {
        
        private readonly List<EquipamentoEletronico> _itens = new List<EquipamentoEletronico>();

        public IEnumerable<EquipamentoEletronico> BuscarPorFiltros(BuscaFiltros filtros)
        {
            List<EquipamentoEletronico> itensFiltrados = new List<EquipamentoEletronico>();


            if (filtros.Nome != string.Empty)
            { 
                itensFiltrados = _itens.FindAll(i => i.Nome.Contains(filtros.Nome));
            }

            if (filtros.TipoEquipamento != 0)
            {
                itensFiltrados.AddRange(_itens.FindAll(i => i.TipoEquipamento == filtros.TipoEquipamento));           
            }
            
            if (filtros.DataInicio != DateTimeOffset.MinValue)
            {
                itensFiltrados.AddRange(_itens.FindAll(i => i.DataInclusao >= filtros.DataInicio));
            }

            if (filtros.DataFim != DateTimeOffset.MinValue)
            {
                itensFiltrados.AddRange(_itens.FindAll(i => i.DataInclusao <= filtros.DataFim));
            }

            itensFiltrados.AddRange(_itens.FindAll(i =>
            {
                if (filtros.EquipamentoEmEstoque == EquipamentoEmEstoqueEnum.EM_ESTOQUE)
                {
                    return i.QuantidadeEstoque > 0;
                }

                if (filtros.EquipamentoEmEstoque == EquipamentoEmEstoqueEnum.NAO_TEM_ESTOQUE)
                {
                    return i.QuantidadeEstoque == 0;
                }

                return true;
            }));

            return itensFiltrados.Distinct().ToList();
        }

        public IEnumerable<EquipamentoEletronico> ListarTodos()
        {
            return _itens.OrderByDescending(i => i.DataInclusao);
        }
        
        public EquipamentoEletronico BuscarPorId(string id, IDocumentSession session = null)
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
