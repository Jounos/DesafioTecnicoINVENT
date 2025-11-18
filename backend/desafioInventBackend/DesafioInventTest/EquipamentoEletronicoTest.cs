using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Enum;
using DesafioInventBackend.Model.Filters;
using DesafioInventBackend.Service;
using DesafioInventTest.Configuracao;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents.Session;

namespace DesafioInventTest
{
    public class EquipamentoEletronicoTest : BaseParaTesteUnitario
    {
        public EquipamentoEletronicoService _service;

        public EquipamentoEletronicoTest(ITestContextAccessor contextAccessor) : base(contextAccessor) { }

        [Fact]
        public void Ao_cadastrar_salva_item_no_banco()
        {

            // Arrange - ao cadastrar deve salvar item no banco [cria um objeto especifico, e salva no banco] - [act - cadastra o item no banco] - [assert - verifica se o item foi realmente salvo no banco, e item que existe no banco e igual ao que você salvou]
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            // Act
            service.Cadastrar(equipamentoEletronico);

            // Assert
            EquipamentoEletronico equipamentoEletronicoBanco = session.Load<EquipamentoEletronico>(equipamentoEletronico.Id);
            Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco);
        }

        [Fact]
        public void Ao_cadastrar_lanca_uma_excecao_ValidationException_por_que_nao_tem_informacoes_minimas_exigidas()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();

            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 0);

            // Act - Assert
            var ex = Assert.Throws<ValidationException>(() => service.Cadastrar(equipamentoEletronico));
            Assert.Equal("Validation failed: \r\n -- QuantidadeEstoque: 'Quantidade Estoque' deve ser superior ou igual a '1'. Severity: Error", ex.Message);
        }

        [Fact]
        public void Ao_editar_deve_retornar_um_equipamento_eletronico_valido()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();

            string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            string nomeEquipamentoAlterado = "Positivo";
            equipamentoEletronico.Nome = nomeEquipamentoAlterado;

            // Act
            service.Atualizar(equipamentoEletronico.Id, equipamentoEletronico);

            // Assert
            EquipamentoEletronico equipamentoEletronicoBanco = service.BuscarPorId(equipamentoEletronico.Id);
            Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco);
        }

        [Fact]
        public void Ao_editar_deve_lancar_uma_excessao_por_tentar_salvar_um_equipamento_sem_nome()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
          
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            equipamentoEletronico.Nome = string.Empty;

            // Act - Assert
            var ex = Assert.Throws<ValidationException>(() => service.Atualizar(equipamentoEletronico.Id, equipamentoEletronico));
            Assert.Equal("Validation failed: \r\n -- Nome: 'Nome' deve ser informado. Severity: Error\r\n -- Nome: 'Nome' deve ser maior ou igual a 2 caracteres. Você digitou 0 caracteres. Severity: Error", ex.Message);
        }

        [Fact]
        public void Ao_excluir_deve_remover_do_banco_de_dados()
        {
            //Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            equipamentoEletronico.QuantidadeEstoque = 0;
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            service.Excluir(equipamentoEletronico.Id);

            // Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => service.BuscarPorId(equipamentoEletronico.Id));
            Assert.Equal($"Não foi possível encontrar um equipamento eletrônico com id {equipamentoEletronico.Id}", ex.Message);
        }

        [Fact]
        public void Ao_excluir_retorna_uma_excessao_ValidationException_ao_tentar_excluir_produto_com_estoque()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act - Assert
            var ex = Assert.Throws<ValidationException>(() => service.Excluir(equipamentoEletronico.Id));
            Assert.Equal("Validation failed: \r\n -- QuantidadeEstoque: 'Quantidade Estoque' deve ser igual a '0'. Severity: Error", ex.Message);
        }

        [Fact]
        public void Ao_buscar_por_id_deve_retornar_um_equipamento_eletronico()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            EquipamentoEletronico equipamentoEletronicoBanco = service.BuscarPorId(equipamentoEletronico.Id);

            // Assert
            Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco);
        }

        [Fact]
        public void Ao_buscar_por_filtro_nome_deve_encontrar_um_equipamento()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            BuscaFiltros filtro = new BuscaFiltros { Nome = "Alienware" };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar(filtro);

            // Assert
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronicoBanco => Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco));
        }

        [Fact]
        public void Ao_buscar_por_filtro_tipo_equipamento_deve_encontrar_um_equipamento()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            BuscaFiltros filtro = new BuscaFiltros { TipoEquipamento = TipoEquipamentoEnum.PC };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar(filtro);

            // Assert
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronicoBanco => Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco));
        }

        [Fact]
        public void Ao_buscar_por_filtro_data_inclusao_deve_encontrar_um_equipamento()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            BuscaFiltros filtro = new BuscaFiltros { DataInicio = DateTimeOffset.Parse("2025-10-20") };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar(filtro);

            // Assert
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronicoBanco => Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco));
        }

        [Fact]
        public void Ao_buscar_por_filtro_data_fim_deve_encontrar_um_equipamento()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            BuscaFiltros filtro = new BuscaFiltros { DataFim = DateTimeOffset.Parse("2025-12-30") };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar(filtro);

            // Assert
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronicoBanco => Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco));
        }

        [Fact]
        public void Ao_buscar_por_filtro_estoque_deve_encontrar_um_equipamento()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            BuscaFiltros filtro = new BuscaFiltros { EquipamentoEmEstoque = EquipamentoEmEstoqueEnum.EM_ESTOQUE };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar(filtro);

            // Assert
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronicoBanco => Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco));
        }

        [Fact]
        public void Ao_buscar_por_filtro_estoque_vazio_nao_deve_encontrar_um_equipamento()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();
            
            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            BuscaFiltros filtro = new BuscaFiltros { EquipamentoEmEstoque = EquipamentoEmEstoqueEnum.NAO_TEM_ESTOQUE };
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar(filtro);

            // Assert
            Assert.Empty(listaEquipamentosEletronicos);
        }

        [Fact]
        public void Ao_listar_todos_e_todos_devem_ser_validos()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var session = escopo.ServiceProvider.GetRequiredService<IDocumentSession>();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();

            const string nomeEquipamento = "Alienware";
            EquipamentoEletronico equipamentoEletronico = _criarEquipamentoEletronico(nomeEquipamento, TipoEquipamentoEnum.PC, 2);
            session.Store(equipamentoEletronico);
            session.SaveChanges();

            // Act
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar();

            // Assert
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronicoBanco => Assert.Equivalent(equipamentoEletronico, equipamentoEletronicoBanco));
        }

        [Fact]
        public void Ao_listar_nenhum_equipamento_deve_ser_encontrado()
        {
            // Arrange
            using var escopo = CriarEscopo();
            var service = escopo.ServiceProvider.GetRequiredService<EquipamentoEletronicoService>();

            // Act
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = service.Buscar();

            // Assert
            Assert.Empty(listaEquipamentosEletronicos);
        }

        private EquipamentoEletronico _criarEquipamentoEletronico(string nome, TipoEquipamentoEnum tipoEquipamentoEnum, int quantidadeEstoque)
        {
            return new EquipamentoEletronico
            {
                Nome = nome,
                TipoEquipamento = tipoEquipamentoEnum,
                QuantidadeEstoque = quantidadeEstoque,
                DataInclusao = DateTimeOffset.Now,
            };
        }
    }
}
