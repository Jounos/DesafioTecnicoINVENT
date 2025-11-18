sap.ui.define([
    "desafio/common/BaseController",
	"desafio/app/servicos/ServiceEquipamentoEletronico",
	"desafio/app/formatters/Formatter",
	'sap/ui/core/BusyIndicator'
], function (BaseController,
	ServiceEquipamentoEletronico,
	Formatter) {
    "use strict";

	const NOME_CONTROLLER = "desafio.app.listagem.Listagem";
	const NOME_MODELO_LISTA = "listaEquipamentosEletronicos";
	const NAME_MODELO_FILTROS = "filtros";
	const NOME_MODELO_SELECTS = "selects";

    return BaseController.extend(NOME_CONTROLLER, {

		formatter: Formatter,

		onInit: function () {
			const rotaListagem = "listagem";
			this.vincularRota(rotaListagem, this._obterParametros);
		},

		_obterParametros: function (event) {
			const nomeParametro = "arguments";
			let querys = event.getParameter(nomeParametro)[this.query];

			this._criarModelos(querys);
			if (querys) {
				this._pesquisar();
			}
		},

		_criarModelos: function (querys) {
			this._criarModeloSelects();
			this._criarModeloFiltros(querys);
		},

		_criarModeloSelects: function () {
			const modelSelects = this._criarSeletcs();
			this.criarModelo(NOME_MODELO_SELECTS, modelSelects);
		},

		_criarModeloFiltros: function(querys = null) {
			const modelFiltros = this._criarFiltros(querys);
			this.criarModelo(NAME_MODELO_FILTROS, modelFiltros);
		},

		_criarSeletcs: function () {
			const labelTodos = 'TODOS';
			const labelPC = 'PC';
			const labelNotebook = 'Notebook';
			const labelMouse = 'Mouse';
			const labelTeclado  = 'Teclado';
			const labelCelular = 'Celular';

			const labelTODOS = 'TODOS';
			const labelHaEstoque = 'Há Estoque';
			const labelNaoHaEstoque = 'Não Há Estoque';

			return {
				tipoEquipamentoCollection: [
					{ label: labelTodos },
					{ label: labelPC, id: 1 },
					{ label: labelNotebook, id: 2 },
					{ label: labelMouse, id: 3 },
					{ label: labelTeclado, id: 4 },
					{ label: labelCelular, id: 5 }
				],
				estoqueCollection: [
					{ label: labelTODOS },
					{ label: labelHaEstoque, id: 1 },
					{ label: labelNaoHaEstoque, id: 2 },
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

		aoClicarBotaoEditar: function (evento) {
			const rotaEdicao = 'edicao';
			const propriedade = "id";
			let id = evento.getSource().getBindingContext(NOME_MODELO_LISTA).getProperty(propriedade);

			this.navegarPara(rotaEdicao, { id: id });
		},

		aoClicarBotaoCadastrar: function () {
			const rotaCadastro = "cadastro";
			const filtros = this._obterFiltrosFormatados();

			this.navegarPara(rotaCadastro, filtros);
		},

		aoClicarBotaoPesquisar: function () {
			this.exibirEspera(() => this._pesquisar());
		},

		_pesquisar() {
			const filtrosFormatados = this._obterFiltrosFormatados();
			return ServiceEquipamentoEletronico.buscarTodos(filtrosFormatados)
				.then(result => this.criarModelo(NOME_MODELO_LISTA, result));
		},

		_obterFiltrosFormatados: function() {
			let filtros = this.obterValorModelo(NAME_MODELO_FILTROS);

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
