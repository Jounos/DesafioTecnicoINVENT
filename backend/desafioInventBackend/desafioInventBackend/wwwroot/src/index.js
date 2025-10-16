sap.ui.define([
    "sap/ui/core/ComponentContainer"
], (ComponentContainer) => {
    "use strict";

    new ComponentContainer({
        name: "invent.desafio.equipamentoseletronicos",
		settings: {
			id: "equipamentoseletronicos"
		},
		async: true
    }).placeAt("content");
});
