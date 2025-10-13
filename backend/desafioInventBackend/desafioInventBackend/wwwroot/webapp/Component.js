sap.ui.define([
    "sap/ui/core/UIComponent",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/resource/ResourceModel",
    "sap/ui/Device"
], (UIComponent, JSONModel, ResourceModel, Device) => {
    "use strict";

    return UIComponent.extend("ui.desafio-tecnico.Component", {
        metadata: {
            interfaces: ["sap.ui.core.IAsyncContentCreation"],
            manifest: "json"
        },        

        init() {
            UIComponent.prototype.init.apply(this, arguments);

            const i18nModel = new ResourceModel({
                bundleName: "ui5.desafio-tecnico.i18n.i18n"
            });
            this.setModel(i18nModel, "i18n");

            const oDeviceModel = new JSONModel(Device);
			oDeviceModel.setDefaultBindingMode("OneWay");
			this.setModel(oDeviceModel, "device");

            this.getRouter().initialize();
        },

        getContentDensityClass() {
			return Device.support.touch ? "sapUiSizeCozy" : "sapUiSizeCompact";
		}
    }); 
});