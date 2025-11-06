sap.ui.define([], function ()  {
	"use strict";

	const STRING_VAZIA = "";

	return {

		formatarTipoEquipamento: function (idTipoEquipamento) {
			const tipoEquipamentoPC = "PC";
			const tipoEquipamentoNotebook = "Notebook";
			const tipoEquipamentoMouse = "Mouse";
			const tipoEquipamentoTeclado = "Teclado";
			const tipoEquipamentoCelular = "Celular";

			switch (idTipoEquipamento) {
				case 1:
					return tipoEquipamentoPC;
				case 2:
					return tipoEquipamentoNotebook;
				case 3:
					return tipoEquipamentoMouse;
				case 4:
					return tipoEquipamentoTeclado;
				case 5:
					return tipoEquipamentoCelular;
				default:
					return STRING_VAZIA;
			}
		},

		formatarDataParaAPI: function (data) {
			const hora = 23, minuto = 59, segundo = 59;
			return new Date(Date.UTC(
				data.getFullYear(),
				data.getMonth(),
				data.getDate(),
				hora, minuto, segundo
			)).toISOString();
		},

		formatarData: function(date) {
			const data = new Date(date);
			if (isNaN(data)) {
				return STRING_VAZIA;
			}

			const localQueRefereData = "pt-BR";
			return data.toLocaleDateString(localQueRefereData);
		}
	};
});
