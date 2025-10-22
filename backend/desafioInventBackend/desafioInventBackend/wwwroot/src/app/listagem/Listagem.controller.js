sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator"
], (Controller) => {
    "use strict";

    return Controller.extend("desafio.app.listagem.Listagem", {
		
		aoClicarBotaoCadastrar: function (event) {
			const oRouter = this.getOwnerComponent().getRouter();
			oRouter.navTo("cadastro");
		},

		aoClicarBotaoPesquisar: function (event) {
			console.log("Clicou Pesquisar");
		},

    });
});
