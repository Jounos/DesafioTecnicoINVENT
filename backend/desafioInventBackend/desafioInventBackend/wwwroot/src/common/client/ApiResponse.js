sap.ui.define(["sap/ui/base/Object"], function(sapBaseObject) {

	const NAMESPACE_CONTROLLER = "desafio.common.client.ApiResponse";
	return sapBaseObject.extend(NAMESPACE_CONTROLLER, {

		_resourceBundle: null,

		constructor: function (resourceBundle) {
			this._resourceBundle = resourceBundle;
		}

		
	});
});
