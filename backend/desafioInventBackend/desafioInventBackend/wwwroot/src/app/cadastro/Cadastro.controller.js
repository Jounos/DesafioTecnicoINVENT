sap.ui.define([
	"desafio/common/BaseController",
	"sap/ui/core/routing/History"
], (BaseController) => {
	"use strict";

	const NOME_CONTROLLER = "desafio.app.cadastro.Cadastro"
	const NOME_MODELO_PARAMETROS = "params";

	return BaseController.extend(NOME_CONTROLLER, {
		onInit() {
			const rotaCadastro = "cadastro";
			this.vincularRota(rotaCadastro, this._obterParametros);
		},

		_obterParametros: function (event) {
			const parametro = "arguments";
			let querys = event.getParameter(parametro)[this.query];

			this.criarModelo(NOME_MODELO_PARAMETROS, querys);
		},

		aoNavegarUltimaPagina: function () {
			const params = this.obterValorModelo(NOME_MODELO_PARAMETROS);
			const rotaListagem = "listagem";
			this.navegarPara(rotaListagem, params);
		}
	});
});
