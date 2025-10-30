import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EquipamentoEletronicoFilter } from '../../library/filters/equipamento-eletronico.filter';

@Injectable({
	providedIn: 'root'
})
export class EquipamentoEletronicoDomain {

	constructor() { }

	criarParametrosPorFiltros(filter: EquipamentoEletronicoFilter): HttpParams {

		let params = new HttpParams();

		Object.keys(filter).forEach(key => {
			if (filter[key]) {
				params = params.append(key, filter[key]);
			}
		});

		return params;
	}
}
