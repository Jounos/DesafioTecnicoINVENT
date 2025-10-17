sap.ui.define([
	"sap/ui/core/UIComponent",
	"sap/ui/model/json/JSONModel",
	"sap/ui/Device",
	"sap/ui/model/resource/ResourceModel",
], (UIComponent, JSONModel, Device, ResourceModel) => {
	"use strict";

	return UIComponent.extend("desafio.Component", {
		metadata: {
			manifest: "json"
		},

		init() {

			const nomeModelo = "device";
			this.setModel(this._createDeviceModel(), nomeModelo);

			const i18nModel = new ResourceModel({
                bundleName: "desafio.i18n.i18n"
            });
            this.setModel(i18nModel, "i18n");

			UIComponent.prototype.init.apply(this, arguments);
		},

		getContentDensityClass() {
			if (this._sContentDensityClass === undefined) {
				if (
					jQuery(document.body).hasClass("sapUiSizeCozy") ||
					jQuery(document.body).hasClass("sapUiSizeCompact")
				) {
					this._sContentDensityClass = "";
				} else if (!Device.support.touch) {
					this._sContentDensityClass = "sapUiSizeCompact";
				} else {
					this._sContentDensityClass = "sapUiSizeCozy";
				}
			}
			return this._sContentDensityClass;
		},

		_createDeviceModel: function () {
			var oModel = new JSONModel(Device);
			oModel.setDefaultBindingMode("OneWay");
			return oModel;
		},


		destroy: function () {
			// call the base component's destroy function
			UIComponent.prototype.destroy.apply(this, arguments);
		},
	});
});
