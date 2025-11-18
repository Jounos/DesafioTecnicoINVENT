sap.ui.define([
	"sap/ui/base/Object",
	"desafio/common/helpers/ArchiveHelper"
], function(sapBaseObject, ArchiveHelper) {

	const TEXTO_ERRO_NA_OPERACAO = "Common.ErroNaOperacao";
	const TEXTO_O_SERVIDOR_RETORNOU = "Common.OServidorRetornou";
	const TEXTO_OCORRE_UM_ERRO_NA_APLICACAO = "Common.OcorreUmErroNaAplicacao";
	const NAMESPACE_CONTROLLER = "desafio.common.client.ApiResponse";
	return sapBaseObject.extend(NAMESPACE_CONTROLLER, {

		_resourceBundle: null,

		constructor: function (resourceBundle) {
			this._resourceBundle = resourceBundle;
		},

		obterErro: function (response) {
			return this._atualizarResponstaComLeituraBody(response)
							.then(() => {
								if (this._erroTemContentTypeProblem(response)) {
									return _erroTemContentTypeProblemDetails(response);
								}

								return this._obterErroJavascript(response);
							}).then(e => {
								e.mensagem = this._escaparCaracteresDeBindings(e.mensagem);
								return e;
							});
		},

		_atualizarResponstaComLeituraBody: function (resposta) {
			const propriedade = 'bodyLido';
			if (resposta.body) {
				return ArchiveHelper
					.lerCorpo(resposta)
					.then(bodyLido => resposta[propriedade] = bodyLido);
			}

			if (resposta.detail) {
				resposta[propriedade] = resposta;
			}

			return Promise.resolve();
		},

		_erroTemContentTypeProblem: function (resposta) {
			const problemTypes = ["application/problem+json", "application/problem+xml"];
			const nomeDoParametroHeaderMai = "Content-Type";
			const nomeDoParametroHeaderMin = "content-type";
			if (!(resposta instanceof Error)) {
				var contentType = resposta.headers.get(nomeDoParametroHeaderMai) || resposta.headers.get(nomeDoParametroHeaderMin);
				return contentType && problemTypes.some(x => contentType.includes(x));
			}

			return false;
		},

		_erroTemContentTypeProblemDetails: function (resposta) {
			const propriedade = "bodyLido";
			let body = resposta[propriedade] || {};

			return {
				titulo: body.title || body.Title,
				mensagem: body.detail || body.Detail,
				stack: (body.Extensions || {}).stack || (body.extensions || {}).stack || (body|| {}).stack,
				textoCabecalho: this._resourceBundle.getText(TEXTO_O_SERVIDOR_RETORNOU)
			}
		},

		_obterErroJavascript: function (resposta) {
			return {
				titulo: this._resourceBundle.getText(TEXTO_ERRO_NA_OPERACAO),
				stack: resposta.stack,
				mensagem: resposta.message,
				textoCabecalho: this._resourceBundle.getText(TEXTO_OCORRE_UM_ERRO_NA_APLICACAO),
			}
		},

		_escaparCaracteresDeBindings: function (texto) {
			const tipoString = "string";
			if (texto && typeof(texto) === tipoString) {
				const caractereDeBindInicial = "{";
				const caractereDeBindFinal = "}";

				return texto
					.replaceAll(caractereDeBindInicial, `\\${caractereDeBindInicial}`)
					.replaceAll(caractereDeBindFinal, `\\${caractereDeBindFinal}`);
			}

			const nomeDaFuncao = "escaparCaracteresDeBindings";

			throw new Error(`[${nomeDaFuncao}] O texto não é do tipo String!`);
		}
	});
});
