sap.ui.define([
	'sap/ui/base/Object',
	'./Validador'
], function(Object, Validador) {
	'use strict';

	const NAMESPACE = 'desafio.common.validadores.ValidadorBase';
	return Object.extend(NAMESPACE, {
		constructor: function() {
			Object.call(this);
			this.validacoes = [];
		},

		validacaoPara: function(nomeDoCampo) {
			var validacao = new Validador(nomeDoCampo);
			this.validacoes.push(validacao);
			return validacao;
		},

		vincularControle: function (nomeDoCampo, controle) {
			var validacao = this.validacoes.find(x => x.nomeDoCampo == nomeDoCampo);
			if (validacao) {
				validacao.vincularControle(controle);
			}
		},

		validarParaCampo: function (nomeDoCampo, objeto) {
			let validacao = this.validacoes.find(x => x.nomeDoCampo === nomeDoCampo);
			if (!validacao) {
				return null;
			}
			return validacao.validar(objeto);
		},

		limparEstadoDosControles: function () {
			this.validacoes.forEach((validacao) => validacao.setarStatusSucesso());
		}
	});
})
