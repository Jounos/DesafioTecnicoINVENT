sap.ui.define([
	"sap/m/Dialog",
	"sap/ui/model/json/JSONModel",
	"sap/m/Text",
	"sap/m/Panel",
	"sap/ui/layout/VerticalLayout",
	"sap/m/Button",
	"sap/ui/core/library"
], function (
	Dialog,
	JSONModel,
	Text,
	Panel,
	VerticalLayout,
	Button,
	coreLibrary
) {
	"use strict";

	const ValueState = coreLibrary.ValueState;
	const BOTAO_ESQUERDO_ID = 'apiErrorConfirmationButton';
	const BOTAO_ESQUERDO_EVENTO = 'botaoEsquerdo';
	const BOTAO_ESQUERDO_AGREGACAO = 'beginButton';
	const CONTEUDO_AGREGACAO = 'content';
	const NAMESPACE = 'desafio.common.control.DialogoDeErro';
	return Dialog.extend(NAMESPACE, {
		metadata: {
			properties: {
				cabecalho: "string",
				mensagem: "string",
				stack: "string",
			},
			events: {
				botaoOk: { }
			}
		},

		onBeforeRendering: function () {
			Dialog.prototype.onBeforeRendering.apply(this, arguments);
			this.setModel(new JSONModel({
				cabecalho: this.getCabecalho(),
				mensagem: this.getMensagem(),
				stack: this.getStack(),
			}));
		},

		init: function () {
			Dialog.prototype.init.call(this, arguments);
			const comprimento = "50%";
			this.setContentWidth(comprimento);
			this.setState(ValueState.Error);
			this.setType("Message");

			var cabecalho = new Text({ text: "{/cabecalho}" });
			var mensagem = new Text('', { text: "{/mensagem}" });
			var texto = new Text({ text: "{/mensagem}" });

			var painel = new Panel({
				backgroundDesign: 'Transparent',
				headerText: '{i18n>Common.StackTrace}',
				expandable: true,
				content: texto
			});

			var layout = new VerticalLayout({
				width: '100%',
				content: [
					cabecalho,
					mensagem,
					painel
				]
			});

			const classeMinima = 'sapUiTinyMargin';
			const classePequena = 'sapSmallMarginBegin';''
			const classeMinimaAcima = 'sapUiTinymarginAbove';

			cabecalho.addStyleClass(classeMinima);
			mensagem.addStyleClass(classePequena);
			texto.addStyleClass(classeMinima);
			painel.addStyleClass(classeMinimaAcima);

			this.addAggregation(CONTEUDO_AGREGACAO, layout);
			this.setAggregation(BOTAO_ESQUERDO_AGREGACAO, new Button(BOTAO_ESQUERDO_ID, {
				text: '{i18n>Common.OK}',
				press: () => {
					this.close();
					this.fireEvent(BOTAO_ESQUERDO_EVENTO)
				}
			}));
		},

		open: function () {
			var dialogo = Dialog.prototype.open.apply(this, arguments);

			return new Promise((resolve) => {
				this.attachAfterClose(() => {
					dialogo.destroy();
					resolve(dialogo);
				})
			})
		},
		renderer: {}
	});
});
