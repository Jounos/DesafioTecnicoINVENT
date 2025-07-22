import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

import { TipoEquipamentoPipe } from '../../library/pipes/tipo-equipamento-pipe';
import { GestaoPage } from './gestao-page/gestao-page';
import { PagesRoutingModule } from './pages-routing-module';
import { SalvarPage } from './salvar-page/salvar-page';
import { ErrorMsg } from '../../library/components/error-msg/error-msg';


@NgModule({
	declarations: [
		GestaoPage,
		SalvarPage
	],
	imports: [
		CommonModule,
		NgSelectModule,
		FormsModule,
		ReactiveFormsModule,
		TipoEquipamentoPipe,
		NgbPagination,
		ErrorMsg,
		PagesRoutingModule,
	],
})
export class PagesModule { }
