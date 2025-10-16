sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator"
], (Controller) => {
    "use strict";

    return Controller.extend("invent.desafio.equipamentoseletronicos.controller.listagem", {
		onInit() {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		}
    });
});
