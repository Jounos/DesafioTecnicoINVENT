import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { IEquipamentoEletronico } from '../../library/models/equipamento-eletronico.model';

@Injectable({
	providedIn: 'root'
})
export class EquipamentoEletronicoService {

	private readonly endpoint = environment.apiUrl.concat('/equipamento-eletronico');

	constructor(private http: HttpClient) { }

	listarEquipamentosEleronicos(): Observable<HttpResponse<IEquipamentoEletronico[]>> {
		return this.http.get<IEquipamentoEletronico[]>(this.endpoint, { observe: 'response', responseType: 'json' });
	}

	buscarEquipamentoEletronicoPorId(id: string): Observable<HttpResponse<IEquipamentoEletronico>> {
		return this.http.get<IEquipamentoEletronico>(`${this.endpoint}/${id}`, { observe: 'response', responseType: 'json' });
	}

	cadastrarEquipamentoEletronico(equipamentoEletronico: { nome: string, tipoEquipamento: number, quantidadeEstoque: number }): Observable<HttpResponse<IEquipamentoEletronico>> {
		return this.http.post<IEquipamentoEletronico>(this.endpoint, equipamentoEletronico, { observe: 'response', responseType: 'json' });
	}

	atualiarEquipamentoEletronico(id: string, equipamentoEletronico: { id: string, nome: string, tipoEquipamento: number, quantidadeEstoque: number }): Observable<HttpResponse<IEquipamentoEletronico>> {
		return this.http.put<IEquipamentoEletronico>(`${this.endpoint}/${id}`, equipamentoEletronico, { observe: 'response', responseType: 'json' });
	}

	deletarEquipamentoEletronico(id: string) {
		return this.http.delete(`${this.endpoint}/${id}`, { observe: 'response', responseType: 'json' })
	}
}
