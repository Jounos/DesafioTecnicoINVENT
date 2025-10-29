sap.ui.define([], function () {
	"use strict";

	const URL = "http://localhost:5031/api";

	return {

		async get(endpoint, callback = null) {

			const GET = "GET";
			const apiUrl = URL + endpoint;
			return await this._ajaxRequest(GET, apiUrl, null, null, callback);
		},

		async _ajaxRequest(type, apiUrl, data, aditionalParams, callback = null) {

			const params = this._obterParametrosHttp(type, data, aditionalParams);

			return await fetch(apiUrl, params).then(response => response.json()).then(response => {
				console.log("houve resposta");
				if (callback && typeof callback == "function") {
					callback(response);
				}

				return response;
			}).catch(error => {
				console.log(error);
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
		}
	}
});
