sap.ui.define(['desafio/common/validadores/ValidadorBase'], function(ValidadorBase) {
	'use strict';

	const NAMESPACE = 'desafio.app.validadores.ValidadorEquipamentoEletronico';
	return ValidadorBase.extend(NAMESPACE, {

		constructor: function (resourceBundle) {
			ValidadorBase.call(this);
			this.validarNome(resourceBundle);
			this.validarQuantidade(resourceBundle);
		},

		validarNome: function (resourceBundle) {
			const nome = 'nome';
			const necessarioNome = 'Cadastro.NomeInput';
			const stringDeverSerMaiorQueTres = 3;
			this.validacaoPara(nome)
				.maiorQue(stringDeverSerMaiorQueTres, resourceBundle.getText(necessarioNome));
		},

		validarQuantidade: function (resourceBundle) {
			const quantidade = 'quantidade';
			const necessarioQuantidade = 'Cadastro.QuantidadeInput';
			const regexNumeroInteiroMaiorQueZero = '^[1-9]\d*$';
			this.validacaoPara(quantidade)
				.segueRegex(regexNumeroInteiroMaiorQueZero , resourceBundle.getText(necessarioQuantidade));
		},
	});
});
