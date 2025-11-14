sap.ui.define([
    "sap/ui/test/Opa5",
    "desafio/test/integration/arrangements/Startup",
	"./criar/JornadaCriarEquipamentoEletronico"
], (Opa5, Startup) => {
    "use strict";

    Opa5.extendConfig({
        arrangements: new Startup(),
        viewNamespace: "desafio.app.",
        autoWait: true
    });
});
