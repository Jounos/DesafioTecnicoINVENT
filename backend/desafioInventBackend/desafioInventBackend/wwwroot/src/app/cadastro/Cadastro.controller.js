sap.ui.define([
	"desafio/common/BaseController",
	"desafio/app/validadores/ValidadorEquipamentoEletronico",
	"desafio/app/servicos/ServiceEquipamentoEletronico"
], (
	BaseController,
	ValidadorEquipamentoEletronico,
	ServiceEquipamentoEletronico) => {
	"use strict";

	const NOME_CONTROLLER = "desafio.app.cadastro.Cadastro"
	const NOME_MODELO_PARAMETROS = "params";
	const NOME_MODELO_FORM = "form";
	return BaseController.extend(NOME_CONTROLLER, {

		_validadorEquipamentoEletronico: null,

		onInit() {
			const rotaCadastro = "cadastro";
			this.vincularRota(rotaCadastro, this._obterParametros);

			this._criarModelos();
		},

		_obterParametros: function (event) {
			const parametro = "arguments";
			let querys = event.getParameter(parametro)[this.query];

			this.criarModelo(NOME_MODELO_PARAMETROS, querys);
			this._prepararValidacoes();
		},

		_criarModelos() {
			this._criarModeloFormualrio();
			this._criarModeloSelects();
		},

		_criarModeloFormualrio() {
			this.criarModelo(NOME_MODELO_FORM, {
				nome: '',
				tipoEquipamento: 1,
				quantidadeEstoque: null,
			});
		},

		_criarModeloSelects() {

			const labelPC = 'PC';
			const labelNotebook = 'Notebook';
			const labelMouse = 'Mouse';
			const labelTeclado  = 'Teclado';
			const labelCelular = 'Celular';

			const tiposEquipamentos =  {
				tipoEquipamentoCollection: [
					{ label: labelPC, id: 1 },
					{ label: labelNotebook, id: 2 },
					{ label: labelMouse, id: 3 },
					{ label: labelTeclado, id: 4 },
					{ label: labelCelular, id: 5 }
				],
			};

			this.criarModelo("tiposEquipamentos", tiposEquipamentos);
		},

		aoNavegarUltimaPagina: function () {
			const params = this.obterValorModelo(NOME_MODELO_PARAMETROS);
			const rotaListagem = "listagem";
			this.navegarPara(rotaListagem, params);
		},

		_prepararValidacoes() {
			this._validadorEquipamentoEletronico = new ValidadorEquipamentoEletronico(this.resourceBundle());

			const propriedadeNome = 'nome';
			const nomeId = 'idInputNome';
			const propriedadeQuantidade = 'quantidadeEstoque';
			const quantidadeId = 'idInputQuantidade';

			this._validadorEquipamentoEletronico.vincularControle(propriedadeNome, this.byId(nomeId));
			this._validadorEquipamentoEletronico.vincularControle(propriedadeQuantidade, this.byId(quantidadeId));
			this._validadorEquipamentoEletronico.limparEstadoDosControles();
		},

		aoClicarBotaoCadastrar: function () {
			this.exibirEspera(() => {
				this._salvar();
			});
		},

		_validarCampos() {
			const nome = 'nome';
			const quantidade = 'quantidadeEstoque';
			const propriedadeTipoEquipamento = '/tipoEquipamento';

			const modeloForm =  this.obterModelo(NOME_MODELO_FORM);

			var nomeValidado = this._validadorEquipamentoEletronico.validarParaCampo(nome, modeloForm);
			var quantidadeValidado = this._validadorEquipamentoEletronico.validarParaCampo(quantidade, modeloForm);
			var tipoEquipamentoValidado = !!modeloForm.getProperty(propriedadeTipoEquipamento);

			let formularioValidado = nomeValidado && quantidadeValidado && tipoEquipamentoValidado;
			if (!formularioValidado) {
				const mensagem = 'Common.PreenchaTodosOsCampos';
				throw new Error(this.getTextOrName(mensagem));
			}
		},

		_salvar() {
			const sucesso = "Common.SalvoComSucesso";
			this.exibirEspera(async () => {
				this._validarCampos();
				const equipamentoEletronico = this.obterModelo(NOME_MODELO_FORM).getData();
				ServiceEquipamentoEletronico.salvar(equipamentoEletronico)
					.then(() => this.exibirMensagem(sucesso, () => this._aoClicarBotaoOkMensagemDeSucesso()));
			});
		},

		_aoClicarBotaoOkMensagemDeSucesso: function() {
			this.aoNavegarUltimaPagina();
		}
	});
});
