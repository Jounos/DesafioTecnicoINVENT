sap.ui.define([
	"desafio/common/BaseController",
	"sap/ui/core/routing/History"
], (BaseController) => {
	"use strict";

	const NOME_MODEL_PARAMS = "params";

	return BaseController.extend("desafio.app.cadastro.Cadastro", {
		onInit() {
			const rotaCadastro = "cadastro";
			this.vincularRota(rotaCadastro, this._obterParametros);
		},

		_obterParametros: function (event) {
			const parametro = "arguments";
			let querys = event.getParameter(parametro)[this.query];

			this.criarModelo(NOME_MODEL_PARAMS, querys);
		},

		aoNavegarUltimaPagina: function () {
			const params = this.obterValorModelo(NOME_MODEL_PARAMS);

			this.navegarPara("listagem", params);
		}
	});
});
