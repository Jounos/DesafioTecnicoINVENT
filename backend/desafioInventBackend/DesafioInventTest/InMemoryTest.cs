using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using FluentValidation;

namespace DesafioInventTest
{
    public class InMemoryTest
    {

        public EquipamentoEletronicoService _service;
        public IRepositoryEquipamentoEletronico<EquipamentoEletronico> _repository;

        public InMemoryTest()
        {
            _repository = new InMemoryRepository();
            _service = new EquipamentoEletronicoService(_repository, new EquipamentoEletronicoValidator(), new EquipamentoEletronicoAlterarValidator(), new EquipamentoEletronicoDeleteValidator());
        }


        [Fact]
        public void Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido()
        {
            
            EquipamentoEletronico equipamentoEletronico = new EquipamentoEletronico
            {
                Nome = "Alienware",
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 2,
            };

            _service.Cadastrar(equipamentoEletronico);
            
            EquipamentoEletronicoValidator equipamentoEletronicoeValidator = new EquipamentoEletronicoValidator();
            EquipamentoEletronico equipamentoEletronicoCadastrado = _service.BuscarPorId("1");
            Assert.True(equipamentoEletronicoeValidator.Validate(equipamentoEletronicoCadastrado).IsValid);
        }

        [Fact]
        public void Cadastrar_equipamento_eletronico_deve_lancar_uma_excecao_ValidationException_ja_que_equipamento_eletronico_nao_tem_informacoes_minimas_exigidas()
        {

            EquipamentoEletronico equipamentoEletronico = new EquipamentoEletronico
            {
                Nome = "Positivo",
            };

            Assert.Throws<ValidationException>(() => _service.Cadastrar(equipamentoEletronico));
        }

        [Fact]
        public void Editar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido()
        {
            
            this.Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            const string ID_EQUIPAMENTO_ELETRONICO = "1";

            EquipamentoEletronico equipamentoEletronico = new EquipamentoEletronico
            {   
                Id = ID_EQUIPAMENTO_ELETRONICO,
                Nome = "Positivo",
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 2,
            };

            _service.Atualizar("1", equipamentoEletronico);

            EquipamentoEletronicoAlterarValidator equipamentoEletronicoeValidator = new EquipamentoEletronicoAlterarValidator();
            equipamentoEletronico = _service.BuscarPorId("1");
            Assert.True(equipamentoEletronicoeValidator.Validate(equipamentoEletronico).IsValid);
        }

        [Fact]
        public void Editar_equipamento_eletronico_deve_lancar_uma_excessao_por_tentar_salvar_um_equipamento_eletronico_sem_nome()
        {

            this.Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();

            EquipamentoEletronico equipamentoEletronico = new EquipamentoEletronico
            {
                Nome = string.Empty,
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 2,
            };

            Assert.Throws<ValidationException>(() => _service.Atualizar("1", equipamentoEletronico));
        }

        [Fact]
        public void Excluir_equipamento_eletronico_deve_excluir_um_equipamento_eletronico_e_nao_o_encontrar_apos_excluido()
        {
            const string ID_EQUIPAMENTO_ELETRONICO = "1";
            
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            EquipamentoEletronico equipamentoEletronico= new EquipamentoEletronico
            {
                Id = ID_EQUIPAMENTO_ELETRONICO,
                Nome = "Positivo",
                TipoEquipamento = TipoEquipamento.PC,
                QuantidadeEstoque = 0,
            };

            _service.Atualizar(ID_EQUIPAMENTO_ELETRONICO, equipamentoEletronico);

            _service.Excluir(ID_EQUIPAMENTO_ELETRONICO);

            Assert.Null(_service.BuscarPorId(ID_EQUIPAMENTO_ELETRONICO));
        }

