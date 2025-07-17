using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using FluentValidation

namespace DesafioInventTest
{
    public class InMemoryTest
    {

        public EquipamentoEletronicoService _service;
        public IRepositoryEquipamentoEletronico<EquipamentoEletronico> _repository;

        public InMemoryTest() {
            _repository = new InMemoryRepository<EquipamentoEletronico>();
            _service = new EquipamentoEletronicoService(_repository);
        }


        [Fact]
        public void CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido()
        {
            
            EquipamentoEletronico ee = new EquipamentoEletronico
            {
                Nome = "Alienware",
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 2,
            };

            var eeResult = _service.Cadastrar(ee);
            
            EquipamentoEletronicoValidator eeValidator = new EquipamentoEletronicoValidator();
            Assert.True(eeValidator.Validate(eeResult).IsValid);
        }

        [Fact]
        public void CadastrarEquipamentoEletronico_DeveLancarUmaExcecaoValidationException()
        {

            EquipamentoEletronico ee = new EquipamentoEletronico
            {
                Nome = "Positivo",
            };

            Assert.Throws<ValidationException>(() => _service.Cadastrar(ee));
        }

        [Fact]
        public void EditarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido()
        {
            
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();

            EquipamentoEletronico ee = new EquipamentoEletronico
            {
                Nome = "Positivo",
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 2,
            };

            var eeResult = _service.Atualizar("1", ee);

            EquipamentoEletronicoAlterarValidator eeValidator = new EquipamentoEletronicoAlterarValidator();
            Assert.True(eeValidator.Validate(eeResult).IsValid);
        }

        [Fact]
        public void EditarEquipamentoEletronico_DeveLancarUmaExcessao()
        {

            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();

            EquipamentoEletronico ee = new EquipamentoEletronico
            {
                Nome = string.Empty,
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 2,
            };

            Assert.Throws<ValidationException>(() => _service.Atualizar("1", ee));
        }

        [Fact]
        public void ExcluirEquipamentoEletronico_DeveRetornarUmEquipamentoExcluido()
        {
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            EquipamentoEletronico ee = new EquipamentoEletronico
            {
                Nome = "Positivo",
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 0,
            };

            var eeResult = _service.Atualizar("1", ee);

            const string ID_EQUIPAMENTO_ELETROONICO = "1";
            _service.Excluir(ID_EQUIPAMENTO_ELETROONICO);
            ee = _service.BuscarPorId(ID_EQUIPAMENTO_ELETROONICO);

            EquipamentoEletronicoDeleteValidator eeValidator = new EquipamentoEletronicoDeleteValidator();
            Assert.True(eeValidator.Validate(ee).IsValid);
        }

        [Fact]
        public void ExcluirEquipamentoEletronico_DeveRetornarUmaExcessao_PorNaoPoderExcluirProdutoComEstoque()
        {
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();

            const string ID_EQUIPAMENTO_ELETRONICO = "1";
            Assert.Throws<ValidationException>(() => _service.Excluir(ID_EQUIPAMENTO_ELETRONICO));
        }

        [Fact]
        public void BuscarEquipamentoEletronicoPorId_DeveRetornarUmEquipamentoEletronico()
        {

            const string ID_ESPERADO = "3";
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();

            EquipamentoEletronico ee = _service.BuscarPorId(ID_ESPERADO);

            Assert.Equal(ID_ESPERADO, ee.Id);
        }

        [Fact]
        public void BuscarEquipamentoEletronicoPorId_NenhumEquipamentoDeveSerEncontrado()
        {

            const string ID_ESPERADO = "5";
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();

            EquipamentoEletronico ee =_service.BuscarPorId(ID_ESPERADO);

            Assert.Null(ee);
        }

        [Fact]
        public void ListarEquipamentosEletronicos_DeveRetornarTodosOsEquipamentosEletronicos()
        {

            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();
            this.CadastrarEquipamentoEletronico_DeveRetornarUmEquipamentoEletronicoValido();


            IEnumerable<EquipamentoEletronico> lista = _service.Listar();

            EquipamentoEletronicoValidator eeValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(lista,
                    e => Assert.True(eeValidator.Validate(e).IsValid),
                    e => Assert.True(eeValidator.Validate(e).IsValid),
                    e => Assert.True(eeValidator.Validate(e).IsValid),
                    e => Assert.True(eeValidator.Validate(e).IsValid)
                );
        }

        [Fact]
        public void ListarEquipamentosEletronicos_NenhumEquipamentoDeveSerEncontrado()
        {
            IEnumerable<EquipamentoEletronico> lista = _service.Listar();

            Assert.Empty(lista);
        }
    }
}
