using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Enum;
using DesafioInventBackend.Model.Filters;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using DesafioInventTest.Configuracao;
using FluentValidation;
using Xunit.Abstractions;

namespace DesafioInventTest
{
    public class EquipamentoEletronicoTest : BaseParaTesteUnitario
    {
        const string ID_EQUIPAMENTO_ELETRONICO = "1";
        public EquipamentoEletronicoService _service;
        public IRepositoryEquipamentoEletronico _repository;

        public EquipamentoEletronicoTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _repository = new InMemoryRepository();
            _service = new EquipamentoEletronicoService(_repository, new EquipamentoEletronicoCadastrarValidator(), new EquipamentoEletronicoAlterarValidator(), new EquipamentoEletronicoDeleteValidator());
        }
        
        private EquipamentoEletronico _criarEquipamentoEletronico(string nome, TipoEquipamentoEnum tipoEquipamentoEnum, int quantidadeEstoque, string id = null)
        {
            return new EquipamentoEletronico
            {
                Id = id,
                Nome = nome,
                TipoEquipamento = tipoEquipamentoEnum,
                QuantidadeEstoque = quantidadeEstoque
            };
        }

        private EquipamentoEletronico _cadastrarEquipamentoEletronico(string nome, TipoEquipamentoEnum tipoEquipamentoEnum, int quantidadeEstoque, string id = null)
        {
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nome, tipoEquipamentoEnum, quantidadeEstoque, id);
            _service.Cadastrar(equipamentoEletronico);
            return equipamentoEletronico;
        }

        [Fact]
        public void Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido()
        {
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            _service.Cadastrar(equipamentoEletronico);
            
            EquipamentoEletronicoValidator equipamentoEletronicoeValidator = new EquipamentoEletronicoValidator();
            EquipamentoEletronico equipamentoEletronicoCadastrado = _service.BuscarPorId(ID_EQUIPAMENTO_ELETRONICO);
            Assert.True(equipamentoEletronicoeValidator.Validate(equipamentoEletronicoCadastrado).IsValid);
        }

        [Fact]
        public void Cadastrar_equipamento_eletronico_deve_lancar_uma_excecao_ValidationException_ja_que_equipamento_eletronico_nao_tem_informacoes_minimas_exigidas()
        {
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 0);
            Assert.Throws<ValidationException>(() => _service.Cadastrar(equipamentoEletronico));
        }

        [Fact]
        public void Cadastrar_equipamento_eletronico_deve_lancar_uma_excecao_ValidationException_Por_tentar_cadastrar_Equipamento_Eletronico_com_Zero_em_estoque()
        {
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 0);
            Assert.Throws<ValidationException>(() => _service.Cadastrar(equipamentoEletronico));
        }

        [Fact]
        public void Editar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido()
        {
            string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            nomeEquipamento = "Positivo";
            equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2, ID_EQUIPAMENTO_ELETRONICO);

            _service.Atualizar(ID_EQUIPAMENTO_ELETRONICO, equipamentoEletronico);

            EquipamentoEletronicoAlterarValidator equipamentoEletronicoeValidator = new EquipamentoEletronicoAlterarValidator();
            equipamentoEletronico = _service.BuscarPorId(ID_EQUIPAMENTO_ELETRONICO);
            Assert.True(equipamentoEletronicoeValidator.Validate(equipamentoEletronico).IsValid);
        }

        [Fact]
        public void Editar_equipamento_eletronico_deve_lancar_uma_excessao_por_tentar_salvar_um_equipamento_eletronico_sem_nome()
        {

            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            equipamentoEletronico = _criarEquipamentoEletronico(string.Empty, TipoEquipamentoEnum.PC, 2);
            
            Assert.Throws<ValidationException>(() => _service.Atualizar(ID_EQUIPAMENTO_ELETRONICO, equipamentoEletronico));
        }

        [Fact]
        public void Excluir_equipamento_eletronico_deve_excluir_um_equipamento_eletronico_e_nao_o_encontrar_apos_excluido()
        {

            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 0, ID_EQUIPAMENTO_ELETRONICO);

            _service.Atualizar(ID_EQUIPAMENTO_ELETRONICO, equipamentoEletronico);

            _service.Excluir(equipamentoEletronico.Id);

            Assert.Null(_service.BuscarPorId(ID_EQUIPAMENTO_ELETRONICO));
        }

        [Fact]
        public void Excluir_equipamento_eletronico_deve_retornar_uma_excessao_ValidationException_por_tentar_excluir_produto_com_estoque()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            EquipamentoEletronico equipamentoEletronico = _service.BuscarPorId(ID_EQUIPAMENTO_ELETRONICO);
            Assert.Throws<ValidationException>(() => _service.Excluir(equipamentoEletronico.Id));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_id_deve_retornar_um_equipamento_eletronico()
        {

            const string ID_ESPERADO = "3";
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            EquipamentoEletronico equipamentoEletronico = _service.BuscarPorId(ID_ESPERADO);

            Assert.Equal(ID_ESPERADO, equipamentoEletronico.Id);
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_filtro_nome_deve_encontrar_um_equipamento()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            BuscaFiltros filtro = new BuscaFiltros { Nome = "Alienware" };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar(filtro);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_filtro_tipo_equipamento_deve_encontrar_um_equipamento()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            BuscaFiltros filtro = new BuscaFiltros { TipoEquipamento = TipoEquipamentoEnum.PC };

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar(filtro);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_filtro_data_inclusao_informando_apenas_data_inicio_deve_encontrar_um_equipamento()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            BuscaFiltros filtro = new BuscaFiltros { DataInicio = DateTimeOffset.Parse("2025-10-20") };

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar(filtro);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_filtro_data_inclusao_informando_apenas_data_fim_deve_encontrar_um_equipamento()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            BuscaFiltros filtro = new BuscaFiltros { DataFim = DateTimeOffset.Parse("2025-12-30") };

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar(filtro);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_filtro_por_estoque_deve_encontrar_um_equipamento()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            BuscaFiltros filtro = new BuscaFiltros { EquipamentoEmEstoque = EquipamentoEmEstoqueEnum.EM_ESTOQUE };

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar(filtro);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_filtro_por_estoque_vazio_nao_deve_encontrar_um_equipamento()
        {
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            BuscaFiltros filtro = new BuscaFiltros { EquipamentoEmEstoque = EquipamentoEmEstoqueEnum.NAO_TEM_ESTOQUE };

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar(filtro);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Empty(listaEquipamentosEletronicos);
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_id_nenhum_equipamento_deve_ser_encontrado()
        {

            const string ID_ESPERADO = "5";
            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2); 
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2); 
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2); 
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2); 

            EquipamentoEletronico equipamentoEletronico =_service.BuscarPorId(ID_ESPERADO);

            Assert.Null(equipamentoEletronico);
        }

        [Fact]
        public void Listar_equipamentos_eletronicos_deve_retornar_todos_equipamentos_eletronicos_e_todos_devem_ser_validos()
        {

            const string nomeEquipamento = "Alienware";
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            _cadastrarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar();

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos,
                    equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid),
                    equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid),
                    equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid),
                    equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid)
                );
        }

        [Fact]
        public void Listar_equipamentos_eletronicos_nenhum_equipamento_deve_ser_encontrado()
        {
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Buscar();

            Assert.Empty(listaEquipamentosEletronicos);
        }

    }
}
