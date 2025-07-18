import { Component, inject, Input, OnInit } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';
import { FormValidator } from '../../validators/form-validator';

@Component({
	selector: 'app-error-msg',
	imports: [],
	templateUrl: './error-msg.html',
	styleUrl: './error-msg.css'
})
export class ErrorMsg implements OnInit{

	@Input() control!: AbstractControl |null;
	@Input() label: string = '';

	private formValidator = inject(FormValidator);

	constructor() { }

	ngOnInit() {
	}

	get errorMessage() {
		for (const propertyName in this.control!.errors) {
			if (this.control?.valid && this.control.touched) {
				if (this.control!.errors.hasOwnProperty(propertyName) &&
					this.control!.touched) {
					return this.formValidator.getErrorMsg(this.label, propertyName, this.control!.errors[propertyName]);
				}
			}
		}

		return null;
	}
}
