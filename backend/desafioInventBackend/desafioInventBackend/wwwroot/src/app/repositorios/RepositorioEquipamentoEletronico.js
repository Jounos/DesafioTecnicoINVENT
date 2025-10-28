sap.ui.define(["desafio/app/common/HttpClient"], function(HttpClient) {
	"use strict";

	const URI_CONTROLLER = "/equipamento-eletronico";

	return {
		obterTodos: _obterTodos
	};

	function _obterParams(filtros) {

		if (!filtros) {
			return undefined;
		}

		const nome = 'nome';
		const tipoEquipamento = 'tipoEquipamento';
		const dataInicio = 'dataInicio';
		const dataFim = 'dataFim';
		const haEstoque = 'haEstoque';

		let params = new URLSearchParams();

		if (filtros?.nome) params.append(nome, filtros.nome);
		if (filtros?.tipoEquipamento) params.append(tipoEquipamento, filtros.tipoEquipamento);
		if (filtros?.dataInicio) params.append(dataInicio, filtros.dataInicio);
		if (filtros?.dataFim) params.append(dataFim, filtros.dataFim);
		if (filtros?.haEstoque) params.append(haEstoque, filtros.haEstoque);

		return `?${params.toString()}`;
	}

	function _obterTodos(filtros, callback = null) {
		const params = _obterParams(filtros);
		const url = URI_CONTROLLER + params;
		return HttpClient.get(url, callback);
	}
});
