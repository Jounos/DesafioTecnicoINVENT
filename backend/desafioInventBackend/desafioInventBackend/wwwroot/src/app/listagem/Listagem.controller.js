sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
	"sap/ui/core/library",
	"sap/ui/core/date/UI5Date",
	"desafio/app/servicos/ServiceEquipamentoEletronico"
], (Controller,
	JSONModel,
	CoreLibrary,
	UI5Date,
	ServiceEquipamentoEletronico) => {
    "use strict";

	var ValueState = CoreLibrary.ValueState;

    return Controller.extend("desafio.app.listagem.Listagem", {

		onInit() {
			var oDRS2 = this.byId("DRS1"),
				dateFrom = UI5Date.getInstance(),
				dateTo = UI5Date.getInstance(),
				oModel = new JSONModel();

			dateFrom.setUTCDate(2);
			dateFrom.setUTCMonth(1);
			dateFrom.setUTCFullYear(2014);

			dateTo.setUTCDate(17);
			dateTo.setUTCMonth(1);
			dateTo.setUTCFullYear(2014);

			oModel.setData({
				nome: '',
				tipoEquipamentoSelected: 1,
				tipoEquipamentoCollection: [
					{ label: 'PC', id: 1 },
					{ label: 'Notebook', id: 2 },
					{ label: 'Mouse', id: 3 },
					{ label: 'Teclado', id: 4 },
					{ label: 'Celular', id: 5 }
				],
				start: dateFrom,
				end: dateTo,
				estoqueSelected: 1,
				estoqueCollection: [
					{ label: 'TODOS', id: 1 },
					{ label: 'Há Estoque', id: 2 },
					{ label: 'Não Há Estoque', id: 3 },
				]
			});
			this.getView().setModel(oModel);

			this._iEvent = 0;
		},

		aoClicarBotaoCadastrar: function (event) {
			const oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("cadastro");
		},

		aoClicarBotaoPesquisar: function (event) {
			ServiceEquipamentoEletronico.buscarTodos({
				tipoEquipamento: 1
			}).then(result => console.log(result));
		},

    });
});
