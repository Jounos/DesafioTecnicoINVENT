sap.ui.define([
    "desafio/common/BaseController",
	"desafio/app/servicos/ServiceEquipamentoEletronico"
], function (BaseController,
	ServiceEquipamentoEletronico) {
    "use strict";

	const NAME_MODEL_FILTROS = "filtros";

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

			const name_model_collections = "collections";
			this.createModel(name_model_collections, modelCollections);
			this.createModel(NAME_MODEL_FILTROS, modelFiltros);
		},

		aoClicarBotaoCadastrar: function () {
			const oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("cadastro");
		},

		aoClicarBotaoPesquisar: function () {

			let filtros = this.getValueModel(NAME_MODEL_FILTROS);
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
