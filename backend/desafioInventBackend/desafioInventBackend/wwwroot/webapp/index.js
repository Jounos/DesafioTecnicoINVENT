sap.ui.define([
    "sap/ui/core/ComponentContainer"
], (ComponentContainer) => {
    "use strict";

    new ComponentContainer({
        name: "ui5.desafio-tecnico",
		settings: {
			id: "desafio-tecnico"
		},
		async: true
    }).placeAt("content");
});