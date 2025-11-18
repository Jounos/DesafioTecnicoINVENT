sap.ui.define(["desafio/app/repositorios/RepositorioEquipamentoEletronico"], function(RepositorioEquipamentoEletronico) {
	"use strict";

	return {
		buscarPorId: _buscarPorId,
		buscarTodos: _buscarTodos,
		salvar: _salvar,
		cadastrar: _cadastrar,
		atualizar: _atualizar,
	};

	function _buscarTodos (filtros, callback = null) {
		return RepositorioEquipamentoEletronico.obterTodos(filtros, callback);
	}

	function _buscarPorId(id, callback = null) {
		return RepositorioEquipamentoEletronico.obterPorId(id, callback)
	}

	function _salvar (equipamentoEletronico, id, callback = null) {
		if (id) {
			return this.atualizar(equipamentoEletronico, id, callback);
		}

		return this.cadastrar(equipamentoEletronico, callback);
	}

	function _cadastrar(equipamentoEletronico, callback = null) {
		let equipamentoEletronicoDto = {
			Nome: equipamentoEletronico.nome,
			TipoEquipamento: Number(equipamentoEletronico.tipoEquipamento),
			QuantidadeEstoque: Number(equipamentoEletronico.quantidadeEstoque)
		}
		return RepositorioEquipamentoEletronico.cadastrar(equipamentoEletronicoDto, callback);
	}

	function _atualizar(equipamentoEletronico, id, callback = null) {
		let equipamentoEletronicoDto = {
			Id: id,
			Nome: equipamentoEletronico.nome,
			TipoEquipamento: Number(equipamentoEletronico.tipoEquipamento),
			QuantidadeEstoque: Number(equipamentoEletronico.quantidadeEstoque)
		}
		return RepositorioEquipamentoEletronico.atualizar(equipamentoEletronicoDto, id, callback);
	}
});