        [Fact]
        public void Excluir_equipamento_eletronico_deve_retornar_uma_excessao_ValidationException_por_tentar_excluir_produto_com_estoque()
        {
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();

            const string ID_EQUIPAMENTO_ELETRONICO = "1";
            Assert.Throws<ValidationException>(() => _service.Excluir(ID_EQUIPAMENTO_ELETRONICO));
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_id_deve_retornar_um_equipamento_eletronico()
        {

            const string ID_ESPERADO = "3";
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();

            EquipamentoEletronico equipamentoEletronico = _service.BuscarPorId(ID_ESPERADO);

            Assert.Equal(ID_ESPERADO, equipamentoEletronico.Id);
        }

        [Fact]
        public void Buscar_equipamento_eletronico_por_id_nenhum_equipamento_deve_ser_encontrado()
        {

            const string ID_ESPERADO = "5";
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();

            EquipamentoEletronico equipamentoEletronico =_service.BuscarPorId(ID_ESPERADO);

            Assert.Null(equipamentoEletronico);
        }

        [Fact]
        public void Buscar_equipamentos_eletronicos_por_filtros_deve_retornar_um_unico_equipamento_eletronicos_que_deve_ser_valido()
        {

            _service.Cadastrar(new EquipamentoEletronico { Nome = "Alienware", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 10, });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Samsung Book3", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 4, });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Gamer", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 9, });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Teclado mecânico", TipoEquipamento = TipoEquipamento.Teclado, QuantidadeEstoque = 15, });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Mouse ", TipoEquipamento = TipoEquipamento.Mouse, QuantidadeEstoque = 4, });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Buxa", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 2, });


            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.BuscarPorFiltros("Ali", TipoEquipamento.Notebook);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamentos_eletronicos_por_filtro_tipo_equipamento_deve_retornar_multiplos_equipamentos_validos()
        {
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Alienware", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 10 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Samsung Book3", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 4 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Gamer", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 9 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Teclado mecânico", TipoEquipamento = TipoEquipamento.Teclado, QuantidadeEstoque = 15 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Mouse ", TipoEquipamento = TipoEquipamento.Mouse, QuantidadeEstoque = 4 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Positivo", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 2 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Apple", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 2 });

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.BuscarPorFiltros(null, TipoEquipamento.Notebook);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos, 
                equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid), 
                equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamentos_eletronicos_por_filtro_nome_equipamento_deve_retornar_multiplos_equipamentos_validos()
        {
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Alienware", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 10 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Samsung Book3", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 4 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Gamer", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 9 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Teclado mecânico", TipoEquipamento = TipoEquipamento.Teclado, QuantidadeEstoque = 15 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Mouse ", TipoEquipamento = TipoEquipamento.Mouse, QuantidadeEstoque = 4 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Buxa", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 2 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Teclado Gamer", TipoEquipamento = TipoEquipamento.Teclado, QuantidadeEstoque = 2 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Mouse Gamer", TipoEquipamento = TipoEquipamento.Teclado, QuantidadeEstoque = 2 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Monitor Gamer", TipoEquipamento = TipoEquipamento.Monitor, QuantidadeEstoque = 2 });

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.BuscarPorFiltros("Gamer", null);

            EquipamentoEletronicoValidator equipamentoEletronicoValidator = new EquipamentoEletronicoValidator();
            Assert.Collection(listaEquipamentosEletronicos,
                equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid),
                equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid),
                equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid),
                equipamentoEletronico => Assert.True(equipamentoEletronicoValidator.Validate(equipamentoEletronico).IsValid));
        }

        [Fact]
        public void Buscar_equipamentos_eletronicos_sem_filtros_deve_retornar_todos_os_esquipamentos_eletronicos_validos()
        {
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Alienware", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 10 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Samsung Book3", TipoEquipamento = TipoEquipamento.Notebook, QuantidadeEstoque = 4 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Gamer", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 9 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Teclado mecânico", TipoEquipamento = TipoEquipamento.Teclado, QuantidadeEstoque = 15 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "Mouse ", TipoEquipamento = TipoEquipamento.Mouse, QuantidadeEstoque = 4 });
            _service.Cadastrar(new EquipamentoEletronico { Nome = "PC Buxa", TipoEquipamento = TipoEquipamento.PC, QuantidadeEstoque = 2 });

            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.BuscarPorFiltros(null, 0);

            Assert.Empty(listaEquipamentosEletronicos);
        }

        [Fact]
        public void Buscar_equipamentos_eletronicos_por_filtros_nenhum_equipamento_deve_ser_encontrado()
        {
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.BuscarPorFiltros("Alienware", TipoEquipamento.Notebook);

            Assert.Empty(listaEquipamentosEletronicos);
        } 
    }
}
