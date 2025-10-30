sap.ui.define([
    "desafio/common/BaseController",
	"desafio/app/servicos/ServiceEquipamentoEletronico",
	"desafio/app/formatters/Formatter",
	'sap/ui/core/BusyIndicator'
], function (BaseController,
	ServiceEquipamentoEletronico,
	Formatter) {
    "use strict";

	const NAME_MODEL_FILTROS = "filtros";
	const NOME_MODELO_SELECTS = "selects";

    return BaseController.extend("desafio.app.listagem.Listagem", {

		formatter: Formatter,

		onInit() {
			const rotaListagem = "listagem";
			this.vincularRota(rotaListagem, this._obterParametros);
		},

		_obterParametros(event) {
			const parametro = "arguments";

			let querys = event.getParameter(parametro)[this.query];

			this._criarModelos(querys);
			if (querys) {
				this._pesquisar();
			}
		},

		_criarModelos: function (querys) {
			this._criarModeloSelects();
			this._criarModeloFiltros(querys);
		},

		_criarModeloSelects: function() {
			const modelSelects = this._criarSeletcs();
			this.criarModelo(NOME_MODELO_SELECTS, modelSelects);
		},

		_criarModeloFiltros: function(querys = null) {
			const modelFiltros = this._criarFiltros(querys);
			this.criarModelo(NAME_MODEL_FILTROS, modelFiltros);
		},

		_criarSeletcs: function () {
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
					{ label: 'TODOS', id: 0 },
					{ label: 'Há Estoque', id: 1 },
					{ label: 'Não Há Estoque', id: 2 },
				]
			};
		},

		_criarFiltros: function (querys = null) {
			if (querys) {
				let dataInicioConvertidaParaFiltro = querys.dataInicio ?  new Date(querys.dataInicio) : null;
				let dataFimConvertidaParaFiltro = querys.dataFim ? new Date(querys.dataFim) : null;

				return {
					nome: querys.nome,
					tipoEquipamento: querys.tipoEquipamento,
					dataInicio: dataInicioConvertidaParaFiltro,
					dataFim: dataFimConvertidaParaFiltro,
					equipamentoEmEstoque: querys.equipamentoEmEstoque,
				};
			} else {
				return {
					nome: null,
					tipoEquipamento: 0,
					dataInicio: null,
					dataFim: null,
					equipamentoEmEstoque: 0,
				};
			}
		},

		aoClicarBotaoCadastrar: function () {
			const rotaCadastro = "cadastro";
			const filtros = this._obterFiltrosFormatados();

			this.navegarPara(rotaCadastro, filtros);
		},

		aoClicarBotaoPesquisar: function () {
			this._pesquisar();
		},

		_pesquisar() {
			this.showBusyIndicator();
			setTimeout(() => {
				const filtros = this._obterFiltrosFormatados();
				const lista_equipamentos_eletronicos = "listaEquipamentosEletronicos";
				ServiceEquipamentoEletronico.buscarTodos(filtros).then(result => {
					this.criarModelo(lista_equipamentos_eletronicos, result)
				}).finally(() => this.hideBusyIndicator());
			}, 1000);
		},

		_obterFiltrosFormatados() {
			let filtros = this.obterValorModelo(NAME_MODEL_FILTROS);

			if (filtros.dataInicio != null) {
				filtros.dataInicio = this.formatter.formatarDataParaAPI(filtros.dataInicio);
			}
			if (filtros.dataInicio != null) {
				filtros.dataFim = this.formatter.formatarDataParaAPI(filtros.dataFim);
			}

			return filtros;
		}
    });
});
