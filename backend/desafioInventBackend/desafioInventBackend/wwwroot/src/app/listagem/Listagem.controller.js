sap.ui.define([
    "desafio/common/BaseController",
    "sap/ui/model/json/JSONModel",
	"sap/ui/core/library",
	"desafio/app/servicos/ServiceEquipamentoEletronico"
], function (BaseController,
	ServiceEquipamentoEletronico) {
    "use strict";

    return BaseController.extend("desafio.app.listagem.Listagem", {

		onInit() {
			const dateFrom = this._criaDataUI5(1, 10, 2025);
			const dateTo = this._criaDataUI5(31, 10, 2025);

			const modelFiltros = {
				nome: null,
				tipoEquipamento: 1,
				dataInicio: dateFrom,
				dataFim: dateTo,
				equipamentoEmEstoqueEnum: 1,
			};
			const modelCollections = {
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
			};

			this.createModel("filtros", modelFiltros);
			this.createModel("collections", modelCollections);
		},

		aoClicarBotaoCadastrar: function () {
			const oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("cadastro");
		},

		aoClicarBotaoPesquisar: async function () {

			let filtros = this._getValueModeloFiltros();
			filtros.dataInicio = this._formatarData(filtros.dataInicio);
			filtros.dataFim = this._formatarData(filtros.dataFim);

			await ServiceEquipamentoEletronico.buscarTodos(filtros).then(result => console.log(result.json()));
		},

		_getValueModeloFiltros() {
			const nome_modelo_filtros = "filtros";
			return this.getValueModel(nome_modelo_filtros);
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
