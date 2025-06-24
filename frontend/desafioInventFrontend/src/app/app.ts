import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
	selector: 'app-root',
	imports: [
		RouterOutlet,
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA],
	templateUrl: './app.html',
	styleUrl: './app.css',
})
export class App {
	protected title = 'Desafio Técnico Invent';
}
