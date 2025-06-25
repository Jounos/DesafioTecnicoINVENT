import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';
import { IRetornoEquipamentoEletronico } from '../library/models/retorno-equipamento-eleronico.model';
import { IEquipamentoEletronico } from '../library/models/equipamento-eletronico.model';
import { IAtualizarEquipamentoEletronico } from '../library/models/atualizar-equipamento-eletronico.model';

@Injectable({
	providedIn: 'root'
})
export class EquipamentoEletronicoService {

	private readonly endpoint = environment.apiUrl.concat('/equipamento-eletronico');

	constructor(private http: HttpClient) { }

	listarEquipamentosEleronicos(): Observable<HttpResponse<IRetornoEquipamentoEletronico[]>> {
		return this.http.get<HttpResponse<IRetornoEquipamentoEletronico[]>>(this.endpoint, { observe: 'body',  responseType: 'json' });
	}

	buscarEquipamentoEletronicoPorId(id: number): Observable<HttpResponse<IRetornoEquipamentoEletronico>> {
		return this.http.get<HttpResponse<IRetornoEquipamentoEletronico>>(`${this.endpoint}/${id}`, { observe: 'body',  responseType: 'json' });
	}

	cadastrarEquipamentoEletronico(equipamentoEletronico: IEquipamentoEletronico): Observable<HttpResponse<IRetornoEquipamentoEletronico>> {
		return this.http.post<HttpResponse<IRetornoEquipamentoEletronico>>(this.endpoint, equipamentoEletronico, { observe: 'body', responseType: 'json' });
	}

	atualiarEquipamentoEletronico(id: number, equipamentoEletronico: IAtualizarEquipamentoEletronico): Observable<HttpResponse<IRetornoEquipamentoEletronico>> {
		return this.http.put<HttpResponse<IRetornoEquipamentoEletronico>>(`${this.endpoint}/${id}`, equipamentoEletronico, { observe: 'body', responseType: 'json' });
	}

	deletarEquipamentoEletronico(id: number): Observable<HttpResponse<void>> {
		return this.http.delete<HttpResponse<void>>(`${this.endpoint}/${id}`, { observe: 'body', responseType: 'json' })
	}
}
