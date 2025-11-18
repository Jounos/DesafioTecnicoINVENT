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
	const PARAMETROS_URL = "arguments";
	const MENSAGEM_SUCESSO = "Common.SalvoComSucesso";

	return BaseController.extend(NOME_CONTROLLER, {

		_equipamentoEletronicoId: null,
		_validadorEquipamentoEletronico: null,

		onInit() {
			const rotaCadastro = "cadastro";
			this.vincularRota(rotaCadastro, this._obterParametros);

			const rotaEdicao = "edicao";
			this.vincularRota(rotaEdicao, this._buscarEquipamentoEletronicoPorId)

			this._prepararValidacoes();
			this._criarModelos();
		},

		_obterParametros: function (event) {
			let querys = event.getParameter(PARAMETROS_URL)[this.query];

			this.criarModelo(NOME_MODELO_PARAMETROS, querys);
		},

		_buscarEquipamentoEletronicoPorId: function (event) {
			this._equipamentoEletronicoId = event.getParameter(PARAMETROS_URL)?.id;
			ServiceEquipamentoEletronico.buscarPorId(this._equipamentoEletronicoId)
					.then((result) => this.criarModelo(NOME_MODELO_FORM, result));
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
			const rotaListagem = "listagem";
			this.navegarPara(rotaListagem);
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
			this.exibirEspera(() => this._salvar());
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
			this._validarCampos();
			const equipamentoEletronico = this.obterModelo(NOME_MODELO_FORM).getData();
			return ServiceEquipamentoEletronico.salvar(equipamentoEletronico, this._equipamentoEletronicoId)
				.then(() => this.exibirMensagem(MENSAGEM_SUCESSO, () => this._aoClicarBotaoOkMensagemDeSucesso()));
		},

		_aoClicarBotaoOkMensagemDeSucesso: function() {
			this.exibirEspera(() => this.aoNavegarUltimaPagina());
		}
	});
});
