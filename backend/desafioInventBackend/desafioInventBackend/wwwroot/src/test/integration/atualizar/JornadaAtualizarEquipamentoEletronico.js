sap.ui.define([
	"sap/ui/test/opaUnit",
	"./Atualiz,arEquipamentoEletronico",
	"./listar/ListarEquipamentoEletronico"
], function (
	opaTest,
	AtualizarEquipamentoEletronico,
	ListarEquipamentoEletronico) {
	"use strict";

	QUnit.module("AtualizarEquipamentoEletronico");

	opaTest("Deve carregar a tela de edição corretamente", function (Given, When, Then) {
		Given.iStartMyApp();

		Then.naPaginaListagemEquipamentosEletronicos.aTelaDeListagemFoiCarregadaCorretamente();
		Then.naPaginaListagemEquipamentosEletronicos.buscouTodosOsEquipamentosEletronicos();

	});
});
