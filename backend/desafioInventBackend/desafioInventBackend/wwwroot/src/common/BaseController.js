sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/model/json/JSONModel",
	'sap/ui/core/BusyIndicator'
], function(Controller, JSONModel, BusyIndicator) {

	const NAMESPACE_CONTROLLER = "desafio.common.BaseController";
	const QUERY = "?query";
	return Controller.extend(NAMESPACE_CONTROLLER, {

		query: QUERY,

		criarModelo: function(nameModel, objectModel) {
			this.getView().setModel(new JSONModel(objectModel), nameModel);
		},

		_obterModelo: function(nameModel) {
			return this.getView().getModel(nameModel)
		},

		obterValorModelo: function(nameModel) {
			const data =  this._obterModelo(nameModel).getData();
			return Object.assign({}, data);
		},

		_criaDataUI5: function (dia, mes, ano) {
			let date = UI5Date.getInstance();
			date.setUTCDate(dia);
			date.setUTCMonth(mes - 1);
			date.setUTCFullYear(ano);
			return date;
		},

		mostrarBusyIndicator: function () {
			BusyIndicator.show(0);
		},

		esconderBusyIndicator() {
			BusyIndicator.hide();
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
				if (estado) {
					BusyIndicator.show(tempoMinimoDeDelay);
				} else {
					BusyIndicator.hide();
				}
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
					const inicioDoTexto = "Catch: ";
					console.log(inicioDoTexto, x.status);
					console.log(x.message);
				}).finally(() => this._carregamentoDaToolPageOuControle(false, busyControl));
			}, 750);
		},
	});
});
