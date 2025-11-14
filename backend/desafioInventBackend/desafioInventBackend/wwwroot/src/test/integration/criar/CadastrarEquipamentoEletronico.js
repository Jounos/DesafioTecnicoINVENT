sap.ui.define([
	'sap/ui/test/Opa5',
	'sap/ui/test/matchers/PropertyStrictEquals',
	'sap/ui/actions/Press'
], function (Opa5, PropertyStrictEquals, Press) {
	"use strict";

	const VIEW_NAME = "cadastro.Cadastro";

	Opa5.createPageObjects({
		naPaginaCriacaoDeEquipamentoEletronico: {
			actions: {
				_clicouNoBotaoComI18N: function (chaveI18N) {
					return this.waitFor({
						viewName: VIEW_NAME,
						controlType: 'sap.m.Button',
						matchers: {
							i18NText: {
								propertyType: 'text',
								key: chaveI18N
							}
						},
						actions: new Press({}),
						success: () => Opa5.assert.ok(true, `Foi clicado no botão ${chaveI18N} com sucesso`),
						errorMessage: `N~]ao foi possível encontrar o botão com a chave ${chaveI18N}`
					});
				},

				clicouNoBotaoCancelar: function () {
					const botaoCancelar = 'Cadastro.CancelarBotao';
					this._clicouNoBotaoComI18N(botaoCancelar);
				}
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
				},

				_verificarSeUmBotaoControleExisteBaseadoNoI18N: function (controle, propriedade, chaveI18N, msgSucesso, msgErro) {
					return this.waitFor({
						viewName: VIEW_NAME,
						controlType: controle,
						matchers: {
							i18NText: {
								propertyName: propriedade,
								key: chaveI18N
							}
						},
						success: () => Opa5.assert.ok(true, msgSucesso),
						errorMessage: msgErro
					});
				},

				oBotaoSalvarFoiCarregadoCorretamente: function () {
					const controle = 'sap.m.Button';
					const propriedade = 'text';
					const chaveI18N = 'Cadastro.SalvarBotao';
					const msgSucesso = 'Botão de salvar da tela de cadastro de Equipamentos Eletrônicos encontrado com sucesso';
					const msgErro = 'Botão de salvar da tela de cadastro de Equipamentos Eletrônicos não foi encontrado'
					return this._verificarSeUmBotaoControleExisteBaseadoNoI18N(controle, propriedade, chaveI18N, msgSucesso, msgErro);
				},

				oBotaoCancelarFoiCorregadoCorretamente: function () {
					const controle = 'sap.m.Button';
					const propriedade = 'text';
					const chaveI18N = 'Cadastro.CancelarBotao';
					const msgSucesso = 'Botão de cancelar da tela de cadastro de Equipamentos Eletrônicos encontrado com sucesso';
					const msgErro = 'Botão de cancelar da tela de cadastro de Equipamentos Eletrônicos não foi encontrado'
					return this._verificarSeUmBotaoControleExisteBaseadoNoI18N(controle, propriedade, chaveI18N, msgSucesso, msgErro);
				}

			}
		}
	});
});
