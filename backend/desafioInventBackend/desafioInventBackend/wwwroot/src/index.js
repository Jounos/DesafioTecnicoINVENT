sap.ui.define([
    "sap/ui/core/ComponentContainer"
], (ComponentContainer) => {
    "use strict";

    new ComponentContainer({
        name: "invent.desafio",
		settings: {
			id: "desafio"
		},
		async: true
    }).placeAt("content");
});
