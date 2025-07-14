using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service;
using DesafioInventBackend.Service.Contract;

namespace DesafioInventTest
{
    public class UnitTest1
    {

        private readonly IEquipamentoEletronicoService _service;

        private UnitTest1(EquipamentoEletronicoService service)
        {
            _service = service;
        }

        [Fact]
        public async Task deve_retornar_uma_lista_vazia()
        {
            ICollection<EquipamentoEletronico> listaEquipamenosEletronicos = await _service.listarEquipamentosEletronicos();

            Assert.Empty(listaEquipamenosEletronicos);
        }
    }
}