import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GestaoPage } from './gestao-page/gestao-page';
import { SalvarPage } from './salvar-page/salvar-page';

const routes: Routes = [
	{
		path: '',
		component: GestaoPage,
	},
	{
		path: 'cadastrar',
		component: SalvarPage,
	},
	{
		path: 'editar/:id',
		component: SalvarPage,
	},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
