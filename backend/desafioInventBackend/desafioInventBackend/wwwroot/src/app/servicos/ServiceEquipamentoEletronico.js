sap.ui.define(["desafio/app/repositorios/RepositorioEquipamentoEletronico"], function(RepositorioEquipamentoEletronico) {
	"use strict";

	return {
		buscarTodos: _buscarTodos,
		salvar: _salvar
	};

	function _buscarTodos (filtros, callback = null) {
		return RepositorioEquipamentoEletronico.obterTodos(filtros, callback);
	}

	function _salvar (equipamentoEletronico) {
		var equipamentoEletronicoDto = {
			equipamentoEletronicoDto: {
				Nome: equipamentoEletronico.nome,
				TipoEquipamento: equipamentoEletronico.tipoEquipamento,
				QuantidadeEstoque: equipamentoEletronico.quantidadeEstoque
			}
		};
		return RepositorioEquipamentoEletronico.salvar(JSON.stringify(equipamentoEletronicoDto));
	}
});

