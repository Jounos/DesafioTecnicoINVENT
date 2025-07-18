import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';


import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TipoEquipamentoPipe } from '../../library/pipes/tipo-equipamento-pipe';
import { GestaoPage } from './gestao-page/gestao-page';
import { PagesRoutingModule } from './pages-routing-module';
import { SalvarPage } from './salvar-page/salvar-page';
import { ErrorInterceptor } from '../interceptors/error-interceptor.depracated';


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
		PagesRoutingModule,
	],
	providers: [
		{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }	]
})
export class PagesModule { }
