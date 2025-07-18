import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { IEquipamentoEletronico } from '../../library/models/equipamento-eletronico.model';
import { IRetornoEquipamentoEletronico } from '../../library/models/retorno-equipamento-eleronico.model';

@Injectable({
	providedIn: 'root'
})
export class EquipamentoEletronicoService {

	private readonly endpoint = environment.apiUrl.concat('/equipamento-eletronico');

	constructor(private http: HttpClient) { }

	listarEquipamentosEleronicos(): Observable<HttpResponse<IRetornoEquipamentoEletronico[]>> {
		return this.http.get<HttpResponse<IRetornoEquipamentoEletronico[]>>(this.endpoint, { observe: 'body',  responseType: 'json' });
	}

	buscarEquipamentoEletronicoPorId(id: string): Observable<HttpResponse<IRetornoEquipamentoEletronico>> {
		return this.http.get<HttpResponse<IRetornoEquipamentoEletronico>>(`${this.endpoint}/${id}`, { observe: 'body',  responseType: 'json' });
	}

	cadastrarEquipamentoEletronico(equipamentoEletronico: IEquipamentoEletronico): Observable<HttpResponse<IRetornoEquipamentoEletronico>> {
		return this.http.post<HttpResponse<IRetornoEquipamentoEletronico>>(this.endpoint, equipamentoEletronico, { observe: 'body', responseType: 'json' });
	}

	atualiarEquipamentoEletronico(id: string, equipamentoEletronico: IEquipamentoEletronico): Observable<HttpResponse<IRetornoEquipamentoEletronico>> {
		return this.http.put<HttpResponse<IRetornoEquipamentoEletronico>>(`${this.endpoint}/${id}`, equipamentoEletronico, { observe: 'body', responseType: 'json' });
	}

	deletarEquipamentoEletronico(id: string): Observable<HttpResponse<boolean>> {
		return this.http.delete<HttpResponse<boolean>>(`${this.endpoint}/${id}`, { observe: 'body', responseType: 'json' })
	}
}
