sap.ui.define(["sap/ui/core/mvc/Controller"], (Controller) => {
	"use strict";

	return Controller.extend("invent.desafio.equipamentoseletronicos.controller.App", {
		onInit() {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		}
	});
});
