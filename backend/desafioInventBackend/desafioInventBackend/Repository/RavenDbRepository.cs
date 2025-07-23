using DesafioInventBackend.Data;
using DesafioInventBackend.Model.Entity;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace DesafioInventBackend.Repository
{
    public class RavenDbRepository : IRepositoryEquipamentoEletronico<EquipamentoEletronico>
    {

        private readonly IDocumentStore _store = RavenDbContext.Store;

        public IEnumerable<EquipamentoEletronico> BuscarPorFiltros(string? nome, TipoEquipamento? tipoEquipamento)
        {
            using IDocumentSession session = _getOpenedSession();

            if (nome == null && tipoEquipamento == 0)
            {
                return session.Query<EquipamentoEletronico>().OrderByDescending(ee => ee.DataInclusao).ToList();
            }
            
            if (nome != null)
            {
                return session.Query<EquipamentoEletronico>().Where(ee => (nome == null ? ee.Nome.Contains(nome) : false)).OrderByDescending(ee => ee.DataInclusao).ToList();
            }
            
            
           return session.Query<EquipamentoEletronico>().Where(ee => ((int)tipoEquipamento != 0 ? ee.TipoEquipamento == tipoEquipamento : false)).ToList();
        }

        public EquipamentoEletronico BuscarPorId(string id)
        {
            using IDocumentSession session = _getOpenedSession();
            return session.Load<EquipamentoEletronico>(id) ?? null;
        }

        public void Cadastrar(EquipamentoEletronico entity)
        {

            entity.DataInclusao = DateTimeOffset.Now;

            using IDocumentSession session = _getOpenedSession();
            session.Store(entity);
            session.SaveChanges();
        }

        public void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {
            using IDocumentSession session = _getOpenedSession();

            EquipamentoEletronico equipamentoEletronico = session.Load<EquipamentoEletronico>(id);
            equipamentoEletronico.Nome = equipamentoEletronicoModificado.Nome;
            equipamentoEletronico.TipoEquipamento = equipamentoEletronicoModificado.TipoEquipamento;
            equipamentoEletronico.QuantidadeEstoque = equipamentoEletronicoModificado.QuantidadeEstoque;

            session.SaveChanges();
        }

        public void Deletar(string id)
        {
            using IDocumentSession session = _getOpenedSession();
            session.Delete(id);
            session.SaveChanges();
        }

        private IDocumentSession _getOpenedSession()
        {
            return _store.OpenSession();
        }
    }
}
