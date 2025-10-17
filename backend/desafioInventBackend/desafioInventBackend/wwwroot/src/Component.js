sap.ui.define([
    "sap/ui/core/UIComponent",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/resource/ResourceModel",
    "sap/ui/Device"
], (UIComponent, JSONModel, ResourceModel, Device) => {
    "use strict";

    return UIComponent.extend("invent.desafio", {
        metadata: {
            manifest: "json"
        },

        init() {
            UIComponent.prototype.init.apply(this, arguments);

			const i18nModel = new ResourceModel({
                bundleName: "invent.desafio.i18n.i18n"
            });
            this.setModel(i18nModel, "i18n");
        },

        getContentDensityClass() {
			return Device.support.touch ? "sapUiSizeCozy" : "sapUiSizeCompact";
		}
    });
});
