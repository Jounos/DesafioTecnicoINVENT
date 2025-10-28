sap.ui.define([
	"sap/ui/core/mvc/Controller",
	"sap/ui/model/json/JSONModel",
	"sap/ui/core/date/UI5Date",
], function(Controller, JSONModel, UI5Date) {

	const NAMESPACE_CONTROLLER = "desafio.common.BaseController";

	return Controller.extend(NAMESPACE_CONTROLLER, {

		createModel: function(nameModel, objectModel) {
			this.getView().setModel(new JSONModel(objectModel), nameModel);
		},

		_getModel: function(nameModel) {
			return this.getView().getModel(nameModel)
		},

		getValueModel: function(nameModel) {
			const data =  this._getModel(nameModel).getData();
			return Object.assign({}, data);
		},

		_criaDataUI5: function (dia, mes, ano) {
			let date = UI5Date.getInstance();
			date.setUTCDate(dia);
			date.setUTCMonth(mes - 1);
			date.setUTCFullYear(ano);
			return date;
		},

	});
});
