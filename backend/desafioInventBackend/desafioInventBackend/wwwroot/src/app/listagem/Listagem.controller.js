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
				oModelFiltros = new JSONModel(),
				oModelCollections = new JSONModel();

			dateFrom.setUTCDate(1);
			dateFrom.setUTCMonth(9);
			dateFrom.setUTCFullYear(2025);

			dateTo.setUTCDate(31);
			dateTo.setUTCMonth(9);
			dateTo.setUTCFullYear(2025);

			oModelFiltros.setData({
				nome: null,
				tipoEquipamento: 1,
				dataInicio: dateFrom,
				dataFim: dateTo,
				equipamentoEmEstoqueEnum: 1,
			});

			oModelCollections.setData({
				tipoEquipamentoCollection: [
					{ label: 'PC', id: 1 },
					{ label: 'Notebook', id: 2 },
					{ label: 'Mouse', id: 3 },
					{ label: 'Teclado', id: 4 },
					{ label: 'Celular', id: 5 }
				],
				estoqueCollection: [
					{ label: 'TODOS', id: 1 },
					{ label: 'Há Estoque', id: 2 },
					{ label: 'Não Há Estoque', id: 3 },
				]
			});

			this.getView().setModel(oModelFiltros, "filtros");
			this.getView().setModel(oModelCollections, "collections");

			this._iEvent = 0;
		},

		aoClicarBotaoCadastrar: function (event) {
			const oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("cadastro");
		},

		aoClicarBotaoPesquisar: function (event) {

			const filtrosModel = this.getView().getModel("filtros").getData();
			let filtros = Object.assign({}, filtrosModel);

			filtros.dataInicio = this._formatarData(filtros.dataInicio);
			filtros.dataFim = this._formatarData(filtros.dataFim);

			ServiceEquipamentoEletronico.buscarTodos(filtros).then(result => console.log(result.json()));
		},

		_formatarData: function (data) {
			const hora = 23, minuto = 59, segundo = 59;
			return new Date(Date.UTC(
				data.getFullYear(),
				data.getMonth(),
				data.getDate(),
				hora, minuto, segundo
			)).toISOString();
		}
    });
});
