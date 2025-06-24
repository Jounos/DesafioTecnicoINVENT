import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { EquipamentoEletronicoService } from '../../../services/equipamento-eletronico-service';
import { IRetornoEquipamentoEletronico } from '../../../library/models/retorno-equipamento-eleronico.model';
import { Router } from '@angular/router';

const FILTER_PAG_REGEX = /[^0-9]/g;


@Component({
	selector: 'app-gestao-page',
	templateUrl: './gestao-page.html',
	styleUrl: './gestao-page.css',
	standalone: false,
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class GestaoPage implements OnInit {

	page = 4;

	tipoEquipamento: number = 0;
	listaTiposEquipamento = [
		{ id: 0, label: 'PC'},
		{ id: 1, label: 'Notebook'},
		{ id: 2, label: 'Mouse'},
		{ id: 3, label: 'Teclado'},
	]

	listaEquipamentosEletronicos: IRetornoEquipamentoEletronico[] = [];


	constructor(
		private equipamentoEletronicoService: EquipamentoEletronicoService,
		private cdr: ChangeDetectorRef
	) {	}

	ngOnInit(): void {
		this.cdr.detectChanges();
	}


	selectPage(page: string) {
		this.page = parseInt(page, 10) || 1;
	}

	formatInput(input: HTMLInputElement) {
		input.value = input.value.replace(FILTER_PAG_REGEX, '');
	}
}
