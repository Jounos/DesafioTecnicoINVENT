import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

import { ErrorMsg } from '../../library/components/error-msg/error-msg';
import { TipoEquipamentoPipe } from '../../library/pipes/tipo-equipamento-pipe';
import { DetalhesModal } from './gestao-page/detalhes-modal/detalhes-modal';
import { GestaoPage } from './gestao-page/gestao-page';
import { PagesRoutingModule } from './pages-routing-module';
import { SalvarPage } from './salvar-page/salvar-page';


@NgModule({
	declarations: [
		DetalhesModal,
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
