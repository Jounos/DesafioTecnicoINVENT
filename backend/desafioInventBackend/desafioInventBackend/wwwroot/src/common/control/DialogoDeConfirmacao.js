sap.ui.define([
	"sap/m/Dialog",
	"sap/m/Button",
	"sap/m/Text",
	"sap/ui/model/json/JSONModel"
], function (
	Dialog,
	Button,
	Text,
	JSONModel
) {
	"use strict";

	const BOTAO_ESQUERDO_AGREGACAO = "beginButton";
	const BOTAO_DIREITO_AGREGACAO = "endButton";
	const TEXTO_CONTEUDO_ID = "idDoTextoDialogoDeConfirmacao";
	const BOTAO_ESQUERDO_ID = "idBotaoDaEsquerdaDoDialogoDeConfirmacao";
	const BOTAO_DIREITO_ID = "idBotaoDaDireitaDoDialogoDeConfirmacao";
	const CONTEUDO_AGREGACAO = "content";
	const BOTAO_ESQUERDO_EVENTO = "botaoEsquerdo";
	const BOTAO_DIREITO_EVENTO = "botaoDireito";

	const NAMESPACE = "desafion.common.control.DialogoDeConfirmacao"
	return Dialog.extend(NAMESPACE, {
		metadata: {
			properties: {
				textoConteudo: "string",
				textoBotaoEsquerdo: "string",
				textoBotaoDireito: "string"
			},
			events: {
				botaoEsquerdo: {},
				botaoDireito: {},
			}
		},

		onBeforeRendering() {
			Dialog.prototype.onBeforeRendering.apply(this, arguments);
			this.setModel(new JSONModel({
				textoConteudo: this.getTextoConteudo(),
				textoEsquerdo: this.getTextoBotaoEsquerdo(),
				textoDireito: this.getTextoBotaoDireito(),
			}));
		},

		init() {
			Dialog.prototype.init.call(this, arguments);
			this.setType("Message");
			this.setDraggable(true);
			this.setTitleAlignment(sap.m.TitleAlignment.Center);

			this.addAggregation(CONTEUDO_AGREGACAO, new Text(TEXTO_CONTEUDO_ID, {
				text: "{/textoConteudo}"
			}));

			this.setAggregation(BOTAO_ESQUERDO_AGREGACAO, new Button(BOTAO_ESQUERDO_ID, {
				text: "{/textoEsquerdo}",
				visible: "{= !!${/textoEsquerdo} }",
				type: sap.m.ButtonType.Ghost,
				press: () => {
					this._fecharDialogo(BOTAO_ESQUERDO_EVENTO);
				}
			}));

			this.setAggregation(BOTAO_DIREITO_AGREGACAO, new Button(BOTAO_DIREITO_ID, {
				text: "{/textoDireito}",
				visible: "{= !!${/textoDireito} }",
				type: sap.m.ButtonType.Emphasized,
				press: () => {
					this._fecharDialog(BOTAO_DIREITO_EVENTO);
				}
			}));
		},

		_fecharDialog(botao) {
			this.close();
			this.fireEvent(botao);
			this.fireAfterClose();
		},

		open() {
			Dialog.prototype.open.apply(this, arguments);
			return new Promise((resolve) => {
				this.attachAfterClose(() => {
					this.destroy();
					resolve();
				})
			})
		},
		renderer: {}
	});
});
