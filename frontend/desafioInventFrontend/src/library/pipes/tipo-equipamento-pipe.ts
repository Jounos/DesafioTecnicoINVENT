import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'tipoEquipamento',
	standalone: false,
})
export class TipoEquipamentoPipe implements PipeTransform {

	transform(value: number): string {
		switch (value) {
			case 0:
				return 'PC';
			case 1:
				return 'Notebook';
			case 2:
				return 'Mouse';
			case 3:
				return 'Teclado';
			default:
				return 'undefined';
		}
	}

}
