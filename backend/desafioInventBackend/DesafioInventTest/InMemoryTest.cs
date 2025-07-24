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
        public IRepositoryEquipamentoEletronico _repository;

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
        public void Listar_equipamentos_eletronicos_deve_retornar_todos_equipamentos_eletronicos_e_todos_devem_ser_validos()
        {

            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();
            Cadastrar_equipamento_eletronico_deve_retornar_um_equipamento_eletronico_valido();


            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.ListarTodos();

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
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.ListarTodos();

            Assert.Empty(listaEquipamentosEletronicos);
        }

    }
}
