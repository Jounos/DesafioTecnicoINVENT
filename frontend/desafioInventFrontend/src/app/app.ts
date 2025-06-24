import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LibraryModule } from '../library/library-module';

@Component({
	selector: 'app-root',
	imports: [
		LibraryModule,
		RouterOutlet,
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA],
	templateUrl: './app.html',
	styleUrl: './app.css',
})
export class App {
	protected title = 'Desafio Técnico Invent';
}
