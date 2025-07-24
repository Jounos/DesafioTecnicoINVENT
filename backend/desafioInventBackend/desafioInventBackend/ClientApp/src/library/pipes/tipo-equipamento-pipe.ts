import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'tipoEquipamento',
	standalone: true,
})
export class TipoEquipamentoPipe implements PipeTransform {

	transform(value: number): string {
		switch (value) {
			case 1:
				return 'PC';
			case 2:
				return 'Notebook';
			case 3:
				return 'Mouse';
			case 4:
				return 'Teclado';
			default:
				return 'undefined';
		}
	}

}
