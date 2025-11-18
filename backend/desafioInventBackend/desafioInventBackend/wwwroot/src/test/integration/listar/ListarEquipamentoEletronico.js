sap.ui.define([
	'sap/ui/test/Opa5'
], function(Opa5) {
	"use strict";

	const VIEW_NAME = "listagem.Listagem";

	Opa5.createPageObjects({
		naPaginaListagemEquipamentosEletronicos: {
			actions: {

			},
			assertions: {
				aTelaDeListagemFoiCarregadaCorretamente: function() {
					return this.waitFor({
						viewName: VIEW_NAME,
						success: () => {
							Opa5.assert.ok(true, 'Tela de listagem de equipamentos eletrônicos carregou corretamente.');
						},
						errorMessage: 'Tela de listagem de equipamentos eletrônicos não foi carregada'
					});
				}
			}
		}
	});
})
