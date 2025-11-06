using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Data
{
    public interface IServicoSessaoRaven
    {
        IDocumentStore Store { get; set; }
        IDocumentSession Session { get; set; }
    }
}
