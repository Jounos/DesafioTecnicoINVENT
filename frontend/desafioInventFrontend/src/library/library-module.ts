import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { ComponentsModule } from './components/components-module';
import { TipoEquipamentoPipe } from './pipes/tipo-equipamento-pipe';

@NgModule({
	declarations: [TipoEquipamentoPipe],
	imports: [
		CommonModule,
		ComponentsModule,
	],
	exports: [
		ComponentsModule,
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LibraryModule { }
