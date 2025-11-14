sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/model/odata/v2/ODataModel"
], function(Opa5, ODataModel) {
	"use strict";

	let equipamentos = [
		{ id: "Equipamento-1-A", nome: "TesteA", tipo: 1, quantidadeEmEstoque: 10, dataDeInclusao: "2025-08-25T13:39:38.1443059Z", temEmEstoque: true },
		{ id: "Equipamento-2-A", nome: "TesteB", tipo: 4, quantidadeEmEstoque: 0, dataDeInclusao: "2025-08-25T13:39:38.1443059Z", temEmEstoque: false }
    ];

	return Opa5.extend("desafio.test.integration.arrangements.Startup", {

		iStartMyApp: function (oOptionsParameter = {}) {
			var oOptions = oOptionsParameter || {};
			this._clearSharedData();

			oOptions.delay = oOptions.delay || 1;

			window.fetch = mockFetch;
			return this.iStartMyAppInAFrame(oOptions.hash);
		},

		iTearDownMyApp: function () {
			window.fetch = undefined;
			return this.iTeardownMyUIComponent();
		},

		_clearSharedData: function() {
			ODataModel.mSharedData = {
				server: {},
				service: {},
				meta: {}
			};
		}
	});

	function mockFetch(url, opcoesFetch) {
		const _url = url;
		const metodo = opcoesFetch?.method;
		switch (metodo) {
			case "POST":
				return adicionar(opcoesFetch);
			case "PUT":

			case "DELETE":

			default:
				return
		}
	}

	function adicionar(opcoesFetch) {
		const novoEquipamento = JSON.parse(opcoesFetch.body);
		novoEquipamento.id = crypto.randomUUID();
		novoEquipamento.dataInclusao = new Date();
		equipamentos.push(novoEquipamento);
	}
});
