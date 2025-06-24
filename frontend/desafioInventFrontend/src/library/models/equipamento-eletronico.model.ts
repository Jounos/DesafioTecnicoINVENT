export interface IEquipamentoEletronico {
	nome: string;
	tipoEquipamento: number;
	quantidadeEstoque: number;
}

export class EquipamentoEletronico implements IEquipamentoEletronico {

	constructor(
		public nome: string,
		public tipoEquipamento: number,
		public quantidadeEstoque: number,
	) {	}
}
