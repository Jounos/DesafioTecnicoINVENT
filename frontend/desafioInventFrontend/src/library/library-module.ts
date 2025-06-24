import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { TipoEquipamentoPipe } from './pipes/tipo-equipamento-pipe';

@NgModule({
	imports: [
		CommonModule,
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LibraryModule { }
