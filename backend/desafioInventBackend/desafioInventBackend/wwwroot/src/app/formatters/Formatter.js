sap.ui.define([], function ()  {
	"use strict";

	return {

		formatarTipoEquipamento: function (idTipoEquipamento) {
			switch (idTipoEquipamento) {
				case 1:
					return "PC";
				case 2:
					return "Notebook";
				case 3:
					return "Mouse";
				case 4:
					return "Teclado";
				case 5:
					return "Celular";
				default:
					return "";
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
				return "";
			}
			return data.toLocaleDateString("pt-BR")
		}
	};
});
