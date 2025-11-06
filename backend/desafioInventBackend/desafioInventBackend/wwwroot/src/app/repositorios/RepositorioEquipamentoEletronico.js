sap.ui.define(["desafio/common/HttpClient"], function(HttpClient) {
	"use strict";

	const URI_CONTROLLER = "/equipamento-eletronico";

	return {
		obterTodos: _obterTodos
	};

	function _obterParams(filtros) {

		if (!filtros) {
			return undefined;
		}

		const parametroNome = 'nome';
		const parametroTipoEquipamento = 'tipoEquipamento';
		const parametroDataInicio = 'dataInicio';
		const parametroDataFim = 'dataFim';
		const parametroEquipamentoEmEstoque = 'equipamentoEmEstoque';

		let params = new URLSearchParams();

		if (filtros?.nome) params.append(parametroNome, filtros.nome);
		if (filtros?.tipoEquipamento) params.append(parametroTipoEquipamento, filtros.tipoEquipamento);
		if (filtros?.dataInicio) params.append(parametroDataInicio, filtros.dataInicio);
		if (filtros?.dataFim) params.append(parametroDataFim, filtros.dataFim);
		if (filtros?.equipamentoEmEstoque) params.append(parametroEquipamentoEmEstoque, filtros.equipamentoEmEstoque);

		return `?${params.toString()}`;
	}

	async function _obterTodos(filtros, callback = null) {
		const params = _obterParams(filtros);
		const url = URI_CONTROLLER + params;
		return await HttpClient.get(url, callback);
	}
});
