sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/model/odata/v2/ODataModel"
], function(Opa5, ODataModel) {
	"use strict";

	return Opa5.extend("desafio.test.integration.arrangements.Startup", {

		iStartMyApp: function (oOptionsParameter = {}) {
			var oOptions = oOptionsParameter || {};
			this._clearSharedData();

			oOptions.delay = oOptions.delay || 1;

			return this.iStartMyAppInAFrame(oOptions.hash);
		},

		_clearSharedData: function() {
			ODataModel.mSharedData = {
				server: {},
				service: {},
				meta: {}
			};
		}
	});
});
