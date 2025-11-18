sap.ui.define(["../helpers/ArchiveHelper"], function (ArchiveHelper) {
	"use strict";

	const URL = "http://localhost:5031/api";

	return {

		get(endpoint, callback = null) {
			const GET = "GET";
			const apiUrl = URL + endpoint;
			return this._ajaxRequest(GET, apiUrl, null, null, callback);
		},

		post(endpoint, data, callback = null) {
			const POST = "POST";
			const apiUrl = URL + endpoint;
			return this._ajaxRequest(POST, apiUrl, data, null, callback);
		},

		put(endpoint, data, callback = null) {
			debugger;
			const PUT = "PUT";
			const apiUrl = URL + endpoint;
			return this._ajaxRequest(PUT, apiUrl, data, null, callback);
		},

		_ajaxRequest(type, apiUrl, data, aditionalParams, callback = null) {
			const params = this._obterParametrosHttp(type, data, aditionalParams);

			return fetch(apiUrl, params).then(response => {
				const tipoCallback = "function"
				if (callback && typeof callback == tipoCallback) {
					callback(response);
				}

				return this._erroOuResponse(response);
			}).then(response => response.json().catch(error => {
				ArchiveHelper.lerCorpo(response);
				console.log(error);
			}));
		},

		_obterParametrosHttp(type, dados, aditionalParams) {
			const CONTENT_TYPE_JSON = "application/json";

			const myHeaders = new Headers();
			myHeaders.append("Content-Type", CONTENT_TYPE_JSON);
			var params = {
				method: type,
				headers: myHeaders,
				body: dados ? JSON.stringify(dados) : null,
				redirect: 'follow'
			};

			Object.assign(params, (aditionalParams || {}));
			return params;
		},

		_ehStatusDeErro(status, estaOk) {
			const erroMinimo = 400;
			const erroMaximo = 500;
			return (status >= erroMinimo && status <= erroMaximo) || !estaOk;
		},

		_erroOuResponse(response) {
			return this._ehStatusDeErro(response.status, response.ok) ?
				Promise.reject(response) :
				response;
		},
	}
});
