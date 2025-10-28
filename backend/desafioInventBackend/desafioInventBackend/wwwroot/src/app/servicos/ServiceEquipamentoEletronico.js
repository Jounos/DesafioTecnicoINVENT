sap.ui.define(["desafio/app/repositorios/RepositorioEquipamentoEletronico"], function(RepositorioEquipamentoEletronico) {
	"use strict";

	return {
		buscarTodos: _buscarTodos
	};

	async function _buscarTodos (filtros, callback = null) {
		return await RepositorioEquipamentoEletronico.obterTodos(filtros, callback);
	}
})

