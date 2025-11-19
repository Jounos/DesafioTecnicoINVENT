sap.ui.define([
	"sap/ui/test/opaQunit",
	"./CadastrarEquipamentoEletronico",
	"../listar/ListarEquipamentoEletronico"
], function(opaTest, CadastrarEquipamentoEletronico, ListarEquipamentosEletronicos) {
	"use strict";

	QUnit.module("CadastrarEquipamentosEletronicos");

	opaTest("Deve exibir a tela de criação de equipamentos eletrônicos", function (Given, When, Then) {
		Given.iStartMyApp({
			hash: "#/cadastro/0/"
		});

		Then.naPaginaCriacaoDeEquipamentoEletronico
				.aTelaDeCadastroFoiCarregadaCorretamente()
				.oBotaoSalvarFoiCarregadoCorretamente()
				.oBotaoCancelarFoiCorregadoCorretamente();
	});

	opaTest("Deve clicar no botão cancelar", function (Given, When, Then){

		When.naPaginaCriacaoDeEquipamentoEletronico.clicoNoBotaoCancelar();
		Then.naPaginaListagemEquipamentosEletronicos.aTelaDeListagemFoiCarregadaCorretamente();

		Then.iTeardownMyApp();
	});

	opaTest("Deve salvar um equipamento eletrônicao", function (Given, When, Then){
		const nomeEquipamentoEletronico = "Macbook Pro Apple M4";
		const selectSelecionado = "1";
		const quantidade = 10;

		Given.iStartMyApp({ hash: '#/cadastro/0/' });

		When.naPaginaCriacaoDeEquipamentoEletronico
			.inserirValorNoCampoNome(nomeEquipamentoEletronico)
			.clicoNoSelectTipoEquipamento(selectSelecionado)
			.ClicoEmNotebookNoSelect()
			.inserirValorNoCampoQuantidade(quantidade);
		When.naPaginaCriacaoDeEquipamentoEletronico.clicoNoBotaoSalvar();

		Then.naPaginaCriacaoDeEquipamentoEletronico.deveExibirModalComMensagemSalvoComSucesso();

		When.naPaginaCriacaoDeEquipamentoEletronico.clicoNoBotaoOKNoDialogComMensagemDeSucesso();

		Then.iTeardownMyApp();
	});

	opaTest("Deve tentar salvar um equipamento eletronico com as informações incompletas", function (Given, When, Then){
		const nomeEquipamentoEletronico = "Macbook Pro Apple M4";
		Given.iStartMyApp({ hash: '#/cadastro/0/' });

		When.naPaginaCriacaoDeEquipamentoEletronico.inserirValorNoCampoNome(nomeEquipamentoEletronico);
		When.naPaginaCriacaoDeEquipamentoEletronico.clicoNoBotaoSalvar();

		Then.naPaginaCriacaoDeEquipamentoEletronico.deveExibirModalComMensagemParaPreencherTodosOsCampos();

		Then.iTeardownMyApp();
	});

})
