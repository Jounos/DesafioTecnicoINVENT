sap.ui.define([
	'sap/ui/test/Opa5',
	'sap/ui/test/matchers/PropertyStrictEquals',
], function (Opa5, PropertyStrictEquals) {
	"use strict";

	const VIEW_NAME = "cadastro.Cadastro";

	Opa5.createPageObjects({
		naPaginaCriacaoDeEquipamentoEletronico: {
			actions: {

            },

            assertions: {
                aTelaDeCadastroFoiCarregadaCorretamente: function () {
					return this.waitFor({
						controlType: 'sap.m.Page',
						matchers: new PropertyStrictEquals({
							name: `title`,
							value: `Electronic Equipment Registration`,
						}),
						success: () => Opa5.assert.ok(true, "A tela de cadastro abriu corretamente"),
						errorMessage: "Não foi possível abrir a tela de cadastro"
					});
				}
            }
		}
	});
});
