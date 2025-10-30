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

		showBusyIndicator: function () {
			BusyIndicator.show(0);
		},

		hideBusyIndicator() {
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
		}
	});
});
