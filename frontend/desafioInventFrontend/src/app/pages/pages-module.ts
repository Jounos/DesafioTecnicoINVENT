import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';


import { PagesRoutingModule } from './pages-routing-module';
import { GestaoPage } from './gestao-page/gestao-page';
import { SalvarPage } from './salvar-page/salvar-page';
import { LibraryModule } from '../../library/library-module';
import { TipoEquipamentoPipe } from '../../library/pipes/tipo-equipamento-pipe';


@NgModule({
	declarations: [
		GestaoPage,
		SalvarPage
	],
	imports: [
		CommonModule,
		NgSelectModule,
		FormsModule,
		TipoEquipamentoPipe,
		LibraryModule,
		NgbPagination,
		PagesRoutingModule,
	]
})
export class PagesModule { }
