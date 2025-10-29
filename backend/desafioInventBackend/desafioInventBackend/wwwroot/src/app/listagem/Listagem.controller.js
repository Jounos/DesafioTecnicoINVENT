sap.ui.define([
    "desafio/common/BaseController",
	"desafio/app/servicos/ServiceEquipamentoEletronico",
	"desafio/app/formatters/Formatter"
], function (BaseController,
	ServiceEquipamentoEletronico,
	Formatter) {
    "use strict";

	const NAME_MODEL_FILTROS = "filtros";
	const NOME_MODELO_COLLECTIONS = "collections";

    return BaseController.extend("desafio.app.listagem.Listagem", {

		formatter: Formatter,

		onInit() {
			const modelCollections = this._criarModelCollections();
			const modelFiltros = this._criarModeloFiltros();
			this.createModel(NOME_MODELO_COLLECTIONS, modelCollections);
			this.createModel(NAME_MODEL_FILTROS, modelFiltros);
		},

		_criarModelCollections: function () {
			return {
				tipoEquipamentoCollection: [
					{ label: 'TODOS', id: 0 },
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
		},

		_criarModeloFiltros: function () {
			return {
				nome: null,
				tipoEquipamento: 0,
				dataInicio: null,
				dataFim: null,
				equipamentoEmEstoque: 1,
			};
		},

		aoClicarBotaoCadastrar: function () {
			const oRouter = this.getOwnerComponent().getRouter();
			const rotaCadastro = "cadastro";
			oRouter.navTo(rotaCadastro);
		},

		aoClicarBotaoPesquisar: function () {

			let filtros = this.getValueModel(NAME_MODEL_FILTROS);

			if (filtros.dataInicio != null) {
				filtros.dataInicio = this.formatter.formatarDataParaAPI(filtros.dataInicio);
			}
			if (filtros.dataInicio != null) {
				filtros.dataFim = this.formatter.formatarDataParaAPI(filtros.dataFim);
			}

			const lista_equipamentos_eletronicos = "listaEquipamentosEletronicos";
			ServiceEquipamentoEletronico.buscarTodos(filtros).then(result => this.createModel(lista_equipamentos_eletronicos, result));
		},
    });
});
