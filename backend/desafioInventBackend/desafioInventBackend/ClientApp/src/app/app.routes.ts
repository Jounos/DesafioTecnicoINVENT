import { Routes } from '@angular/router';
import { ErrorPage } from './pages/error-page/error-page';

export const routes: Routes = [
	{
		path: '',
		loadChildren: () => import('./pages/pages-module').then(m=> m.PagesModule),
	},
	{
		path: '**',
		pathMatch: 'full',
		component: ErrorPage
	}
];
