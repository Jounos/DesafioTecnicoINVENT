sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"./client/ApiResponse",
	"sap/ui/model/json/JSONModel",
	"./control/DialogoDeErro"
], function(
	Controller,
	ApiResponse,
	JSONModel,
	DialogoDeErro
) {

	const NAMESPACE_CONTROLLER = "desafio.common.BaseController";
	const QUERY = "?query";
	return Controller.extend(NAMESPACE_CONTROLLER, {

		query: QUERY,

		apiResponse: function () {
			if (this._apiResponse === null ||  this._apiResponse === undefined) {
				this._apiResponse = new ApiResponse(this.resourceBundle());
			}

			return this._apiResponse;
		},

		resourceBundle: function () {
			if (this._resourceBundle === null || this._resourceBundle === undefined) {
				this._resourceBundle = this.getResourceBundle();
			}

			return this._resourceBundle;
		},

		getResourceBundle: function() {
			const nome = 'i18n';
			return this.getOwnerComponent().getModel(nome).getResourceBundle();
		},

		criarModelo: function(nameModel, objectModel) {
			this.getView().setModel(new JSONModel(objectModel), nameModel);
		},

		obterModelo: function(nameModel) {
			return this.getView().getModel(nameModel)
		},

		obterValorModelo: function(nameModel) {
			const data =  this.obterModelo(nameModel).getData();
			return Object.assign({}, data);
		},

		_criaDataUI5: function (dia, mes, ano) {
			let date = UI5Date.getInstance();
			date.setUTCDate(dia);
			date.setUTCMonth(mes - 1);
			date.setUTCFullYear(ano);
			return date;
		},

		vincularRota: function (routeName, func) {
			const router = this._getRouter();

			if (routeName) {
				router.getRoute(routeName).attachPatternMatched(func, this);
			} else {
				router.attachPatternMatched(func, this);
			}
		},

		navegarPara(rota, param = null) {
			const oRouter = this._getRouter();

			if (param) {

				for (let key in param) {
					if (!param[key]) {
						delete param[key];
					}
				}

				oRouter.navTo(rota, { query: param });
			} else {
				oRouter.navTo(rota);
			}
		},

		_getRouter: function () {
			return this.getOwnerComponent().getRouter();
		},

		/**
		 * @param {boolean} estado
		 */
		_setarCarregamentoDaToolPage: function(estado) {
			const aggregation = "mainContents";
			const conteudo = 0;

			const idAppView = "app";
			let toolPageBase = this
				.getOwnerComponent()
				.byId(idAppView);

			this.toolPage = (toolPageBase.getAggregation(aggregation) || [])[conteudo] || toolPageBase;

			this._setarCarregamento(estado, this.toolPage);
		},

		/**
		 * @param {boolean} estado
		 * @param {Object} busyControl
		 */
		_setarCarregamento: function(estado, busyControl) {
			if (busyControl) {
				const tempoMinimoDeDelay = 0;
				busyControl.setBusyIndicatorDelay(tempoMinimoDeDelay);
				busyControl.setBusy(estado);
			}
		},

		/**
		 * @param {boolean} estado
		 * @param {Object} [busyControl]
		 */
		_carregamentoDaToolPageOuControle: function(estado, busyControl) {
			if (busyControl) {
				this._setarCarregamento(estado, busyControl);
			} else {
				this._setarCarregamentoDaToolPage(estado);
			}
		},

		_executarEObterPromiseDaAction: function(action, busyControl) {
			let prom = null;
			try {
				this._carregamentoDaToolPageOuControle(true, busyControl);
				let result = action();
				const nomeDoMetodoThen = "then";
				const nomeDoTipo = "function";

				if (result === null || result === undefined) {
					prom = Promise.resolve();
				} else if (typeof(result[nomeDoMetodoThen]) !== nomeDoTipo) {
					prom = Promise.resolve(result);
				} else {
					prom = result;
				}

			} catch (e) {
				prom = Promise.reject(e);
			}

			return prom;
		},

		/**
		 * @param {function} action
		 * @param {Object} [busyControl]
		 */
		exibirEspera: function(action, busyControl) {
			let prom = this._executarEObterPromiseDaAction(action, busyControl);
			setTimeout(() => {
				prom.catch((x) => {
						this.apiResponse().obterErro(x)
										.then(erro => this._criarDialogDeErro(erro))
					}
					).finally(() => this._carregamentoDaToolPageOuControle(false, busyControl));
			}, 750);
		},

		_criarDialogDeErro: function (erro) {
			const falhaDeComunicacao = 'failed to fetch';
			const traducaoDeFalhaDeComunicacao = 'Common.FalhaAoRequisitarServidor';
			let mensagemMinusculo = erro.mensagem.toLowerCase();
			const idDialogoDeErro = 'apiErrorDialog';

			var dialogo = new DialogoDeErro(idDialogoDeErro, {
				title: erro.titulo,
				cabecalho: erro.textoCabecalho,
				mensagem: mensagemMinusculo === falhaDeComunicacao ? this.getTextOrName(traducaoDeFalhaDeComunicacao) : erro.mensagem,
				stack: erro.stack
			});

			this._setarI18nNoControle(dialogo);
			return dialogo.open();
		},

		_setarI18nNoControle: function (dialog) {
			const nomeModeloI18n = 'i18n';
			var modelo = this.getOwnerComponent().getModel(nomeModeloI18n);
			dialog.setModel(modelo, nomeModeloI18n);
		},

		getTextOrName: function (i18nNameOrMessage, arrayDeParametros = undefined) {
			return this.resourceBundle().hasText(i18nNameOrMessage)
						? this.resourceBundle().getText(i18nNameOrMessage, arrayDeParametros)
						: i18nNameOrMessage;
		}
	});
});
