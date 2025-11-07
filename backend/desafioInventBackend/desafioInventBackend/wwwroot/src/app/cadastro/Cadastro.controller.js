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

			this._criarModelos();
		},

		_criarModelos() {
			this._criarModeloFormualrio();
			this._criarModeloSelects();
		},

		_criarModeloFormualrio() {

			this.criarModelo("form", {
				nome: '',
				tipoEquipamento: 1,
				quantidade: null,
			});
		},

		_criarModeloSelects() {

			const labelPC = 'PC';
			const labelNotebook = 'Notebook';
			const labelMouse = 'Mouse';
			const labelTeclado  = 'Teclado';
			const labelCelular = 'Celular';

			const tiposEquipamentos =  {
				tipoEquipamentoCollection: [
					{ label: labelPC, id: 1 },
					{ label: labelNotebook, id: 2 },
					{ label: labelMouse, id: 3 },
					{ label: labelTeclado, id: 4 },
					{ label: labelCelular, id: 5 }
				],
			};

			this.criarModelo("tiposEquipamentos", tiposEquipamentos);
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
