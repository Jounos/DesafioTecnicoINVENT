sap.ui.define(["desafio/app/repositorios/RepositorioEquipamentoEletronico"], function(RepositorioEquipamentoEletronico) {
	"use strict";

	return {
		buscarTodos: _buscarTodos,
		salvar: _salvar
	};

	function _buscarTodos (filtros, callback = null) {
		return RepositorioEquipamentoEletronico.obterTodos(filtros, callback);
	}

	function _salvar (equipamentoEletronico, callback = null) {
		let equipamentoEletronicoDto = {
			Nome: equipamentoEletronico.nome,
			TipoEquipamento: Number(equipamentoEletronico.tipoEquipamento),
			QuantidadeEstoque: Number(equipamentoEletronico.quantidadeEstoque)
		}
		return RepositorioEquipamentoEletronico.salvar(equipamentoEletronicoDto, callback);
	}
});

