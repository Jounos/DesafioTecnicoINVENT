sap.ui.define(["desafio/app/repositorios/RepositorioEquipamentoEletronico"], function(RepositorioEquipamentoEletronico) {
	"use strict";

	return {
		buscarTodos: _buscarTodos
	};

	function _buscarTodos (filtros, callback = null) {
		return RepositorioEquipamentoEletronico.obterTodos(filtros, callback);
	}
})

