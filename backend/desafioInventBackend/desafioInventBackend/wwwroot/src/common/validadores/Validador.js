sap.ui.define([
	'sap/ui/base/Object',
	'sap/ui/core/Library'
], function(Object, Library) {
	'use strict';

	var ValueState = Library;
	const NAMESPACE = 'desafio.common.validadores.Validador';
	const TEXTO_CAMPO = "campo";
	const TEXTO_DEVE_MAIOR_QUE = "deve ser maior que";
	const TEXTO_CARACTERES = "caracteres";
	const TEXTO_INVALIDO = "inválido";
	const NOME_TIPO_INPUT = sap.m.Input.getMetadata().getName();
	const NOME_TIPO_MASK_INPUT = sap.m.MaskInput.getMetadata().getName();
	const FUNCAO_SET_VALUE_STATE = "setValueState";
	const FUNCAO_SET_VALUE_STATE_TEXT = "setValueStateText";
	const TIPO_FUNCAO = "function";

	return Object.extend(NAMESPACE, {

		controle: null,

		constructor: function(nomeDoCampo) {
			Object.call(this);
			this.nomeDoCampo = nomeDoCampo;
			this.validacoes = [];
		},

		vincularControle: function (controle) {
			this.controle = controle;
		},

		maiorQue: function(tamanhoDoCampo, mensagem) {
			mensagem = mensagem || TEXTO_CAMPO + ' ' + this.nomeDoCampo + ' ' + TEXTO_DEVE_MAIOR_QUE + ' ' + tamanhoDoCampo + ' ' + TEXTO_CARACTERES;
			this.adicionarValidacao(valor => valor && valor.trim().length > tamanhoDoCampo , mensagem);
			return this;
		},

		numeroMaiorQue: function (numero, mensagem) {
			mensagem = mensagem || `${TEXTO_CAMPO} ${this.nomeDoCampo} ${TEXTO_INVALIDO}`;
			this.adicionarValidacao(valor => valor && valor > numero, mensagem);
			return this;
		},

		adicionarValidacao: function(regraDeValidacao, mensagem) {
			this.validacoes.push({
				regraDeValidacao: regraDeValidacao,
				mensagem: mensagem
			});
		},

		validar: function (objeto) {
			const campos =  this.nomeDoCampo.split('/');
			const valor = campos.reduce((o, i) => (o ? o[i] : undefined), objeto.getData());
			let mensagens = '';

			let validacoesFalhas = this.validacoes.filter(validacao => !validacao.regraDeValidacao(valor));

			if (validacoesFalhas) {
				validacoesFalhas.forEach((validacao) => {
					mensagens += validacao.mensagem + "\n";
				});
			}

			mensagens ? this.setarStatusErro(mensagens) : this.setarStatusSucesso();

			return this.validadoComSucesso;
		},

		setarStatusErro(mensagens) {
			var tipoDoControle = this.controle.getMetadata().getName();

			switch (tipoDoControle) {
				case NOME_TIPO_INPUT:
				case NOME_TIPO_MASK_INPUT:
					this.controle.setValueState("Error");
					this.controle.setValueStateText(mensagens);
					break;
				default:
					if (typeof(this.controle[FUNCAO_SET_VALUE_STATE]) === TIPO_FUNCAO) {
						this.controle.setValueState("Error");
					}
					if (typeof(this.controle[FUNCAO_SET_VALUE_STATE_TEXT]) === TIPO_FUNCAO) {
						this.controle.setValueStateText(mensagens);
					}
			}

			this.validadoComSucesso = false;
		},

		setarStatusSucesso: function () {
			var tipoDoControle = this.controle.getMetadata().getName();

			switch (tipoDoControle) {
				case NOME_TIPO_INPUT:
					this.controle.setValueState("None");
					break;
				default:
					if (typeof(this.controle[FUNCAO_SET_VALUE_STATE]) === TIPO_FUNCAO) {
						this.controle.setValueState(ValueState.None);
					}
			}

			this.validadoComSucesso = true;
		},
	});
});
