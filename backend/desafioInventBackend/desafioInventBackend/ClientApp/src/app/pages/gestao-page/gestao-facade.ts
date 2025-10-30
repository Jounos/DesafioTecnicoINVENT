import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { IEquipamentoEletronico } from '../../../library/models/equipamento-eletronico.model';
import { EquipamentoEletronicoDomain } from '../../domains/equipamento-eletronico-domain';
import { EquipamentoEletronicoService } from '../../services/equipamento-eletronico-service';
import { EquipamentoEletronicoFilter } from '../../../library/filters/equipamento-eletronico.filter';

@Injectable({
	providedIn: 'root'
})
export class GestaoFacade {

	constructor(
		private equipamentoEletronicoDomain: EquipamentoEletronicoDomain,
		private equipamentoEletronicoService: EquipamentoEletronicoService
	) { }

	pesquisar(filter: EquipamentoEletronicoFilter): Observable<HttpResponse<IEquipamentoEletronico[]>> {
		const params = this.equipamentoEletronicoDomain.criarParametrosPorFiltros(filter);
		return this.equipamentoEletronicoService.pesquisarEquipamentoEletronico(params).pipe(
			map(result => {
				result.body?.forEach(value => value.temEstoque = value.quantidadeEstoque > 0);
				return result;
			})
		);
	}
}
