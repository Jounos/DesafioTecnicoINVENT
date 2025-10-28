sap.ui.define([
    "desafio/common/BaseController",
], function (BaseController) {
    "use strict";

    return BaseController.extend("desafio.app.App", {
		onInit() {
			this.getView().addStyleClass(this.getOwnerComponent().getContentDensityClass());
		}
    });
});
