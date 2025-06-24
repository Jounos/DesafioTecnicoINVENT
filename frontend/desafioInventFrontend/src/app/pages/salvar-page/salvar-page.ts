import { Component } from '@angular/core';

@Component({
	selector: 'app-salvar-page',
	templateUrl: './salvar-page.html',
	styleUrl: './salvar-page.css',
	standalone: false,
})
export class SalvarPage {

	tipoEquipamento: number = 0;
	listaTiposEquipamento = [
		{ id: 0, label: 'PC'},
		{ id: 1, label: 'Notebook'},
		{ id: 2, label: 'Mouse'},
		{ id: 3, label: 'Teclado'},
	]

}
