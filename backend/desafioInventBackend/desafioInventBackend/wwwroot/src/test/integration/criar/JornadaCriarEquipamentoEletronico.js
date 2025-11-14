sap.ui.define([
	"sap/ui/test/opaQunit",
	"./CadastrarEquipamentoEletronico"
], function(opaTest) {
	"use strict";

	QUnit.module("CadastrarEquipamentosEletronicos");

	opaTest("Deve exibir a tela de criação de equipamentos eletrônicos", function (Given, When, Then) {
		Given.iStartMyApp({
			hash: '/index.html#/cadastro'
		});

		Then.naPaginaCriacaoDeEquipamentoEletronico
				.aTelaDeCadastroFoiCarregadaCorretamente()
				.oBotaoSalvarFoiCarregadoCorretamente()
				.oBotaoCancelarFoiCorregadoCorretamente();

		Then.iTeardownMyApp();
	});

	opaTest("Deve clicar no botão voltar", function (Given, When, Then){
		When.naPaginaCriacaoDeEquipamentoEletronico.clicouNoBotaoCancelar();

		// Criar teste para validar que a tela de listagem foi renderizada.
	});

	// opaTest("Deve tentar salvar um equipamento eletronico com as informações incompletas", function (Given, Whenm, Then){ });
	// opaTest("Deve salvar um equipamento eletrônicao", function (Given, Whenm, Then){ });
})
