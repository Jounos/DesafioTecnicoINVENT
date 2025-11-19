sap.ui.define([
	'sap/ui/test/Opa5',
	'sap/ui/test/actions/Press',
	'sap/ui/test/matchers/BindingPath'
], function(Opa5, Press, BindingPath) {
	"use strict";

	const VIEW_NAME = "listagem.Listagem";

	Opa5.createPageObjects({
		naPaginaListagemEquipamentosEletronicos: {
			actions: {

				buscouTodosOsEquipamentosEletronicos: function () {
					const botaoPesquisar = 'Listagem.PesquiarBotao';
					this._clicouNoBotaoComI18N(botaoPesquisar);
				},

				_clicouNoBotaoComI18N: function (chaveI18N) {
					return this.waitFor({
						viewName: VIEW_NAME,
						controlType: 'sap.m.Button',
						matchers: {
							i18NText: {
								propertyName: 'text',
								key: chaveI18N
							}
						},
						actions: new Press({}),
						success: () => Opa5.assert.ok(true, `Foi clicado no botão ${chaveI18N} com sucesso`),
						errorMessage: `Não foi possível encontrar o botão com a chave ${chaveI18N}`
					});
				},

				clicoNoBotaoPresenteNoPrimeiroElementoDaLista: function (iconeDoButton) {
					return this.waitFor({
						viewName: VIEW_NAME,
						controlType: 'sap.m.CustomListItem',
						matchers: new BindingPath({
							path: '/0',
							modelName: 'listaEquipamentosEletronicos'
						}),
						actions: (oListItem) => {
							const oBotao = oListItem.getAggregation("content").find(c => c.isA('sap.m.Button') && c.getIcon() === iconeDoButton);
							new Press().executeOn(oBotao);
						},
						success: () => {
							Opa5.assert.ok(true, `O botão com o icone `)
						}
					});
				}

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
