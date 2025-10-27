sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
	"sap/ui/core/library",
	"sap/ui/core/date/UI5Date"
], (Controller, JSONModel, CoreLibrary, UI5Date) => {
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

		handleChange: function (oEvent) {
			var sFrom = oEvent.getParameter("from"),
				sTo = oEvent.getParameter("to"),
				bValid = oEvent.getParameter("valid"),
				oEventSource = oEvent.getSource(),
				oText = this.byId("TextEvent");

			this._iEvent++;

			oText.setText("Id: " + oEventSource.getId() + "\nFrom: " + sFrom + "\nTo: " + sTo);

			if (bValid) {
				oEventSource.setValueState(ValueState.None);
			} else {
				oEventSource.setValueState(ValueState.Error);
			}
			console.log(sFrom);
			console.log(sTo);
		},

		aoClicarBotaoCadastrar: function (event) {
			const oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("cadastro");
		},

		aoClicarBotaoPesquisar: function (event) {
			console.log("Clicou Pesquisar");
		},

    });
});
