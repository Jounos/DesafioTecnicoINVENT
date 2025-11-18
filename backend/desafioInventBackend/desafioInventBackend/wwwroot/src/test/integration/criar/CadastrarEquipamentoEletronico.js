sap.ui.define([
	'sap/ui/test/Opa5',
	'sap/ui/test/matchers/PropertyStrictEquals',
	'sap/ui/test/actions/Press',
	'sap/ui/test/actions/EnterText',
], function (
	Opa5,
	PropertyStrictEquals,
	Press,
	EnterText,
) {
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
								propertyName: 'text',
								key: chaveI18N
							}
						},
						actions: new Press({}),
						success: () => Opa5.assert.ok(true, `Foi clicado no botão ${chaveI18N} com sucesso`),
						errorMessage: `Não foi possível encontrar o botão com a chave ${chaveI18N}`
					});
				},

				clicoNoBotaoCancelar: function () {
					const botaoCancelar = 'Cadastro.CancelarBotao';
					this._clicouNoBotaoComI18N(botaoCancelar);
				},

				clicoNoBotaoSalvar: function () {
					const botaoCancelar = 'Cadastro.SalvarBotao';
					this._clicouNoBotaoComI18N(botaoCancelar);
				},

				inserirValorNoCampoNome: function (valor) {
					const nomeId = 'idInputNome';
					return this._inserirValorNoCampoComInput(nomeId, valor);
				},

				inserirValorNoCampoQuantidade: function (valor) {
					const quantidadeId = 'idInputQuantidade';
					return this._inserirValorNoCampoComInput(quantidadeId, valor);
				},

				_inserirValorNoCampoComInput: function (id, valorInserido) {
					return this.waitFor({
						id: id,
						viewName: VIEW_NAME,
						actions: new EnterText({
							text: valorInserido
						}),
						errorMessage: `O input com id ${id} não foi encontrado`
					});
				},

				clicoNoSelectTipoEquipamento: function (tipoSelecionado) {
					return this.waitFor({
						controlType: 'sap.m.Select',
						matchers: new PropertyStrictEquals({
							name: 'selectedKey',
							value: tipoSelecionado
						}),
						actions: new Press(),
						success: () => Opa5.assert.ok(true, 'Foi clicado no select tipo equipamento eletrõnico com secesso'),
						errorMessage: 'Não foi possível encontrar o select tipo equipamento eletrônico'
					});
				},

				ClicoEmNotebookNoSelect() {
					const tipoEquipamento = "Notebook";
					return this._selecionaitemNoSelect(tipoEquipamento);
				},

				_selecionaitemNoSelect: function (tipoEquipamento) {
					return this.waitFor({
						viewName: VIEW_NAME,
						controlType: 'sap.ui.core.Item',
						matchers: new PropertyStrictEquals({
							name: 'text',
							value: tipoEquipamento
						}),
						actions: new Press(),
						success: () => Opa5.assert.ok(true, "Elemento Tipo Equipamento Eletrônico do select foi clicado corretamente."),
                        errorMessage: "Elemento Tipo Equipamento Eletrônico do select não foi clicado corretamente."
					});
				},

				clicoNoBotaoOKNoDialogComMensagemDeSucesso: function () {
					const textoDoBotao = "OK";
					return this._clicoNoBotaoDaModalDeConfirmacaoComTexto(textoDoBotao);
				},

				clicoNoBotaoOKNoDialogComMensagemDeErro: function () {
					const textoDoBotao = "OK";
					return this._clicoNoBotaoDaModalDeConfirmacaoComTexto(textoDoBotao);
				},

				_clicoNoBotaoDaModalDeConfirmacaoComTexto: function (texto) {
					return this.waitFor({
                        searchOpenDialogs: true,
                        controlType: 'sap.m.Button',
                        matchers: new PropertyStrictEquals({
                            name: 'text',
                            value: texto
                        }),
                        actions: new Press(),
                        success: () => Opa5.assert.ok(true, `Foi clicado no botão ${texto} com sucesso`),
                        errorMessage: `Não foi possível encontrar o botão com o texto: ${texto}`
                    });
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

				deveExibirModalComMensagemParaPreencherTodosOsCampos: function () {
					const mensagemDeConfirmacao = 'All fields must be filled in';
					this._verificaSeExisteTextoEmUmaDialog(mensagemDeConfirmacao);
				},

				deveExibirModalComMensagemSalvoComSucesso: function () {
					const mensagemDeConfirmacao = 'Saved Successfully';
					this._verificaSeExisteTextoEmUmaDialog(mensagemDeConfirmacao);
				},

				_verificaSeExisteTextoEmUmaDialog: function (texto) {
					return this.waitFor({
						controlType: 'sap.m.Dialog',
						matchers: {
							descendant: {
								controlType: 'sap.m.Text',
                                properties: {
                                    text: texto
                                }
							}
						},
						success: () => Opa5.assert.ok(true, `Dialog com texto ${texto} foi aberto com sucesso`),
						errorMessage: `Dialog com texto ${texto} não foi aberto`
					});
				},
			}
		}
	});
});
