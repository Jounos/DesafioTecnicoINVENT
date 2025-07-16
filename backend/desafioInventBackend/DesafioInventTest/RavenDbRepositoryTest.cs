using AutoMapper;
using DesafioInventBackend.Controller;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service
using Moq;

namespace DesafioInventTest
{
    internal class RavenDbRepositoryTest
    {

        private readonly Mock<EquipamentoEletronicoService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;

        public RavenDbRepositoryTest()
        {
            _serviceMock = new Mock<EquipamentoEletronicoService>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
        }


        public async Task ListaTodos_Retorna_ListaEquipamentosEletronicos()
        {

            // Arrange
            var tresult = _serviceMock.Setup(x => x.Listar()).Returns(new List<EquipamentoEletronico>());

            // Act
            var controller = new EquipamentoEletronicoController(_serviceMock.Object, _mapperMock.Object);
            var result = controller.ListarTodosEquipamentosEletronicos();

            // assert

            Assert.Equal(tresult, result);
        }
    }
}
