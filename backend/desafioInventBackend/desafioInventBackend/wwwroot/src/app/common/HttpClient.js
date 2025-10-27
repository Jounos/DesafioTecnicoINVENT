sap.ui.define([], function () {
	"use strict";

	const URL = "/api";

	return {

		get(endpoint, callback = null) {

			const GET = "GET";
			const apiUrl = URL + endpoint;
			return this._ajaxRequest(GET, apiUrl, null, null, callback);
		},

		_ajaxRequest(type, apiUrl, data, aditionalParams, callback = null) {

			const params = this._obterParametrosHttp(type, data, aditionalParams);

			return fetch(apiUrl, params).then(response => {
				if (callback && typeof callback == "function") {
					callback(response);
				}

				return this._errorOuResponse(response);
			});
		},

		_obterParametrosHttp(type, body, aditionalParams) {
			const CONTENT_TYPE_JSON = "application/json";
			var params = {
				method: type,
				headers: {
					Accept: CONTENT_TYPE_JSON,
					"Content-Type": CONTENT_TYPE_JSON
				},
				body: body
			};

			Object.assign(params, (aditionalParams || {}));

			return params;
		},

		_errorOuResponse(response) {
			return this._ehStatusError(response.status, response.ok)
					? Promise.reject(response)
					: response;
		},

		_ehStatusError(status, estaOk) {
			const errorMinimo = 400;
			const errorMaximo = 500;
			return (status >= errorMinimo && status <= errorMaximo) || !estaOk;
		}
	}
})
