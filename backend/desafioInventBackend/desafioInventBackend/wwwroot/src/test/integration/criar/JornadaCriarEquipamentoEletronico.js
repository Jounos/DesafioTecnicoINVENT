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

		Then.naPaginaCriacaoDeEquipamentoEletronico.aTelaDeCadastroFoiCarregadaCorretamente();

		Then.iTeardownMyApp();
	});
})
