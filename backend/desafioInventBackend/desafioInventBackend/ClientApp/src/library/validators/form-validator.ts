import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class FormValidator {

	constructor() { }

	getErrorMsg(fieldName: string, validatorName: string, validatorValue?: any) {
		const config: IErrorMsg = {
			'required': `${fieldName} é obrigatório.`,
			'minlength': `${fieldName} precisa ter no mínimo ${validatorValue.requiredLength} caracteres.`,
			'maxlength': `${fieldName} precisa ter no máximo ${validatorValue.requiredLength} caracteres.`,
			'cepInvalido': 'CEP inválido.',
			'emailInvalido': 'Email já cadastrado!',
			'equalsTo': 'Campos não são iguais',
			'pattern': 'Campo inválido'
		};

		return config[validatorName];
	}
}

export interface IErrorMsg {
	[key: string]: string;
}
