sap.ui.define([
    "desafio/common/BaseController",
], function (BaseController) {
    "use strict";

	const NOME_CONTROLLER = "desafio.app.App";
    return BaseController.extend(NOME_CONTROLLER, {
		onInit() {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		}
    });
});
